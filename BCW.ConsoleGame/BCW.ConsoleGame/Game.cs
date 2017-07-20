using BCW.ConsoleGame.Data;
using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Characters;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame
{
    public class Game
    {
        private IPlayer player;

        public IDataProvider DataProvider { get; set; }
        public IUserInterface UserInterface { get; set; }
        public List<IScene> Scenes { get; set; }

        public Game(IDataProvider dataProvider, IUserInterface userInterface)
        {
            DataProvider = dataProvider;
            UserInterface = userInterface;

            Scenes = DataProvider.Scenes;

            subscribeToEvents();

            player = new Player()
            {
                Health = 20
            };

            gotoPosition(DataProvider.StartPosition);
        }

        void gotoPosition(MapPosition position)
        {
            var scene = Scenes.FirstOrDefault(s => s.MapPosition.X == position.X && s.MapPosition.Y == position.Y);

            if (scene != null)
            {
                scene.Enter(player);
            }
        }

        private void gameMenuSelected(object sender, GameEventArgs args)
        {
            switch (args.Keys.ToLower())
            {
                case "p":
                    // pick up the treasure
                    pickupTreasure(args.Scene);
                    break;

                case "x":
                    DataProvider.SaveGameState();
                    Environment.Exit(0);
                    break;
            }
        }

        private void sceneNavigated(object sender, NavigationEventArgs args)
        {
            var toPosition = new MapPosition(args.Scene.MapPosition.X, args.Scene.MapPosition.Y);

            switch (args.Direction)
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

            var nextScene = Scenes.FirstOrDefault(s => s.MapPosition.X == toPosition.X && s.MapPosition.Y == toPosition.Y);

            if (nextScene != null)
            {
                DataProvider.StartPosition = nextScene.MapPosition;
                nextScene.Enter(player);
            }
        }

        private void playerAttacked(object sender, AttackEventArgs args)
        {
            attackMonsters(args.Scene);
            attackPlayer(args.Scene);
        }

        private void attackMonsters(IScene scene)
        {
            var didHitMonster = false;
            var monsters = scene.GetItems("Monsters");

            foreach (var item in monsters)
            {
                var monster = (IMonster)item;

                var damage = monster.Defend();

                if (damage > 0)
                {
                    didHitMonster = true;

                    if (monster.Health == 0)
                    {
                        scene.Feedback = $"You killed a {monster.Name}!";

                        // if the monster has any treasure move it to the scene items
                        var treasures = monster.GetItems("Treasures");

                        if(treasures.Count > 0)
                        {
                            scene.Feedback += $"\nThe {monster.Name} dropped some treasure!";
                        }

                        foreach(var treasure in treasures)
                        {
                            scene.AddItem("Treasures", treasure);
                        }

                        // remove dead monsters from the scene
                        scene.RemoveItem("Monsters", item);

                        if (scene.GetItems("Monsters").Count == 0)
                        {
                            scene.Feedback += "\nThere are no monsters left.";
                        }
                    }
                    else
                    {
                        scene.Feedback = $"You hit a {monster.Name} and inflicted {damage} points of damage.";
                    }

                    break;
                }
            }

            if (!didHitMonster)
            {
                scene.Feedback = $"You missed!";
            }
        }

        private void attackPlayer(IScene scene)
        {
            var monsters = scene.GetItems("Monsters");

            foreach(var item in monsters)
            {
                var monster = (IMonster)item;

                var damage = monster.Attack();

                if(damage > 0)
                {
                    player.Health -= damage;

                    scene.Feedback += $"\nYou were hit by a {monster.Name} and took {damage} points of damage!";

                    if (player.Health <= 0)
                    {
                        player.Health = 0;
                        scene.Feedback += "\nYou died.";
                        scene.Commands = scene.Commands.Where(c => c is IGameCommand && c.Keys.ToLower() == "x").ToList();
                    }

                    break;
                }
            }
        }

        private void pickupTreasure(IScene scene)
        {
            var treasures = scene.GetItems("Treasures");

            foreach(var treasure in treasures)
            {
                player.AddItem("Treasures", treasure);
            }

            scene.RemoveItems("Treasures");
        }

        private void subscribeToEvents()
        {
            foreach (var scene in Scenes)
            {
                scene.UserInterface = UserInterface;
                scene.GameMenuSelected += gameMenuSelected;
                scene.Navigated += sceneNavigated;
                scene.Attacked += playerAttacked;
            }
        }

    }
}
