using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models.Characters;
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

        public Scene(string title, string description, int difficulty, MapPosition position)
            : this(title, description, difficulty, position, new List<ICommand>())
        {
        }

        public Scene(string title, string description, int difficulty, MapPosition position, params List<ICommand>[] commands)
        {
            items = new List<IComposite>();

            Title = title;
            Description = description;
            Difficulty = difficulty;
            MapPosition = position;
            Commands = new List<ICommand>();

            foreach (var collection in commands)
            {
                Commands.AddRange(collection);
            }

            setCommandEvents();

            var monsters = MonsterFactory.Instance().CreateMonsters(this);

            foreach(var monster in monsters)
            {
                AddItem("Monsters", monster);
            }
        }

        #region IScene Implementation
        public event EventHandler<GameEventArgs> GameMenuSelected;
        public event EventHandler<NavigationEventArgs> Navigated;

        public IUserInterface UserInterface { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Visited { get; set; }
        public MapPosition MapPosition { get; set; }
        public int Difficulty { get; set; }

        public List<ICommand> Commands { get; set; }

        public virtual void Enter()
        {
            var monsterFactory = MonsterFactory.Instance();
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

            var monsters = GetItems("Monsters");

            if(monsters.Count > 0)
            {
                var zombieCount = monsters.Count(m => m.Name == "Zombie");
                var orcCount = monsters.Count(m => m.Name == "Orc");
                var trollCount = monsters.Count(m => m.Name == "Troll");
                var dragonCount = monsters.Count(m => m.Name == "Dragon");

                var zombieText = zombieCount > 0 ? zombieCount > 1 ? $"{zombieCount} zombies" : "1 zombie"  : "";
                var orcText = orcCount > 0 ? orcCount > 1 ? $"{orcCount} orcs" : "1 orc" : "";
                var trollText = trollCount > 0 ? trollCount > 1 ? $"{trollCount} trolls" : "1 troll" : "";
                var dragonText = dragonCount > 0 ? dragonCount > 1 ? $"{dragonCount} dragons" : "1 dragon" : "";

                var monsterText = zombieText;

                monsterText += orcText.Length > 0 ? monsterText.Length > 0 ? $" and {orcText}" : orcText : "";
                monsterText += trollText.Length > 0 ? monsterText.Length > 0 ? $" and {trollText}" : trollText : "";
                monsterText += dragonText.Length > 0 ? monsterText.Length > 0 ? $" and {dragonText}" : dragonText : "";

                var isAre = monsters.Count > 1 ? "are" : "is";

                UserInterface.Display($"There {isAre} {monsterText} in the room.");
            }

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
