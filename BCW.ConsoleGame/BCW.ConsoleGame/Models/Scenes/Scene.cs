using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Scenes
{
    public class Scene : Composite, IScene
    {
        public Scene()
        {
            items = new List<IComposite>();
        }

        public Scene(string title, string description, MapPosition position)
            : this(title, description, position, new List<ICommand>())
        {
        }

        public Scene(string title, string description, MapPosition position, params List<ICommand>[] commands)
        {
            items = new List<IComposite>();

            Title = title;
            Description = description;
            MapPosition = position;
            Commands = new List<ICommand>();

            foreach (var collection in commands)
            {
                Commands.AddRange(collection);
            }

            setCommandEvents();
        }

        #region IScene Implementation
        public event EventHandler<GameEventArgs> GameMenuSelected;
        public event EventHandler<NavigationEventArgs> Navigated;

        public IUserInterface UserInterface { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Visited { get; set; }
        public MapPosition MapPosition { get; set; }

        public List<ICommand> Commands { get; set; }

        public virtual void Enter()
        {
            ICommand action = null;
            string error = "";

            while (action == null)
            {
                display(error);

                var choice = UserInterface.GetInput("Choose an action: ");

                action = Commands.FirstOrDefault(c => c.Keys.ToLower() == choice.ToLower());

                if (action == null) error = "Invalid Choice!";
                else action.Action();
            }
        }

        protected virtual void display(string error)
        {
            UserInterface.Clear();
            UserInterface.Display("");
            UserInterface.Display(Title);
            UserInterface.Display(new String('-', Title.Length));
            UserInterface.Display(Description);

            UserInterface.Display("");
            UserInterface.Display("Actions");
            UserInterface.Display(new String('-', "Actions".Length));

            if (Commands != null && Commands.Count > 0)
            {
                foreach (var command in Commands.OrderBy(c => c.Keys))
                {
                    UserInterface.Display($"{command.Keys} = {command.Description}");
                }
            }

            UserInterface.Display("");
            if (error.Length > 0) UserInterface.Display(error);
        }

        private void setCommandEvents()
        {
            foreach (var command in Commands.Where(c => c is INavigationCommand))
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
    #endregion

    }
}
