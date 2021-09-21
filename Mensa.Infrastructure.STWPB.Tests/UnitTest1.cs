using System.Threading.Tasks;
using NUnit.Framework;

namespace Mensa.Infrastructure.STWPB.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            try
            {
                var api = new MensaAPI();
                var meals = await api.GetMeals();
                Assert.AreEqual(10, meals.Count);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
