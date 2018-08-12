using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuessingGame.Controllers;
using GuessingGame.Models;
using System.Collections.Generic;

namespace GuessingGameTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string _gameName = "Guessing Game";

        private GuessingController controller = null;

        public bool DoQuestions(string trait, string thoughtAnimal, List<Entity> entities = null)
        {
            bool result = false;
            if (entities == null) entities = controller.Root.Children;

            if (entities.Count > 1)
            {
                entities.ForEach(e =>
                {
                    if (controller.End) return;
                    if (e.Children.Count == 0)
                    {
                        End(trait, thoughtAnimal, e);
                    }
                    else
                    {
                        //DialogResult result = MessageBox.Show($"Does the animal that you thought about {e.Description}?", _gameName, MessageBoxButtons.YesNo);
                        if (e.Description.Equals(trait))
                        {
                            DoQuestions(trait, thoughtAnimal, e.Children);
                        }
                    }
                });
            }
            else
            {
                result = End(trait, thoughtAnimal, entities[0]);
            }

            return result;
        }

        private bool End(string trait, string thoughtAnimal, Entity entity)
        {
            bool result = false;
            //DialogResult result = MessageBox.Show($"Is the animal that you thought about a {entity.Description}?", _gameName, MessageBoxButtons.YesNo);
            if (entity.Description.Equals(thoughtAnimal))
            {
                //MessageBox.Show("I win again!", _gameName);
                result = true;
            }
            else
            {
                //Caso seja necessário adicionar um no elemento, o mesmo deve ser feito após o termino da navegação na lista
                controller.NewEntityParent = entity.Parent ?? controller.Root;
                //while (String.IsNullOrWhiteSpace(controller.NewAnimal))
                //{
                //controller.NewAnimal = Interaction.InputBox("What was the animal that you thought about?", _gameName);
                controller.NewAnimal = thoughtAnimal;
                //}
                //while (String.IsNullOrWhiteSpace(controller.NewTrait))
                //{
                //controller.NewTrait = Interaction.InputBox($"A {controller.NewAnimal} ______ but a {entity.Parent.Children.Last().Description} does not (Fill it with an animal trait, like \"lives in water\").", _gameName);
                controller.NewTrait = trait;
                //}
            }
            controller.End = true;
            return result;
        }

        //public void Start()
        //{
        //    while (MessageBox.Show("Think about an animal", _gameName, MessageBoxButtons.OKCancel) != DialogResult.Cancel)
        //    {
        //        if (controller.NewEntityParent != null) controller.AddNew();
        //        controller.Reset();
        //        DoQuestions(null, thoughtAnimal);
        //    }
        //}

        [TestMethod]
        public void TestMethod1()
        {
            controller = new GuessingController();
            controller.AddChild("Monkey");
            Entity entity = controller.AddChild("lives on water");
            entity = controller.AddChild(entity, "Shark");
            
            string trait = "lives on water";
            string thoughtAnimal = "Shark";
            DoQuestions(trait, thoughtAnimal, null);
        }
    }
}
