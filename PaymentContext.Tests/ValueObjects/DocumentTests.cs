using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErroWhenCNPJIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.Cnpj);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("12549676000118", Domain.Enums.EDocumentType.Cnpj);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShouldReturnErroWhenCPFIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.Cpf);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var doc = new Document("52536969037", Domain.Enums.EDocumentType.Cpf);
            Assert.IsTrue(doc.Valid);
        }
    }
}

