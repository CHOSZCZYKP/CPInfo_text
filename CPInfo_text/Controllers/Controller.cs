using CPInfo_text.Models;
using CPInfo_text.Views;
using LibreHardwareMonitor.Hardware;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CPInfo_text.Controllers
{
    internal class Controller
    {
        private Model _model;
        private View _view;
        
        public string JednostkaTemperatury { get; set; }
        public string AktualizacjaInterwalow { get; set; }
        public List<string> KolumnyWyswietlane { get; set; }


        public Controller(Model model, View view)
        {
            this._model = model;
            this._view = view;
            this.JednostkaTemperatury = "°C";
            this.AktualizacjaInterwalow = "1 s";
            this.KolumnyWyswietlane = new List<string>() { "Wartość"};

        }

        public void ControlerGlowneMenu()
        {
            string wyborMenuGlowne = _view.WidokGlowneMenu();
            switch (wyborMenuGlowne)
            {
                case "Ustawienia":
                    ControlerUstawienia();
                    break;
                case "Informacje o podzespołach":
                    ControlerWyborPodzespoluDoMonitorowania();
                    break;
                case "Pobieranie specyfikacji komputera":
                    ControlerPobierzSpecyfikacjeKomputera();
                    break;
                case "Informacje":
                    ControlerInformacjeOProgramie();
                    break;
                case "Wyjdź":
                    _model.Dispose();
                    return;
            }
        }

        public void ControlerUstawienia()
        {
            string wyborUstawienia = _view.WidokUstawienia();
            switch (wyborUstawienia)
            {
                case "Wyświetlanie kolumny":
                    ControlerWyswietlanieKolumny();
                    ControlerGlowneMenu();
                    break;
                case "Jednostka temperatury":
                    ControlerJednostkaTemperatury();
                    ControlerGlowneMenu();
                    break;
                case "Aktualizacja interwałów":
                    ControlerAktualizacjaInterwalow();
                    ControlerGlowneMenu();
                    break;
                case "Wróć":
                    ControlerGlowneMenu();
                    break;
            }
        }

        public void ControlerWyborPodzespoluDoMonitorowania()
        {
            string wyborPodzespolu = _view.WidokWyborPodzespoluDoMonitorowania();

            if (wyborPodzespolu.Equals("Wróć"))
            {
                ControlerGlowneMenu();
            }
            else
            {
                ControlerInformacjeOPodzespolach(wyborPodzespolu);
            }
        }
        
        public void ControlerWyswietlanieKolumny()
        {
            List<string> listaKolumnWyswietlanych = _view.WidokKolumny();
            if (listaKolumnWyswietlanych.Contains("Wróć") && listaKolumnWyswietlanych.Count > 1)
            {
                bool potwierdzenie = _view.WidokPotwierdzenie();
                if (potwierdzenie.Equals(false))
                {
                    ControlerWyswietlanieKolumny();
                }
                else
                {
                    return;
                }
            }
            else if (listaKolumnWyswietlanych.Contains("Wróć") && listaKolumnWyswietlanych.Count == 1)
            {
                return;
            }
            else
            {
                KolumnyWyswietlane.Clear();
                KolumnyWyswietlane = listaKolumnWyswietlanych;
            }

        }

        public void ControlerJednostkaTemperatury()
        {
            string wyborTemperatury = _view.WidokTemperatur();
            string jednostka = string.Empty;
            if (wyborTemperatury.Equals("Stopnie Celciusza"))
            {
                JednostkaTemperatury = "°C";
            }
            else if (wyborTemperatury.Equals("Stopnie Farenheita"))
            {
                JednostkaTemperatury = "°F";
            }

        }
        
        public void ControlerAktualizacjaInterwalow()
        {
            string wyborAktualizajiInterwalow = _view.WidokAktualizacjaInterwalow();
            string aktualizacja = string.Empty;

            if (!wyborAktualizajiInterwalow.Equals("Wróć"))
            {
                AktualizacjaInterwalow = wyborAktualizajiInterwalow;
            }
        }

        public void ControlerInformacjeOProgramie()
        {
            string wyborInformacjeOProgramie = _view.WidokInformacjaOProgramie();
            if (wyborInformacjeOProgramie.Equals("Wróć"))
            {
                ControlerGlowneMenu();
            }
        }

        public void ControlerInformacjeOPodzespolach(string wyborPodzespolu)
        {
            _model.DaneCzujnikow(wyborPodzespolu, JednostkaTemperatury);
            _view.TworzenieTabeli(KolumnyWyswietlane);

            _view.InicjalizacjaTabeli(_model.ListaCzujnikowInfo, KolumnyWyswietlane, JednostkaTemperatury);
            int czasOdswiezania = HelperKonwerter.KonverterMilisekundy(AktualizacjaInterwalow);
            bool wyjscieDoMenu = _view.WyswietlanieTabeli(_model, czasOdswiezania, KolumnyWyswietlane, JednostkaTemperatury);
            if (wyjscieDoMenu)
            {
                _model.WylaczenieWszystkichPodzespolow();
                ControlerGlowneMenu();
            }
        }

        public void ControlerPobierzSpecyfikacjeKomputera()
        {
            string wybor = _view.WidokPobireaniaSpecyfikacji();

            switch(wybor)
            {
                case "Pulpit":
                    ControlerPulpitPobierz();
                    ControlerGlowneMenu();
                    break;
                case "Pobrane":
                    ControlerPobranePobierz();
                    ControlerGlowneMenu();
                    break;
                case "Dokumenty":
                    ControlerDokumentyPobierz();
                    ControlerGlowneMenu();
                    break;
                case "Dysk systemowy":
                    ControlerDyskSystemowyPobierz();
                    ControlerGlowneMenu();
                    break;
                case "Inne":
                    ControlerInneMiejscePobierz();
                    ControlerGlowneMenu();
                    break;
                case "Wróć":
                    ControlerGlowneMenu();
                    break;
            }
        }

        public void ControlerPulpitPobierz()
        {
            string nazwaPliku = _view.WidokNazwaPliku();
            if (string.IsNullOrEmpty(nazwaPliku))
            {
                return;
            }
            string pulpitSciezka = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
            string calaSciezka = Path.Combine(pulpitSciezka, nazwaPliku + ".txt");
            if (!File.Exists(calaSciezka))
            {
                SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                _view.WidokProgressBar(calaSciezka, specyfikacjaKomputera);
            }
            else
            {
                _view.WidokPlikIstnieje(nazwaPliku);
                ControlerPulpitPobierz();
            }

        }

        public void ControlerPobranePobierz()
        {
            string nazwaPliku = _view.WidokNazwaPliku();
            if (string.IsNullOrEmpty(nazwaPliku))
            {
                return;
            }
            string pobraneSciezka = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string calaSciezka = Path.Combine(pobraneSciezka, nazwaPliku + ".txt");
            if (!File.Exists(calaSciezka))
            {
                SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                _view.WidokProgressBar(calaSciezka, specyfikacjaKomputera);
            }
            else
            {
                _view.WidokPlikIstnieje(nazwaPliku);
                ControlerPobranePobierz();
            }
        }

        public void ControlerDokumentyPobierz()
        {
            string nazwaPliku = _view.WidokNazwaPliku();
            if (string.IsNullOrEmpty(nazwaPliku))
            {
                return;
            }
            string dokumentySciezka = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");
            string calaSciezka = Path.Combine(dokumentySciezka, nazwaPliku + ".txt");
            if (!File.Exists(calaSciezka))
            {
                SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                _view.WidokProgressBar(calaSciezka, specyfikacjaKomputera);
            }
            else
            {
                _view.WidokPlikIstnieje(nazwaPliku);
                ControlerDokumentyPobierz();
            }
        }

        public void ControlerDyskSystemowyPobierz()
        {
            string nazwaPliku = _view.WidokNazwaPliku();
            if (string.IsNullOrEmpty(nazwaPliku))
            {
                return;
            }
            string dyskSystemowySciezka = Path.GetPathRoot(Environment.SystemDirectory);
            string calaSciezka = Path.Combine(dyskSystemowySciezka, nazwaPliku + ".txt");
            if (!File.Exists(calaSciezka))
            {
                SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                _view.WidokProgressBar(calaSciezka, specyfikacjaKomputera);
            }
            else
            {
                _view.WidokPlikIstnieje(nazwaPliku);
                ControlerDyskSystemowyPobierz();
            }
        }

        public void ControlerInneMiejscePobierz()
        {
            string calaSciezka = _view.WidokCalaSciezkaDoPliku();
            if (string.IsNullOrEmpty(calaSciezka))
            {
                return;
            }

            string calaSciezkaZRozszerzeniem = $"{calaSciezka}.txt";
            string sciezkaKatalogow = Path.GetDirectoryName(calaSciezka);
            if (Directory.Exists(sciezkaKatalogow))
            {
                if (!File.Exists(calaSciezka))
                {
                    SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                    _view.WidokProgressBar(calaSciezkaZRozszerzeniem, specyfikacjaKomputera);
                }
                else
                {
                    _view.WidokPlikIstnieje(Path.GetFileName(calaSciezkaZRozszerzeniem));
                    ControlerInneMiejscePobierz();
                }
            }
            else
            {
                _view.WidokKatalogNieIstnieje(sciezkaKatalogow);
                ControlerInneMiejscePobierz();
            }
        }

    }
}
