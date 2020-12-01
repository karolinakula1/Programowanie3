using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
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

namespace karolinakulalista3
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public static ObservableCollection<Class1> ludzie = new ObservableCollection<Class1>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<Class1> Add()
        {
            try
            {
                ludzie.Add(new Class1()
                {
                    imie = imie.Text,
                    nazwisko = nazwisko.Text,
                    pesel = long.Parse(pesel.Text),
                    rokur = int.Parse(rokur.Text),
                    specjalizacja = specjalizacja.Text,
                    oddzial = oddzial.Text,
                    zdjecie = (BitmapSource)zdjecie.Source
                }) ;
                MessageBox.Show("Dodałeś");
                return ludzie;
                
            }
            catch(Exception er)
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
            if(zdj.ShowDialog() == true)
            {
                Uri fileuri = new Uri(zdj.FileName);
                zdjecie.Source = new BitmapImage(fileuri);
            }
        }
    }
}

