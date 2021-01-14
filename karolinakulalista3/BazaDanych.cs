using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace karolinakulalista3
{
    public class BazaDanych
    {

        public void connectBase()
        {
            try
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                MessageBox.Show("Connection Open  !");
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }

        }


        public void createTable()
        {
            try
            {
                string connetionString;
                string sql = @"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES Where Table_Name='uzytkownicy') Select 1 else select 0";
                string sql1 = @"Create Table uzytkownicy(imie char(50), naziwsko varchar(50), pesel char(30), rokur char(4), specjalizacja char(40), oddzial char(40))";
                connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlCommand cmd1;
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                int x = Convert.ToInt32(cmd.ExecuteScalar());

                if (x == 1)
                {

                }
                else
                {
                    cmd1 = new SqlCommand(sql1, cnn);
                    cmd1.ExecuteNonQuery();
                }

                cnn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }

        }



        public void insertBase(string imie, string nazwisko, string pesel, string rokur, string specjalizacja, string oddzial)
        {
            try
            {
                SqlCommand command;
                string connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string sql = "";
                sql = "Insert into uzytkownicy (imie,naziwsko,pesel,rokur,specjalizacja,oddzial) values(@imie,@naziwsko,@pesel,@rokur,@specjalizacja,@oddzial)";
                command = new SqlCommand(sql, cnn);
                command.Parameters.Add("@imie", imie);
                command.Parameters.Add("@naziwsko", nazwisko);
                command.Parameters.Add("@pesel", pesel);
                command.Parameters.Add("@rokur", rokur);
                command.Parameters.Add("@specjalizacja", specjalizacja);
                command.Parameters.Add("@oddzial", oddzial);
                command.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Użytkownik dodany do bazy danych");
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public void updateDataBase(string imie, string naziwsko, string pesel, string rokur, string specjalizacja, string oddzial, string biuro)
        {
            try
            {
                SqlCommand command;
                string connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string sql = "";
                sql = "Update uzytkownicy set imie = @imie, nazwisko = @nazwisko, pesel = @pesel, rokur = @rokur, specjalizacja = @specjalizacja, oddzial = @oddzial, biuro = @biuro where pesel = " + pesel + "";
                command = new SqlCommand(sql, cnn);
                command.Parameters.Add("@imie", imie);
                command.Parameters.Add("@nazwisko", naziwsko);
                command.Parameters.Add("@pesel", pesel);
                command.Parameters.Add("@rokur", rokur);
                command.Parameters.Add("@specjalizacja", specjalizacja);
                command.Parameters.Add("@oddzial", oddzial);
                command.Parameters.Add("@biuro", biuro);
                command.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Użytkownik zaktualizowany w bazie danych");
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public void loadDataBase(ObservableCollection<Class1> people)
        {
            try
            {
                SqlCommand command;
                string connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string sql = "Select * from uzytkownicy";
                command = new SqlCommand(sql, cnn);
                SqlDataAdapter adatper = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adatper.Fill(dt);
                people.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    people.Add(new Class1()
                    {
                        imie = dt.Rows[i][0].ToString(),
                        nazwisko = dt.Rows[i][1].ToString(),
                        pesel = dt.Rows[i][2].ToString(),
                        rokur = dt.Rows[i][3].ToString(),
                        specjalizacja = dt.Rows[i][4].ToString(),
                        oddzial = dt.Rows[i][5].ToString(),
                    });
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void loadBiuro(ObservableCollection<Class1> people, string biuro)
        {
            try
            {
                SqlCommand command;
                string connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string sql = "Select * from uzytkownicy where biuro=@biuro ";
                command = new SqlCommand(sql, cnn);
                command.Parameters.Add("@biuro", biuro);
                SqlDataAdapter adatper = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adatper.Fill(dt);
                people.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    people.Add(new Class1()
                    {
                        imie = dt.Rows[i][0].ToString(),
                        nazwisko = dt.Rows[i][1].ToString(),
                        pesel = dt.Rows[i][2].ToString(),
                        rokur = dt.Rows[i][3].ToString(),
                        specjalizacja = dt.Rows[i][4].ToString(),
                        oddzial = dt.Rows[i][5].ToString(),
                    });
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void zliczanie()
        {
            try
            {
                SqlCommand command;
                string connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string sql = "";
                sql = "select dbo.zliczanie()";
                command = new SqlCommand(sql, cnn);
                int x = Convert.ToInt32(command.ExecuteScalar());
                cnn.Close();
                MessageBox.Show("Liczba pracowników wynosi: " + x);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public void tabelaryczna(ObservableCollection<Class1> people, string oddzial)
        {
            try
            {
                SqlCommand command;
                string connetionString = @"Data Source=LAPTOP-NCDQS944;Initial Catalog=lista6;User ID=karolina;Password=kula";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string sql = "";
                sql = "select * from dbo.tabelaryczna(@oddzial)";
                command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@oddzial", oddzial);
                SqlDataAdapter adatper = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adatper.Fill(dt);
                MainWindow.ludzie.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MainWindow.ludzie.Add(new Class1()
                    {
                        imie = dt.Rows[i][0].ToString(),
                        nazwisko = dt.Rows[i][1].ToString(),
                        pesel = dt.Rows[i][2].ToString(),
                        rokur = dt.Rows[i][3].ToString(),
                        specjalizacja = dt.Rows[i][4].ToString(),
                        oddzial = dt.Rows[i][5].ToString(),
                    });
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
