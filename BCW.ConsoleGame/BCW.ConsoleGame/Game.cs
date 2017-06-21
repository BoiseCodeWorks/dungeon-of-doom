using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Models.Scenes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private void gameMenuSelected(object sender, GameEventArgs args)
        {
            switch (args.Keys.ToLower())
            {
                case "x":
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
                nextScene.Enter();
            }
        }

        private void loadScenes()
        {
            Scenes = loadData();

            foreach (var scene in Scenes)
            {
                scene.GameMenuSelected += gameMenuSelected;
                scene.Navigated += sceneNavigated;
            }
        }

        private List<IScene> loadData()
        {
            var scenes = new List<IScene>();
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");

            using (StreamReader reader = File.OpenText(dataFilePath))
            {
                var sceneData = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                var scenesJson = (JArray)sceneData.GetValue("Scenes");

                scenes = scenesJson.Select(s => new Scene
                (
                    (string)s["Title"],
                    (string)s["Description"],
                    new MapPosition((int)s["MapPosition"]["X"], (int)s["MapPosition"]["Y"]),
                    (s["NavigationCommands"] as JArray).Select(c => new NavigationCommand
                    {
                        Keys = (string)c["Keys"],
                        Description = (string)c["Description"],
                        Direction = (Direction)Enum.Parse(typeof(Direction), (string)c["Direction"])
                    }).ToList<ICommand>(),
                    new List<ICommand> { new GameCommand { Keys = "X", Description = "Exit The Game"} }
                )).ToList<IScene>();
            }

            return scenes;
        }
    }
}
