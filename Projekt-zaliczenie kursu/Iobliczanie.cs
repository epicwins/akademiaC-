using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie_kursu
{
    public interface Iobliczanie
    {
        int obliczanie(int ilosc);

        float obliczanie(int iloscdosprzedania, float naleznosc);
    }
}
