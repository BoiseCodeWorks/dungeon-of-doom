using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame
{
    public enum Direction { North, South, East, West};

    public class Game
    {
        public List<IScene> Scenes { get; set; }

        public Game()
        {
            Scenes = new List<IScene>();
            loadScenes();

            gotoPosition(new MapPosition(9, 5));
        }

        void gotoPosition(MapPosition position)
        {
            var scene = Scenes.FirstOrDefault(s => s.MapPosition.X == position.X && s.MapPosition.Y == position.Y);

            if (scene != null)
            {
                scene.Enter();
            }
        }

        private void move(MapPosition fromPosition, Direction direction)
        {
            var toPosition = new MapPosition(fromPosition.X, fromPosition.Y);

            switch (direction)
            {
                case Direction.North:
                    toPosition.Y -= 1;
                    break;

                case Direction.South:
                    toPosition.Y += 1;
                    break;

                case Direction.East:
                    toPosition.X += 1;
                    break;

                case Direction.West:
                    toPosition.X -= 1;
                    break;
            }

            var scene = Scenes.FirstOrDefault(s => s.MapPosition.X == toPosition.X && s.MapPosition.Y == toPosition.Y);

            if (scene != null)
            {
                scene.Enter();
            }
        }

        private void loadScenes()
        {
            var scene1 = new Scene
            {
                Title = "Entry Way",
                Description = "You're standing in a small empty room with a door on the north wall.",
                MapPosition = new MapPosition(9, 5)
            };

            scene1.Commands.Add(new Command { Keys = "n", Description = "Go North", Action = () => { move(scene1.MapPosition, Direction.North); } });
            Scenes.Add(scene1);

        }
    }
}
