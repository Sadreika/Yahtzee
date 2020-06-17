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
            Player player2Object = new Player();
            for(int i = 0; i < 3; i++)
            {
                playerObject.playersTurn("Player1");
                Console.Clear();
                player2Object.playersTurn("Player2");
                Console.Clear();
            }
            Console.WriteLine("First player sum: " + playerObject.result());
            Console.WriteLine("Second player sum: " + player2Object.result());
            if(playerObject.result() > player2Object.result())
            {
                Console.WriteLine("Player1 won");
            }
            else if(playerObject.result() < player2Object.result())
            {
                Console.WriteLine("Player2 won");
            }
            else
            {
                Console.WriteLine("Draw");
            }
        }
    }
}
