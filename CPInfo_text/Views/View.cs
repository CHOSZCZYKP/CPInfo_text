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
        private Table _tableAllData;

        public void TytulAplikacji()
        {
            string tytul = @"
___________________.___        _____           __                   __   
\_   ___ \______   \   | _____/ ____\____    _/  |_  ____ ___  ____/  |_ 
/    \  \/|     ___/   |/    \   __\/  _ \   \   __\/ __ \\  \/  /\   __\
\     \___|    |   |   |   |  \  | (  <_> )   |  | \  ___/ >    <  |  |  
 \______  /____|   |___|___|  /__|  \____/____|__|  \___  >__/\_ \ |__|  
        \/                  \/          /_____/         \/      \/       ";
            var tytulAplikacji = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAplikacji.Justification = Justify.Center;
            AnsiConsole.Write(tytulAplikacji);
        }

        private static string WysrodkujTekst(string tekst, int calaSzerowkosc)
        {
            int padding = (calaSzerowkosc - tekst.Length) / 2;
            return tekst.PadLeft(padding + tekst.Length).PadRight(calaSzerowkosc);
        }

        public string WidokGlowneMenu()
        {
            string tytul = @"___________________.___        _____           __                   __   
\_   ___ \______   \   | _____/ ____\____    _/  |_  ____ ___  ____/  |_ 
/    \  \/|     ___/   |/    \   __\/  _ \   \   __\/ __ \\  \/  /\   __\
\     \___|    |   |   |   |  \  | (  <_> )   |  | \  ___/ >    <  |  |  
 \______  /____|   |___|___|  /__|  \____/____|__|  \___  >__/\_ \ |__|  
        \/                  \/          /_____/         \/      \/       ";
            var tytulAplikacji = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAplikacji.Justification = Justify.Center;
            AnsiConsole.Write(tytulAplikacji);

            //int szerokosc = AnsiConsole.Profile.Width;
            //string[] strings = { "Ustawienia", "Informacje o podzespołach", "Pobieranie specyfikacji komputera", "Informacje", "Wyjdź" };
            //var centred = strings.Select(s => WysrodkujTekst(s, szerokosc)).ToList();
            //AnsiConsole.Clear();
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(6)
                .AddChoices(//centred
                    //"Ustawienia",
                    

                    @"     __  ___              ___             
 |  | /__`  |   /\  |  | | |__  |\ | |  /\  
 \__/ .__/  |  /~~\ |/\| | |___ | \| | /~~\ 
                                           ",
                    @"       ___  __   __              __        ___     __      __   __   __  __  ___  __   __   __        __       
 | |\ | |__  /  \ |__)  |\/|  /\  /  `    | |__     /  \    |__) /  \ |  \  / |__  /__` |__) /  \  /\  /  ` |__| 
 | | \| |    \__/ |  \  |  | /~~\ \__, \__/ |___    \__/    |    \__/ |__/ /_ |___ .__/ |    \__/ /~~\ \__, |  | 
                                                                                                                ",
                    @"__   __   __     ___  __               ___     __   __   ___  __       ___              __                  __         __       ___  ___  __       
 |__) /  \ |__) | |__  |__)  /\  |\ | | |__     /__` |__) |__  /  ` \ / |__  | |__/  /\  /  `    | |    |__/ /  \  |\/| |__) |  |  |  |__  |__)  /\  
 |    \__/ |__) | |___ |  \ /~~\ | \| | |___    .__/ |    |___ \__,  |  |    | |  \ /~~\ \__, \__/ |    |  \ \__/  |  | |    \__/  |  |___ |  \ /~~\ 
                                                                                                                                                    ",
                    @"       ___  __   __              __        ___ 
 | |\ | |__  /  \ |__)  |\/|  /\  /  `    | |__  
 | | \| |    \__/ |  \  |  | /~~\ \__, \__/ |___ 
                                                ",
                    @"              __  _/_ 
 |  | \ /    | |  \  / 
 |/\|  |  \__/ |__/ /_ 
                      "
                )
            );

            /*                    @"
      __  ___              ___             
|  | /__`  |   /\  |  | | |__  |\ | |  /\  
\__/ .__/  |  /~~\ |/\| | |___ | \| | /~~\ 
                                           ",
                    @"
        ___  __   __              __        ___     __      __   __   __  __  ___  __   __   __        __       
| |\ | |__  /  \ |__)  |\/|  /\  /  `    | |__     /  \    |__) /  \ |  \  / |__  /__` |__) /  \  /\  /  ` |__| 
| | \| |    \__/ |  \  |  | /~~\ \__, \__/ |___    \__/    |    \__/ |__/ /_ |___ .__/ |    \__/ /~~\ \__, |  | 
                                                                                                                ",                 
@"
 __   __   __     ___  __               ___     __   __   ___  __       ___              __                  __         __       ___  ___  __       
|__) /  \ |__) | |__  |__)  /\  |\ | | |__     /__` |__) |__  /  ` \ / |__  | |__/  /\  /  `    | |    |__/ /  \  |\/| |__) |  |  |  |__  |__)  /\  
|    \__/ |__) | |___ |  \ /~~\ | \| | |___    .__/ |    |___ \__,  |  |    | |  \ /~~\ \__, \__/ |    |  \ \__/  |  | |    \__/  |  |___ |  \ /~~\ 
                                                                                                                                                    ",
                    @"
        ___  __   __              __        ___ 
| |\ | |__  /  \ |__)  |\/|  /\  /  `    | |__  
| | \| |    \__/ |  \  |  | /~~\ \__, \__/ |___ 
                                                ",
                    @" 
               __  _/_ 
|  | \ /    | |  \  / 
|/\|  |  \__/ |__/ /_ 
                      "*/
            AnsiConsole.Clear();
            return wybor;
        }
        public string WidokUstawienia()
        {
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Ustawienia")
                .PageSize(5)
                .AddChoices(new[] 
                {
                    "Wyświetlanie kolumny",
                    "Jednostka temperatury",
                    "Aktualizacja interwałów",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }
        
        public string WidokWyborPodzespoluDoMonitorowania()
        {
            var wyborPodzespoluDoMoniotorwania = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wybierz które podzespoły chcesz monitorować:")
                .PageSize(9)
                .AddChoices(new[]
                {
                    "CPU",
                    "Płyta główna",
                    "Pamięć RAM",
                    "Karta graficzna",
                    "Dyski twarde",
                    "Karty sieciowe",
                    "Bateria",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wyborPodzespoluDoMoniotorwania;
        }

        public List<string> WidokKolumny()
        {
            var wyborKolumn = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("Wybierz jakie kolumny wyświetlać:")
                .PageSize(4)
                .AddChoices(new[]
                {
                    "Wartość",
                    "Min",
                    "Max",
                    "Wróć",
                })
            );
            AnsiConsole.Clear();
            return wyborKolumn;
        }

        public string WidokTemperatur()
        {
            string tytul = @"___________                                        __                       
\__    ___/___   _____ ______   ________________ _/  |_ __ ______________   
  |    |_/ __ \ /     \\____ \_/ __ \_  __ \__  \\   __\  |  \_  __ \__  \  
  |    |\  ___/|  Y Y  \  |_> >  ___/|  | \// __ \|  | |  |  /|  | \// __ \_
  |____| \___  >__|_|  /   __/ \___  >__|  (____  /__| |____/ |__|  (____  /
             \/      \/|__|        \/           \/                       \/ ";
            var tytulAplikacji = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAplikacji.Justification = Justify.Center;
            AnsiConsole.Write(tytulAplikacji);

            int szerokosc = AnsiConsole.Profile.Width;
            string[] strings = { "Stopnie Celciusza", "Stopnie Farenheita", "Wróć" };
            var centred = strings.Select(s => WysrodkujTekst(s, szerokosc)).ToList();

            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                //.Title("Temperatura")
                .PageSize(3)
                .AddChoices(new[]
                {
                    @"__  ___  __   __          ___     __   ___       __          __  __      
 /__`  |  /  \ |__) |\ | | |__     /  ` |__  |    /  ` | |  | /__`  /  /\  
 .__/  |  \__/ |    | \| | |___    \__, |___ |___ \__, | \__/ .__/ /_ /~~\ 
                                                                          ",
                    @"__  ___  __   __          ___     ___       __   ___            ___   ___      
 /__`  |  /  \ |__) |\ | | |__     |__   /\  |__) |__  |\ | |__| |__  |  |   /\  
 .__/  |  \__/ |    | \| | |___    |    /~~\ |  \ |___ | \| |  | |___ |  |  /~~\ 
                                                                                ",
                    @"     __   _/_  _/_  
 |  | |__) /  \ /   
 |/\| |  \ \__/ \__, 
                    "
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }

        public string WidokInformacjaOProgramie()
        {
            AnsiConsole.Write(new Rule("Informacje o programie").RuleStyle("green").Centered());
            Table tableInfo = new Table();
            tableInfo.Border = TableBorder.Double;
            tableInfo.AddColumn("Nazwa");
            tableInfo.AddColumn("Opis");
            tableInfo.AddRow("Nazwa programu", "CPInfo_text");
            tableInfo.AddRow("Wersja", "1.0.0.1");
            tableInfo.AddRow("Uwagi", "W przypadku niektórych płyt głównych w laptopach program nie jest w stanie pobierać informacji o płycie głównej.");

            AnsiConsole.Write(tableInfo);
            var wybor = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title(@"Kliknij ""Wróć"" jeśli chcesz wrócić do menu głównego")
               .AddChoices("Wróć")
            );

            AnsiConsole.Clear();
            return wybor;
        }

        public string WidokAktualizacjaInterwalow()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wybór aktualizacji interwałów")
                .PageSize(7)
                .AddChoices(new[]
                {
                    "250 ms",
                    "500 ms",
                    "1 s",
                    "2 s",
                    "5 s",
                    "10 s",
                    "Wróć"
                })
            ); 
            
            AnsiConsole.Clear();
            return wybor;
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

        
        public void TworzenieTabeli()
        {
            _table = new Table();
            _table.AddColumn("Urządzenie");
            _table.AddColumn("Nazwa czujnika");
            _table.AddColumn("Wartość");
            _table.AddColumn("Typ jednostki");
        }

        public void TworzenieTabeliAllData(List<string> listaKolumn)
        {
            _tableAllData = new Table();
            _tableAllData.AddColumn("Urządzenie");
            _tableAllData.AddColumn("Nazwa czujnika");
            if (listaKolumn.Contains("Wartość"))
            {
                _tableAllData.AddColumn("Wartość");
            }
            if (listaKolumn.Contains("Min"))
            {
                _tableAllData.AddColumn("MIN");
            }
            if (listaKolumn.Contains("Max"))
            {
                _tableAllData.AddColumn("MAX");
            }
            //_tableAllData.AddColumn("Typ jednostki");
        }

        /*public void InicjalizacjaTabeli(List<ISensor> czujnikiInfos, string jednostkaTemperatury)
        {
            foreach (var sensor in czujnikiInfos)
            {  
                _table.AddRow(sensor.Hardware.Name, sensor.Name, sensor.Value.GetValueOrDefault().ToString(), HelperKonwerter.KonwerterTypuNaJednostke(sensor.SensorType, jednostkaTemperatury));
            }
        }*/

        public void InicjalizacjaTabeliAllData(List<CzujnikiInfo> czujnikiInfos, List<string> listaKolumn, string jednostkaTemperatury)
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
                    //data.Add(sensor.Max?.ToString() ?? "");
                    data.Add(sensor.Max);
                }
                //data.Add(sensor.TypJednostki.ToString());
                /*data.Add(sensor.Hardware.Name);
                data.Add(sensor.Name);
                if (listaKolumn.Contains("Wartość"))
                {
                    data.Add(sensor.Value.GetValueOrDefault().ToString());
                }
                if (listaKolumn.Contains("Min"))
                {
                    data.Add(sensor.Min.GetValueOrDefault().ToString());
                }
                if (listaKolumn.Contains("Max"))
                {
                    //data.Add(sensor.Max?.ToString() ?? "");
                    data.Add(sensor.Max.GetValueOrDefault().ToString());
                }
                data.Add(HelperKonwerter.KonwerterTypuNaJednostke(sensor.SensorType, jednostkaTemperatury));
                _tableAllData.AddRow(data.ToArray());*/
                //data.Add(HelperKonwerter.KonwerterTypuNaJednostke(sensor.TypJednostki, jednostkaTemperatury));
                _tableAllData.AddRow(data.ToArray());
            }
        }

        /*public void AktualizacjaTabeli(List<ISensor> czujnikiInfos)
        {
            for (int i = 0; i < czujnikiInfos.Count; i++)
            {
                var sensor = czujnikiInfos[i];
                //_table.UpdateCell(i, 2, sensor.Wartosc.ToString());
                _table.UpdateCell(i, 2, sensor.Value.ToString());
            }

        }*/



        //tą całą metodę prawdopodobnie będę musiał przerobić i przenieść do kontrolera
        public void AktualizacjaTabeliAllData(List<CzujnikiInfo> czujnikiInfos, List<string> listaKolumn) //, string jednostkaTemperatury
        {
            for (int i = 0; i < czujnikiInfos.Count; i++)
            {
                var sensor = czujnikiInfos[i];
                int liczbaKolumn = 2;
        
                if (listaKolumn.Contains("Wartość"))
                {
                    _tableAllData.UpdateCell(i, liczbaKolumn, sensor.Wartosc.ToString());
                    liczbaKolumn++;
                }
                if (listaKolumn.Contains("Min"))
                {
                    _tableAllData.UpdateCell(i, liczbaKolumn, sensor.Min.ToString());
                    liczbaKolumn++;
                }
                if (listaKolumn.Contains("Max"))
                {
                    _tableAllData.UpdateCell(i, liczbaKolumn, sensor.Max.ToString());
                }
            }
            /*for (int i = 0; i < czujnikiInfos.Count; i++)
            {
                var sensor = czujnikiInfos[i];
                int liczbaKolumn = 2;
                if (listaKolumn.Contains("Wartość"))
                {
                    if (sensor.SensorType == SensorType.Temperature && jednostkaTemperatury.Equals("°F"))
                    {
                        float temperatura = sensor.Value.GetValueOrDefault();
                        _tableAllData.UpdateCell(i, liczbaKolumn, HelperKonwerter.KonwerterCelciuszNaFahrennheit(temperatura).ToString());
                    }
                    else
                    {
                        _tableAllData.UpdateCell(i, liczbaKolumn, sensor.Value.ToString());
                    }
                    liczbaKolumn++;
                }
                if (listaKolumn.Contains("Min"))
                {
                    if (sensor.SensorType == SensorType.Temperature && jednostkaTemperatury.Equals("°F"))
                    {
                        float temperatura = sensor.Min.GetValueOrDefault();
                        _tableAllData.UpdateCell(i, liczbaKolumn, HelperKonwerter.KonwerterCelciuszNaFahrennheit(temperatura).ToString());
                    }
                    else
                    {
                        _tableAllData.UpdateCell(i, liczbaKolumn, sensor.Min.ToString());
                    }
                    liczbaKolumn++;
                }
                if (listaKolumn.Contains("Max"))
                {
                    if (sensor.SensorType == SensorType.Temperature && jednostkaTemperatury.Equals("°F"))
                    {
                        float temperatura = sensor.Max.GetValueOrDefault();
                        _tableAllData.UpdateCell(i, liczbaKolumn, HelperKonwerter.KonwerterCelciuszNaFahrennheit(temperatura).ToString());
                    }
                    else
                    {
                        _tableAllData.UpdateCell(i, liczbaKolumn, sensor.Max.ToString());
                    }
                    
                }
                
            }*/
        }

        /*public bool WyswietlenieTabeli(Model model, int czasOdswiezania)
        {
            bool check = false;
            AnsiConsole.Live(_table)
                .Start(x =>
                {
                    AnsiConsole.Markup("[yellow]Naciśnij [bold]Q[/] aby zatrzymać wyświetlanie tabeli.[/]\n");
                    while (true)
                    {
                        model.AktualizacjaCzujnikow();
                        AktualizacjaTabeli(model.ListaCzujnikowInfo);
                        x.Refresh();
                        Thread.Sleep(czasOdswiezania);
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                        {
                            AnsiConsole.Clear();
                            check = true;
                            break;
                        }
                    }
                });
            AnsiConsole.Clear();
            return check;
        }*/

        public bool WyswietlanieTabeliAllData(Model model, int czasOdswiezania, List<string> lisaKolumn, string jednostkaTemperatury)//, string jednostkaTemperatury
        {
            /*bool check = false;
            AnsiConsole.Live(_tableAllData)
                .Start(x =>
                {
                    //to spróbować przenieść do kontrolera chociaż to jeszcze przemyśle
                    int liczbaSensorow = model.ListaCzujnikowInfo.Count;
                    if (liczbaSensorow > 0)
                    {
                        ISensor sensor = model.ListaCzujnikowInfo.First();
                        AnsiConsole.Markup($"[yellow]Naciśnij [bold]Q[/] aby zatrzymać wyświetlanie tabeli.[/]\n[red]{sensor.Hardware.Name.ToString() ?? ""}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.Markup($"[yellow]Naciśnij [bold]Q[/] aby zatrzymać wyświetlanie tabeli.[/]\n[red]Nie wykryto urządzenia[/]\n");
                    }

                    while (true)
                    {
                        model.AktualizacjaCzujnikow();
                        
                        AktualizacjaTabeliAllData(model.ListaCzujnikowInfo, lisaKolumn, jednostkaTemperatury);
                        x.Refresh();
                        Thread.Sleep(czasOdswiezania);
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                        {
                            AnsiConsole.Clear();
                            check = true;
                            break;
                        }
                    }
                });
            AnsiConsole.Clear();
            return check;*/
            bool check = false;
            AnsiConsole.Live(_tableAllData)
                .Start(x =>
                {
                    //to spróbować przenieść do kontrolera chociaż to jeszcze przemyśle
                    int liczbaSensorow = model.ListaCzujnikowInfo.Count;
                    if (liczbaSensorow > 0)
                    {
                        CzujnikiInfo sensor = model.ListaCzujnikowInfo.First();
                        AnsiConsole.Markup($"[yellow]Naciśnij [bold]Q[/] aby zatrzymać wyświetlanie tabeli.[/]\n[red]{sensor.NazwaUrzadzenia ?? ""}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.Markup($"[yellow]Naciśnij [bold]Q[/] aby zatrzymać wyświetlanie tabeli.[/]\n[red]Nie wykryto urządzenia[/]\n");
                    }

                    while (true)
                    {
                        model.AktualizacjaCzujnikow(jednostkaTemperatury);

                        AktualizacjaTabeliAllData(model.ListaCzujnikowInfo, lisaKolumn);//, jednostkaTemperatury
                        x.Refresh();
                        Thread.Sleep(czasOdswiezania);
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
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

        public void WidokCzyszczeniaDysku()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wybierz co chcesz zrobić:")
                .PageSize(3)
                .AddChoices(new[]
                {
                    "Oprróżnij kosz",
                    "Wyczyść pliki tymczasowe",
                    "Wróć"
                })
            );
        }

        public string WidokPobireaniaSpecyfikacji()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Gdzie chcesz zapisać plik tekstowy ze specyfikacją")
                .PageSize(6)
                .AddChoices(new[]
                {
                    "Pulpit",
                    "Pobrane",
                    "Dokumenty",
                    "Dysk systemowy",
                    "Inne",
                    "Wróć"
                })
            );
            return wybor;
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
            AnsiConsole.Markup($"Istaniej plik o nazwie: {sciezka}\nKliknij jakiś przycisk aby przejść dalej");
            Console.ReadKey();
        }

        public void WidokKatalogNieIstnieje(string sciezka)
        {
            AnsiConsole.Markup($"Nie istnieje taka ścieżka do katalogów {sciezka}\nKliknij jakiś przycisk aby przejść dalej");
            Console.ReadKey();
        }

        public void WidokProgressBar(string calaSciezka, SpecyfikacjaKomputera specyfikacjaKomputera)
        {
            AnsiConsole.MarkupLine("Trwa zapis danych...");
            AnsiConsole.Progress()
                .Start(x =>
                {
                    //SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();
                    var task = x.AddTask("[green]Zapisuje dane[/]");
                    specyfikacjaKomputera.ZapisDoPliku(calaSciezka, task);
                    /*using (StreamWriter streamWriter = new StreamWriter(calaSciezka))
                    {
                        SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();

                        foreach (var info in specyfikacjaKomputera.Specyfikacja)
                        {
                            streamWriter.WriteLine(info);
                            task.Increment(1);
                            Thread.Sleep(1);
                        }

                    }*/
                });
            AnsiConsole.MarkupLine("[bold green]Zapisano![/]");
            Thread.Sleep(1000);

        }
    }
}
