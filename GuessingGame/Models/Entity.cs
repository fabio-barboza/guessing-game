using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    /// <summary>
    /// Classe modelo que reprsenta um Nó
    /// </summary>
    public class Entity
    {

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="description">A descrição do Nó.</param>
        public Entity(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// A descrição do Nó.
        /// </summary>
        public string Description
        {
            set; get;
        }

        /// <summary>
        /// O pai do Nó.
        /// </summary>
        public Entity Parent
        {
            set; get;
        }

        /// <summary>
        /// Cada Nó pode conter outros Nós como filhos.
        /// </summary>
        public List<Entity> Children { set; get; } = new List<Entity>();

        /// <summary>
        /// Hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Description.GetHashCode();
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">O outro Nó a ser comparado</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Description.Trim().ToLower().Equals(((Entity)obj).Description.Trim().ToLower());
        }
    }
}
