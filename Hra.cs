using System;
using System.ComponentModel.Design;

namespace LodeHra
{
    class Hra
    {
        Hrac h1, h2;

        public Hra(Hrac a, Hrac b)
        {
            h1 = a;
            h2 = b;
        }

        public void Spust()
        {
            Hrac aktualni = h1;
            Hrac protivnik = h2;

            while (h1.MaLode() && h2.MaLode())
            {
                Console.Clear();

                Console.WriteLine("Na tahu: " + aktualni.Jmeno);
                protivnik.Vykresli(true);

                int r = aktualni.Nacti("Řádek: ");
                int s = aktualni.Nacti("Sloupec: ");

                bool zasah = protivnik.Strel(r, s);

                Console.ReadKey();

                if (!zasah)
                {
                    var temp = aktualni;
                    aktualni = protivnik;
                    protivnik = temp;
                }
            }

            Console.Clear();

            if (h1.MaLode())
                Console.WriteLine(h1.Jmeno + " vyhrál");
            else if (h2.MaLode())
                Console.WriteLine(h2.Jmeno + " vyhrál");
            else
                Console.WriteLine("Remíza");
        }
    }
}
