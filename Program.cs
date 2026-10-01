using System;

namespace LodeHra
{
    class Team
    {
        public string Nazev;
        public Hrac Hrac1;
        public Hrac Hrac2;

        public Team(string nazev, string jmeno1, string jmeno2)
        {
            Nazev = nazev;

            Hrac1 = new Hrac(jmeno1);
            Hrac2 = new Hrac(jmeno2);
        }

        public bool MaLode()
        {
            return Hrac1.MaLode() || Hrac2.MaLode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Lodě 2v2";
            Console.CursorVisible = false;

            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("          LODĚ - 2v2");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Tým 1:");
            Console.WriteLine("  Hráč 1");
            Console.WriteLine("  Hráč 2");
            Console.WriteLine();
            Console.WriteLine("Tým 2:");
            Console.WriteLine("  Hráč 3");
            Console.WriteLine("  Hráč 4");
            Console.WriteLine();
            Console.WriteLine("Každý hráč má vlastní herní pole.");
            Console.WriteLine("Každý hráč umístí lodě velikosti 4, 3, 2 a 1.");
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu pro začátek...");
            Console.ReadKey();

            Hrac h1 = new Hrac("Hráč 1");
            Hrac h2 = new Hrac("Hráč 2");
            Hrac h3 = new Hrac("Hráč 3");
            Hrac h4 = new Hrac("Hráč 4");

            // =========================
            // UMÍSTĚNÍ LODÍ
            // =========================

            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("       PŘÍPRAVA - TÝM 1");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Nyní bude své lodě umisťovat Hráč 1.");
            Console.WriteLine("Tým 2 se během umisťování nedívá.");
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu...");
            Console.ReadKey();

            h1.UmistiLode();

            Console.Clear();
            Console.WriteLine("Hráč 1 má lodě připravené.");
            Console.WriteLine();
            Console.WriteLine("Nyní bude své lodě umisťovat Hráč 2.");
            Console.WriteLine("Hráč 1 ani tým 2 se během umisťování nedívají.");
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu...");
            Console.ReadKey();

            h2.UmistiLode();

            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("       PŘÍPRAVA - TÝM 2");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Nyní bude své lodě umisťovat Hráč 3.");
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu...");
            Console.ReadKey();

            h3.UmistiLode();

            Console.Clear();
            Console.WriteLine("Hráč 3 má lodě připravené.");
            Console.WriteLine();
            Console.WriteLine("Nyní bude své lodě umisťovat Hráč 4.");
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu...");
            Console.ReadKey();

            h4.UmistiLode();

            // =========================
            // ZAČÁTEK HRY
            // =========================

            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("          HRA ZAČÍNÁ!");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Tým 1: Hráč 1 + Hráč 2");
            Console.WriteLine("Tým 2: Hráč 3 + Hráč 4");
            Console.WriteLine();
            Console.WriteLine("Pořadí tahů:");
            Console.WriteLine("Hráč 1 → Hráč 3 → Hráč 2 → Hráč 4");
            Console.WriteLine();
            Console.WriteLine("Pravidla tahu:");
            Console.WriteLine("- Zásah = hráč pokračuje.");
            Console.WriteLine("- Vedle = tah přechází na dalšího hráče.");
            Console.WriteLine("- Každý hráč může střílet na jednoho ze dvou soupeřů.");
            Console.WriteLine("- Tým vyhraje, pokud soupeř přijde o všechny lodě.");
            Console.WriteLine();
            Console.WriteLine("Stiskni klávesu pro zahájení hry...");
            Console.ReadKey();

            Hra hra = new Hra(h1, h2, h3, h4);

            hra.Spust();
        }
    }
}
