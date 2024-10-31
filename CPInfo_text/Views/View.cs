using CPInfo_text.Models;
using LibreHardwareMonitor.Hardware;
using Spectre.Console;
using System;
using System.Collections.Generic;
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
        public void TytulAplikacji()
        {
            string tytul = @"\_   ___ \ \______   \|   |  ____  _/ ____\  ____         _/  |_   ____  ___  ____/  |_ 
/    \  \/  |     ___/|   | /    \ \   __\  /  _ \        \   __\_/ __ \ \  \/  /\   __\
\     \____ |    |    |   ||   |  \ |  |   (  <_> )        |  |  \  ___/  >    <  |  |  
 \______  / |____|    |___||___|  / |__|    \____/  ______ |__|   \___  >/__/\_ \ |__|  
        \/                      \/                 /_____/            \/       \/       ";
            var tytulAplikacji = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAplikacji.Justification = Justify.Center;
            AnsiConsole.Write(tytulAplikacji);
        }

        public string WidokGlowneMenu()
        {
            AnsiConsole.Clear();
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wybierz opcję:")
                .PageSize(5)
                .AddChoices(new[] {
                    "Ustawienia",
                    "Informacje o podzespołach",//dodać jescze opcję jakipodzespół ma jaki sterownik
                    "Czyszczenie dysku",
                    "Informacje",
                    "Wyjdź" 
                })
            );

            AnsiConsole.Clear();
            //System.Threading.Thread.Sleep(100);
            return wybor;
        }
        public string WidokUstawienia()
        {
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Ustawienia")
                .PageSize(6)
                .AddChoices(new[] 
                {
                    "Reset MIN/MAX",
                    "Wyświetlanie kolumny",
                    "Wybór podzespoółów do monitorowania",
                    "Jednostka temperatury",
                    "Aktualizacja interwałów",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }
        
        public List<string> WidokWyborPodzespolowDoMonitorowania()
        {
            var wyborPodzespolowDoMoniotorwania = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("Wybierz które podzespoły chcesz monitorować:")
                .PageSize(9)
                .AddChoices(new[]
                {
                    "CPU",
                    "Płyta główna",
                    "Pamięć RAM",
                    "Karta graficzna",
                    "Kontroler wentylatorów",
                    "Dyski twarde",
                    "Karty sieciowe",
                    "Bateria",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wyborPodzespolowDoMoniotorwania;
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
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Temperatura")
                .PageSize(3)
                .AddChoices(new[]
                {
                    "Stopnie Celciusza",
                    "Stopnie Farenheita",
                    "Wróć"
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
        public void InicjalizacjaTabeli(List<ISensor> czujnikiInfos)
        {
            /*foreach (var item in czujnikiInfos)
            {
                _table.AddRow(item.NazwaUrzadzenia, item.NazwaCzujnika, item.Wartosc.ToString(), item.TypJednostki);
            }*/
            foreach (var sensor in czujnikiInfos)
            {
                _table.AddRow(sensor.Name, sensor.SensorType.ToString(), sensor.Value.HasValue ? $"{sensor.Value:F1} °C" : "Brak danych");
            }
        }

        public void AktualizacjaTabeli(List<ISensor> czujnikiInfos)
        {
            for (int i = 0; i < czujnikiInfos.Count; i++)
            {
                var sensor = czujnikiInfos[i];
                //_table.UpdateCell(i, 2, sensor.Wartosc.ToString());
                _table.UpdateCell(i, 2, sensor.Value.HasValue ? $"{sensor.Value:F1} °C" : "Brak danych");
            }
            /*int i = 0;
            foreach (var czujnik in czujnikiInfos)
            {
                _table.UpdateCell(i, 2, czujnik.Wartosc.ToString());
                i++;
            }*/
        }

        /*public void WyswietlenieTabeli(Model model)
        {
            AnsiConsole.Live(_table)
                .Start(x =>
                {
                    /*model.AktualizacjaCzujnikow();
                    AktualizacjaTabeli(model.ListaCzujnikowInfo);
                    x.Refresh();
                    Thread.Sleep(1000);*/
        /*while (true) // Dodajemy pętlę, aby kontynuować odświeżanie danych
        {
            model.AktualizacjaCzujnikow();
            AktualizacjaTabeli(model.ListaCzujnikowInfo);
            x.Refresh();
            Thread.Sleep(1000);
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                return;
            }
        }
    });
}*/

        public bool WyswietlenieTabeli(Model model)
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
                        Thread.Sleep(1000);
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                        {
                            AnsiConsole.Clear();
                            check = true;
                            break;
                        }
                    }
                });
            return check;
        }


    }
}
