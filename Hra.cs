using System;

namespace LodeHra
{
    class Hra
    {
        Hrac h1, h2, h3, h4;

        public Hra(Hrac a, Hrac b, Hrac c, Hrac d)
        {
            h1 = a;
            h2 = b;
            h3 = c;
            h4 = d;
        }

        public void Spust()
        {
            // Pořadí tahů:
            // Hráč 1 → Hráč 3 → Hráč 2 → Hráč 4
            Hrac[] hraci = { h1, h3, h2, h4 };

            int index = 0;

            while ((h1.MaLode() || h2.MaLode()) &&
                   (h3.MaLode() || h4.MaLode()))
            {
                Hrac aktualni = hraci[index];

                // Pokud už hráč nemá žádnou loď,
                // jeho tah přeskočíme.
                if (!aktualni.MaLode())
                {
                    index++;

                    if (index >= hraci.Length)
                    {
                        index = 0;
                    }

                    continue;
                }

                // Určení týmu aktuálního hráče
                string tymAktualni;

                if (aktualni == h1 || aktualni == h2)
                    tymAktualni = "Tým 1";
                else
                    tymAktualni = "Tým 2";

                // Určení soupeřů
                Hrac souper1;
                Hrac souper2;

                if (aktualni == h1 || aktualni == h2)
                {
                    souper1 = h3;
                    souper2 = h4;
                }
                else
                {
                    souper1 = h1;
                    souper2 = h2;
                }

                // Pokud jeden ze soupeřů už nemá lodě,
                // nabídneme pouze druhého.
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              NOVÝ TAH");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("Na tahu je: " + aktualni.Jmeno);
                Console.WriteLine("Tým: " + tymAktualni);

                if (aktualni == h1 || aktualni == h2)
                {
                    Console.WriteLine("Spoluhráč: " +
                                      (aktualni == h1 ? h2.Jmeno : h1.Jmeno));
                }
                else
                {
                    Console.WriteLine("Spoluhráč: " +
                                      (aktualni == h3 ? h4.Jmeno : h3.Jmeno));
                }

                Console.WriteLine();
                Console.WriteLine("Vyber hráče soupeřova týmu:");

                if (souper1.MaLode())
                    Console.WriteLine("1 - " + souper1.Jmeno);

                if (souper2.MaLode())
                    Console.WriteLine("2 - " + souper2.Jmeno);

                int volba;

                while (true)
                {
                    Console.Write("Volba: ");

                    if (int.TryParse(Console.ReadLine(), out volba))
                    {
                        if (volba == 1 && souper1.MaLode())
                            break;

                        if (volba == 2 && souper2.MaLode())
                            break;
                    }

                    Console.WriteLine("Neplatná volba. Vyber hráče, který má ještě lodě.");
                }

                Hrac protivnik;

                if (volba == 1)
                    protivnik = souper1;
                else
                    protivnik = souper2;

                // Střelba
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("             STŘELBA");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("Na tahu: " + aktualni.Jmeno);
                Console.WriteLine("Tým: " + tymAktualni);
                Console.WriteLine();
                Console.WriteLine("Cíl: " + protivnik.Jmeno);
                Console.WriteLine();
                Console.WriteLine("Herní pole soupeře:");
                Console.WriteLine();

                protivnik.Vykresli(true);

                Console.WriteLine();

                int r = aktualni.Nacti("Řádek: ");
                int s = aktualni.Nacti("Sloupec: ");

                Console.WriteLine();

                bool zasah = protivnik.Strel(r, s);

                Console.WriteLine();

                if (zasah)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ZÁSAH!");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine(aktualni.Jmeno +
                                      " zasáhl hráče " +
                                      protivnik.Jmeno + ".");

                    Console.WriteLine();
                    Console.WriteLine("Protože byl zásah, " +
                                      aktualni.Jmeno +
                                      " zůstává na tahu.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("VEDLE!");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine(aktualni.Jmeno +
                                      " minul hráče " +
                                      protivnik.Jmeno + ".");

                    Console.WriteLine();
                    Console.WriteLine("Tah přechází na dalšího hráče.");
                }

                Console.WriteLine();
                Console.WriteLine("Stiskni klávesu pro pokračování...");
                Console.ReadKey();

                // Pokud hráč minul, pokračuje další hráč.
                // Pokud zasáhl, zůstává na tahu.
                if (!zasah)
                {
                    index++;

                    if (index >= hraci.Length)
                    {
                        index = 0;
                    }
                }
            }

            // =========================
            // KONEC HRY
            // =========================

            Console.Clear();

            bool tym1Zije = h1.MaLode() || h2.MaLode();
            bool tym2Zije = h3.MaLode() || h4.MaLode();

            Console.WriteLine("========================================");
            Console.WriteLine("             KONEC HRY");
            Console.WriteLine("========================================");
            Console.WriteLine();

            if (tym1Zije)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("VYHRÁL TÝM 1!");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Hráči:");
                Console.WriteLine("- " + h1.Jmeno);
                Console.WriteLine("- " + h2.Jmeno);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("VYHRÁL TÝM 2!");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Hráči:");
                Console.WriteLine("- " + h3.Jmeno);
                Console.WriteLine("- " + h4.Jmeno);
            }

            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu pro ukončení...");
            Console.ReadKey();
        }
    }
}