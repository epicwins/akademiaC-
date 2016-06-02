using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;


/* Mój pierwszy program w języku C# i pierwszy pisany obiektowo :)
    
    ZOSTAŁY STWORZONE 3 KLASY SPRZEDAŻ, PRZEDMIOTY I SZUKANIE
    KLASA SPRZEDAŻ DZIEDZICZY INTERFEJSY Z KLASY I OBLICZANIE
    ZOSTAŁ ZASTOSOWANY POLIMORFIZM STATYCZNY CZYLI PRZECIĄŻENIE FUNKCJI W KLASIE SPRZEDAŻ ORAZ SZUKANIE
    OBSŁUGA WYJĄTKÓW GDY ZOSTAŁA WPISANA NIEPRAWIDŁOWA WARTOŚĆ LUB NIE ZOSTAŁA WCALE WPISANA
    ENUM W KLASIE "PRZEDMIOTY"
    UŻYCIE SWOJEGO INTERFEJSU "IOBLICZANIE" WYKORZYSTANIE GO W KLASIE SPRZEDAŻ
    JAKO KOLEKCJI UŻYCIE LISTY
*/
namespace Projekt_zaliczenie_kursu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Magazyn> MagazynList { get; set; }

        public ObservableCollection<Magazyn> SzukanaList { get; set; }

        public Sprzedaz sprzedaz;

        public Szukanie szukanie;

        private Magazyn magazyn;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            MagazynList = new ObservableCollection<Magazyn>();   //UŻYCIE KOLEKCJI LIST

            SzukanaList = new ObservableCollection<Magazyn>();  //UŻYCIE KOLEKCJI LIST

            this.DziałComboBox.ItemsSource = Enum.GetValues(typeof(Dział));

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string przedmiot = this.PrzedmiotTextBox.Text;

            try
            {
                int nrkatalogowy = int.Parse(this.NrkatalogowyTextBox.Text);

                int ilosc = int.Parse(this.IloscTextBox.Text);

                try
                {
                    Dział dział = (Dział)Enum.Parse(typeof(Dział), this.DziałComboBox.Text);
                    Magazyn magazyn = new Magazyn(przedmiot, nrkatalogowy, dział, ilosc);
                    MagazynList.Add(magazyn);
                }
                catch
                {
                    MessageBox.Show("Wybierz dział", "Błąd dodawania przedmiotu");
                }
            }
            catch
            {
                MessageBox.Show("Wpisz wartość liczbową do pola nr katalogowy lub ilość", "Błąd dodawania przedmiotu");
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.MagazynList.RemoveAt(this.ListView1.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("Wybierz do usunięcia", "Błąd usuwania");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Skład"; // Default file name
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filePath = dlg.FileName;

                ListToXmlFile(filePath);
            }
        }

        private void ListToXmlFile(string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Magazyn>));
                serializer.Serialize(sw, MagazynList);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = dlg.FileName;
            }

            if (File.Exists(filename))
            {
                XmlFileToList(filename);
            }
            else
            {
                MessageBox.Show("Plik nie istnieje", "Bład odczytu");
            }
        }

        private void XmlFileToList(string filename)
        {
            using (var sr = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(ObservableCollection<Magazyn>));
                ObservableCollection<Magazyn> tmpList = (ObservableCollection<Magazyn>)deserializer.Deserialize(sr);
                foreach (var item in tmpList)
                {
                    MagazynList.Add(item);
                }
            }
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float cena = float.Parse(this.CenaTextBox.Text);

                int ilosc = int.Parse(this.IlośćTextBox.Text);

                float zaplata = float.Parse(this.ZapłataTextBox.Text);

                int wart = this.ListView3.SelectedIndex;
                var cos = MagazynList[wart];

                int nrkatalogowy;
                string przedmiot;
                Dział dział;

                int sprzedano = 0;
                float kasa = 0;
                float reszta = 0;

                if (ilosc <= cos.Ilosc)
                {
                    Sprzedaz sprzedaz = new Sprzedaz(cena, ilosc, zaplata, out reszta, out sprzedano, out kasa);
                }
                if (reszta != zaplata)
                {
                    przedmiot = cos.Przedmiot;
                    dział = cos.dział;
                    nrkatalogowy = cos.Nrkatalogowy;
                    ilosc = cos.Ilosc - ilosc;  

                    if (ilosc > 0)
                    {
                        Magazyn magazyn = new Magazyn(przedmiot, nrkatalogowy, dział, ilosc);
                        MagazynList.Insert(wart, magazyn);
                    }
                    this.MagazynList.RemoveAt(this.ListView3.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Zbyt mała ilość produktu lub zbyt niska zapłata", "Błąd sprzedaży");
                }
                textBlock.Text = reszta.ToString() + "zł";
                SprzedaneBlock.Text = sprzedano.ToString();
                PrzychodBlock.Text = kasa.ToString() + "zł";
            }
            catch
            {
                MessageBox.Show("Wpisz wszystkie elementy i zaznacz produkt", "Błąd");
            }
       }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int szukaj = int.Parse(this.WyszukajTextBox.Text);

                Szukanie szukanie = new Szukanie(szukaj, ref magazyn, MagazynList);

                SzukanaList.Add(magazyn);
            }
            catch
            {
                MessageBox.Show("Wpisz nr katalogwy do wyszukania", "Błąd wyszukiwania");
            }
        }
    }
}
