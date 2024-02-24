using Autofac;
using NSubstitute;
using Tanks.Interfaces;

namespace Tanks.UnitTests
{
    public class GameInitializationTests
    {
        [Test]
        public void When_Application_instantiated_Initialize_should_be_invoked()
        {
            // Arrange Act
            var gameMock = Substitute.For<IGame>();
            _ = new Application(gameMock);

            // Assert
            gameMock.Received().Initialize();
        }

        [Test]
        public void When_Application_ctor_Game_param_is_null_should_throw_ArgumentNullException()
        {
            // Arrange Act Assert
            Assert.Throws<ArgumentNullException>(() => { _ = new Application(null); });
        }

        [Test]
        public void When_Game_Initialize_invoked_following_calls_to_it_should_throw() 
        {
            // Arrange
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act Assert
            Assert.Throws<InvalidOperationException>(() => 
            { 
                game.Initialize();
                game.Initialize();
            });
        }

        //When_game_is_not_initialized_shutdown_idempotentShould_not_throw_any_ex
        //When_idempotentShutdown_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //When_idempotentRunLoop_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //Given_idempotentInitialize_was_not_invoked_when_idempotentRunLoop_invoked_should_throw_ex
    }
}