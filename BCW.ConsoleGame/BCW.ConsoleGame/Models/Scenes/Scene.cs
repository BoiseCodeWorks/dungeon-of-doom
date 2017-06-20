using BCW.ConsoleGame.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Scenes
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Scene : IScene
    {
        public event EventHandler<NavigationEventArgs> Navigated;

        public string Title { get; set; }
        public string Description { get; set; }
        public bool Visited { get; set; }
        public MapPosition MapPosition { get; set; }

        [JsonIgnore]
        public List<ICommand> Commands { get; set; }

        public Scene()
        {
        }

        public Scene(string title, string description, MapPosition position) 
            :this(title, description, position, new List<ICommand>())
        {
        }

        public Scene(string title, string description, MapPosition position, List<ICommand> commands)
        {
            Title = title;
            Description = description;
            MapPosition = position;
            Commands = commands;

            setNavigationEvents();
        }

        public virtual void Enter()
        {
            ICommand action = null;
            string error = "";

            while (action == null)
            {
                display(error);

                var choice = Console.ReadLine();

                action = Commands.FirstOrDefault(c => c.Keys == choice);

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

            if (Commands != null)
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

        private void setNavigationEvents()
        {
            var navCommands = Commands.Where(c => c is INavigationCommand);

            foreach(var command in navCommands)
            {
                command.Action = () =>
                {
                    Navigated?.Invoke(this, new NavigationEventArgs(this, (command as NavigationCommand).Direction));
                };
            }
        }
    }
}
