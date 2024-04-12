using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;
using System.Data;

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for prf_managment.xaml
    /// </summary>
    public partial class prf_managment : Window
    {
        public prf_managment()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PAQPO7P;Initial Catalog=student_management;Integrated Security=True");
        public void LoadGrid()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Professeur", con))
                {
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        dt.Load(sdr);
                    }
                }

                datagrid.ItemsSource = dt.DefaultView;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur d'accès à la base de données: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void reset()
        {
            cin_in.Text = "";
            prenom_in.Text = "";
            nom_in.Text = "";
        }



        private void return_btn(object sender, RoutedEventArgs e)
        {
            Admin_page aP = new Admin_page();
            aP.Show();
            Visibility= Visibility.Collapsed;
        }

        private void logout_btn(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Visibility = Visibility.Collapsed;
        }


        private void ajouter_btn_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string cin = cin_in.Text; 
                string prenom = prenom_in.Text; 
                string nom = nom_in.Text; 

                if (!string.IsNullOrEmpty(nom) && !string.IsNullOrEmpty(prenom) && !string.IsNullOrEmpty(cin))
                {
                    string querySelectProf = "SELECT cin FROM Professeur WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (readerProf.Read())
                            {
                                MessageBox.Show("Le professeur avec le CIN saisi existe déjà.");
                                return;
                            }
                        }
                    }

                    string insertQuery = "INSERT INTO Professeur (cin, nom, prenom) VALUES (@cin, @nom, @prenom);";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@cin", cin);
                        cmdInsertProf.Parameters.AddWithValue("@prenom", prenom);
                        cmdInsertProf.Parameters.AddWithValue("@nom", nom);
                        cmdInsertProf.ExecuteNonQuery();
                    }

                    LoadGrid();
                    reset();
                }
                else
                {
                    MessageBox.Show("Remplissez tous les champs.");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur d'accès à la base de données: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string cin = cin_in.Text;
                string prenom = prenom_in.Text;
                string nom = nom_in.Text;

                if (!string.IsNullOrEmpty(nom) && !string.IsNullOrEmpty(prenom) && !string.IsNullOrEmpty(cin))
                {
                    string querySelectProf = "SELECT cin FROM Professeur WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("Le professeur avec le CIN saisi n'existe pas.");
                                return;
                            }
                        }
                    }

                    string insertQuery = "UPDATE Professeur SET nom = @nom, prenom = @prenom WHERE cin = @cin;";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@cin", cin);
                        cmdInsertProf.Parameters.AddWithValue("@prenom", prenom);
                        cmdInsertProf.Parameters.AddWithValue("@nom", nom);
                        cmdInsertProf.ExecuteNonQuery();
                    }

                    LoadGrid();
                    reset();
                }
                else
                {
                    MessageBox.Show("Remplissez tous les champs.");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur d'accès à la base de données: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void supprimer_btn_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string cin = cin_in.Text;

                if (!string.IsNullOrEmpty(cin))
                {
                    string querySelectProf = "SELECT cin FROM Professeur WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("Le professeur avec le CIN saisi n'existe pas");
                                return;
                            }
                        }
                    }

                    string insertQuery = "DELETE FROM Professeur WHERE cin = @cin;";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@cin", cin);
                        cmdInsertProf.ExecuteNonQuery();
                    }

                    LoadGrid();
                    reset();
                }
                else
                {
                    MessageBox.Show("Entrez le CIN à supprimer.");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur d'accès à la base de données: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string cin = cin_in.Text;

                if (!string.IsNullOrEmpty(cin))
                {
                    string querySelectProf = "SELECT cin FROM Professeur WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("Le professeur avec le CIN saisi n'existe pas");
                                return;
                            }
                        }
                    }

                    string insertQuery = "SELECT nom, prenom FROM Professeur WHERE cin = @cin;";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@cin", cin);
                        using (SqlDataReader readerProf = cmdInsertProf.ExecuteReader())
                        {
                            readerProf.Read();
                            nom_in.Text = readerProf.GetString(0);
                            prenom_in.Text = readerProf.GetString(1);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Entrez le CIN à rechercher.");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur d'accès à la base de données: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
