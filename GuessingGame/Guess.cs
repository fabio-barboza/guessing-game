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
    class Guess
    {
        private const string _gameName = "Guessing Game";

        private GuessingController service = new GuessingController();

        public Guess()
        {
            service.AddChild("Monkey");
            Entity entity = service.AddChild("lives on water");
            entity = service.AddChild(entity, "Shark");
        }

        public void DoQuestions(List<Entity> entities = null)
        {
            if (entities == null) entities = service.Root.Children;

            if (entities.Count > 1)
            {
                entities.ForEach(e =>
                {
                    if (service.Found) return;
                    if (e.Children.Count == 0)
                    {
                        End(e);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show($"Does the animal that you thought about {e.Description}?", _gameName, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            DoQuestions(e.Children);
                        }
                    }
                });
            }
            else
            {
                End(entities[0]);
            }
        }

        private void End(Entity entity)
        {
            DialogResult result = MessageBox.Show($"Is the animal that you thought about a {entity.Description}?", _gameName, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("I win again!", _gameName);
            }
            else
            {
                //Caso seja necessário adicionar um no elemento, o mesmo deve ser feito após o termino da navegação na lista
                service.NewEntityParent = entity.Parent ?? service.Root;
                while (String.IsNullOrWhiteSpace(service.NewAnimal))
                {
                    service.NewAnimal = Interaction.InputBox("What was the animal that you thought about?", _gameName);
                }
                while (String.IsNullOrWhiteSpace(service.NewTrait))
                {
                    service.NewTrait = Interaction.InputBox($"A {service.NewAnimal} ______ but a {entity.Parent.Children.Last().Description} does not (Fill it with an animal trait, like \"lives in water\").", _gameName);
                }
            }
            service.Found = true;
            return;
        }

        public void Start()
        {
            while (MessageBox.Show("Think about an animal", _gameName, MessageBoxButtons.OKCancel) != DialogResult.Cancel)
            {
                if (service.NewEntityParent != null) service.AddNew();
                service.Reset();
                DoQuestions();
            }
        }
    }
}