using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Scenes
{
    public class Scene : IScene
    {
        public event EventHandler<GameEventArgs> GameMenuSelected;
        public event EventHandler<NavigationEventArgs> Navigated;

        public string Title { get; set; }
        public string Description { get; set; }
        public bool Visited { get; set; }
        public MapPosition MapPosition { get; set; }

        public List<ICommand> Commands { get; set; }

        public Scene()
        {
        }

        public Scene(string title, string description, MapPosition position) 
            :this(title, description, position, new List<ICommand>())
        {
        }

        public Scene(string title, string description, MapPosition position, params List<ICommand>[] commands)
        {
            Title = title;
            Description = description;
            MapPosition = position;
            Commands = new List<ICommand>();

            foreach(var collection in commands)
            {
                Commands.AddRange(collection);
            }

            setCommandEvents();
        }

        public virtual void Enter()
        {
            ICommand action = null;
            string error = "";

            while (action == null)
            {
                display(error);

                var choice = Console.ReadLine();

                action = Commands.FirstOrDefault(c => c.Keys.ToLower() == choice.ToLower());

                if (action == null) error = "Invalid Choice!";
                else action.Action();
            }
        }

        protected virtual void display(string error)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(Title);
            Console.WriteLine(new String('-', Title.Length));
            Console.WriteLine(Description);

            Console.WriteLine("");
            Console.WriteLine("Actions");
            Console.WriteLine(new String('-', "Actions".Length));

            if (Commands != null && Commands.Count > 0)
            {
                foreach (var command in Commands.OrderBy(c => c.Keys))
                {
                    Console.WriteLine("{0} = {1}", command.Keys, command.Description);
                }
            }

            Console.WriteLine("");
            if (error.Length > 0) Console.WriteLine(error);
            Console.Write("Choose an action: ");
        }

        private void setCommandEvents()
        {
            foreach(var command in Commands.Where(c => c is INavigationCommand))
            {
                command.Action = () =>
                {
                    Navigated?.Invoke(this, new NavigationEventArgs(this, (command as NavigationCommand).Direction));
                };
            }

            foreach (var command in Commands.Where(c => c is IGameCommand))
            {
                command.Action = () =>
                {
                    GameMenuSelected?.Invoke(this, new GameEventArgs(this, command.Keys));
                };
            }
        }
    }
}
