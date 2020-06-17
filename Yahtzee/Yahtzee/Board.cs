using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Board
    {
        private List<string> boardAtributesNames = new List<string>();
        private List<int> values = new List<int>();
        private List<int> finalPlayerValues = new List<int>();

        public int fillingFinalValues(string choice)
        {
            int cantWriteNewValue = 0;
            if(finalPlayerValues[Int16.Parse(choice) - 1] == 0)
            {
                finalPlayerValues[Int16.Parse(choice) - 1] = values[Int16.Parse(choice) - 1];
            }
            else
            {
                cantWriteNewValue = 1;
            }
            return cantWriteNewValue;
        }

        public void fillingLists()
        {
            string[] attributes = {
            "Fives", "One Pair",
            "Chance", "Total score"};
            boardAtributesNames.AddRange(attributes);
            for(int i = 0; i < boardAtributesNames.Count; i++)
            {
                values.Add(0);
                finalPlayerValues.Add(0);
            }
        }
        public void showboard()
        {
            
            for (int i = 0; i < boardAtributesNames.Count; i++)
            {
                if(boardAtributesNames[i].Length <= 6)
                {
                    Console.WriteLine(boardAtributesNames[i] + "\t\t" + values[i] + "\tFinal: " + finalPlayerValues[i]);
                }
                else
                {
                    Console.WriteLine(boardAtributesNames[i] + "\t" + values[i] + "\tFinal: " + finalPlayerValues[i]);
                }
            }
            Console.WriteLine();
        }
        public void calculatingValues(int[] dicesArray)
        {
            for(int i = 0; i < values.Count; i++)
            {
                values[i] = 0;
            }

            int howManyFives = 0;

            for(int i = 0; i < dicesArray.Length; i++)
            {
                if(dicesArray[i] == 5)
                {
                    howManyFives = howManyFives + 1;
                }
                values[2] = values[2] + dicesArray[i];
            }
            values[0] = howManyFives * 5;

            for (int i = 0; i < dicesArray.Length - 1; i++)
            {
                for (int j = i + 1; j < dicesArray.Length; j++)
                {
                    if (dicesArray[i].Equals(dicesArray[j]))
                    {
                        if(dicesArray[i] * 2 > values[1])
                        {
                            values[1] = dicesArray[i] * 2;
                        }
                    }
                }
            }
        }
    }
}
