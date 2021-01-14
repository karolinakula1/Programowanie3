using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace karolinakulalista3
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        XmlSerializer ab;
        ObservableCollection<Class1> people;

        public Window1()
        {
            InitializeComponent();
            ab = new XmlSerializer(typeof(ObservableCollection<Class1>));
            people = new ObservableCollection<Class1>();
            DataContext = MainWindow.ludzie;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("C:\\Users\\Karolina\\Desktop\\karolinakulalista3\\XMLFile1.xml", FileMode.Create, FileAccess.Write);

                ab.Serialize(fs, MainWindow.ludzie);
                fs.Close();
                System.Windows.MessageBox.Show("Pliki zapisane");
            }
            catch (Exception er)
            {
                System.Windows.MessageBox.Show("Błąd: " + er);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("C:\\Users\\Karolina\\Desktop\\karolinakulalista3\\XMLFile1.xml", FileMode.Open, FileAccess.Read);
                ObservableCollection<Class1> wczytanie = new ObservableCollection<Class1>();
                wczytanie = (ObservableCollection<Class1>)ab.Deserialize(fs);
                MainWindow.ludzie.Clear();
                foreach (Class1 l in wczytanie)
                {
                    MainWindow.ludzie.Add(l);
                }
                fs.Close();
                System.Windows.MessageBox.Show("XML wczytany");
            }
            catch (Exception er)
            {
                System.Windows.MessageBox.Show("Błąd: " + er);
            }
        }

        private void listaosob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool mouse = System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed;
            if (mouse)
            {
                try
                {
                    Class1 class1 = (Class1)listaosob.SelectedItem;
                    string imie = class1.imie;
                    string nazwisko = class1.nazwisko;
                    string pesel = class1.pesel;
                    string rokur = class1.rokur;
                    string specjalizacja = class1.specjalizacja;
                    string oddzial = class1.oddzial;
                    ImageSource zdjecie = class1.zdjecie;

                    imie2.Text = imie;
                    nazwisko2.Text = nazwisko;
                    pesel2.Text = pesel.ToString();
                    rokur2.Text = rokur.ToString();
                    specjalizacja2.Text = specjalizacja;
                    oddzial2.Text = oddzial;
                    zdjecie2.Source = zdjecie;

                    Show();
                }

                catch
                {
                    System.Windows.MessageBox.Show("Zaznacz element");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Zaznacz");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //edytuj

            Class1 class1 = (Class1)listaosob.SelectedItem;

            if (listaosob.SelectedValue == null)
            {
                System.Windows.MessageBox.Show("Zaznacz");
            }
            else
            {
                class1.imie = imie2.Text;
                class1.nazwisko = nazwisko2.Text;
                class1.pesel = pesel2.Text;
                class1.rokur = rokur2.Text;
                class1.specjalizacja = specjalizacja2.Text;
                class1.oddzial = oddzial2.Text;
                class1.zdjecie = (BitmapSource)zdjecie2.Source;

                listaosob.ItemsSource = null;
                listaosob.ItemsSource = MainWindow.ludzie;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //zmiana zdjecia

            Microsoft.Win32.OpenFileDialog zdj1 = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = zdj1.ShowDialog();

            if (result == true)
            {
                Uri fileuri = new Uri(zdj1.FileName);
                zdjecie2.Source = new BitmapImage(fileuri);
            }
        }

        private void rokur2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //dodaj do bazy

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BazaDanych bb = new BazaDanych();
            bb.insertBase(imie2.Text, nazwisko2.Text, pesel2.Text, rokur2.Text, specjalizacja2.Text, oddzial2.Text);
        }

        private void zliczanie_1(object sender, RoutedEventArgs e)
        {
            BazaDanych bb = new BazaDanych();
            bb.zliczanie();
        }

        private void tab_1(object sender, RoutedEventArgs e)
        {
            BazaDanych bb = new BazaDanych();
            bb.tabelaryczna(people,ss.Text);
        }
    }
}
