using Autofac;
using Autofac.Configuration;
using BCW.ConsoleGame.Data;
using BCW.ConsoleGame.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "autofac.json");
            var config = new ConfigurationBuilder();

            config.AddJsonFile(configFilePath);

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();

            builder.RegisterModule(module);

            var container = builder.Build();

            var game = new Game(container.Resolve<IDataProvider>(), container.Resolve<IUserInterface>());
        }
    }
}
