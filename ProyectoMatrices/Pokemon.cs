using System;
using System.Media;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Quemados.ProyectoFinal;

namespace Pokemon
{
    class TresJuegos
    {
        static void Main(string[] args)
        {



            bool salir = false;

            do
            {
                SoundPlayer Fondo = new SoundPlayer("Casino.wav");
                Fondo.Play();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                string P1 = "--------------Bienvenido a la selección de juegos------------";
                Console.WriteLine("");
                Console.WriteLine(P1.PadLeft((Console.WindowWidth / 2) + (P1.Length / 2)));
                Console.ForegroundColor = ConsoleColor.Green;
                string P2 = "1. Atrapalos Ya";
                Console.WriteLine(P2.PadLeft((Console.WindowWidth / 2) + (P2.Length / 2)));
                Console.ForegroundColor = ConsoleColor.Red;
                string P3 = "2. Quemados";
                Console.WriteLine(P3.PadLeft((Console.WindowWidth / 2) + (P3.Length / 2)));
                Console.ForegroundColor = ConsoleColor.Gray;
                string P4 = "3. BattleShips";
                Console.WriteLine(P4.PadLeft((Console.WindowWidth / 2) + (P4.Length / 2)));
                Console.ForegroundColor = ConsoleColor.Yellow;
                string P5 = "4. Creditos";
                Console.WriteLine(P5.PadLeft((Console.WindowWidth / 2) + (P5.Length / 2)));
                Console.ForegroundColor = ConsoleColor.Magenta;
                string P6 = "5. Salir";
                Console.WriteLine(P6.PadLeft((Console.WindowWidth / 2) + (P6.Length / 2)));
                Console.ForegroundColor = ConsoleColor.Cyan;
                string P7 = "Elige una opción: ";
                Console.WriteLine(P7.PadLeft((Console.WindowWidth / 2) + (P7.Length / 2)));


                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        IniciarJuegoAtrapalosYa();
                        break;
                    case "2":
                        Quemados1.InicioJuego();
                        break;
                    case "3":
                        IniciarBattleShip();
                        break;
                    case "4":
                        Creditos();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }

            } while (!salir);
        }

        static void IniciarJuegoAtrapalosYa()
        {
            char[,] tablero = new char[10, 10]; // Tablero del jugador
            int jugador = 6; // Número de pokémon
            bool easterEgg = false; // Variable para rastrear si se ha golpeado el POKEMON LEGENDARIO O SI O SI
            List<string> pokemonNames = new List<string> { "Charmander", "Pikachu", "Bulbasaur", "Squirtle", "Snorlax", "Dragonite" };
            SoundPlayer soundPlayer; // Reproductor de sonido para la canción de fondo principal
            Dictionary<string, bool> pokemonFound = new Dictionary<string, bool>(); // Diccionario para rastrear qué pokemones se han encontrado

            soundPlayer = new SoundPlayer(@"poke1.wav"); // Cambiar "ruta_del_archivo_de_audio" a la ruta del archivo de audio
            soundPlayer.Load();
            soundPlayer.PlayLooping();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-----------¡BIENVENIDO A BUSCAR AL!----------");
            string imagenEnString = ConvertirImagenAStringConColores("Pokemon_Titulo");
            Console.WriteLine(imagenEnString);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Presiona 1 si quieres que te explique las instrucciones");
            string instruccion = Console.ReadLine().Trim(); // Eliminar espacios en blanco alrededor de la entrada
            Console.Clear();
            if (instruccion == "1")
            {
                Console.WriteLine("|Cuando el juego comience Veras el campo de hierba alta.\n|En el que habrán 6 pokémon repartidos aleatoriamente\n|A los cuales deberas de intentar atraparlos lanzandoles pokebolas\n|Cada posicion esta marcada con un numero de fila y un numero de columna\n|Debes indicar la pósicion a la que quieres lanzar la pokebola\n|Luego de eso veremos si lograste atraparlo o no ...");
            }

            imagenEnString = ConvertirImagenAStringConColores("start");
            Console.WriteLine(imagenEnString);
            Console.WriteLine("presiona enter para comenzar el juego ;) ");
            Console.ReadLine();

            InitializeGrid(tablero); // Crear el tablero del jugador
            Posicionar(tablero); // Colocar los pokemon y el easter egg en posiciones aleatorias
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("----¡ATRAPA TU EQUIPO POKEMON!----");
            DisplayGrid(tablero);

            // Inicializar el diccionario de pokemon encontrados
            foreach (string pokemon in pokemonNames)
            {
                pokemonFound[pokemon] = false;
            }

            while (jugador > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nTu turno:");
                PlayerTurn(tablero, ref jugador, ref easterEgg, pokemonNames, soundPlayer, pokemonFound); // Turno del jugador

                Console.Clear(); // Limpiar la consola después de cada turno
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("----¡ATRAPA TU EQUIPO POKEMON!----");
                DisplayGrid(tablero); // Mostrar el tablero del jugador actualizado
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("¡Felicidades! ¡ENCONTRASTE A TODOS LOS POKéMON!");
            Console.WriteLine("¡Has ganado la partida!");
            Console.ReadLine();
        }

        static void InitializeGrid(char[,] grid)
        {
            // Inicializar el tablero con espacios en blanco
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    grid[i, j] = '-';
                }
            }
        }

        static void Posicionar(char[,] tablero)
        {
            Random rand = new Random();

            // Colocar los barcos en posiciones aleatorias
            for (int pkmn = 0; pkmn < 6; pkmn++)
            {
                int row = rand.Next(10);
                int column = rand.Next(10);

                // Verificar si la posición ya está ocupada
                while (tablero[row, column] != '-')
                {
                    row = rand.Next(10);
                    column = rand.Next(10);
                }

                tablero[row, column] = 'B'; // Colocar el barco en la posición aleatoria
            }

            // Colocar el easter egg en una posición aleatoria
            int easterEggRow = rand.Next(10);
            int easterEggColumn = rand.Next(10);

            // Verificar si la posición del easter egg está ocupada por un barco
            while (tablero[easterEggRow, easterEggColumn] == 'B')
            {
                easterEggRow = rand.Next(10);
                easterEggColumn = rand.Next(10);
            }

            tablero[easterEggRow, easterEggColumn] = 'E'; // Colocar el easter egg en la posición aleatoria
        }

        static void DisplayGrid(char[,] grid)
        {
            // Mostrar el tablero en la consola
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n   0 1 2 3 4 5 6 7 8 9");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i}  ");
                for (int j = 0; j < 10; j++)
                {
                    if (grid[i, j] == 'B' || grid[i, j] == 'E') // Si la celda contiene un pokemon o el easter egg
                    {
                        Console.Write("5 "); // Ocultar los pokemon y el easter egg al jugador/ Poner "X" para ver la ubicacion 
                    }
                    else
                    {
                        Console.Write(grid[i, j] + " "); // Mostrar cualquier otro carácter en la celda
                    }
                }
                Console.WriteLine();
            }
        }

        static void PlayerTurn(char[,] tablero, ref int jugador, ref bool easterEgg, List<string> pokemonNames, SoundPlayer soundPlayer, Dictionary<string, bool> pokemonFound)
        {
            // Turno del jugador
            int row, column;
            bool validInput = false;

            do
            {
                Console.WriteLine("Ingresa la fila (0-9):");
                string rowInput = Console.ReadLine();
                if (int.TryParse(rowInput, out row) && row >= 0 && row <= 9)
                {
                    validInput = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingresa un número válido entre 0 y 9.");
                }
            } while (!validInput);

            validInput = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Ingresa la columna (0-9):");
                string columnInput = Console.ReadLine();
                if (int.TryParse(columnInput, out column) && column >= 0 && column <= 9)
                {
                    validInput = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingresa un número válido entre 0 y 9.");
                }
            } while (!validInput);

            if (tablero[row, column] == 'B')
            {
                string pokemonName = pokemonNames[6 - jugador];
                Console.WriteLine($"¡ENCONTRASTE A {pokemonName}!");
                string imagenEnString = ConvertirImagenAStringConColores(pokemonName);
                // Reproducir el sonido del Pokémon encontrado
                if (jugador != 0 && tablero[row, column] != 'E') // Verificar si es el primer, segundo, tercero, cuarto o quinto Pokémon encontrado
                {
                    if (!pokemonFound[pokemonName]) // Verificar si es el primer Pokémon encontrado
                    {
                        SoundPlayer pokemonSoundPlayer = new SoundPlayer($"{pokemonName}.wav");
                        pokemonSoundPlayer.Play();
                        pokemonFound[pokemonName] = true;
                    }
                }
                Console.WriteLine(imagenEnString);
                Console.ReadLine();
                tablero[row, column] = 'X';
                jugador--;
            }
            else if (tablero[row, column] == 'E')
            {
                string imagenEnString = ConvertirImagenAStringConColores("Kirby");
                Console.WriteLine(imagenEnString);
                Console.WriteLine("Uy! Parece que este no es un Pokémon");
                tablero[row, column] = 'X';
                easterEgg = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¡Has fallado!");
                tablero[row, column] = 'O';
            }

            Console.ReadLine();

            // Volver a reproducir la canción de fondo principal
            soundPlayer.Play();
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


        static void IniciarBattleShip()
        {
            SoundPlayer Fondo = new SoundPlayer("Batalla.wav");
            Fondo.Play();
            bool continuar = true;
            while (continuar)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Clear();
                Console.WriteLine("~~~~ BATTLE SHIPS ~~~~");
                Console.WriteLine($"BIENVENIDO, {Player}! \nLISTOS PARA LA BATALLA!!!");
                Console.WriteLine("\n~~~~ MENÚ DE ABORDAJE ~~~~");
                Console.WriteLine("1. JUGAR");
                Console.WriteLine("2. AJUSTES");
                Console.WriteLine("3. SALIDA");
                Console.Write("\nSELECCIONE UNA OPCION: ");
                string Opción = Console.ReadLine();

                Console.Clear();

                switch (Opción)
                {
                    case "1":
                        Console.Clear();
                        CrearTab();
                        Barcos();
                        BarcosCPU();

                        while (true)
                        {
                            DisparoPlay();
                            if (Victoria(TabCPU))
                            {
                                Console.WriteLine("¡HAS DERRIBADO TODOS LOS BARCOS ENEMIGOS! \n¡HAS GANADO LA BATALLA!");
                                break;
                            }

                            DisparoCPU();
                            if (Victoria(TabPlay))
                            {
                                Console.WriteLine($"¡La {CPU} HA UNDIDO TODOS TUS BARCOS! \n¡HAS PERDIDO LA BATALLA!");
                                break;
                            }

                            Console.Clear();
                        }

                        break;
                    case "2":
                        Console.Clear();
                        Ajustes();

                        break;

                    case "3":
                        Console.WriteLine("~~~~ SALIDA ~~~~");
                        Console.WriteLine("MUCHAS GRACIAS POR PROBAR ESTE JUEGO. \n¡HASTA LUEGO, " + Player + "!");
                        continuar = false;

                        break;
                    default:

                        Console.WriteLine("OPCION INVALIDA");
                        break;
                }
            }
        }

        static string Player = "CAPITAN";
        static string CPU = "CPU";
        static char[,] TabPlay = new char[10, 10];
        static char[,] TabPlayDisp = new char[10, 10];
        static char[,] TabCPU = new char[10, 10];

        static void CrearTab()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    TabPlay[i, j] = '~';
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    TabCPU[i, j] = '~';
                }
            }

            for (int fila = 0; fila < 10; fila++)
            {
                for (int columna = 0; columna < 10; columna++)
                {
                    TabPlayDisp[fila, columna] = '~';
                }
            }
        }

        static void MostrarTab(char[,] tablero)
        {
            Console.Write("   ");
            for (char columna = 'A'; columna <= 'J'; columna++)
            {
                Console.Write(columna + " ");
            }

            Console.WriteLine();

            for (int fila = 0; fila < 10; fila++)
            {
                Console.Write((fila + 1).ToString().PadLeft(2) + " ");
                for (int columna = 0; columna < 10; columna++)
                {
                    if (tablero[fila, columna] == '~' || tablero[fila, columna] == 'O')
                    {
                        Console.Write(tablero[fila, columna] + " ");
                    }
                    else
                    {
                        Console.Write(tablero[fila, columna] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void Barcos()
        {
            Console.WriteLine($"~~ ORGANIZA TUS BARCOS {Player} ~~\n");
            MostrarTab(TabPlay);

            Console.WriteLine("\nBARCOS DISPONIBLES:");
            Console.WriteLine("1. LANCHA       ■    (1x1)");
            Console.WriteLine("2. SUBMARINO    ■■   (2x1)");
            Console.WriteLine("3. BUQUE        ■■■  (3x1)");
            Console.WriteLine("4. PORTAAVIONES ■■■■ (4x1)");

            ColocarBarcosPlay(TabPlay, "LANCHA");
            MostrarTab(TabPlay);
            ColocarBarcosPlay(TabPlay, "SUBMARINO");
            MostrarTab(TabPlay);
            ColocarBarcosPlay(TabPlay, "BUQUE");
            MostrarTab(TabPlay);
            ColocarBarcosPlay(TabPlay, "PORTAAVIONES");
            MostrarTab(TabPlay);

            Console.WriteLine($"\n¡LISTO PARA COMENZAR LA {Player}");
            Console.WriteLine("\nPRESIONA CUALQUIER TECLA PARA CONTINUAR...");
            Console.ReadKey();
            Console.Clear();
        }

        static void ColocarBarcosPlay(char[,] Tab, string Barco)
        {
            int Tamaño = 1;
            switch (Barco)
            {
                case "VELERO":
                    Tamaño = 1;
                    break;
                case "SUBMARINO":
                    Tamaño = 2;
                    break;
                case "BUQUE":
                    Tamaño = 3;
                    break;
                case "PORTAAVIONES":
                    Tamaño = 4;
                    break;
            }

            bool colocado = false;

            while (!colocado)
            {
                Console.WriteLine($"\nCOLOCA UN {Barco} DE {Tamaño}x1\n");

                Console.Write("INGRESA LA FILA (1-10): ");
                int Fila = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.Write("INGRESA LA COLUMNA (A-J): ");
                char ColumnaInicio = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                Console.Write("INGRESA LA ORIENTACIÓN \n(H = HORIZONTAL, V = VERTICAL): ");
                char Orientacion = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                int ColumnaInicioIndex = ColumnaInicio - 'A';

                if (Orientacion != 'H' && Orientacion != 'V')
                {
                    Console.WriteLine("\nORIENTACIÓN INVÁLIDA");
                    continue;
                }

                if (Fila < 0 || Fila >= Tab.GetLength(0) || ColumnaInicioIndex < 0 || ColumnaInicioIndex >= Tab.GetLength(1))
                {
                    Console.WriteLine("\nFUERA DEL TABLERO. INGRESA UNA POSICIÓN VÁLIDA.");
                    continue;
                }

                if (Orientacion == 'H' && ColumnaInicioIndex + Tamaño > Tab.GetLength(1))
                {
                    Console.WriteLine("\nSIN ESPACIO EN EL TABLERO. INGRESA UNA POSICIÓN VÁLIDA.");
                    continue;
                }
                else if (Orientacion == 'V' && Fila + Tamaño > Tab.GetLength(0))
                {
                    Console.WriteLine("\nSIN ESPACIO EN EL TABLERO. INGRESA UNA POSICIÓN VÁLIDA.");
                    continue;
                }

                bool HayBarco = false;
                if (Orientacion == 'H')
                {
                    for (int Columna = ColumnaInicioIndex; Columna < ColumnaInicioIndex + Tamaño; Columna++)
                    {
                        if (Tab[Fila, Columna] != '~')
                        {
                            HayBarco = true;
                            break;
                        }
                    }
                }

                else
                {
                    for (int FilaIndice = Fila; FilaIndice < Fila + Tamaño; FilaIndice++)
                    {
                        if (Tab[FilaIndice, ColumnaInicioIndex] != '~')
                        {
                            HayBarco = true;
                            break;
                        }
                    }
                }

                if (HayBarco)
                {
                    Console.WriteLine("\n¡YA HAY UN BARCO EN ESA POSICIÓN! INGRESA UNA POSICIÓN VÁLIDA.");
                    continue;
                }

                if (Orientacion == 'H')
                {
                    for (int Columna = ColumnaInicioIndex; Columna < ColumnaInicioIndex + Tamaño; Columna++)
                    {
                        Tab[Fila, Columna] = '■';
                    }
                }

                else
                {
                    for (int FilaIndice = Fila; FilaIndice < Fila + Tamaño; FilaIndice++)
                    {
                        Tab[FilaIndice, ColumnaInicioIndex] = '■';
                    }
                }
                colocado = true;
                Console.Clear();
            }
        }

        static void BarcosCPU()
        {
            Random rnd = new Random();

            ColocarBarcosCPU(TabCPU, rnd, "LANCHA");
            ColocarBarcosCPU(TabCPU, rnd, "SUBMARINO");
            ColocarBarcosCPU(TabCPU, rnd, "BUQUE");
            ColocarBarcosCPU(TabCPU, rnd, "PORTAAVIONES");

            Console.WriteLine($"{CPU} ESTA COLOCANDO SUS BARCOS...");
            Thread.Sleep(2500);
            Console.Clear();
        }

        static void ColocarBarcosCPU(char[,] tablero, Random rnd, string barco)
        {
            int tamaño = 0;
            switch (barco)
            {
                case "LANCHA":
                    tamaño = 1;
                    break;
                case "SUBMARINO":
                    tamaño = 2;
                    break;
                case "BUQUE":
                    tamaño = 3;
                    break;
                case "PORTAAVIONES":
                    tamaño = 4;
                    break;
            }

            bool colocado = false;

            while (!colocado)
            {
                int fila = rnd.Next(10);
                int columna = rnd.Next(10);
                int orientacion = rnd.Next(2);

                if (orientacion == 0 && columna + tamaño <= 10)
                {
                    bool ocupado = false;
                    for (int i = columna; i < columna + tamaño; i++)
                    {
                        if (tablero[fila, i] != '~')
                        {
                            ocupado = true;
                            break;
                        }
                    }

                    if (!ocupado)
                    {
                        for (int i = columna; i < columna + tamaño; i++)
                        {
                            tablero[fila, i] = '■';
                        }
                        colocado = true;
                    }
                }

                else if (orientacion == 1 && fila + tamaño <= 10)
                {
                    bool ocupado = false;
                    for (int i = fila; i < fila + tamaño; i++)
                    {
                        if (tablero[i, columna] != '~')
                        {
                            ocupado = true;
                            break;
                        }
                    }

                    if (!ocupado)
                    {
                        for (int i = fila; i < fila + tamaño; i++)
                        {
                            tablero[i, columna] = '■';
                        }
                        colocado = true;
                    }
                }
            }
        }
        static void DisparoPlay()
        {
            Console.Clear();
            Console.WriteLine($"~~ ¡TU TURNO {Player}! ~~");
            MostrarTab(TabPlayDisp);
            Console.Write("\nINGRESA LA FILA A DISPARAR (1-10): ");
            int fila = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("INGRESA LA COLUMNA A DISPARAR (A-J): ");
            char columna = char.ToUpper(Console.ReadKey().KeyChar);
            int columnaIndex = columna - 'A';
            Console.WriteLine();

            if (TabCPU[fila, columnaIndex] == '■')
            {
                Console.WriteLine("\n¡BARCO IMPACTADO!\n");
                TabPlayDisp[fila, columnaIndex] = 'X';
                TabCPU[fila, columnaIndex] = 'X';
            }
            else
            {
                Console.WriteLine("\n¡HAS FALLADO EL DISPARO!\n");
                TabPlayDisp[fila, columnaIndex] = 'O';
            }
            MostrarTab(TabPlayDisp);
            Console.WriteLine("\nPRESIONA CUALQUIER TECLA PARA CONTINUAR...");
            Console.ReadKey();
            Console.Clear();
        }

        static void DisparoCPU()
        {
            Console.WriteLine($"~~ !TURNO DE {CPU}¡ ~~");
            Thread.Sleep(1000);
            Random rnd = new Random();
            int fila = rnd.Next(10);
            int columna = rnd.Next(10);

            if (TabPlay[fila, columna] != '~')
            {
                return;
            }

            if (TabPlay[fila, columna] == '■')
            {
                Console.WriteLine($"\n{CPU} HA ACERTADO EN {Coords(fila, columna)}\n");
                TabPlay[fila, columna] = 'X';
            }
            else
            {
                Console.WriteLine($"\n{CPU} HA FALLADO EN {Coords(fila, columna)}\n");
                TabPlay[fila, columna] = 'O';
            }

            MostrarTab(TabPlay);
            Console.WriteLine("\nPRESIONA CUALQUIER TECLA PARA CONTINUAR...");
            Console.ReadKey();
            Console.Clear();
        }

        static string Coords(int fila, int columna)
        {
            return $"{(char)(columna + 'A')}{fila + 1}";
        }

        static bool Victoria(char[,] tablero)
        {
            foreach (var cell in tablero)
            {
                if (cell == '■')
                {
                    return false;
                }
            }
            return true;
        }
        static void Ajustes()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("~~~~ AJUSTES ~~~~");
            Console.WriteLine("1. CAMBIAR NOMBRE DE JUGADOR");
            Console.WriteLine("2. CAMBIAR NOMBRE DE CPU");
            Console.WriteLine("3. VOLVER A MENU PRINCIPAL");
            Console.Write("\nSELECCIONA UNA OPCION: ");
            string OpciónAjus = Console.ReadLine();

            switch (OpciónAjus)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("INGRESA TU NUEVO NOMBRE: ");
                    Player = Console.ReadLine().ToUpper();
                    Console.WriteLine("\nNOMBRE CAMBIADO CON EXITO");
                    Console.WriteLine($"TU NUEVO NOMBRE ES: {Player}");
                    Console.WriteLine("\nPRESIONA CUALQUIER TECLA PARA CONTINUAR...");
                    Console.ReadKey();
                    Console.Clear();
                    Ajustes();
                    break;

                case "2":
                    Console.Write($"INGRESA EL NUEVO NOMBRE DE {CPU}: ");
                    CPU = Console.ReadLine().ToUpper();
                    Console.WriteLine("\nNOMBRE CAMBIADO CON EXITO");
                    Console.WriteLine($"EL NUEVO NOMBRE DE TU ENEMIGO AHORA ES: {CPU}");
                    Console.WriteLine("\nPRESIONA CUALQUIER TECLA PARA CONTINUAR...");
                    Console.ReadKey();
                    Console.Clear();
                    Ajustes();
                    break;

                default:
                    Console.WriteLine("OPCION INVALIDA.");
                    break;
            }



        }



        static void Creditos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string Z1 = "~~~~ CREDITOS ~~~~";
            Console.WriteLine(Z1.PadLeft((Console.WindowWidth / 2) + (Z1.Length / 2)));
            string Z2 = "JUEGO DESARROLADO POR 4D&N";
            Console.WriteLine(Z2.PadLeft((Console.WindowWidth / 2) + (Z2.Length / 2)));
            string Z3 = "-Melki Ortiz";
            Console.WriteLine(Z3.PadLeft((Console.WindowWidth / 2) + (Z3.Length / 2)));
            string Z4 = "-Mario Cerna";
            Console.WriteLine(Z4.PadLeft((Console.WindowWidth / 2) + (Z4.Length / 2)));
            string Z5 = "-Tulio Quintana";
            Console.WriteLine(Z5.PadLeft((Console.WindowWidth / 2) + (Z5.Length / 2)));
            string Z6 = "-Jairo Molina";
            Console.WriteLine(Z6.PadLeft((Console.WindowWidth / 2) + (Z6.Length / 2)));
            string Z7 = "-Naser Martinez";
            Console.WriteLine(Z7.PadLeft((Console.WindowWidth / 2) + (Z7.Length / 2)));
            string Z8 = "Agradecimientos especiales a:";
            Console.WriteLine(Z8.PadLeft((Console.WindowWidth / 2) + (Z8.Length / 2)));
            string Z9 = "--Depresion--";
            Console.WriteLine(Z9.PadLeft((Console.WindowWidth / 2) + (Z9.Length / 2)));
            string Z10 = "--Ansiedad--";
            Console.WriteLine(Z10.PadLeft((Console.WindowWidth / 2) + (Z10.Length / 2)));
            string Z11 = "--Miedo--";
            Console.WriteLine(Z11.PadLeft((Console.WindowWidth / 2) + (Z11.Length / 2)));
            string Z12 = "\nPRESIONA CUALQUIER TECLA PARA CONTINUAR...";
            Console.WriteLine(Z12.PadLeft((Console.WindowWidth / 2) + (Z12.Length / 2)));
            Console.ReadKey();
        }


    }

}
