using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    internal class Board
    {
        public string[,] Game { get; set; }
        public int PlayerOne { get; set; }
        public int PlayerTwo { get; set; }
        public int TotalPlay { get; set; }

        public Board()
        {
            Game = new string[3, 3];
            for (int l = 0; l < Game.GetLength(0); l++)
            {
                for (int c = 0; c < Game.GetLength(1); c++)
                {
                    Game[l, c] = "_";
                }
            }
            PlayerOne = 0;
            PlayerTwo = 0;
            TotalPlay = 0;
        }

        public override string ToString()
        {
            return @"\t \/\/\/\/\ CRIANDO O JOGO /\/\/\/\/";
        }

        public void NewGame(string[,] newGame)
        {
            Game = newGame;
        }

        public void Play()
        {
            int position = 0, player = 0;
            string choice, status = "Iniciado", item;
            bool flag = true, flagPosition = true, flagBoard = true;

            Console.Clear();
            Console.WriteLine("\n     .......::::: Bem-vindo ao Jogo da Velha :::::.......\n");
            InitialBoard();
            do // referente ao jogo ao todo
            {
                do // flag escolha de posicao
                {
                    Console.Clear();
                    Console.WriteLine($"\n Status do jogo: {status}");
                    Console.WriteLine($"\n Total de Jogadas: {TotalPlay}\n");
                    Console.WriteLine($" Jogador 1 ( X ): {PlayerOne}\n");
                    Console.WriteLine($" Jogador 2 ( O ): {PlayerTwo}\n");
                    
                    do
                    {
                        Console.Write("\n ..: Deseja rever as posicoes? Digite 1 para rever. R: ");
                        choice = Console.ReadLine();
                        int.TryParse(choice, out position);
                        if (position == 1)
                        {
                            InitialBoard("t");
                            Console.WriteLine($"\n Status do jogo: {status}");
                            Console.WriteLine($"\n Total de Jogadas: {TotalPlay}\n");
                            Console.WriteLine($" Jogador 1 ( X ): {PlayerOne}\n");
                            Console.WriteLine($" Jogador 2 ( O ): {PlayerTwo}\n");
                        }
                        flagBoard = false;
                    } while (flagBoard);

                    Console.WriteLine("\n ----------------------------------- \n");
                    Console.WriteLine("\n ..:: Tabuleiro no momento ::..\n");
                    StatusBoard();

                    if (player == 0)
                    {
                        PlayerOne++;
                        TotalPlay++;
                        item = "X";
                        player = 1;
                    }
                    else if ((PlayerOne > 0) && (player % 2 != 0))
                    {
                        PlayerOne++;
                        TotalPlay++;
                        item = "X";
                        player = 1;
                    }
                    else
                    {
                        PlayerTwo++;
                        TotalPlay++;
                        item = "O";
                        player = 2;
                    }
                    Console.WriteLine("\n ..: Atencao: Pressione a letra ' E ' e de ENTER caso deseje encerrar o jogo.");
                    Console.Write($"\n Jogador {player} ({item}): Qual posicao jogar? R: ");
                    choice = Console.ReadLine();

                    if ((choice == "E") || (choice == "e"))
                    {
                        Console.WriteLine($"\n xxxx Jogo encerrado pelo Jogador {player}.");
                        Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu inicial...");
                        Console.ReadKey();
                        return;

                    }
                    else
                    {
                        int.TryParse(choice, out position);

                        if ((position < 1) || (position > 9))
                        {
                            Console.WriteLine("\n xxxx Digite apenas de 1 a 9.\n");
                            Console.WriteLine("\n xxxx Pressione ENTER para voltar.");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (VerifyPosition(position))
                            {
                                ConfirmPosition(position, item);
                                if ((TotalPlay > 4) && (TotalPlay < 9))
                                {
                                    // verificar ganhador retorna o bool pra encerrar o jogo
                                    if (VerifyWinner(item))
                                    {
                                        Winner(player, item);
                                        flagPosition = false;
                                        flag = false;
                                    }
                                }
                                else if (TotalPlay == 9)
                                {
                                    Draw();
                                    flagPosition = false;
                                    flag = false;
                                }
                                player++;
                            }
                            else
                            {
                                Console.WriteLine("\n xxxx Nessa posicao já existe uma marcacao. Escolha outra.\n");
                                Console.WriteLine("\n xxxx Pressione ENTER para voltar.");
                                Console.ReadKey();
                            }
                        }
                    }
                } while (flagPosition);
            } while (flag);

            // funcao para mostrar o ganhador
        }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("\n ....::: TEMOS UM EMPATE :::....\n");
            Console.WriteLine($"\n Após {TotalPlay} jogadas temos um empate!\n");
            Console.WriteLine(" ...:: Abaixo temos o tabuleiro finalizado\n");
            StatusBoard();
            Console.WriteLine("\n oooo Vamos para a proxima?");
            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu inicial...");
            Console.ReadKey();
        }

        public void Winner(int player, string item)
        {
            Console.Clear();
            Console.WriteLine("\n ....::: TEMOS UM GANHADOR :::....\n");
            Console.WriteLine($"\n Após {TotalPlay} jogadas, o Jogador {player} marcando ' {item} ' ganhou o jogo!\n");
            Console.WriteLine(" ...:: Abaixo temos o tabuleiro finalizado\n");
            StatusBoard();
            Console.WriteLine($"\n ...:: PARABENS Jogador {player}!!!! ::... \n");
            Console.WriteLine("\n oooo Vamos para a proxima?");
            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu inicial...");
            Console.ReadKey();
        }

        public bool VerifyWinner(string item)
        {
            bool flag = false;
            // verifica as linhas
            for (int l = 0; l < Game.GetLength(0); l++)
            {
                if ((Game[l, 0] == item) && (Game[l, 1] == item) && (Game[l, 2] == item))
                {
                    flag = true;
                }
            }

            // verifica as colunas
            for (int c = 0; c < Game.GetLength(1); c++)
            {
                if ((Game[0, c] == item) && (Game[1, c] == item) && (Game[2, c] == item))
                {
                    flag = true;
                }
            }

            // verifica a diagonal principal
            if ((Game[0, 0] == item) && (Game[1, 1] == item) && (Game[2, 2] == item))
            {
                flag = true;
            }

            // verifica a diagonal secundaria
            if ((Game[0, 2] == item) && (Game[1, 1] == item) && (Game[2, 0] == item))
            {
                flag = true;
            }
            return flag;
        }

        public void ConfirmPosition(int position, string item)
        {
            switch (position)
            {
                case 1:
                    Game[0, 0] = item;
                    break;
                case 2:
                    Game[0, 1] = item;
                    break;
                case 3:
                    Game[0, 2] = item;
                    break;
                case 4:
                    Game[1, 0] = item;
                    break;
                case 5:
                    Game[1, 1] = item;
                    break;
                case 6:
                    Game[1, 2] = item;
                    break;
                case 7:
                    Game[2, 0] = item;
                    break;
                case 8:
                    Game[2, 1] = item;
                    break;
                default:
                    Game[2, 2] = item;
                    break;
            }
        }

        public bool VerifyPosition(int position)
        {
            switch (position)
            {
                case 1:
                    if (Game[0, 0] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 2:
                    if (Game[0, 1] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 3:
                    if (Game[0, 2] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 4:
                    if (Game[1, 0] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 5:
                    if (Game[1, 1] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 6:
                    if (Game[1, 2] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 7:
                    if (Game[2, 0] != "_")
                        return false;
                    else
                        return true;
                    break;
                case 8:
                    if (Game[2, 1] != "_")
                        return false;
                    else
                        return true;
                    break;
                default:
                    if (Game[2, 2] != "_")
                        return false;
                    else
                        return true;
                    break;
            }
        }

        public void InitialBoard(string viewBoard = "")
        {
            //string[,] tempBoard = new string[3, 3];

            if (viewBoard != "")
            {
                Console.Clear();
                Console.WriteLine(" As posicoes sao conforme o exemplo abaixo: \n");

                Console.WriteLine(" +-----------------+");
                Console.WriteLine(" |  1  |  2  |  3  |");
                Console.WriteLine(" |-----------------|");
                Console.WriteLine(" |  4  |  5  |  6  |");
                Console.WriteLine(" |-----------------|");
                Console.WriteLine(" |  7  |  8  |  9  |");
                Console.WriteLine(" +-----------------+");

                Console.WriteLine("\n Pressione ENTER para voltar ao jogo...");
                Console.ReadKey();
                Console.Clear();

            }
            else
            {
                Console.WriteLine("\n ...: Inicialmente iremos mostrar as posicoes de jogada \n");
                Console.WriteLine(" Sempre que for sua vez, será pedido para selecionar uma posicao para jogar.");
                Console.WriteLine(" As posicoes vao de 1 a 9, contando da esquerda para a direita, de cima para baixo.");
                Console.WriteLine(" Segue um exemplo abaixo: \n");

                Console.WriteLine(" +-----------------+");
                Console.WriteLine(" |  1  |  2  |  3  |");
                Console.WriteLine(" |-----------------|");
                Console.WriteLine(" |  4  |  5  |  6  |");
                Console.WriteLine(" |-----------------|");
                Console.WriteLine(" |  7  |  8  |  9  |");
                Console.WriteLine(" +-----------------+");

                Console.WriteLine("\n Os jogadores são identificados conforme segue:");
                Console.WriteLine("\n Jogador 1 => X");
                Console.WriteLine("\n Jogador 2 => O");

                Console.WriteLine("\n Pressione ENTER para comecar o jogo...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void StatusBoard()
        {
            Console.WriteLine(" +-------------------+");
            //Console.WriteLine();
            for (int l = 0; l < Game.GetLength(0); l++)
            {
                for (int c = 0; c < Game.GetLength(1); c++)
                {
                    if (c == 0)
                        Console.Write($" |  {Game[l, c]}  |");
                    if (c == 1)
                        Console.Write($"   {Game[l, c]}  ");
                    if (c == 2)
                        Console.Write($" |  {Game[l, c]}  |");
                }
                Console.WriteLine();
                if (l != 2)
                    Console.WriteLine();
            }
            Console.WriteLine(" +-------------------+");
        }
    }
}
