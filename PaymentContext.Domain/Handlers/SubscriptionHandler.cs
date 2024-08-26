using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;   

        public SubscriptionHandler(IStudentRepository repository,
            IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se documento cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF/CNPJ já está em uso");

            //verificar se email cadastrado
            if (_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-Mail já está em uso");

            //gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighbordhood, command.City, command.State,
                command.Country, command.ZipCode);

            //Gerar entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber,
                command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupando as notificações
            AddNotifications(name, document, email, address, student, subscription, payment);

            if(Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //salvar informações
            _repository.CreateSubscription(student);

            //enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

            //retorna informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}
