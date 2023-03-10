using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string[,] matrizJogo = new string[3, 3];

        IniciaTabuleiro(matrizJogo);

        ImprimeTabuleiro(matrizJogo);

        Jogo(matrizJogo);
    }

    static void ImprimeTabuleiro(string[,] tabuleiro)
    {
        Console.Clear();

        for (int i = 0; i < 3; i++)
        {
            Console.Write($"  {tabuleiro[i, 0]}  |");
            Console.Write($"  {tabuleiro[i, 1]}  |");
            Console.Write($"  {tabuleiro[i, 2]}  \n");
        }
    }

    static void IniciaTabuleiro(string[,] tabuleiro)
    {
        for (int linha = 0; linha < 3; linha++)
        {
            for (int coluna = 0; coluna < 3; coluna++)
            {
                tabuleiro[linha, coluna] = " ";
            }
        }
    }

    static bool PosicaoVazia(string[,] tabuleiro, int posicaoLinha, int posicaoColuna)
    {
        if (string.IsNullOrWhiteSpace(tabuleiro[posicaoLinha, posicaoColuna]))
        {
            return false;
        }

        return true;
    }

    static void Jogo(string[,] tabuleiro)
    {
        int jogador = 1, jogadas = 0, posicaoLinha, posicaoColuna;
        string l, c;
        bool ganhador = false;

        do
        {
            do
            {
                if (jogador == 1)
                {
                    Console.Write($"Escolha a linha que deseja jogar {jogador} (X): ");
                    l = Console.ReadLine();

                    if(!int.TryParse(l, out posicaoLinha))
                    {
                        Console.WriteLine("Digite apenas numeros!");
                    }


                    Console.Write($"Escolha a coluna que deseja jogar {jogador} (X): ");
                    c = Console.ReadLine();

                    if (!int.TryParse(c, out posicaoColuna))
                    {
                        Console.WriteLine("Digite apenas numeros!");
                    }
                }
                else
                {
                    Console.Write($"Escolha a linha que deseja jogar {jogador} (O): ");
                    l = (Console.ReadLine());

                    if (!int.TryParse(l, out posicaoLinha))
                    {
                        Console.WriteLine("Digite apenas numeros!");
                    }

                    Console.Write($"Escolha a coluna que deseja jogar {jogador} (O): ");
                    c = Console.ReadLine();

                    if (!int.TryParse(c, out posicaoColuna))
                    {
                        Console.WriteLine("Digite apenas numeros!");
                    }
                }

                if ((posicaoLinha < 0 || posicaoLinha > 2) || (posicaoColuna < 0 || posicaoColuna > 2))
                {
                    Console.WriteLine("Essa posição não existe, escolha uma posição válida!");
                }
            } while ((posicaoLinha < 0 || posicaoColuna > 2) || (posicaoColuna < 0 || posicaoColuna > 2));

            if (PosicaoVazia(tabuleiro, posicaoLinha, posicaoColuna))
            {
                Console.WriteLine("Essa posição já foi escolhida, escolha outra posição!");
                if (jogador == 1)
                {
                    jogador++;
                }
                else
                {
                    jogador = 1;
                }
            }

            if (!PosicaoVazia(tabuleiro, posicaoLinha, posicaoColuna))
            {
                if (jogador == 1)
                {
                    tabuleiro[posicaoLinha, posicaoColuna] = "X";
                }
                else
                {
                    tabuleiro[posicaoLinha, posicaoColuna] = "O";
                }

                ImprimeTabuleiro(tabuleiro);

                if (jogador == 1)
                {
                    ganhador = VerificaGanhador(tabuleiro, "X");
                }
                else
                {
                    ganhador = VerificaGanhador(tabuleiro, "O");
                }

                jogadas++;
            }

            if (ganhador)
            {
                Console.WriteLine($"O jogador {jogador} ganhou!");
                return;
            }

            jogador++;

            if (jogador > 2)
            {
                jogador = 1;
            }

        } while (jogadas < 9 && ganhador == false);
        Console.WriteLine("O jogo empatou!");
    }

    static bool VerificaLinha(string[,] tabuleiro, string letra)
    {
        for (int linha = 0; linha < 3; linha++)
        {
            int coluna = 0;
            if (tabuleiro[linha, coluna] == letra && tabuleiro[linha, coluna + 1] == letra && tabuleiro[linha, coluna + 2] == letra)
            {
                return true;
            }
        }

        return false;
    }

    static bool VerificaColuna(string[,] tabuleiro, string letra)
    {
        for (int coluna = 0; coluna < 3; coluna++)
        {
            int linha = 0;
            if (tabuleiro[linha, coluna] == letra && tabuleiro[linha + 1, coluna] == letra && tabuleiro[linha + 2, coluna] == letra)
            {
                return true;
            }
        }

        return false;
    }

    static bool VerificaDiagonalPrincipal(string[,] tabuleiro, string letra)
    {
        if (tabuleiro[0, 0] == letra && tabuleiro[1, 1] == letra && tabuleiro[2, 2] == letra)
        {
            return true;
        }

        return false;
    }

    static bool VerificaDiagonalSecundaria(string[,] tabuleiro, string letra)
    {
        int diagonal = 0;

        if (tabuleiro[diagonal, diagonal + 2] == letra && tabuleiro[diagonal + 1, diagonal + 1] == letra && tabuleiro[diagonal + 2, diagonal] == letra)
        {
            return true;
        }

        return false;
    }

    static bool VerificaGanhador(string[,] tabuleiro, string letra)
    {
        if (VerificaLinha(tabuleiro, letra))
        {
            return true;
        }
        else if (VerificaColuna(tabuleiro, letra))
        {
            return true;
        }
        else if (VerificaDiagonalPrincipal(tabuleiro, letra))
        {
            return true;
        }
        else if (VerificaDiagonalSecundaria(tabuleiro, letra))
        {
            return true;
        }

        return false;
    }
}