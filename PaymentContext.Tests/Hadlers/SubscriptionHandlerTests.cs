using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Hadlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExist()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(),
                new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Luiz";
            command.LastName = "Felipe";
            command.Document = "9999999999";
            command.Email = "hello2@balta.io";
            command.BarCode = "123456789";
            command.BoletoNumber = "123";
            command.PaymentNumber = "123123123";
            command.PaidDate = DateTime.Now;
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "WAYNE CORP";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = Domain.Enums.EDocumentType.Cpf;
            command.PayerEmail = "batman@dc.com";
            command.Street = "asdasdasd";
            command.Number = "asdasdasd";
            command.Neighbordhood = "asdasdasd";
            command.City = "asdasdasd";
            command.State = "asdasdasd";
            command.Country = "asdasdasd";
            command.ZipCode = "12345678";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
