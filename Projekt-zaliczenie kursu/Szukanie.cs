using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie_kursu
{
    public class Szukanie
    {
        private int _nrkatalogowy1 { get; set; }
        private Dział _dzial1 { get; set; }
        private int _ilosc1 { get; set; }
        private string _nazwa1 { get; set; }


        public Szukanie() { }

      
        public Szukanie(int szukaj, ref Magazyn wartosc, ObservableCollection<Magazyn> magazynList)
        {
            foreach (Magazyn element in magazynList)
            {
                if (element.Nrkatalogowy == szukaj)
                {
                    _nrkatalogowy1 = element.Nrkatalogowy;
                    _ilosc1 = element.Ilosc;
                    _dzial1 = element.dział;
                    _nazwa1 = element.Przedmiot;
                    Magazyn pomocnicza = new Magazyn(_nazwa1, _nrkatalogowy1, _dzial1, _ilosc1);
                    wartosc = pomocnicza;
                }
            }
        }
    }
}
