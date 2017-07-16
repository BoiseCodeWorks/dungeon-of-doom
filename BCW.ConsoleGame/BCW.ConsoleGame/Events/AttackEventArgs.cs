using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Events
{
    public class AttackEventArgs
    {
        public IScene Scene { get; set; }

        public AttackEventArgs(IScene scene)
        {
            Scene = scene;
        }
    }
}
