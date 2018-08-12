using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    class Entity
    {
        private List<Entity> _children = new List<Entity>();

        public Entity(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            set; get;
        }

        public Entity Parent
        {
            set; get;
        }

        public List<Entity> Children
        {
            set
            {
                this._children = value;
            }
            get
            {
                return this._children;
            }
        }

        public override int GetHashCode()
        {
            return this.Description.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Description.Trim().ToLower().Equals(((Entity)obj).Description.Trim().ToLower());
        }
    }
}
