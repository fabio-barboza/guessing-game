using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuessingGame;
using System.Collections.Generic;

namespace GuessingGameTest
{
    /// <summary>
    /// Classe de Testes do Jogo
    /// </summary>
    [TestClass]
    public class GameTests
    {
        
        /// <summary>
        /// Efetua o Test padrão com apenas o Tubarão e o Macaco.
        /// </summary>
        [TestMethod]
        public void DefaultTest()
        {
            Game Game = new Game();
            Game.DoQuestions(null, new string[] { "lives on water" }, "", "Shark");
            Assert.IsTrue(Game.Controller.Win);
        }

        /// <summary>
        /// Efetua 2 testes, o primeiro o Computador perde e adiciona um novo Trait e o segundo o Jogador ganha.
        /// </summary>
        [TestMethod]
        public void AddingNewTraitAndAnimal()
        {
            Game Game = new Game();
            Game.DoQuestions(null, new string[] { "lives on water" }, "jumps", "Dolphin");
            Assert.IsFalse(Game.Controller.Win);
            if (Game.Controller.NewEntityParent != null) Game.Controller.AddNew();
            Game.Controller.Reset();
            Game.DoQuestions(null, new string[] { "lives on water", "jumps" }, "jumps", "Dolphin");
            Assert.IsTrue(Game.Controller.Win);
        }

    }
}
