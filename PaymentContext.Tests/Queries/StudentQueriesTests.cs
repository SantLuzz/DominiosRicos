using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests() 
        {
            _students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Domain.ValueObjects.Name("ALuno", i.ToString()),
                    new Domain.ValueObjects.Document("11111111111" + i.ToString(), Domain.Enums.EDocumentType.Cpf),
                    new Domain.ValueObjects.Email(i.ToString() + "@balta.io")));
            }
        }


        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists() 
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, studn);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("111111111111");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, studn);
        }
    }
}
