using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie_kursu
{
    public enum Dział { Spozywczy, Przemysłowy, Chemiczny, Monopolowy }   //WYKORZYSTANIE ENUM

    public class Magazyn 
    {

        public string Przedmiot { get; set; } 

        public int Nrkatalogowy { get; set; } 

        public Dział dział { get; set; }

        public int Ilosc { get; set; }

        public Magazyn() { }

        public Magazyn(string przedmiot, int nrkatalogowy, Dział dział, int ilosc)
        {
            this.Przedmiot = przedmiot;
            this.Nrkatalogowy = nrkatalogowy;
            this.dział = dział;
            this.Ilosc = ilosc;
        }

    }
}
