using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    internal class GamesList
    {
        public Board Game { get; set; }
        public int TotalGames { get; set; }
        public string Xs { get; set; }
        public string Os { get; set; }

        public GamesList()
        {
            Game = null;
            Xs = null;
            Os = null;
            TotalGames = 0;
        }

        /*public void CreateListGames(Board newGame)
        {
            Game = newGame;
            TotalGames++;

            Play(Game);
        }

        public void Play(Board game)
        {
            Console.Clear();
            Console.WriteLine("\n Jogador 1 => X");
            Console.WriteLine("\n Jogador 2 => O");
            Console.WriteLine("\n Status do jogo: Iniciado");

            Console.WriteLine("\n ----------------------------------- \n");
            Console.WriteLine("\n Jogador 1 ");
        }

        public string[,] StatusBoard(Board game)
        {
            for(int l = 0; l < game.)
        }*/
    }
}
