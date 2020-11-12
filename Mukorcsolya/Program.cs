using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mukorcsolya
{
    class Program
    {
        public static double Osszpontszam(Versenyzo versenyzo)
        {
            return versenyzo.KomponensPont + versenyzo.TechnikaiPont - versenyzo.HibaPont;
        }
        static void Main(string[] args)
        {
            List<string> sorok = File.ReadAllLines("rovidprogram.csv", Encoding.UTF8).Skip(1).ToList();
            List<Versenyzo> rovidprogram = new List<Versenyzo>();
            foreach (string item in sorok)
            {
                rovidprogram.Add(new Versenyzo(item));
            }

            sorok = File.ReadAllLines("donto.csv", Encoding.UTF8).Skip(1).ToList();
            List<Versenyzo> donto = new List<Versenyzo>();
            foreach (string item in sorok)
            {
                donto.Add(new Versenyzo(item));
            }

            Console.WriteLine("2.feladat: ");
            Console.WriteLine($"\tA rövidprogramban {rovidprogram.Count} induló volt");

            Console.WriteLine("3. feladat: ");

            for (int i = 0; i < donto.Count; i++)
            {
                if(donto[i].OrszagKod=="HUN")
                {
                    Console.WriteLine($"\tA magyar versenyző bejutott a kűrbe.");
                    break;
                }
            }


            Console.WriteLine("4.feladat:");
            Console.Write("\tKérem a versenyző nevét: ");
            string nev = Console.ReadLine();
            bool vanIlyenVersenyzo = false;

            for (int i = 0; i < rovidprogram.Count; i++)
            {
                if (rovidprogram[i].Nev.ToLower() == nev.ToLower())
                {
                    vanIlyenVersenyzo = true;
                    break;
                }

            }
            if(!vanIlyenVersenyzo)
            {
                Console.WriteLine("Ilyen nevű induló nem volt.");
            }
            Console.WriteLine("6.feladat:");
            Console.Write("\tA versenyző összpontszáma: ");

            for (int i = 0; i < rovidprogram.Count; i++)
            {
                rovidprogram[i].TeljesPontszam += Osszpontszam(rovidprogram[i]);
                for (int j = 0; j < donto.Count; j++)
                {
                    if(donto[j].Nev==rovidprogram[i].Nev)
                    {
                        rovidprogram[i].TeljesPontszam += Osszpontszam(donto[j]);
                    }
                }

            }



            var versenyzoPontja = rovidprogram.Single(x => x.Nev.ToLower() == nev.ToLower());




            Console.WriteLine(versenyzoPontja.TeljesPontszam);
            Console.WriteLine("7.feladat: ");
            var tovabbjutottak = donto.GroupBy(x => x.OrszagKod);

            foreach (var item in tovabbjutottak)
            {
                if(item.Count()>1)
                {
                    Console.WriteLine($"\t{item.Key}: {item.Count()}");

                }
                
            }

            var osszesVersenyzo = rovidprogram.OrderByDescending(x => x.TeljesPontszam);
            int m = 0;
            string sorrend = string.Empty;
            foreach (var item in osszesVersenyzo)
            {
                sorrend+=$"{m};{item.Nev};{item.OrszagKod};{item.TeljesPontszam}"+Environment.NewLine;
                m++;
            }

            using (StreamWriter kiiras = new StreamWriter("vegeredmeny.csv", false, Encoding.UTF8))
            {
                kiiras.WriteLine(sorrend);
            }

            Console.ReadKey();
        }
    }
}
