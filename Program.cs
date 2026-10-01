using System;
using System.Security.Cryptography;

namespace LodeHra
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Lodě - 2 hráči";
            Console.CursorVisible = false;

            Hrac h1 = new Hrac("Hráč 1");
            Hrac h2 = new Hrac("Hráč 2");

            h1.UmistiLode();
            Console.Clear();

            h2.UmistiLode();
            Console.Clear();

            Hra hra = new Hra(h1, h2);
            hra.Spust();
        }
    }
}
