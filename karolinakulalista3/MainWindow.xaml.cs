using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace karolinakulalista3
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Class1> ludzie = new ObservableCollection<Class1>();
        BazaDanych bb = new BazaDanych();

        public MainWindow()
        {
            InitializeComponent();
            bb.createTable();
        }

        public ObservableCollection<Class1> Add()
        {
            try
            {
                ludzie.Add(new Class1()
                {
                    imie = imie.Text,
                    nazwisko = nazwisko.Text,
                    pesel = pesel.Text,
                    rokur = rokur.Text,
                    specjalizacja = specjalizacja.Text,
                    oddzial = oddzial.Text,
                    zdjecie = (BitmapSource)zdjecie.Source
                });
                MessageBox.Show("Dodałeś");
                return ludzie;

            }
            catch (Exception er)
            {
                MessageBox.Show("Błąd: " + er);
            }
            return ludzie;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 bWin = new Window1();
            bWin.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //zdjecie
            OpenFileDialog zdj = new OpenFileDialog();
            if (zdj.ShowDialog() == true)
            {
                Uri fileuri = new Uri(zdj.FileName);
                zdjecie.Source = new BitmapImage(fileuri);
            }
        }

        private void pesel_TextChanged(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text) || pesel.Text.Length > 10;
            if (new Regex("[^0-9]+").IsMatch(e.Text))
            {
                MessageBox.Show("Wpisz tylko cyfry");
            }
        }

        private void rokur_TextChanged(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text) || rokur.Text.Length > 3 || new Regex("19[5-9]/d|200[0-2]").IsMatch(e.Text);
            if (new Regex("199[5-9]/d|200[0-2]").IsMatch(e.Text))
            {
                MessageBox.Show("Wpisz tylko cyfry");
            }
        }

        private void imie_TextChanged(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Z]").IsMatch(e.Text);
            if (new Regex("[^a-zA-Z]").IsMatch(e.Text))
            {
                MessageBox.Show("Tylko litery");
            }
        }
    }
}

