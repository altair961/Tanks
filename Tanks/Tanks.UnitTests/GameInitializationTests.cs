using NSubstitute;
using Tanks.Interfaces;

namespace Tanks.UnitTests
{
    public class GameInitializationTests
    {
        [Test]
        public void When_Game_instantiated_idempotentInitialize_should_be_invoked()
        {
            // Arrange Act
            var gameMock = Substitute.For<IGame>();
            _ = new Application(gameMock);

            // Assert
            gameMock.Received().IdempotentInitialize();
        }

        [Test]
        public void When_Application_ctor_Game_param_is_null_should_throw_ArgumentNullException()
        {
            // Arrange Act Assert
            Assert.Throws<ArgumentNullException>(() => { _ = new Application(null); });
        }

        //When_Application_instance_created_should_invoke_idempotentInitialize_on_game
        //When_idempotentInitialize_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //When_game_is_not_initialized_shutdown_idempotentShould_not_throw_any_ex
        //When_idempotentShutdown_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //When_idempotentRunLoop_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //Given_idempotentInitialize_was_not_invoked_when_idempotentRunLoop_invoked_should_throw_ex
    }
}