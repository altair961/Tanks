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
            // Arrange
            var gameMock = Substitute.For<IGame>();
            
            // Act
            _ = new Application(gameMock);

            // Assert
            gameMock.Received().Initialize();
        }

        [Test]
        public void When_Application_ctor_Game_param_is_null_should_throw_ArgumentNullException()
        {
            // Arrange
            IGame game = null;

            // Act
            TestDelegate testedCode = () => new Application(game);
            

            // Assert
            Assert.Throws<ArgumentNullException>(testedCode);
        }

        [Test]
        public void When_Game_Initialize_invoked_following_calls_to_it_should_throw() 
        {
            // Arrange
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = () =>
            {
                game.Initialize();
                game.Initialize();
            };

            // Assert
            Assert.Throws<InvalidOperationException>(testedCode);
        }

        [Test]
        public void Given_Game_Is_Initialized_Shutdown_throws_no_ex() 
        {
            // Arrange 
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = () =>
            {
                game.Initialize();
                game.ShutDown();
            };

            // Assert
            Assert.DoesNotThrow(testedCode);
        }

        [Test]
        public void Given_Game_Is_not_Initialized_Shutdown_throws_no_ex()
        {
            // Arrange 
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = game.ShutDown;

            // Assert
            Assert.DoesNotThrow(testedCode);
        }

        [Test]
        public void Given_Shutdown_is_invoked_already_following_call_to_it_throws() 
        {
            // Arrange 
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = () =>
            {
                game.ShutDown();
                game.ShutDown();
            };

            // Assert
            Assert.Throws<InvalidOperationException>(testedCode);
        }

        //shutdown twice throws
        //When_idempotentShutdown_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //When_idempotentRunLoop_was_invoked_following_calls_to_it_should_not_throw_any_ex
        //Given_idempotentInitialize_was_not_invoked_when_idempotentRunLoop_invoked_should_throw_ex
    }
}