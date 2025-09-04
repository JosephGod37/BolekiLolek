using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace listaZadanBolkaILolka;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
}