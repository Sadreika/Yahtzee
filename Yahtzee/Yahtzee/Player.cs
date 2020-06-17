using System;
using System.Collections.Generic;
using System.Linq;

namespace Yahtzee
{
    public class Player : Board
    {
        private int[] DicesArray = new int[5];
        private bool[] ActiveDicesArray = new bool[5];
        private Random diceValue = new Random();
        private int throws;
        private bool turnEnds;

        public Player()
        {
            fillingLists();
        }
        public void settingTurn()
        {
            turnEnds = false;
            throws = 0;
            for(int i = 0; i < 5; i++)
            {
                DicesArray[i] = 0;
                ActiveDicesArray[i] = false;
            }
        }
        public void playersTurn(string whoPlays)
        {
            Console.WriteLine(whoPlays + " turn\n");
            settingTurn();

            throwingAllDices();
            throws = throws + 1;
            while(throws <= 3)
            {
                calculatingValues(DicesArray);
                showboard();
                showingData();
                asking(throws);
                if(turnEnds == true)
                {
                    break;
                }
                else
                {
                    rollDices();
                    throws = throws + 1;
                }
            }
        }
        public void rollDice(bool isActive, int whichDice)
        {
            if(isActive.Equals(false))
            {
                DicesArray[whichDice] = diceValue.Next(1, 7);
            }
        }
        public void rollDices()
        {
            for(int i = 0; i < 5; i++)
            {
                if(ActiveDicesArray[i].Equals(false))
                {
                    rollDice(ActiveDicesArray[i], i);
                }
            }   
        }
        public void throwingAllDices()
        {
            for (int i = 0; i < 5; i++)
            {
                rollDice(false, i);
            }
        }
        public void disablingDices(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (ActiveDicesArray[(Int16.Parse(list[i]) - 1)] == true)
                {
                    ActiveDicesArray[(Int16.Parse(list[i]) - 1)] = false;
                }
                else
                {
                    ActiveDicesArray[(Int16.Parse(list[i]) - 1)] = true;
                }
            }
        }
        public string gettingCorrectResultRowNumber()
        {
            Console.WriteLine("Write number of result row");
            string answer = Console.ReadLine();
            int correctNumber = checkingAnswer(answer);
            if (correctNumber == 1)
            {
                while (correctNumber == 1)
                {
                    Console.WriteLine("Write number of result row");
                    answer = Console.ReadLine();
                    correctNumber = checkingAnswer(answer);
                }
            }
            return answer;
        }
        public void enteringValues()
        {
            string row = gettingCorrectResultRowNumber();
            int valuesExists = fillingFinalValues(row);
            if (valuesExists == 1)
            {
                while (valuesExists == 1)
                {
                    Console.WriteLine("Value already exist in final result board");
                    row = gettingCorrectResultRowNumber();
                    valuesExists = fillingFinalValues(row);
                }
            }
        }
        public int askingAboutResult(int throws)
        {
            int writingFinalRezult = 0;
            if(throws == 3)
            {
                Console.WriteLine("You must enter last value");
                enteringValues();
            }
            else
            {
                Console.WriteLine("Do you want to write result?(y/n)");
                string answer = Console.ReadLine();
                if (answer.Equals("y") || answer.Equals("n"))
                {
                    if (answer.Equals("y"))
                    {
                        enteringValues();
                        turnEnds = true;
                        writingFinalRezult = 1;
                    }
                    else
                    {
                        writingFinalRezult = 0;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong value");
                    writingFinalRezult = 2;
                }
            }
            return writingFinalRezult;
        }
        public int checkingAnswer(string answer)
        {
            int isWrongValue = 0;
            try
            {
                int answerInt = Int16.Parse(answer);
                if(answerInt > 3 || answerInt < 1)
                {
                    Console.WriteLine("Value is wrong");
                    isWrongValue = 1;
                }
            } 
            catch(Exception)
            {
                Console.WriteLine("It is not a number");
                isWrongValue = 1;
            }
            return isWrongValue;
        }
        public void asking(int throws)
        {
            int validResult = askingAboutResult(throws);
      
            if (validResult == 0 && throws != 3)
            {
                Console.WriteLine("Which dice you want to keep? (write only numbers, if you want to reroll all dices, press enter) ");
                Console.WriteLine("Example: 1,2,3. ',' is a must");
                Console.WriteLine("If dice is already disabled, entering its number you will enable it and it will be rerolled ");
                string answer = Console.ReadLine();
                List<string> answerList = answer.Split(',').ToList();
                if (answerList.Count == 1 && (answerList[0] == ""))
                {
                    Console.WriteLine("No dices were selected");
                }
                else
                {
                    if (answerList.Count > 5)
                    {
                        Console.WriteLine("Too many numbers were entered");
                        asking(throws);
                    }
                    else
                    {
                        if (checkingIfAllElementsAreNumbers(answerList) == 1)
                        {
                            asking(throws);
                        }
                        else if (doNumbersReapeat(answerList) == 1)
                        {
                            asking(throws);
                        }
                        else
                        {
                            disablingDices(answerList);
                        }
                    }
                }
            }
            else if(validResult == 2)
            {
                asking(throws);
            }
            else
            {
                Console.WriteLine("Data writen to final list");
            }
        }
        public int checkingIfAllElementsAreNumbers(List<string> list)
        {
            int areNumbers = 0;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    int answerIntValue = Int16.Parse(list[i]);
                    if (answerIntValue > 5)
                    {
                        Console.WriteLine((i + 1) + " element is too large");
                        areNumbers = 1;
                    }
                    else if (answerIntValue < 1)
                    {
                        Console.WriteLine((i + 1) + " element is too small");
                        areNumbers = 1;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine((i + 1) + " element is not number");
                    areNumbers = 1;
                }
            }
            return areNumbers;
        }
        public int doNumbersReapeat(List<string> list)
        {
            int doesReapeat = 0;
            if(list.Count > 1)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (list[i].Equals(list[j]))
                        {
                            Console.WriteLine("Number reapeats");
                            doesReapeat = 1;
                            break;
                        }
                    }
                }
            }
            return doesReapeat;
        }
        public void showingData()
        {
            Console.WriteLine("Your dice values: " + DicesArray[0] +
                                               "|" + DicesArray[1] +
                                               "|" + DicesArray[2] +
                                               "|" + DicesArray[3] +
                                               "|" + DicesArray[4]);
        }
        public int result()
        {
            var finalPlayerValues = gettingFinalPlayerValues();
            int sum = 0;
            for (int i = 0; i < finalPlayerValues.Count; i++)
            {
                sum = sum + finalPlayerValues[i];
            }
            return sum;
        }
    }
}
