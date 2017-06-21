using BCW.ConsoleGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCW.ConsoleGame.Models.Scenes;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Events;

namespace BCW.ConsoleGame.JsonData
{
    public class Provider : IDataProvider
    {
        public List<IScene> LoadScenes()
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
                    new List<ICommand> { new GameCommand { Keys = "X", Description = "Exit The Game" } }
                )).ToList<IScene>();
            }

            return scenes;
        }
    }
}
