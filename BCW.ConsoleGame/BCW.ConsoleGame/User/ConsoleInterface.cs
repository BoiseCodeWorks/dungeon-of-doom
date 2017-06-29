using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.User
{
    public class ConsoleInterface : IUserInterface
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void Display(string text)
        {
            Console.WriteLine(text);
        }

        public string GetInput(string prompt)
        {
            Console.Write(prompt);

            return Console.ReadLine();
        }
    }
}
