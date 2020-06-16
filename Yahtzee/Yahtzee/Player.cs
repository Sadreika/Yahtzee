using StatsdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Player : Board
    {
        private int[] DicesArray = new int[5];
        private bool[] ActiveDicesArray = new bool [5];
        private int throws = 0;
        private Random diceValue = new Random();
        private bool turnEnds;
        public void playersTurn()
        {
            turnEnds = false;
            throws = 0;
            fillingLists();
            throwingAllDices();
            throws = throws + 1;
            while(throws < 3)
            {
                calculatingValues(DicesArray);
                showboard();
                showingData();
                asking();
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
            showingData();
            
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

        public int askingAboutResult()
        {
            int writingFinalRezult = 0;
            Console.WriteLine("Do you want to write result?(y/n)");
            string answer = Console.ReadLine();
            if (answer.Equals("y"))
            {
                Console.WriteLine("Write number of result row");
                answer = Console.ReadLine();
                if (checkingAnswer(answer) == 1)
                {
                    askingAboutResult();
                }
                else
                {
                    writingFinalRezult = 1;
                    if (fillingFinalValues(answer) == 1)
                    {
                        Console.WriteLine("Value already exist in final result board");
                        askingAboutResult();
                    }
                    else
                    {
                        turnEnds = true;
                    }
                }
            }
            else if (answer.Equals("n"))
            {
            }
            else
            {
                Console.WriteLine("Wrong value");
                askingAboutResult();
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
        public void asking()
        {
            if(askingAboutResult() == 0)
            {
                Console.WriteLine("Which dice you want to keep? (write only numbers, if you want to reroll all dices, press enter) ");
                Console.WriteLine("Example: 1,2,3. ',' is a must");
                string answer = Console.ReadLine();
                List<string> answerList = answer.Split(',').ToList();
                if (answerList.Count == 1 && (answerList[0] == ""))
                {
                    Console.WriteLine("No dices were saved");
                }
                else
                {
                    if (answerList.Count > 5)
                    {
                        Console.WriteLine("Too many numbers were entered");
                        asking();
                    }
                    else
                    {
                        if (checkingIfAllElementsAreNumbers(answerList) == 1)
                        {
                            asking();
                        }
                        else if (doNumbersReapeat(answerList) == 1)
                        {
                            asking();
                        }
                        else
                        {
                            disablingDices(answerList);
                        }
                    }
                }
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
    }
}
