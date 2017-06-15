using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Events
{
    public enum Direction { North, South, East, West };

    public class NavigationEventArgs : EventArgs
    {
        public IScene Scene { get; set; }
        public Direction Direction { get; set; }

        public NavigationEventArgs(IScene scene, Direction direction)
        {
            Scene = scene;
            Direction = direction;
        }
    }
}
