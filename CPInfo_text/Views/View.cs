﻿using CPInfo_text.Controllers;
using CPInfo_text.Models;
using LibreHardwareMonitor.Hardware;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPInfo_text.Views
{
    internal class View
    {
        private Table _table;

        private static void WysrodkowanieWPionie()
        {
            int wysokosc = AnsiConsole.Profile.Height;
            int puteLinie = (wysokosc / 3);

            for (int i = 0; i < puteLinie; i++)
            {
                AnsiConsole.WriteLine();
            }

        }

        private static string WysrodkowanieWPoziomie(string tekst)
        {
            int szerokosc = AnsiConsole.Profile.Width;
            int padding = (szerokosc - tekst.Length) / 2;
            return tekst.PadLeft(padding + tekst.Length).PadRight(szerokosc);
        }

        public string WidokGlowneMenu()
        {
            AnsiConsole.Clear();
            string tytul = @"___________________.___        _____           __                   __   
\_   ___ \______   \   | _____/ ____\____    _/  |_  ____ ___  ____/  |_ 
/    \  \/|     ___/   |/    \   __\/  _ \   \   __\/ __ \\  \/  /\   __\
\     \___|    |   |   |   |  \  | (  <_> )   |  | \  ___/ >    <  |  |  
 \______  /____|   |___|___|  /__|  \____/____|__|  \___  >__/\_ \ |__|  
        \/                  \/          /_____/         \/      \/       ";
            var tytulAplikacji = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAplikacji.Justification = Justify.Center;

            WysrodkowanieWPionie();
            AnsiConsole.Write(tytulAplikacji);

            
            string[] menuWyboru = { "Ustawienia", "Informacje o podzespołach", "Pobieranie specyfikacji komputera", "Informacje", "Wyjdź" };
            var wysrodkowane = menuWyboru.Select(s => WysrodkowanieWPoziomie(s)).ToList();

            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(6)
                .AddChoices(wysrodkowane)
            );

            AnsiConsole.Clear();
            return wybor.Trim();
        }
        public string WidokUstawienia()
        {
            string tytul = @" ____ ___         __                .__              .__        
|    |   \_______/  |______ __  _  _|__| ____   ____ |__|____   
|    |   /  ___/\   __\__  \\ \/ \/ /  |/ __ \ /    \|  \__  \  
|    |  /\___ \  |  |  / __ \\     /|  \  ___/|   |  \  |/ __ \_
|______//____  > |__| (____  /\/\_/ |__|\___  >___|  /__(____  /
             \/            \/               \/     \/        \/ ";
            var tytulUstawienia = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulUstawienia.Justification = Justify.Center;

            WysrodkowanieWPionie();
            AnsiConsole.Write(tytulUstawienia);


            string[] menuWyboru = { "Wyświetlanie kolumny", 
                "Jednostka temperatury", 
                "Aktualizacja interwałów", 
                "Wróć" };
            var wysrodkowane = menuWyboru.Select(s => WysrodkowanieWPoziomie(s)).ToList();
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(5)
                .AddChoices(wysrodkowane)
            );

            AnsiConsole.Clear();
            return wybor.Trim();
        }
        
        public string WidokWyborPodzespoluDoMonitorowania()
        {
            string tytul = @"  _________ __                                    .___                                   .__         
 /   _____//  |______    ____   ______   ____   __| _/_______ ____   ____________   ____ |  |  __ __ 
 \_____  \\   __\__  \  /    \  \____ \ /  _ \ / __ |\___   // __ \ /  ___/\____ \ /  _ \|  |/ |  |  \
 /        \|  |  / __ \|   |  \ |  |_> >  <_> ) /_/ | /    /\  ___/ \___ \ |  |_> >  <_> )  |_ |  |  /
/_______  /|__| (____  /___|  / |   __/ \____/\____ |/_____ \\___  >____  >|   __/ \____/|____/ ____/ 
        \/           \/     \/  |__|               \/      \/    \/     \/ |__|                      ";
            var tytulMonitoringu = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulMonitoringu.Justification = Justify.Center;

            WysrodkowanieWPionie();
            AnsiConsole.Write(tytulMonitoringu);


            string[] menuWyboru = { "CPU",
                    "Płyta główna",
                    "Pamięć RAM",
                    "Karta graficzna",
                    "Dyski twarde",
                    "Karty sieciowe",
                    "Bateria",
                    "Wróć" };
            var wysrodkowane = menuWyboru.Select(s => WysrodkowanieWPoziomie(s)).ToList();
            var wyborPodzespoluDoMoniotorwania = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(9)
                .AddChoices(wysrodkowane)
            );

            AnsiConsole.Clear();
            return wyborPodzespoluDoMoniotorwania.Trim();
        }

        public List<string> WidokKolumny()
        {
            string tytul = @" ____  __.     .__                                                              .__        __  .__                        
|    |/ _|____ |  |  __ __  _____   ____ ___.__. __  _  _____.__. ___/____  _  _|__| _____/  |_|  | _____    ____   ____  
|      < /  _ \|  | |  |  \/     \ /    <   |  | \ \/ \/ <   |  |/  ___/\ \/ \/ /  |/ __ \   __\  | \__  \  /    \_/ __ \ 
|    |  (  <_> )  |_|  |  /  Y Y  \   |  \___  |  \     / \___  |\___ \  \     /|  \  ___/|  | |  |__/ __ \|   |  \  ___/ 
|____|__ \____/|____/____/|__|_|  /___|  / ____|   \/\_/  / ____/____  >  \/\_/ |__|\___  >__| |____(____  /___|  /\___  >
        \/                      \/     \/\/               \/         \/                 \/               \/     \/     \/ ";
            var tytulWidokuKolumn = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulWidokuKolumn.Justification = Justify.Center;

            WysrodkowanieWPionie();
            AnsiConsole.Write(tytulWidokuKolumn);

            string[] menuWyboru = { "Wartość",
                    "Min",
                    "Max",
                    "Wróć",};
            var wysrodkowane = menuWyboru.Select(s => WysrodkowanieWPoziomie(s)).ToList();
            var wyborKolumn = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .PageSize(4)
                .AddChoices(wysrodkowane)
            );
            AnsiConsole.Clear();

            List<string> trimLista = wyborKolumn.Select(s => s.Trim()).ToList();
            return trimLista;
        }

        public string WidokTemperatur()
        {

            string tytul = @"___________                                        __                       
\__    ___/___   _____ ______   ________________ _/  |_ __ ______________   
  |    |_/ __ \ /     \\____ \_/ __ \_  __ \__  \\   __\  |  \_  __ \__  \  
  |    |\  ___/|  Y Y  \  |_> >  ___/|  | \// __ \|  | |  |  /|  | \// __ \_
  |____| \___  >__|_|  /   __/ \___  >__|  (____  /__| |____/ |__|  (____  /
             \/      \/|__|        \/           \/                       \/ ";
            var tytulTemperatura = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulTemperatura.Justification = Justify.Center;

            WysrodkowanieWPionie();

            AnsiConsole.Write(tytulTemperatura);

            string[] temperaturaWybor = { "Stopnie Celciusza", "Stopnie Farenheita", "Wróć" };
            var wysrodkowane = temperaturaWybor.Select(s => WysrodkowanieWPoziomie(s)).ToList();

            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(3)
                .AddChoices(wysrodkowane)
            );

            AnsiConsole.Clear();
            return wybor.Trim();
        }

        public string WidokInformacjaOProgramie()
        {
            string tytul = @"________    __________                                           .__        
\_____  \   \______   \_______  ____   ________________    _____ |__| ____  
 /   |   \   |     ___/\_  __ \/  _ \ / ___\_  __ \__  \  /     \|  |/ __ \ 
/    |    \  |    |     |  | \(  <_> ) /_/  >  | \// __ \|  Y Y  \  \  ___/ 
\_______  /  |____|     |__|   \____/\___  /|__|  (____  /__|_|  /__|\___  >
        \/                          /_____/            \/      \/        \/ ";


            string[] InformacjeOProgramie = new string[]
            {
                "Nazwa programu: CPInfo_text",
                "Wersja: 1.0.2.1",
                "Autor: Paweł Choszczyk",
                "Uwagi: W przypadku niektórych płyt głównych w laptopach program nie jest w stanie pobierać informacji o płycie głównej.",
                "Zalecenia: Kolory tła, kolory czionki i rozmiar oraz wiekość okna należy dostosować do własnych preferencji wchodząc w właściowści \"Wiersza poleceń\" ",
                "[blue]Kliknij\"Escape\" żeby wrócić do menu[/]"
            };

            var tytulOProgramie = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulOProgramie.Justification = Justify.Center;

            WysrodkowanieWPionie();
            AnsiConsole.Write(tytulOProgramie);
            var wysrodkowane = InformacjeOProgramie.Select(s => WysrodkowanieWPoziomie(s)).ToList();

            foreach (var info in wysrodkowane)
            {
                AnsiConsole.MarkupLine(info);
            }

            while(true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            AnsiConsole.Clear();
            return "Wróć";
        }

        public string WidokAktualizacjaInterwalow()
        {
            string tytul = @"   _____   __      __               .__  .__                           __         _________                  __               __                 
  /  _  \ |  | ___/  |_ __ _______  |  | |__|____________    ____     |__|____    \_   ___ \__________ __   |__| ____   ____ |  | ____/___  _  __
 /  /_\  \|  |/ /\   __\  |  \__  \ |  | |  \___   /\__  \ _/ ___\    |  \__  \   /    \  \/\___   /  |  \  |  |/    \ /  _ \|  |/ /  _ \ \/ \/ /
/    |    \    <  |  | |  |  // __ \|  |_|  |/    /  / __ \\  \___    |  |/ __ \_ \     \____/    /|  |  /  |  |   |  (  <_> )    <  <_> )     / 
\____|__  /__|_ \ |__| |____/(____  /____/__/_____ \(____  /\___  >\__|  (____  /  \______  /_____ \____/\__|  |___|  /\____/|__|_ \____/ \/\_/  
        \/     \/                 \/              \/     \/     \/\______|    \/          \/      \/    \______|    \/            \/             ";
            var tytulAktualizacjaCzujnikow = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAktualizacjaCzujnikow.Justification = Justify.Center;

            WysrodkowanieWPionie();

            AnsiConsole.Write(tytulAktualizacjaCzujnikow);

            string[] aktualizacjaWybor = { "250 ms",
                    "500 ms",
                    "1 s",
                    "2 s",
                    "5 s",
                    "10 s",
                    "Wróć" };
            var wysrodkowane = aktualizacjaWybor.Select(s => WysrodkowanieWPoziomie(s)).ToList();

            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(7)
                .AddChoices(wysrodkowane)); 
            
            AnsiConsole.Clear();
            return wybor.Trim();
        }

        public bool WidokPotwierdzenie()
        {
            var potwierdzenie = AnsiConsole.Prompt(
                new ConfirmationPrompt(@"Czy chcesz wrócić do ustawień nie zapusując zmian? (Nie zaznaczaj ""Wróć"" ta opcja służy do powrotu do ustawień jeśli nie chcesz wprowadzać zmian.)")
                .ShowChoices()
            );

            AnsiConsole.Clear();

            return potwierdzenie;
        }

        public void TworzenieTabeli(List<string> listaKolumn)
        {
            _table = new Table();
            _table.AddColumn("Urządzenie");
            _table.AddColumn("Nazwa czujnika");
            if (listaKolumn.Contains("Wartość"))
            {
                _table.AddColumn("Wartość");
            }
            if (listaKolumn.Contains("Min"))
            {
                _table.AddColumn("MIN");
            }
            if (listaKolumn.Contains("Max"))
            {
                _table.AddColumn("MAX");
            }
        }

        public void InicjalizacjaTabeli(List<CzujnikiInfo> czujnikiInfos, List<string> listaKolumn, string jednostkaTemperatury)
        {
            foreach (var sensor in czujnikiInfos)
            {
                List<string> data = new List<string>();
                data.Add(sensor.NazwaUrzadzenia);
                data.Add(sensor.NazwaCzujnika);
                if (listaKolumn.Contains("Wartość"))
                {
                    data.Add(sensor.Wartosc);
                }
                if (listaKolumn.Contains("Min"))
                {
                    data.Add(sensor.Min);
                }
                if (listaKolumn.Contains("Max"))
                {
                    data.Add(sensor.Max);
                }

                _table.AddRow(data.ToArray());
            }
        }

        public void AktualizacjaTabeli(List<CzujnikiInfo> czujnikiInfos, List<string> listaKolumn)
        {
            for (int i = 0; i < czujnikiInfos.Count; i++)
            {
                var sensor = czujnikiInfos[i];
                int liczbaKolumn = 2;
        
                if (listaKolumn.Contains("Wartość"))
                {
                    _table.UpdateCell(i, liczbaKolumn, sensor.Wartosc.ToString());
                    liczbaKolumn++;
                }
                if (listaKolumn.Contains("Min"))
                {
                    _table.UpdateCell(i, liczbaKolumn, sensor.Min.ToString());
                    liczbaKolumn++;
                }
                if (listaKolumn.Contains("Max"))
                {
                    _table.UpdateCell(i, liczbaKolumn, sensor.Max.ToString());
                }
            }
        }

        public bool WyswietlanieTabeli(Model model, int czasOdswiezania, List<string> lisaKolumn, string jednostkaTemperatury)
        {
            bool check = false;
            AnsiConsole.Live(_table)
                .Start(x =>
                {
                    int liczbaSensorow = model.ListaCzujnikowInfo.Count;
                    if (liczbaSensorow > 0)
                    {
                        CzujnikiInfo sensor = model.ListaCzujnikowInfo.First();
                        AnsiConsole.Markup($"[blue]Naciśnij [bold]Escape[/] aby zatrzymać wyświetlanie tabeli.[/]\n[green]Urządzenie: {sensor.NazwaUrzadzenia ?? ""}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.Markup($"[blue]Naciśnij [bold]Escape[/] aby zatrzymać wyświetlanie tabeli.[/]\n[red]Nie wykryto urządzenia[/]\n");
                    }

                    while (true)
                    {
                        model.AktualizacjaCzujnikow(jednostkaTemperatury);

                        AktualizacjaTabeli(model.ListaCzujnikowInfo, lisaKolumn);
                        x.Refresh();
                        Thread.Sleep(czasOdswiezania);
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            AnsiConsole.Clear();
                            check = true;
                            break;
                        }
                    }
                });
            AnsiConsole.Clear();
            return check;
        }

        public string WidokPobireaniaSpecyfikacji()
        {
            string tytul = @"   _____  .__            __                      __________             .__              
  /     \ |__| ____     |__| ______ ____  ____   \____    /____  ______ |__| ________ __ 
 /  \ /  \|  |/ __ \    |  |/  ___// ___\/ __ \    /     /\__  \ \____ \|  |/  ___/  |  \
/    Y    \  \  ___/    |  |\___ \\  \__\  ___/   /     /_ / __ \|  |_> >  |\___ \|  |  /
\____|__  /__|\___  >\__|  /____  >\___  >___  > /_______ (____  /   __/|__/____  >____/ 
        \/        \/\______|    \/     \/    \/          \/    \/|__|           \/       ";
            var tytulMiejsceZapisu = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulMiejsceZapisu.Justification = Justify.Center;

            WysrodkowanieWPionie();

            AnsiConsole.Write(tytulMiejsceZapisu);

            string[] miejsceZapisuWybor = { "Pulpit",
                    "Pobrane",
                    "Dokumenty",
                    "Dysk systemowy",
                    "Inne",
                    "Wróć" };
            var wysrodkowane = miejsceZapisuWybor.Select(s => WysrodkowanieWPoziomie(s)).ToList();

            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(6)
                .AddChoices(wysrodkowane)
            );
            AnsiConsole.Clear() ;
            return wybor.Trim();
        }
        public string WidokNazwaPliku()
        {
            AnsiConsole.Clear();
            var nazwa = AnsiConsole.Prompt(
                new TextPrompt<string>(@"Wprowadź nazwę pliku (lub zostaw puste i kliknij ""enter"", by wrócić): ").AllowEmpty());      
            return nazwa;
        }

        public string WidokCalaSciezkaDoPliku()
        {
            AnsiConsole.Clear();
            var calaSciekzka = AnsiConsole.Prompt(
                new TextPrompt<string>(@"Wprowadź całą ścieżkę do pliku (lub zostaw puste i kliknij ""enter"", by wrócić): ").AllowEmpty());
            return calaSciekzka;
        }

        public void WidokPlikIstnieje(string sciezka)
        {
            AnsiConsole.Markup($"[red]Istaniej plik o nazwie: \"{sciezka}\". Wymyśl inną nazwę[/]\nKliknij jakiś przycisk aby przejść dalej.");
            Console.ReadKey();
        }

        public void WidokKatalogNieIstnieje(string sciezka)
        {
            AnsiConsole.Markup($"[red]Nie istnieje taka ścieżka do katalogów \"{sciezka}\".[/]\nKliknij jakiś przycisk aby przejść dalej");
            Console.ReadKey();
        }

        public void WidokProgressBar(string calaSciezka, SpecyfikacjaKomputera specyfikacjaKomputera)
        {
            AnsiConsole.MarkupLine("Trwa zapis danych...");
            AnsiConsole.Progress()
                .Start(x =>
                {
                    var task = x.AddTask("[green]Zapisuje dane[/]");
                    specyfikacjaKomputera.ZapisDoPliku(calaSciezka, task);

                });
            AnsiConsole.MarkupLine("[bold green]Zapisano![/]");
            Thread.Sleep(1000);
            AnsiConsole.Clear() ;
        }
    }
}
