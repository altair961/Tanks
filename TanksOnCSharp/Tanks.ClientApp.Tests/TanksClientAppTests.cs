using NUnit.Framework;

namespace Tanks.ClientApp.Tests
{
    [TestFixture]
    public class TanksClientAppTests
    {
        [Test]
        public void TanksClientApp_Should_Dispose_GameEngine_When_Exits() 
        {
            // Arrange
            using (var container = new ContainerProvider().GetContainer()) 
            {
                // Arrange
                var sut = container.Resolve<IService>();


                // Act

                // Assert

            }
        }
    }
}