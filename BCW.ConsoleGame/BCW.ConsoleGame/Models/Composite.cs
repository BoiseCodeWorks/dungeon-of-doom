using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models
{
    public abstract class Composite : IComposite
    {
        protected List<IComposite> items;

        public string Name { get; set; }

        public int Count
        {
            get
            {
                var count = 0;

                foreach (var item in items)
                {
                    var itemCount = item.Count;

                    count += itemCount > 0 ? itemCount : 1;
                }

                return count;
            }
        }

        public void AddItem(IComposite item)
        {
            items.Add(item);
        }

        public void AddItem(string path, IComposite item)
        {
            IComposite composite = this;

            var names = path.Split('/');

            foreach (var name in names)
            {
                var child = composite.GetItem(name);

                if (child == null)
                {
                    var itemType = item.GetType();

                    child = (IComposite)Activator.CreateInstance(itemType);
                    child.Name = name;

                    composite.AddItem(child);
                }

                composite = child;
            }

            composite.AddItem(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public IComposite GetItem(string name)
        {
            return items.FirstOrDefault(i => i.Name == name);
        }

        public IList<IComposite> GetItems()
        {
            return items;
        }

        public IList<IComposite> GetItems(string path)
        {
            IComposite inventory = this;

            var names = path.Split('/');

            foreach (var name in names)
            {
                inventory = inventory.GetItem(name);

                if (inventory == null) return new List<IComposite>();
            }

            return inventory.GetItems();
        }

        public void RemoveItem(IComposite item)
        {
            items.Remove(item);
        }

        public void RemoveItem(string name)
        {
            var item = items.FirstOrDefault(i => i.Name == name);

            if(item != null)
            {
                items.Remove(item);
            }
        }

        public void RemoveItem(string path, string name)
        {
            IComposite inventory = this;

            var segments = path.Split('/');

            foreach (var segment in segments)
            {
                inventory = inventory.GetItem(segment);

                if (inventory == null) return;
            }

            inventory.RemoveItem(name);

            if (inventory.Count < 1)
            {
                prune(path);
            }
        }

        public void RemoveItem(string path, IComposite item)
        {
            IComposite inventory = this;

            var names = path.Split('/');

            foreach (var name in names)
            {
                inventory = inventory.GetItem(name);

                if (inventory == null) return;
            }

            inventory.RemoveItem(item);

            if (inventory.Count < 1)
            {
                prune(path);
            }
        }

        public void RemoveItems(string path)
        {
            IComposite inventory = this;

            var names = path.Split('/');

            foreach (var name in names)
            {
                inventory = inventory.GetItem(name);

                if (inventory == null) return;
            }

            inventory.Clear();

            prune(path);
        }

        private void prune(string path)
        {
            var segments = path.Split('/').ToList();

            segments.Remove(segments.Last());

            var parent = String.Join("/", segments);

            var count = GetItems(path).Count;

            while(count < 1)
            {
                var segment = segments.Last();

                segments.Remove(segment);

                var newPath = String.Join("/", segments);

                RemoveItems(newPath);

                count = GetItems(newPath).Count;
            }
        }
    }
}
