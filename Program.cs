using System;

namespace JogoDaVelha
{
    internal class Program
    {
        public static int Menu()
        {
            bool flag = true;
            string choice;
            int op = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("\n     .......::::: Bem-vindo ao Jogo da Velha :::::.......\n");
                Console.WriteLine("\n ...:: Menu ::...\n");
                Console.WriteLine(" 1 - Iniciar Novo Jogo");
                Console.WriteLine(" 2 - Sair do Jogo\n");
                Console.Write(" Escolha: ");
                choice = Console.ReadLine();
                int.TryParse(choice, out op);

                if ((op < 1) || (op > 2))
                {
                    Console.WriteLine("\n xxxx Opcao invalida.\n");
                    Console.WriteLine(" xxxx Pressione ENTER para voltar ao menu...\n");
                    Console.ReadKey();
                }
                else
                {
                    flag = false;
                }
            } while (flag);

            return op;
        }

        static void Main(string[] args)
        {
            bool flag = true;
            int option = 0;

            Board Game;

            option = Menu();

            do 
            {
                switch (option)
                {
                    case 1:
                        Console.WriteLine(" Novo Jogo");
                        Game = new Board();
                        Game.Play();
                        option = Menu();
                        break;
                    case 2:
                        flag = false;
                        break;
                }
            } while (flag);

            Console.WriteLine("\n\n oooo Obrigado por jogar conosco! \n\n");
        }
    }
}
