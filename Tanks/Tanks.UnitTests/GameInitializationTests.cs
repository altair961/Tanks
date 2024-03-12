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
        public void Given_Application_ctor_Game_param_is_null_should_throw()
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

        [Test]
        public void Given_Initialize_was_not_yet_invoked_when_RunLoop_invoked_should_throw()
        {
            // Arrange 
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = game.RunLoop;

            // Assert
            Assert.Throws<InvalidOperationException>(testedCode);
        }

        [Test]
        public void Given_Initialized_was_invoked_when_RunLoop_invoked_should_not_throw()
        {
            // Arrange
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = () =>
            {
                game.Initialize();
                game.RunLoop();
            };

            // Assert
            Assert.DoesNotThrow(testedCode);
        }

        [Test]
        public void Given_RunLoop_has_already_been_invoked_following_calls_to_it_should_throw()
        {
            // Arrange
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            TestDelegate testedCode = () =>
            {
                game.Initialize();
                game.RunLoop();
                game.RunLoop();
            };

            // Assert
            Assert.Throws<InvalidOperationException>(testedCode);
        }

        [Test]
        public void When_Game_has_not_been_initialized_yet_should_have_IsShutDown_set_to_true()
        {
            // Arrange Act
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            //Assert
            Assert.True(game.IsShutDown);
        }

        [Test]
        public void When_Game_has_been_initialized_should_set_IsShutDown_to_false()
        {
            // Arrange
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            game.Initialize();

            // Assert
            Assert.False(game.IsShutDown);
        }

        [Test]
        public void When_Game_ctor_Window_param_is_null_should_throw()
        {
            // Arrange
            IWindow window = null;

            // Act
            TestDelegate testedCode = () => new Game(window);

            // Assert
            Assert.Throws<ArgumentNullException>(testedCode);
        }

        [Test]
        public void When_Game_is_getting_initialized_Window_should_get_initialized()
        {
            // Arrange
            var windowMock = Substitute.For<IWindow>();
            windowMock.IsInitialized.Returns(true);
            var game = new Game(windowMock);

            // Act
            game.Initialize();

            // Assert
            windowMock.Received().Initialize();
        }

        [Test]
        public void When_Window_Initialize_invoked_following_calls_to_it_should_throw()
        { 
            // Arrange
            var window = IoCContainer.CompositionRoot().Resolve<IWindow>();

            // Act            
            TestDelegate testedCode = () =>
            {
                window.Initialize();
                window.Initialize();
            };

            // Assert
            Assert.Throws<InvalidOperationException>(testedCode);
        }

        [Test]
        public void When_Game_has_not_been_initialized_yet_should_have_IsInitialized_set_to_false() 
        {
            // Arrange Act
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Assert
            Assert.False(game.IsInitialized);
        }

        [Test]
        public void When_Game_is_initialized_should_have_IsInitialized_set_to_true() 
        {
            // Arrange 
            var game = IoCContainer.CompositionRoot().Resolve<IGame>();

            // Act
            game.Initialize();

            // Assert
            Assert.True(game.IsInitialized);
        }

        [Test]
        public void When_Window_has_not_been_initialized_yet_should_have_IsInitialized_set_to_false() 
        {
            // Arrange Act
            var window = IoCContainer.CompositionRoot().Resolve<IWindow>();

            // Assert
            Assert.False(window.IsInitialized);
        }

        [Test]
        public void When_Window_is_initialized_should_have_IsInitialized_set_to_true() 
        {
            // Arrange
            var window = IoCContainer.CompositionRoot().Resolve<IWindow>();

            //Act
            window.Initialize();

            // Assert
            Assert.True(window.IsInitialized);
        }


    }
}