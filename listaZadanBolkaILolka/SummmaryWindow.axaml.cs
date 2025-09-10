using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace listaZadanBolkaILolka
{
    public partial class SummaryWindow : Window
    {
        public SummaryWindow(DateTime selectedDate, List<MainWindow.Zadanie> wszystkieZadania)
        {
            InitializeComponent();

            SummaryTitle.Text = $"Zadania na {selectedDate:dddd, dd.MM}";
            
            var filtered = wszystkieZadania
                .Where(z => z.Date.Date == selectedDate.Date)
                .ToList();

            foreach (var zadanie in filtered)
            {
                SummaryList.Items.Add(
                    $"{zadanie.NazwaZadania} | {zadanie.ComboBox} | {zadanie.RadioButton} | {string.Join(", ", zadanie.CheckBox)}"
                );
            }
        }
    }
}