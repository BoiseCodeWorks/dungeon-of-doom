using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Events
{
    public class GameEventArgs : EventArgs
    {
        public IScene Scene { get; set; }
        public string Keys { get; set; }

        public GameEventArgs(IScene scene, string keys)
        {
            Scene = scene;
            Keys = keys;
        }
    }
}
