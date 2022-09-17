using System;
using System.Collections.Generic;
using System.Threading;


namespace Jogo_da_Velha {
    class Program {

        static int InserirMatriz(string[,] matriz, List<string> indexNumeros, int index) {
            for (int i = 0; i < matriz.GetLength(0); i++) {
                for (int j = 0; j < matriz.GetLength(1); j++) {
                    matriz[i, j] = index.ToString();
                    indexNumeros.Add(index.ToString());
                    index++;
                }
            }

            return index;
        }

        static void ImprimirMatriz(string[,] matriz) {
            Console.WriteLine("\n*** JOGO DA VEIA(lha) ***\n");
            for (int i = 0; i < matriz.GetLength(0); i++) {
                for (int j = 0; j < matriz.GetLength(1); j++) {
                    Console.Write($" [{matriz[i, j]}] ");
                }
                Console.WriteLine();
            }
        }

        static void MensagemVitoria(string jogador) {

            if (jogador == "O") {
                Console.Write("\nO Ganhador [ O ] VENCEU!!!");
                Console.WriteLine("\nFim de jogo!!!");
            }
            else {
                Console.Write("\nO Ganhador [ x ] VENCEU!!!");
                Console.WriteLine("\nFim de jogo!!!");
            }
        }

        static void ImprimirEmpatou(int cont, string[,] matriz) {
            if (cont == 9) {

                ImprimirMatriz(matriz);
                Console.WriteLine();
                Console.WriteLine("O Jogo Empatou!!!");
                Console.WriteLine("\nNão Houve Vencedor!!!");
            }
            Console.WriteLine();
        }

        static void VerificarMatriz(string[,] matriz, string turno, List<string> indexNumeros, string jogada) {
            for (int i = 0; i < matriz.GetLength(0); i++) {
                for (int j = 0; j < matriz.GetLength(1); j++) {
                    if (matriz[i, j] == jogada && indexNumeros.Contains(jogada)) {
                        matriz[i, j] = turno;
                        indexNumeros.Remove(jogada);
                    }
                }
            }
        }

        static bool MtDiagonal(string[,] mtJogo, string jog) {
            if (mtJogo[0, 0] == mtJogo[1, 1] && mtJogo[1, 1] == mtJogo[2, 2] ||
                mtJogo[0, 2] == mtJogo[1, 1] && mtJogo[1, 1] == mtJogo[2, 0]) {

                return true;

            }
            else {
                return false;
            }
        }

        static bool MtLinha(string[,] mtJogo, string jog) {
            if (mtJogo[0, 0] == mtJogo[0, 1] && mtJogo[0, 1] == mtJogo[0, 2] ||
                mtJogo[1, 0] == mtJogo[1, 1] && mtJogo[1, 1] == mtJogo[1, 2] ||
                mtJogo[2, 0] == mtJogo[2, 1] && mtJogo[2, 1] == mtJogo[2, 2]) {

                return true;
            }
            else {
                return false;
            }
        }

        static bool MtColuna(string[,] mtJogo, string jog) {

            if (mtJogo[0, 0] == mtJogo[1, 0] && mtJogo[1, 0] == mtJogo[2, 0] ||
                mtJogo[0, 1] == mtJogo[1, 1] && mtJogo[1, 1] == mtJogo[2, 1] ||
                mtJogo[0, 2] == mtJogo[1, 2] && mtJogo[1, 2] == mtJogo[2, 2]) {

                return true;
            }
            else {

                return false;
            }
        }

        static string Resposta(string jogador) {

            string jogada = "";

            Console.Write($"\nJogador [{jogador}] digite uma posição do 1 ao 9: ");
            jogada = Console.ReadLine();

            return jogada;
        }

        static void OperarJogo() {

            string[,] matriz = new string[3, 3];
            string turno = "X";

            List<string> indexNumeros = new List<string> { };

            int index = 1;

            int tentativas = 1;



            index = InserirMatriz(matriz, indexNumeros, index);

            ImprimirMatriz(matriz);

            string jogada = Resposta(turno);

            Console.Clear();


            while (tentativas < 9) {

                VerificarMatriz(matriz, turno, indexNumeros, jogada);
                ImprimirMatriz(matriz);

                if (MtDiagonal(matriz, turno) == true) {
                    MensagemVitoria(turno);
                    break; ;
                }
                else if (MtLinha(matriz, turno) == true) {
                    MensagemVitoria(turno);
                    break;
                }
                else if (MtColuna(matriz, turno) == true) {
                    MensagemVitoria(turno);
                    break;
                }

                if (turno == "X") {
                    turno = "O";
                }
                else {
                    turno = "X";
                }

                Console.WriteLine();

                jogada = Resposta(turno);
                while (!indexNumeros.Contains(jogada)) {
                    Console.WriteLine();
                    Console.Write("\nJogada Inválida. Tente Novamente: \n");
                    Console.Write("\nPosição já pode estar Preenchida ou digito Escolhido é uma Formatação Inválida!!! \n");

                    jogada = Resposta(turno);
                }

                tentativas++;

                Console.Clear();
            }

            ImprimirEmpatou(tentativas, matriz);

        }

        static void StartGame() {
            char opc;

            do {
                OperarJogo();
                Console.WriteLine("\nAperte Enter para Continuar...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\nDeseja Jogar Novamente?");
                Console.WriteLine("Sim - S / Não - N");
                Console.Write("Escolha: ");
                opc = char.Parse(Console.ReadLine().ToUpper());

                if (opc == 'N') {
                    Console.WriteLine("\n\nSaindo do Jogo...");
                    Thread.Sleep(2000);

                }
                Console.Clear();
            } while (opc != 'N');
        }

        static void Main(string[] args) {
            StartGame();

        }


    }
}

