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
        /// <summary>
        /// Flag que determina se o questionário acabou
        /// </summary>
        public bool End { set; get; } = false;

        /// <summary>
        /// Flag que determina quem venceu. (True o computador, False o jogador)
        /// </summary>
        public bool Win { set; get; } = false;

        /// <summary>
        /// Novo Trait a ser adicionado
        /// </summary>
        public string NewTrait { set; get; } = null;

        /// <summary>
        /// Novo Animal a ser adicionado
        /// </summary>
        public string NewAnimal { set; get; } = null;

        /// <summary>
        /// A Nó Pai a qual o Animal a ser adicionado pertence.
        /// </summary>
        public Entity NewEntityParent { set; get; } = null;

        /// <summary>
        /// Adiciona um Nó filho ao Nó principal
        /// </summary>
        /// <param name="child">A descrição do Nó filho.</param>
        /// <returns>Rertorna o Nó criado.</returns>
        public Entity AddChild(String child)
        {
            return AddChild(Root, child);
        }

        /// <summary>
        /// Adiciona um Filho a um Nó
        /// </summary>
        /// <param name="entity">O Nó pai</param>
        /// <param name="child">A descrição do Nó filho</param>
        /// <returns>Retona o Nó criado.</returns>
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

        public Entity Root { get; } = new Entity("");

        /// <summary>
        /// Adiciona um novo Trait/Animal
        /// </summary>
        public void AddNew()
        {
            Entity newEntity = AddChild(NewEntityParent, NewTrait);
            AddChild(newEntity, NewAnimal);
        }

        /// <summary>
        /// Reinicia o Controller
        /// </summary>
        public void Reset()
        {
            NewTrait = null;
            NewAnimal = null;
            NewEntityParent = null;
            Win = false;
            End = false;
        }

    }
}
