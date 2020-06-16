using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Program : Player
    {
        static void Main(string[] args)
        {
            Player playerObject = new Player();
            playerObject.playersTurn();
            Console.WriteLine("TurnEnded");
            
        }
    }
}
