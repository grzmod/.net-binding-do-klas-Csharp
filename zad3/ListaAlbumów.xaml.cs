using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace zad3
{
    public partial class ListaAlbumów : Window
    {
        // Kolekcja albumów
        public ObservableCollection<Album> Albumy { get; } = new ObservableCollection<Album>();
        ListBox lista;

        public ListaAlbumów()
        {
            DataContext = this;
            InitializeComponent();
            lista = (ListBox)FindName("Lista");
        }

        // przycisk Edytuj handler
        private void Edytuj(object sender, RoutedEventArgs e)
        {
            Album wybrany = (Album)lista.SelectedItem;
            if (wybrany != null)
                new WidokAlbumu(wybrany).Show();
        }

        // przycisk Dodaj handler
        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Album nowy = new Album();
            Albumy.Add(nowy);
            new WidokAlbumu(nowy).Show();
        }

        // przycisk Usuń handler
        private void Usuń(object sender, RoutedEventArgs e)
        {
            Album wybrany = (Album)lista.SelectedItem;
            Albumy.Remove(wybrany);
        }

        // przycisk Importuj handler
        private void Importuj(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".json";
            openFileDialog.Filter = "Pliki JSON (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                var importedAlbumy = JsonConvert.DeserializeObject<List<Album>>(json);

                if (importedAlbumy != null)
                {
                    Albumy.Clear();
                    foreach (var album in importedAlbumy)
                    {
                        Albumy.Add(album);
                    }
                }
            }
        }

        // przycisk Eksportuj handler
        private void Eksportuj(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.Filter = "Pliki JSON (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(Albumy.ToList(), Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }
    }
}