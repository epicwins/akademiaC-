using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie_kursu
{
    public class Sprzedaz : Iobliczanie  //DZIEDZICZENIE INTERFEJSU
    {
        private float _naleznosc;

        private float _reszta { get; set; }

        public float Cena { get; set; }

        public int IloscDoSprzedania { get; set; }

        public float Zaplata { get; set; }

        public float Reszta {get; set;}

        public int Sprzedano { get; private set; }

        static int licznik { get; set; }

        static float kasa { get; set; }

        public Sprzedaz() { }   //PRECIĄŻANIE FUNKCJI-POLIMIRFIZM STATYCZNY

        public Sprzedaz(float cena, int iloscdosprzedania, float zaplata, out float Reszta, out int Sprzedano, out float Kasa)
        {
            this.Cena = cena;
            this.IloscDoSprzedania = iloscdosprzedania;
            this.Zaplata = zaplata;

            Reszta = _obliczenieReszty(zaplata, iloscdosprzedania, cena);

            Sprzedano = obliczanie(iloscdosprzedania);

            Kasa = obliczanie(iloscdosprzedania, cena);
        }

        public int obliczanie( int iloscdosprzedania)
        {
            licznik = licznik + iloscdosprzedania;

            return licznik;
        }    //ODZIEDZICZONY INTERFEJS Z DWIEMA METODAMI WRAZ Z POLIMORIZMEMEM STATYCZNYM (PRZECIĄŻENIE FUNKCJI)
        

        public float obliczanie(int iloscdosprzedania, float cena)
        {
            _naleznosc = cena * iloscdosprzedania;
            kasa = kasa + _naleznosc ;
            return kasa;
        }

        private float _obliczenieReszty(float zaplata, int iloscdosprzedania, float cena)
        {
            float _reszta = zaplata - iloscdosprzedania * cena;
            if(_reszta < 0)
            {
                _reszta = zaplata; 
            }
            return _reszta;
            
        }
    }
}
