using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace zad3
{
    public class Album : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private static Dictionary<string, ICollection<string>> powiązaneWłaściwości
            = new()
            {
                ["Tytuł"] = new string[] { "FormatTytułAutor" },
                ["Autor"] = new string[] { "FormatTytułAutor" },
                ["Nośnik"] = new string[] { "SkrótSzczegółów" },
                ["Długość"] = new string[] { "SkrótSzczegółów" },
                ["DataWydania"] = new string[] { "SkrótSzczegółów" }
            };

        private void NotyfikujZmianę(
            [CallerMemberName] string? nazwaWłaściwości = null,
            HashSet<string> jużZałatwione = null
            )
        {
            if (jużZałatwione == null)
                jużZałatwione = new();
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nazwaWłaściwości)
                );
            jużZałatwione.Add(nazwaWłaściwości);
            if (powiązaneWłaściwości.ContainsKey(nazwaWłaściwości))
            {
                foreach (var powiązana in powiązaneWłaściwości[nazwaWłaściwości])
                {
                    if (!jużZałatwione.Contains(powiązana))
                        NotyfikujZmianę(powiązana, jużZałatwione);
                }
            }
        }

        private string _tytuł;
        public string Tytuł
        {
            get { return _tytuł; }
            set { _tytuł = value; NotyfikujZmianę(); }
        }

        private string _autor;
        public string Autor
        {
            get { return _autor; }
            set { _autor = value; NotyfikujZmianę(); }
        }

        private string _wydawca;
        public string Wydawca
        {
            get { return _wydawca; }
            set { _wydawca = value; NotyfikujZmianę(); }
        }

        private string _nośnik;
        public string Nośnik
        {
            get { return _nośnik; }
            set { _nośnik = value; NotyfikujZmianę(); }
        }

        private string _długość;
        public string Długość
        {
            get { return _długość; }
            set { _długość = value; NotyfikujZmianę(); }
        }

        private DateTime _dataWydania;
        public DateTime DataWydania
        {
            get { return _dataWydania; }
            set { _dataWydania = value; NotyfikujZmianę(); }
        }

        public string SkrótSzczegółów =>
            $"Nośnik: {Nośnik}, Długość: {Długość}, Data Wydania: {DataWydania}";

        public string FormatTytułAutor =>
            $"{Tytuł} ({Autor})";

        public Album()
        {
        }
    }
}