using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mukorcsolya
{
    class Versenyzo
    {
        public string Nev { get; set; }
        public string OrszagKod { get; set; }
        public double TechnikaiPont { get; set; }
        public double KomponensPont { get; set; }
        public int HibaPont { get; set; }
        public double TeljesPontszam { get; set; }

        public Versenyzo(string sor)
        {
            List<string> adatok = sor.Split(';').ToList();

            Nev = adatok[0];
            OrszagKod = adatok[1];
            TechnikaiPont = double.Parse(adatok[2].Replace('.', ','));
            KomponensPont = double.Parse(adatok[3].Replace('.', ','));
            HibaPont = int.Parse(adatok[4]);
        }

        
    }
}
