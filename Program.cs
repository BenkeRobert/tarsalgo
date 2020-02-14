using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tarsalgo
{
    class Program
    {
        static List<Tarsalgo> szemelyek = new List<Tarsalgo>();
        static int szemelyszam;

        static void Beolvas()
        {
            string forras = @"..\..\ajto.txt";

            if (!File.Exists(forras))
            {
                Console.WriteLine("A forrásfájl nem található!");
            }
            else
            {
                using (StreamReader sr = new StreamReader(forras, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] darabol = sr.ReadLine().Split(' ');
                        szemelyek.Add(new Tarsalgo(int.Parse(darabol[0]), int.Parse(darabol[1]), int.Parse(darabol[2]), darabol[3]));
                    }
                }
            }
        }

        static void Elso_belep_utolso_kilep()
        {
            Console.WriteLine("Az elsõ belépő: {0}", szemelyek.Find(x => x.be_ki.Equals("be")).azon.ToString());
            Console.WriteLine("Az utolsó kilépő: {0}", szemelyek.FindLast(x => x.be_ki.Equals("ki")).azon.ToString());
        }

        static void Fajlba_kiir()
        {
            // Különböztesse meg az azonosítókat, számolja meg be_ki tartalmát egyes azonosítóknál

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter sw = new StreamWriter(Path.Combine(docPath, "athaladas.txt")))
            {
                foreach (var item in szemelyek.GroupBy(x => x.azon).Select(y => new { azon = y.Key, db = y.Count() }).OrderBy(z => z.azon))
                {
                    sw.WriteLine($"{item.azon} {item.db}");
                }
            }
        }

        static void Bentmaradtak()
        {
            /* Az utolsó 'be' állapot vizsgálása (nem működik jól) */

            Console.Write($"A végén a társalgóban voltak:");

            var eredmeny = szemelyek.GroupBy(x => x.azon).Select(y => new { azon = y.Key, felt = y.Last().Equals("be") }).OrderBy(z => z.azon);

            foreach (var i in eredmeny)
            {
                Console.Write($" {i.azon}");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            /* Fájlból adatok tárolása */
            Beolvas();

            /* Az elõször belépõ és az utoljára távozó személyek azonosítói */
            Console.WriteLine("2. Feladat:");
            Elso_belep_utolso_kilep();

            /* Azonosítók és az áthaladások száma fájlba kiírva */
            Fajlba_kiir();

            /* A vizsgált idõszak után bent lévõ személyek azonosítóinak kiíratása */
            Console.WriteLine("4. Feladat:");
            Bentmaradtak();

            string beker;

            Console.WriteLine("6. Feladat:");
            Console.WriteLine("Adja meg a személy azonosítóját!");

            do
            {
                beker = Convert.ToString(Console.ReadLine());
                if (Int32.TryParse(beker, out szemelyszam) == false)
                {
                    Console.WriteLine("Nem számot adott meg! Próbálja meg újra!");
                }
                else
                {
                    Console.WriteLine("Azonosító elfogadva.");
                }
            }
            while (Int32.TryParse(beker, out szemelyszam) == false);
            

            Console.ReadKey();
        }
    }
    class Tarsalgo
    {
        public int ora, perc, azon;
        public string be_ki;

        public Tarsalgo(int ora, int perc, int azon, string be_ki)
        {
            this.ora = ora;
            this.perc = perc;
            this.azon = azon;
            this.be_ki = be_ki;
        }
    }
}
