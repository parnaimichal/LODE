using System;

namespace LodeHra
{
    class Hrac
    {
        public string Jmeno;
        public char[,] Pole = new char[10, 10];

        public Hrac(string jmeno)
        {
            Jmeno = jmeno;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Pole[i, j] = '~';
                }
            }
        }

        public void Vykresli(bool skryt = false)
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");

            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + " ");

                for (int j = 0; j < 10; j++)
                {
                    char c = Pole[i, j];

                    if (skryt && c == 'L')
                        c = '~';

                    if (c == 'L')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (c == 'X')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (c == 'O')
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else
                        Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write(c + " ");
                }

                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void UmistiLode()
        {
            int[] velikosti = { 4, 3, 2, 1 };

            Console.WriteLine($"\n{Jmeno} umisťuje lodě");

            foreach (int velikost in velikosti)
            {
                bool hotovo = false;

                while (!hotovo)
                {
                    Console.Clear();
                    Vykresli();


                    Console.WriteLine("\nLoď velikosti " + velikost);

                    int r = Nacti("Řádek: ");
                    int s = Nacti("Sloupec: ");

                    Console.Write("Směr (h/v): ");
                    char smer = Console.ReadLine()[0];

                        if (smer != 'h' && smer != 'v')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Chyba: zadej pouze h nebo v");
                            Console.ResetColor();

                            Console.WriteLine("Stiskni klávesu...");
                            Console.ReadKey();

                            continue;
                        }
                   

                    if (LzeUmistit(r, s, velikost, smer))
                    {
                        for (int i = 0; i < velikost; i++)
                        {
                            if (smer == 'h')
                                Pole[r, s + i] = 'L';
                            else
                                Pole[r + i, s] = 'L';
                        }
                        

                        hotovo = true;
                    }
                    else
                    {

                        Console.WriteLine("Stiskni klávesu...");
                        Console.ReadKey();

                        Console.Clear();
                    }
                }
            }
        }

        private bool LzeUmistit(int r, int s, int vel, char smer)
        {
            if (r < 0 || r >= 10 || s < 0 || s >= 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chyba: souřadnice jsou mimo pole!");
                Console.ResetColor();
                return false;
            }

            if (smer == 'h')
            {
                if (s + vel > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chyba: loď je mimo pole!");
                    Console.ResetColor();
                    return false;
                }

                for (int i = 0; i < vel; i++)
                {
                    if (Pole[r, s + i] == 'L')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chyba: kolize s jinou lodí!");
                        Console.ResetColor();
                        return false;
                    }
                }
            }
            else
            {
                if (r + vel > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chyba: loď je mimo pole!");
                    Console.ResetColor();
                    return false;
                }

                for (int i = 0; i < vel; i++)
                {
                    if (Pole[r + i, s] == 'L')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chyba: kolize s jinou lodí!");
                        Console.ResetColor();
                        return false;
                    }
                }
            }

            return true;
        }
        public bool Strel(int r, int s)
        {
            if (r < 0 || r >= 10 || s < 0 || s >= 10)
                return false;

            if (Pole[r, s] == 'L')
            {
                Pole[r, s] = 'X';

                Console.WriteLine("Zásah");

                if (JeLoďPotopenaPoZásahu(r, s))
                {
                    Console.WriteLine("Loď byla potopena");
                }

                return true;
            }

            if (Pole[r, s] == '~')
            {
                Pole[r, s] = 'O';
                Console.WriteLine("Vedle");
                return false;
            }

            return false;
        }

        public bool MaLode()
        {
            foreach (char c in Pole)
            {
                if (c == 'L')
                {
                    return true;
                }
            }
            return false;
        }

        public int Nacti(string text)
        {
            int x;
            Console.Write(text);

            while (!int.TryParse(Console.ReadLine(), out x) || x < 0 || x > 9)
                Console.Write("0–9: ");

            return x;
        }
        public bool JeLoďPotopenaPoZásahu(int r, int s)
        {
           
            int i = s;
            while (i >= 0 && Pole[r, i] != '~')
            {
                if (Pole[r, i] == 'L') return false;
                i--;
            }

            i = s + 1;
            while (i < 10 && Pole[r, i] != '~')
            {
                if (Pole[r, i] == 'L') return false;
                i++;
            }


            int j = r;
            while (j >= 0 && Pole[j, s] != '~')
            {
                if (Pole[j, s] == 'L') return false;
                j--;
            }

            j = r + 1;
            while (j < 10 && Pole[j, s] != '~')
            {
                if (Pole[j, s] == 'L') return false;
                j++;
            }

            return true;
        }
    }
}

