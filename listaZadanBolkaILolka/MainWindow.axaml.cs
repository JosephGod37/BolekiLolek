using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace listaZadanBolkaILolka;

public partial class MainWindow : Window
{
    public ObservableCollection<Zadanie> Zadania { get; set; } = new ObservableCollection<Zadanie>();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        
        MyCalendar.PropertyChanged += MyCalendar_OnPropertyChanged;
    }

    private void MyComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var comboBoxValue = (MyComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "No selection";
        if (comboBoxValue == "Bolek")
        {
            var uri = new Uri("avares://listaZadanBolkaILolka/Assets/Bolek.webp");
            var asset = AssetLoader.Open(uri);
            MyImage.Source = new Bitmap(asset);

        }
        else if (comboBoxValue == "Lolek")
        {
            var uri = new Uri("avares://listaZadanBolkaILolka/Assets/Lolek.webp");
            var asset = AssetLoader.Open(uri);
            MyImage.Source = new Bitmap(asset);

        }
    }
    
    
    private DateTime _selectedDate = DateTime.Today;

    private void MyCalendar_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == Calendar.SelectedDateProperty)
        {
            
            _selectedDate = MyCalendar.SelectedDate ?? DateTime.Today;
        }
    }

    private void AddTask(object? o, RoutedEventArgs routedEventArgs)
    {
        var comboBoxValue = (MyComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "No selection";
        var nazwaZadania = MyTextBox.Text;

        string priorytet = "brak";
        if (niskiRadioButton.IsChecked == true) priorytet = "niski";
        else if (normalnyRadioButton.IsChecked == true) priorytet = "normalny";
        else if (wysokiRadioButton.IsChecked == true) priorytet = "wysoki";

        List<string> attractions = new List<string>();
        if (CheckBox1.IsChecked == true) attractions.Add("na dworze");
        if (CheckBox2.IsChecked == true) attractions.Add("potrzebny sprzet");
        if (CheckBox3.IsChecked == true) attractions.Add("z udzialem innych kolegow");

        var noweZadanie = new Zadanie
        {
            NazwaZadania = nazwaZadania,
            ComboBox = comboBoxValue,
            RadioButton = priorytet,
            CheckBox = attractions,
            Date = _selectedDate
        };

        Zadania.Add(noweZadanie);
        UpdateList();
        


    }

    private void UpdateList()
    {
        TaskList.Items.Clear();

        foreach (var zadanie in Zadania)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10,
            };
            var Opis = new TextBlock
            {
                Text = $"{zadanie.Date:dddd, dd.MM} | {zadanie.NazwaZadania} | {zadanie.ComboBox} | {zadanie.RadioButton} | {string.Join(", ", zadanie.CheckBox)}",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            var deleteButton = new Button
            {
                Content = "Usuń",
                VerticalAlignment = VerticalAlignment.Center
            };
            deleteButton.Click += (sender, args) =>
            {
                Zadania.Remove(zadanie);
                UpdateList();
            };
            stackPanel.Children.Add(Opis);
            stackPanel.Children.Add(deleteButton);
            TaskList.Items.Add(stackPanel);
        }
        
    }
    private void ShowSummary(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var summaryWindow = new SummaryWindow(_selectedDate, Zadania.ToList());
        summaryWindow.ShowDialog(this); 
    }
    public class Zadanie
    {
        public string NazwaZadania { get; set; }
        public string ComboBox { get; set; }
        public string RadioButton { get; set; }
        public List<string> CheckBox { get; set; }
        public DateTime Date { get; set; }
    }
}