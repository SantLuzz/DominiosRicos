using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreatedDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            Active = true;
            ExpireDate = expireDate;
            _payments = new List<Payment>();
        }

        public DateTime  CreatedDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments 
            => _payments.ToArray();

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser no futuro!"));

            if(Valid)
                _payments.Add(payment);
        }

        public void Activated()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Inactivated()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}