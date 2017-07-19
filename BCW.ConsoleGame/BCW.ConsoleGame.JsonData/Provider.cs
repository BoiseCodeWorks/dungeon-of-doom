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
using BCW.ConsoleGame.Models.Characters;

namespace BCW.ConsoleGame.JsonData
{
    public class Provider : IDataProvider
    {
        private JObject gameData;

        public MapPosition StartPosition { get; set; }
        public List<IScene> Scenes { get; set; }
        public List<IMonsterType> MonsterTypes { get; set; }

        public Provider()
        {
            MonsterTypes = loadMonsterTypes();

            MonsterFactory.Instance(MonsterTypes);

            Scenes = loadScenes();
            StartPosition = loadStartPosition();
        }

        public void SaveGameState()
        {
            saveGameData();
        }

        private List<IMonsterType> loadMonsterTypes()
        {
            var monsterTypes = new List<IMonsterType>();
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "MonsterTypes.json");

            using (StreamReader reader = File.OpenText(dataFilePath))
            {
                var monsterData = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                var monsterJson = (JArray)monsterData.GetValue("Types");

                monsterTypes = monsterJson.Select(t => new MonsterType
                (
                    (string)t["Name"],
                    (int)t["Health"]["Min"],
                    (int)t["Health"]["Max"],
                    (int)t["Agility"]["Min"],
                    (int)t["Agility"]["Max"],
                    (int)t["Damage"]["Min"],
                    (int)t["Damage"]["Max"],
                    (int)t["Defense"]["Min"],
                    (int)t["Defense"]["Max"],
                    (int)t["Vitality"]["Min"],
                    (int)t["Vitality"]["Max"],
                    (t["Odds"] as JArray).Select(o => new Odds
                        (
                            (int)o["Level"],
                            (int)o["Exist"],
                            (int)o["Count"]["Min"],
                            (int)o["Count"]["Max"]
                        )
                    ).ToDictionary(o => o.Level)
                )).ToList<IMonsterType>();
            }

            return monsterTypes;
        }

        private List<IScene> loadScenes()
        {
            var scenes = new List<IScene>();
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");

            using (StreamReader reader = File.OpenText(dataFilePath))
            {
                gameData = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                var scenesJson = (JArray)gameData.GetValue("Scenes");

                scenes = scenesJson.Select(s => new Scene
                (
                    (string)s["Title"],
                    (string)s["Description"],
                    (int)s["Difficulty"],
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

        private MapPosition loadStartPosition()
        {
            return new MapPosition((int)gameData["StartPosition"]["X"], (int)gameData["StartPosition"]["Y"]);
        }

        private void saveGameData()
        {
            gameData = JObject.FromObject(new
            {
                StartPosition = new
                {
                    X = StartPosition.X,
                    Y = StartPosition.Y
                },
                Scenes = from s in Scenes
                         select new
                         {
                             Title = s.Title,
                             Description = s.Description,
                             MapPosition = new
                             {
                                 X = s.MapPosition.X,
                                 Y = s.MapPosition.Y
                             },
                             NavigationCommands = from c in s.Commands.Where(c => c is INavigationCommand)
                                                  select new
                                                  {
                                                      Keys = c.Keys,
                                                      Description = c.Description,
                                                      Direction = Enum.GetName(typeof(Direction), (c as INavigationCommand).Direction)
                                                  }
                         }
            });

            var fileData = Encoding.ASCII.GetBytes(gameData.ToString());

            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");

            using (FileStream writer = File.Open(dataFilePath, FileMode.Truncate))
            {
                writer.Write(fileData, 0, fileData.Length);
            }
        }
    }
}
