using GuessingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Controllers
{
    public class GuessingController
    {
        private readonly Entity _root = new Entity("");

        public bool Found { set; get; } = false;

        public string NewTrait { set; get; } = null;
        public string NewAnimal { set; get; } = null;
        public Entity NewEntityParent { set; get; } = null;

        public Entity AddChild(String child)
        {
            return AddChild(_root, child);
        }

        public Entity AddChild(Entity entity, string child)
        {
            Entity newEntity = new Entity(child)
            {
                Parent = entity
            };
            entity.Children.Add(newEntity);
            //Força o pai da entidade a ordenar de forma descrescente os filhos pelo numero de filhos que cada um tem.
            if (entity.Parent != null)
            {
                entity.Parent.Children = entity.Parent.Children.OrderByDescending(e => e.Children.Count).ToList<Entity>();
            }
            return newEntity;
        }

        public Entity Root
        {
            get
            {
                return _root;
            }
        }

        public void AddNew()
        {
            Entity newEntity = AddChild(NewEntityParent, NewTrait);
            AddChild(newEntity, NewAnimal);
        }

        public void Reset()
        {
            NewTrait = null;
            NewAnimal = null;
            NewEntityParent = null;
            Found = false;
        }

    }
}
