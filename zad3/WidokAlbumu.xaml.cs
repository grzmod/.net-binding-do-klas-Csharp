using System.Windows;

namespace zad3
{
    public partial class WidokAlbumu : Window
    {
        // Album do edycji
        public Album Album { get; set; }

        public WidokAlbumu(Album album)
        {
            Album = album;
            DataContext = Album;
            InitializeComponent();
        }

        // przycisk Dodaj handler
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            // Zamyka okno po dodaniu albumu
            Close();
        }
    }
}