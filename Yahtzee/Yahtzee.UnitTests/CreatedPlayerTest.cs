using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yahtzee.UnitTests
{
    [TestClass]
    public class CreatedPlayerTest
    {
        [TestMethod]
        public void CheckingDialogWithPlayers_DiceToKeep_Return_0()
        {
            Random how_many_elements = new Random();
            Player testObjectPlayer = new Player();
            List<string> numberList = new List<string>();
            numberList.Add("5");
            numberList.Add("2");

            var result = testObjectPlayer.doNumbersReapeat(numberList);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CheckingAnswer_CheckingIfFunctionGetValidData_Return_0()
        {
            Player testObjectPlayer = new Player();
            List<string> goodAnswers = new List<string>();
            goodAnswers.Add("1");
            goodAnswers.Add("2");
            goodAnswers.Add("3");

            for(int i = 0; i < goodAnswers.Count; i++)
            {
                var result = testObjectPlayer.checkingAnswer(goodAnswers[i]);
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void CheckingAnswer_CheckingIfFunctionGetValidData_Return_1()
        {
            Player testObjectPlayer = new Player();
            List<string> badAnswers = new List<string>();
            badAnswers.Add("k");
            badAnswers.Add("+");
            badAnswers.Add("5");
            badAnswers.Add("-1");

            for (int i = 0; i < badAnswers.Count; i++)
            {
                var result = testObjectPlayer.checkingAnswer(badAnswers[i]);
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void checkingIfAllElementsAreNumber_Return_0()
        {
            Player testObjectPlayer = new Player();
            List<string> goodAnswers = new List<string>();
            goodAnswers.Add("1");
            goodAnswers.Add("2");
            goodAnswers.Add("3");
            goodAnswers.Add("4");

            var result = testObjectPlayer.checkingIfAllElementsAreNumbers(goodAnswers);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void checkingIfAllElementsAreNumber_Return_1()
        {
            Player testObjectPlayer = new Player();
            List<string> badAnswers = new List<string>();
            badAnswers.Add("-1");
            badAnswers.Add("6");
            badAnswers.Add("*");
            badAnswers.Add("4");

            var result = testObjectPlayer.checkingIfAllElementsAreNumbers(badAnswers);
            Assert.AreEqual(1, result);
        }
    }
}
