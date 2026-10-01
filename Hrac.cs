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

        // =========================================
        // UMÍSTĚNÍ LODÍ
        // =========================================

        public void UmistiLode()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("         UMISŤOVÁNÍ LODÍ");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Hráč: " + Jmeno);
            Console.WriteLine();
            Console.WriteLine("Jak chceš rozmístit lodě?");
            Console.WriteLine();
            Console.WriteLine("1 - Ručně");
            Console.WriteLine("2 - Automaticky");
            Console.WriteLine();

            int volba;

            while (true)
            {
                Console.Write("Volba: ");

                if (int.TryParse(Console.ReadLine(), out volba) &&
                    (volba == 1 || volba == 2))
                {
                    break;
                }

                Console.WriteLine("Zadej pouze 1 nebo 2.");
            }

            if (volba == 1)
            {
                UmistiLodeRucne();
            }
            else
            {
                UmistiLodeAutomaticky();
            }
        }

        // =========================================
        // RUČNÍ UMÍSTĚNÍ
        // =========================================

        private void UmistiLodeRucne()
        {
            int[] velikosti = { 4, 3, 2, 1 };

            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("       RUČNÍ UMISŤOVÁNÍ LODÍ");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Hráč: " + Jmeno);
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu pro pokračování...");
            Console.ReadKey();

            foreach (int velikost in velikosti)
            {
                bool hotovo = false;

                while (!hotovo)
                {
                    Console.Clear();

                    Console.WriteLine("========================================");
                    Console.WriteLine("       RUČNÍ UMISŤOVÁNÍ LODÍ");
                    Console.WriteLine("========================================");
                    Console.WriteLine();
                    Console.WriteLine("Hráč: " + Jmeno);
                    Console.WriteLine();
                    Console.WriteLine("Umísťuješ loď velikosti: " + velikost);
                    Console.WriteLine();

                    Vykresli();

                    Console.WriteLine();

                    int r = Nacti("Řádek: ");
                    int s = Nacti("Sloupec: ");

                    Console.Write("Směr (h/v): ");
                    string vstup = Console.ReadLine();

                    if (string.IsNullOrEmpty(vstup))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chyba: musíš zadat h nebo v.");
                        Console.ResetColor();

                        Console.WriteLine("Stiskni klávesu...");
                        Console.ReadKey();

                        continue;
                    }

                    char smer = char.ToLower(vstup[0]);

                    if (smer != 'h' && smer != 'v')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chyba: zadej pouze h nebo v.");
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
                            {
                                Pole[r, s + i] = 'L';
                            }
                            else
                            {
                                Pole[r + i, s] = 'L';
                            }
                        }

                        hotovo = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Loď sem nelze umístit.");
                        Console.WriteLine("Loď se nevejde nebo se překrývá s jinou lodí.");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine("Stiskni klávesu...");
                        Console.ReadKey();
                    }
                }
            }

            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("       LODĚ HRÁČE " + Jmeno);
            Console.WriteLine("========================================");
            Console.WriteLine();

            Vykresli();

            Console.WriteLine();
            Console.WriteLine("Všechny lodě jsou umístěny.");
            Console.WriteLine("Stiskni klávesu pro pokračování...");
            Console.ReadKey();
        }

        // =========================================
        // AUTOMATICKÉ UMÍSTĚNÍ
        // =========================================

        private void UmistiLodeAutomaticky()
        {
            int[] velikosti = { 4, 3, 2, 1 };

            Random random = new Random();

            foreach (int velikost in velikosti)
            {
                bool umisteno = false;

                while (!umisteno)
                {
                    int r = random.Next(0, 10);
                    int s = random.Next(0, 10);

                    char smer;

                    if (random.Next(0, 2) == 0)
                    {
                        smer = 'h';
                    }
                    else
                    {
                        smer = 'v';
                    }

                    if (LzeUmistitAutomaticky(r, s, velikost, smer))
                    {
                        for (int i = 0; i < velikost; i++)
                        {
                            if (smer == 'h')
                            {
                                Pole[r, s + i] = 'L';
                            }
                            else
                            {
                                Pole[r + i, s] = 'L';
                            }
                        }

                        umisteno = true;
                    }
                }
            }

            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("       AUTOMATICKÉ UMISŤOVÁNÍ");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Hráč: " + Jmeno);
            Console.WriteLine();
            Console.WriteLine("Lodě byly automaticky rozmístěny.");
            Console.WriteLine();

            Vykresli();

            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu pro pokračování...");
            Console.ReadKey();
        }

        // =========================================
        // KONTROLA RUČNÍHO UMÍSTĚNÍ
        // =========================================

        private bool LzeUmistit(int r, int s, int vel, char smer)
        {
            // Kontrola hranic

            if (smer == 'h')
            {
                if (s + vel > 10)
                {
                    return false;
                }
            }
            else
            {
                if (r + vel > 10)
                {
                    return false;
                }
            }

            // Kontrola překrytí s jinou lodí

            for (int i = 0; i < vel; i++)
            {
                int radek;
                int sloupec;

                if (smer == 'h')
                {
                    radek = r;
                    sloupec = s + i;
                }
                else
                {
                    radek = r + i;
                    sloupec = s;
                }

                if (Pole[radek, sloupec] == 'L')
                {
                    return false;
                }
            }

            return true;
        }

        // =========================================
        // KONTROLA AUTOMATICKÉHO UMÍSTĚNÍ
        // =========================================

        private bool LzeUmistitAutomaticky(int r, int s, int vel, char smer)
        {
            // Kontrola hranic

            if (smer == 'h')
            {
                if (s + vel > 10)
                {
                    return false;
                }
            }
            else
            {
                if (r + vel > 10)
                {
                    return false;
                }
            }

            // Kontrola celého okolí lodi.
            //
            // Kolem lodi musí zůstat minimálně
            // jeden prázdný řádek/sloupec.
            //
            // Kontrolujeme také diagonály.

            for (int i = r - 1;
                 i <= r + 1 + (smer == 'v' ? vel - 1 : 0);
                 i++)
            {
                for (int j = s - 1;
                     j <= s + 1 + (smer == 'h' ? vel - 1 : 0);
                     j++)
                {
                    if (i < 0 || i >= 10 ||
                        j < 0 || j >= 10)
                    {
                        continue;
                    }

                    if (Pole[i, j] == 'L')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // =========================================
        // STŘELBA
        // =========================================

        public bool Strel(int r, int s)
        {
            if (r < 0 || r >= 10 ||
                s < 0 || s >= 10)
            {
                return false;
            }

            if (Pole[r, s] == 'L')
            {
                Pole[r, s] = 'X';

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ZÁSAH!");
                Console.ResetColor();

                if (JeLoďPotopenaPoZásahu(r, s))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Celá loď byla potopena!");
                    Console.ResetColor();
                }

                return true;
            }

            if (Pole[r, s] == '~')
            {
                Pole[r, s] = 'O';

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("VEDLE!");
                Console.ResetColor();

                return false;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Na toto místo už bylo stříleno!");
            Console.ResetColor();

            return false;
        }

        // =========================================
        // KONTROLA, ZDA MÁ HRÁČ LODĚ
        // =========================================

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

        // =========================================
        // NAČÍTÁNÍ ČÍSLA 0–9
        // =========================================

        public int Nacti(string text)
        {
            int x;

            Console.Write(text);

            while (!int.TryParse(Console.ReadLine(), out x) ||
                   x < 0 ||
                   x > 9)
            {
                Console.Write("Zadej číslo od 0 do 9: ");
            }

            return x;
        }

        // =========================================
        // KONTROLA POTOPENÍ LODĚ
        // =========================================

        public bool JeLoďPotopenaPoZásahu(int r, int s)
        {
            int i = s;

            // Kontrola doleva
            while (i >= 0 && Pole[r, i] != '~')
            {
                if (Pole[r, i] == 'L')
                {
                    return false;
                }

                i--;
            }

            // Kontrola doprava
            i = s + 1;

            while (i < 10 && Pole[r, i] != '~')
            {
                if (Pole[r, i] == 'L')
                {
                    return false;
                }

                i++;
            }

            // Kontrola nahoru
            int j = r;

            while (j >= 0 && Pole[j, s] != '~')
            {
                if (Pole[j, s] == 'L')
                {
                    return false;
                }

                j--;
            }

            // Kontrola dolů
            j = r + 1;

            while (j < 10 && Pole[j, s] != '~')
            {
                if (Pole[j, s] == 'L')
                {
                    return false;
                }

                j++;
            }

            return true;
        }

        // =========================================
        // RADAR
        // =========================================

        public bool Radar(int r, int s)
        {
            if (r < 0 || r >= 10 ||
                s < 0 || s >= 10)
            {
                return false;
            }

            bool nalezenaLod = false;

            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = s - 1; j <= s + 1; j++)
                {
                    if (i >= 0 && i < 10 &&
                        j >= 0 && j < 10)
                    {
                        if (Pole[i, j] == 'L')
                        {
                            nalezenaLod = true;
                        }
                    }
                }
            }

            if (nalezenaLod)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Radar: V okolí se nachází loď!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Radar: V okolí není žádná loď.");
                Console.ResetColor();
            }

            return nalezenaLod;
        }
    }
}
