using NUnit.Framework;
using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.User;
using Moq;

namespace BCW.ConsoleGame.Tests
{
    [TestFixture]
    public class SceneTests
    {
        private Mock<IUserInterface> mockUserInterface;
        private Scene initializedScene;
        private Scene constructedScene;

        [SetUp]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();

            initializedScene = new Scene
            {
                Title = "Test Scene",
                Description = "This is a test scene",
                MapPosition = new MapPosition(1, 1),
                Commands = new List<ICommand>
                {
                    new Command
                    {
                        Keys = "x",
                        Description = "Exit",
                        Action = () => { return; }
                    }
                }
            };

            constructedScene = new Scene(
                "Test Scene",
                "This is a test scene",
                1,
                new MapPosition(1, 1),
                new List<ICommand>
                {
                    new NavigationCommand
                    {
                        Keys = "n",
                        Description = "Go North",
                        Direction = Direction.North
                    },
                    new GameCommand
                    {
                        Keys = "x",
                        Description = "Quit"
                    }
                }
            );

            constructedScene.UserInterface = mockUserInterface.Object;
            constructedScene.Navigated += sceneNavigated;
            constructedScene.GameMenuSelected += sceneGameMenuSelected;
        }

        [Test]
        public void DefaultConstructorDoesNotSetProperties()
        {
            var scene = new Scene();

            Assert.IsNull(scene.Title);
            Assert.IsNull(scene.Description);
            Assert.IsFalse(scene.Visited);
            Assert.IsNull(scene.MapPosition);
            Assert.IsNull(scene.Commands);
        }

        [Test]
        public void DefaultConstructorSupportsObjectInitialization()
        {
            Assert.IsNotNull(initializedScene);
            Assert.IsNotNull(initializedScene.Title);
            Assert.AreEqual(initializedScene.Title, "Test Scene");
            Assert.IsNotNull(initializedScene.Description);
            Assert.AreEqual(initializedScene.Description, "This is a test scene");
            Assert.IsNotNull(initializedScene.MapPosition);
            Assert.AreEqual(initializedScene.MapPosition.X, 1);
            Assert.AreEqual(initializedScene.MapPosition.Y, 1);
            Assert.IsNotNull(initializedScene.Commands);
            Assert.AreEqual(initializedScene.Commands.Count, 1);
            Assert.AreEqual(initializedScene.Commands[0].Keys, "x");
            Assert.AreEqual(initializedScene.Commands[0].Description, "Exit");
            Assert.DoesNotThrow(() => { initializedScene.Commands[0].Action(); });
        }

        [Test]
        public void ConstructorsBindCommandEvents()
        {
            Assert.DoesNotThrow(() => { constructedScene.Commands[0].Action(); });
            Assert.DoesNotThrow(() => { constructedScene.Commands[1].Action(); });
        }

        [Test]
        public void EnterDisplaysDetails()
        {
            mockUserInterface.Setup(ui => ui.GetInput("Choose an action: ")).Returns("x");

            constructedScene.Enter();

            mockUserInterface.Verify(ui => ui.Clear());
            mockUserInterface.Verify(ui => ui.Display("Test Scene"));
            mockUserInterface.Verify(ui => ui.Display("This is a test scene"));
        }

        [Test]
        public void EnterDisplaysCommandChoices()
        {
            mockUserInterface.Setup(ui => ui.GetInput("Choose an action: ")).Returns("x");

            constructedScene.Enter();

            mockUserInterface.Verify(ui => ui.Display("n = Go North"));
            mockUserInterface.Verify(ui => ui.Display("x = Quit"));
        }

        private void sceneNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Direction != Direction.North)
            {
                throw new ArgumentException();
            }
        }

        private void sceneGameMenuSelected(object sender, GameEventArgs e)
        {
            if (e.Keys != "x")
            {
                throw new ArgumentException();
            }
        }
    }
}
