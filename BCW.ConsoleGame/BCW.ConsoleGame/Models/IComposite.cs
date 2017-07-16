using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models
{
    public interface IComposite
    {
        string Name { get; set; }
        int Count { get; }
        void AddItem(IComposite item);
        void AddItem(string path, IComposite item);
        void Clear();
        void RemoveItem(IComposite item);
        void RemoveItem(string name);
        void RemoveItem(string path, string name);
        void RemoveItems(string path);
        void RemoveItem(string path, IComposite item);
        IComposite GetItem(string name);
        IList<IComposite> GetItems();
        IList<IComposite> GetItems(string path);
    }
}
