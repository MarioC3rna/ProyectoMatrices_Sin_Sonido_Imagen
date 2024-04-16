using System;
using System.Drawing;
using System.Media;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
namespace Quemados.ProyectoFinal
{
    public class Quemados1
    {
        static char[,] playergrid = new char[6, 3];
        static char[,] computergrid = new char[6, 3];
        static int playerships = 5;
        static int computerships = 5;
        static bool Reproductor = false;

        public static void InicioJuego()
        {
            SoundPlayer Fondo = new SoundPlayer("OP.wav");
            Fondo.Play();

            int selectedOption = 0;
            while (selectedOption != 4)
            {
                DisplayMenu();
                selectedOption = GetMenuChoice();

                switch (selectedOption)
                {
                    case 1:
                        PlayGame();
                        break;
                    case 2:
                        Console.WriteLine("Saliendo del juego...");
                        return;
                        break;
                    case 4:
                        DisplayFinalFantasy7EasterEgg();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intenta de nuevo.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                      ___                                                                             ___         ");
            Console.WriteLine("                  .:::---:::.                                                                     .:::---:::.     ");
            Console.WriteLine("                .'--:     :--'.        -------------------------------------------              .'--:     :--'.   ");
            Console.WriteLine("               /.'   \\   /   `\\\\    ---------BIENVENIDO AL MODO DE QUEMADOS---------           /.'   \\   /   `\\\\ ");
            Console.WriteLine("              |/'._ /:::\\ _.' \\ |      -------------------------------------------            |/'._ /:::\\ _.' \\ | ");
            Console.WriteLine("              |/    |:::::|    \\|                                                             |/    |:::::|    \\| ");
            Console.WriteLine("              |:\\ .''-:::-''. /:|                                                             |:\\ .''-:::-''. /:| ");
            Console.WriteLine("               \\:|    `|`    |:/                                                               \\:|    `|`    |:/  ");
            Console.WriteLine("                '.'._.:::._.'.'                                                                 '.'._.:::._.'.'    ");
            Console.WriteLine("                  '-:::::::-'                                                                     '-:::::::-'      ");
            Console.WriteLine();
            Console.WriteLine("1. Jugar");
            Console.WriteLine("2. Salir");
            Console.WriteLine("4.Algo Nuevo");
            Console.WriteLine("");

        }

        static int GetMenuChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Opción no válida. Intenta de nuevo.");
            }
            return choice;
        }

        static void PlayGame()
        {
            initializegrid(playergrid);
            initializegrid(computergrid);
            Placeships(playergrid);
            Placeships(computergrid);

            while (playerships > 0 && computerships > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("");
                string T1 = "  -------Elije a donde quieres tirar--------";
                Console.WriteLine(T1.PadLeft((Console.WindowWidth / 2) + (T1.Length / 2)));
                playerturn();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                string T2 = "Turno del equipo enemigo >:)";
                Console.WriteLine(T2.PadLeft((Console.WindowWidth / 2) + (T2.Length / 2)));

                computerturn();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("");
                string T3 = "....Tu lado de la cancha.....";
                Console.WriteLine(T3.PadLeft((Console.WindowWidth / 2) + (T3.Length / 2)));

                displaygrid(playergrid, isplayergrid: true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                string T4 = "....El lado del equipo enemigo....";
                Console.WriteLine(T4.PadLeft((Console.WindowWidth / 2) + (T4.Length / 2)));
                displaygrid(computergrid, isplayergrid: false);
            }

            if (playerships == 0)
            {
                SoundPlayer Game = new SoundPlayer("Gamer Over.wav");
                Game.Play();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("¡El equipo enemigo a eliminado a todo tu equipo! perdiste el partido :(".PadLeft((Console.WindowWidth / 2) + ("¡El equipo enemigo a eliminado a todo tu equipo! perdiste el partido :(".Length / 2)));
            }
            else
            {
                SoundPlayer Victoria = new SoundPlayer("Victoria.wav");
                Victoria.Play();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¡Felicidades eliminaste a todo el equipo rival! has ganado el partido :D".PadLeft((Console.WindowWidth / 2) + ("¡Felicidades eliminaste a todo el equipo rival! has ganado el partido :D".Length / 2)));
            }

            Console.ReadLine();
        }


        static void DisplayFinalFantasy7EasterEgg()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("¡Felicidades, has encontrado el Easter Egg de Final Fantasy 7!");
            Console.WriteLine("La materia Bahamut ZERO brilla con un destello intenso...");
            string imagenEnString = ConvertirImagenAStringConColores("KirbyFF");
            Console.WriteLine(imagenEnString);
            Console.ReadLine();
        }
        static string ConvertirImagenAStringConColores(string nombre)
        {
            Bitmap imagen = new Bitmap($"{nombre}.png");
            StringBuilder sb = new StringBuilder();

            // Recorrer cada píxel de la imagen
            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color color = imagen.GetPixel(x, y);

                    // Agregar el código ANSI de color correspondiente al píxel
                    string codigoColor = ColorACodigoAnsi(color);
                    sb.Append(codigoColor);

                    // Agregar un carácter de representación (por ejemplo, un bloque)
                    sb.Append('█');

                    // Restablecer el color al final de cada carácter
                    sb.Append("\u001b[0m");
                }
                sb.AppendLine(); // Agregar un salto de línea después de cada fila
            }

            imagen.Dispose();
            return sb.ToString();
        }

        static string ColorACodigoAnsi(Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;

            // Código ANSI para establecer el color del texto en la consola
            return $"\u001b[38;2;{r};{g};{b}m";
        }

        static void initializegrid(char[,] grid)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid[i, j] = '-';
                }
            }
        }

        static void Placeships(char[,] grid)
        {
            Random rand = new Random();
            for (int ship = 0; ship < 5; ship++)
            {
                int row, column;
                if (grid == playergrid)
                {
                    Console.WriteLine($"Ingresa la fila (0-5) donde quieres colocar tu jugador {ship + 1}:");
                    row = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Ingresa la columna (0-2) donde quieres colocar tu jugador {ship + 1}:");
                    column = int.Parse(Console.ReadLine());
                }
                else
                {
                    row = rand.Next(6);
                    column = rand.Next(3);
                }
                while (grid[row, column] != '-')
                {
                    if (grid == playergrid)
                    {
                        Console.WriteLine("Esa posición ya está ocupada. Intenta de nuevo:");
                        row = int.Parse(Console.ReadLine());
                        column = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        row = rand.Next(6);
                        column = rand.Next(3);
                    }
                }
                grid[row, column] = 'J';
            }
        }

        static void displaygrid(char[,] grid, bool isplayergrid)
        {
            Console.WriteLine("");
            string T02 = " A B C";
            Console.WriteLine(T02.PadLeft((Console.WindowWidth / 2) + (T02.Length / 2)));

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                string line = $"{i}  ";
                for (int j = 0; j < 3; j++)
                {
                    if (isplayergrid)
                    {
                        line += grid[i, j] + " ";
                    }
                    else if (grid[i, j] == 'X' || grid[i, j] == 'o')
                    {
                        line += grid[i, j] + " ";
                    }
                    else
                    {
                        line += "- ";
                    }
                }
                Console.WriteLine(line.PadLeft((Console.WindowWidth / 2) + (line.Length / 2)));
            }
        }

        static void playerturn()
        {
            string T5 = "Ingresa la fila (0-5):";
            Console.WriteLine(T5.PadLeft((Console.WindowWidth / 2) + (T5.Length / 2)));
            int row = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            string T6 = "Ingresa la columna (0-2):";
            Console.WriteLine(T6.PadLeft((Console.WindowWidth / 2) + (T6.Length / 2)));
            int column = int.Parse(Console.ReadLine());
            if (computergrid[row, column] == 'J')
            {
                SoundPlayer Pelotazo = new SoundPlayer("Pelota.wav");
                Pelotazo.Play();
                Thread.Sleep(1000);
                SoundPlayer Fondo = new SoundPlayer("OP.wav");
                Fondo.Play(); ;
                string T7 = "--Has quemado a un jugador rival--";
                Console.Write(T7.PadLeft((Console.WindowWidth / 2) + (T7.Length / 2)));
                computergrid[row, column] = 'X';
                computerships--;
            }
            else
            {
                Console.WriteLine("");
                string T8 = "--No le diste a nadie--";
                Console.Write(T8.PadLeft((Console.WindowWidth / 2) + (T8.Length / 2)));
                computergrid[row, column] = 'o';
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            string T9 = ">>Presiona ENTER para continuar con el partido<<";
            Console.Write(T9.PadLeft((Console.WindowWidth / 2) + (T9.Length / 2)));
            Console.ReadLine();
        }

        static void computerturn()
        {
            Random rand = new Random();
            int row = rand.Next(6);
            int column = rand.Next(3);

            if (playergrid[row, column] == 'J')
            {
                SoundPlayer Pelotazo = new SoundPlayer("Pelota.wav");
                Pelotazo.Play();
                Thread.Sleep(1000);
                SoundPlayer Fondo = new SoundPlayer("OP.wav");
                Fondo.Play();
                string T10 = $"--El jugadores enemigos ha golpeado uno de tus jugadores en la fila {row}, columna {column}--";
                Console.Write(T10.PadLeft((Console.WindowWidth / 2) + (T10.Length / 2)));
                playergrid[row, column] = 'X';
                playerships--;
            }
            else
            {
                Console.WriteLine("");
                string T11 = "--El jugador enemigo no le dio a nadie--";
                Console.Write(T11.PadLeft((Console.WindowWidth / 2) + (T11.Length / 2)));
                if (playergrid[row, column] == '-')
                {
                    playergrid[row, column] = 'o';
                }
            }

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            string mensaje = ">>Presiona ENTER para continuar con el partido<<";
            Console.Write(mensaje.PadLeft((Console.WindowWidth / 2) + (mensaje.Length / 2)));

            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
            }

            Console.ReadLine();
        }
    }
}