using CPInfo_text.Models;
using CPInfo_text.Views;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Start()
        {
            _view.TytulAplikacji();
            ControlerGlowneMenu();

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
                    ControlerInformacjeOPodzespolach();
                    break;
                case "Czyszczenie dysku":

                    break;

                case "Informacje":
                    ControlerInformacjeOProgramie();
                    break;
                case "Wyjdź":

                    return;
            }
        }
        //trzeba dokończyć ustawienia bo tylko działa powrót do menu główengo
        public void ControlerUstawienia()
        {
            string wyborUstawienia = _view.WidokUstawienia();
            switch (wyborUstawienia)
            {
                case "Reset MIN/MAX":
                    AnsiConsole.WriteLine("W budowie...");
                    break;
                case "Wyświetlanie kolumny":
                    ControlerWyswietlanieKolumny();
                    ControlerGlowneMenu();
                    break;
                case "Wybór podzespoółów do monitorowania":
                    ControlerInformacjeOPodzespolach();
                    ControlerGlowneMenu();
                    //ControlerWyborPodzespolowDoMonitorowania();  to będzie do usunięcia bo trzeba zrobić coś takiego że używkownik wybiera jaki podzespół chce monitorować i tylko ten monitoruje
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

        /*public void ControlerWyborPodzespolowDoMonitorowania()
        {
            List<string> listaPodzespolowDoMonitorowania = _view.WidokWyborPodzespolowDoMonitorowania();
            if (listaPodzespolowDoMonitorowania.Contains("Wróć") && listaPodzespolowDoMonitorowania.Count > 1)
            {
                bool potwierdzenie = _view.WidokPotwierdzenie();
                if (potwierdzenie.Equals(false))
                {
                    ControlerWyborPodzespolowDoMonitorowania();
                }
                else
                {
                    return;
                }
            }
            else if (listaPodzespolowDoMonitorowania.Contains("Wróć") && listaPodzespolowDoMonitorowania.Count == 1)
            {
                return;
            }
            else
            {
                PodzespolyDoMonitorowania.Clear();
                PodzespolyDoMonitorowania = listaPodzespolowDoMonitorowania;
            }
        }*/

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

        /*public void ControlerInformacjeOPodzespolach()
        {
            //while (true)
            //{
                AnsiConsole.Clear();
                List<CzujnikiInfo> listaCzujnikowInfo = _model.DaneCzujnikow();
                _view.WidokInformacjiOPodzespolach(listaCzujnikowInfo);
            Console.ReadKey();
                //Thread.Sleep(KonverterMilisekundy());
                
                
            //}
            //_model.Dispose();
        }*/
        /*public void ControlerInformacjeOPodzespolach()
        {
            _model.DaneCzujnikow();
            _view.TworzenieTabeli(_model.ListaCzujnikowInfo);
             bool _wyswietlaniePodzespolow = true;

            while (_wyswietlaniePodzespolow)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)  
                {
                    _wyswietlaniePodzespolow = false;
                }
                _model.AktualizacjaCzujnikow();
                _view.AktualizacjaTabeli(_model.ListaCzujnikowInfo);
                _view.WyswietlenieTabeli();
                Thread.Sleep(1000);
            }
            _model.Dispose();
        }*/
        public void ControlerInformacjeOPodzespolach()
        {
            _model.DaneCzujnikow();
            _view.TworzenieTabeli();
            _view.InicjalizacjaTabeli(_model.ListaCzujnikowInfo);
            bool wyjscieDoMenu = _view.WyswietlenieTabeli(_model);
            _model.Dispose();
            if (wyjscieDoMenu)
            {
                ControlerGlowneMenu();
            }
        }

        public int KonverterMilisekundy()
        {
            var regex = new Regex(@"(\d+)\s*(ms|s)", RegexOptions.IgnoreCase);
            var dopasowanie = regex.Match(AktualizacjaInterwalow);

            if (dopasowanie.Success)
            {
                int wartosc = int.Parse(dopasowanie.Groups[1].Value);
                string jednostka = dopasowanie.Groups[2].Value.ToLower();

                if (jednostka == "ms")
                {
                    return wartosc;
                }
                else
                {
                    return wartosc * 1000;
                }
            }
            else
            {
                throw new ArgumentException($"Nieprawidłowy format: {AktualizacjaInterwalow}");
            }
        }
    }
}
