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
            Player pcObject = new Player();
            for(int i = 0; i < 3; i++)
            {
                playerObject.playersTurn("Player1");
                Console.Clear();
                pcObject.playersTurn("PC");
                Console.Clear();
            }
        }
    }
}
