using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private Student _student;
        private Subscription _subscription;
        private Name _name;
        private Address _address;
        private Document _document;
        private Email _email;

        public StudentTests() 
        {
            _name = new Name("Bruce", "Wayne");
            _address = new Address("Rua 1", "123", "bairro", "Gotham", "MS", "BR", "79540000");
            _document = new Document("53138886094", Domain.Enums.EDocumentType.Cpf);
            _email = new Email("batman@dc.com");
           

            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
           
        }

        [TestMethod]
        public void ShouldReturnErrorWhenActiveSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
            10, 10, "WAYNE CORP", _document, _address, _email);
            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenHadSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            _subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
            10, 10, "WAYNE CORP", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}
