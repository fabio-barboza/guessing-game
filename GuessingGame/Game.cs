using GuessingGame.Controllers;
using GuessingGame.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessingGame
{
    /// <summary>
    /// Classe principal do Jogo
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Nome do Jogo
        /// </summary>
        private const string _gameName = "Guessing Game";

        /// <summary>
        /// Controlador do jogo que armazena os Traits e Animais
        /// </summary>
        public GuessingController Controller { set; get; } = new GuessingController();

        /// <summary>
        /// Contrutor Default
        /// </summary>
        public Game()
        {
            Controller.AddChild("Monkey");
            Entity entity = Controller.AddChild("lives on water");
            entity = Controller.AddChild(entity, "Shark");
        }

        /// <summary>
        /// Inicia um novo jogo
        /// </summary>
        private void PlayGame()
        {
            Controller.Reset();
            DoQuestions();
            if (Controller.NewEntityParent != null) Controller.AddNew();
            MessageBox.Show(Controller.Win ? "I win again!" : "You win!", _gameName);
        }

        /// <summary>
        /// Verifica se o animal possui o Trait
        /// </summary>
        /// <param name="traits">Os Traits do animal</param>
        /// <param name="trait">O Trait a verificar</param>
        /// <returns></returns>
        private Boolean HasTrait(string[] traits, string trait)
        {
            bool result = false;
            traits.ToList<String>().ForEach(t => {
                if (t.Equals(trait)) result = true;
            });
            return result;
        }

        /// <summary>
        /// Método que finaliza o questionário.
        /// </summary>
        /// <param name="entity">A entidade</param>
        /// <param name="newTrait">Novo Trait (Utilizado em casos de teste)</param>
        /// <param name="thoughtAnimal">Novo Animal (Utilizado em casos de teste)</param>
        private void End(Entity entity, string newTrait = null, string thoughtAnimal = null)
        {
            DialogResult result;
            Controller.NewTrait = newTrait;
            Controller.NewAnimal = thoughtAnimal;

            if (String.IsNullOrWhiteSpace(thoughtAnimal))
            {
                result = MessageBox.Show($"Is the animal that you thought about a {entity.Description}?", _gameName, MessageBoxButtons.YesNo);
            }
            else
            {
                result = entity.Description.Equals(thoughtAnimal) ? DialogResult.Yes : DialogResult.No;
            }

            if (result == DialogResult.Yes)
            {
                Controller.Win = true;
            }
            else
            {
                //Caso seja necessário adicionar um no elemento, o mesmo deve ser feito após o termino da navegação na lista
                Controller.NewEntityParent = entity.Parent ?? Controller.Root;
                while (String.IsNullOrWhiteSpace(Controller.NewAnimal))
                {
                    Controller.NewAnimal = Interaction.InputBox("What was the animal that you thought about?", _gameName);
                }
                while (String.IsNullOrWhiteSpace(Controller.NewTrait))
                {
                    Controller.NewTrait = Interaction.InputBox($"A {Controller.NewAnimal} ______ but a {entity.Parent.Children.Last().Description} does not (Fill it with an animal trait, like \"lives in water\").", _gameName);
                }
            }
            Controller.End = true;
            return;
        }

        /// <summary>
        /// Método que faz as perguntas
        /// </summary>
        /// <param name="entities">A lista de entidades onde deverá ser feita a iteração</param>
        /// <param name="traits">A lista de traits do animal (Utilizado em casos de teste)</param>
        /// <param name="newTrait">O novo Trait (Utilizado em casos de teste)</param>
        /// <param name="thoughtAnimal">O animal pensado (Utilizado em casos de teste)</param>
        public void DoQuestions(List<Entity> entities = null, string[] traits = null, string newTrait = null, string thoughtAnimal = null)
        {
            if (entities == null) entities = Controller.Root.Children;

            if (entities.Count > 1)
            {
                entities.ForEach(e =>
                {
                    if (Controller.End) return;
                    if (e.Children.Count == 0)
                    {
                        End(e, newTrait, thoughtAnimal);
                    }
                    else
                    {
                        DialogResult result;
                        if (traits == null) //Caso não tenha nenhum Trait associado o sistema pergunta.
                        {
                            result = MessageBox.Show($"Does the animal that you thought about {e.Description}?", _gameName, MessageBoxButtons.YesNo);
                        }
                        else
                        {
                            result = HasTrait(traits, e.Description) ? DialogResult.Yes : DialogResult.No;
                        }
                        if (result == DialogResult.Yes)
                        {
                            DoQuestions(e.Children, traits, newTrait, thoughtAnimal);
                        }
                    }
                });
            }
            else
            {
                End(entities[0], newTrait, thoughtAnimal);
            }
        }

        /// <summary>
        /// Loop para manter o jogador até que cancele.
        /// </summary>
        public void Start()
        {
            while (MessageBox.Show("Think about an animal", _gameName, MessageBoxButtons.OKCancel) != DialogResult.Cancel)
            {
                PlayGame();
            }
        }
    }
}