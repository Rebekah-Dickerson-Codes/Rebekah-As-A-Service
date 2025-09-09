using NUnit.Framework;
using Rebekah_As_A_Service.Controllers;

namespace Rebekah_As_A_Service.Tests
{
    [TestFixture]
    public class FactControllerTests
    {
        private readonly FactsController _factsController;

        [Test]
        [TestCase(null)]
        public void FactController(string category) 
        {
            //act
            var resp = _factsController.GetRebekahFactsByCategoryAsync(category);

            //assert
            Assert.That(resp, Is.Not.Null);
        }
    }
}
