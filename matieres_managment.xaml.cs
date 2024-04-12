using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.DirectoryServices.ActiveDirectory;

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for matieres_managment.xaml
    /// </summary>
    public partial class matieres_managment : Window
    {
        public matieres_managment()
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
                using (SqlCommand cmd = new SqlCommand("SELECT id_matiere, libelle, coefficient, Professeur.cin FROM Matiere JOIN Professeur ON Matiere.id_prof = Professeur.id_prof;", con))
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
            libelle_in.Text = "";
            cin_in.Text = "";
            coefficient_in.Text = "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin_page aP = new Admin_page();
            aP.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
                string libelle = libelle_in.Text;
                double coefficient;
                if (!double.TryParse(coefficient_in.Text, out coefficient))
                {
                    MessageBox.Show("Entrez un coefficient correct.");
                    return;
                }
                string prof_cin = cin_in.Text;

                if (!string.IsNullOrEmpty(prof_cin) && !string.IsNullOrEmpty(libelle) && coefficient > 0)
                {
                    string querySelectProf = "SELECT cin FROM Matiere JOIN Professeur ON Professeur.id_prof = Matiere.id_prof WHERE cin = @prof_cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@prof_cin", prof_cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (readerProf.Read())
                            {
                                MessageBox.Show("Professeur non disponible.");
                                return;
                            }
                        }
                    }


                    int id_prof;
                    string querySelect_2 = "SELECT id_prof FROM Professeur WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelect_2, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", prof_cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("Le professeur avec le numéro CIN saisi n'existe pas.");
                                return;
                            }
                            id_prof = readerProf.GetInt32(0);
                        }
                    }

                    string querySelect_3 = "SELECT libelle FROM Matiere WHERE libelle = @libelle";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelect_3, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@libelle", libelle);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (readerProf.Read())
                            {
                                MessageBox.Show("La matière existe déjà.");
                                return;
                            }
                            
                        }
                    }


                    string insertQuery = "INSERT INTO Matiere (libelle, coefficient, id_prof) VALUES  (@libelle, @coefficient, @id_prof);";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@coefficient", coefficient);
                        cmdInsertProf.Parameters.AddWithValue("@id_prof", id_prof);
                        cmdInsertProf.Parameters.AddWithValue("@libelle", libelle);
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
                string libelle = libelle_in.Text;

                if (!string.IsNullOrEmpty(libelle))
                {
                    string querySelectProf = "SELECT libelle FROM Matiere WHERE libelle = @libelle";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@libelle", libelle);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("La matière choisie n'existe pas.");
                                return;
                            }
                        }
                    }

                    string insertQuery = "DELETE FROM Matiere WHERE libelle = @libelle;";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@libelle", libelle);
                        cmdInsertProf.ExecuteNonQuery();
                    }

                    LoadGrid();
                    reset();
                }
                else
                {
                    MessageBox.Show("Entrez le libellé à supprimer.");
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
                string libelle = libelle_in.Text;
                double coefficient;
                if (!double.TryParse(coefficient_in.Text, out coefficient))
                {
                    MessageBox.Show("Entrez un coefficient correct.");
                    return;
                }
                string prof_cin = cin_in.Text;

                    if (!string.IsNullOrEmpty(prof_cin) && !string.IsNullOrEmpty(libelle) && coefficient > 0)
                    {

                        string querySelect_2 = "SELECT id_prof FROM Professeur WHERE cin = @cin";
                        using (SqlCommand cmdSelectProf = new SqlCommand(querySelect_2, con))
                        {
                            cmdSelectProf.Parameters.AddWithValue("@cin", prof_cin);

                            using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                            {
                                if (!readerProf.Read())
                                {
                                    MessageBox.Show("Le professeur avec le numéro CIN saisi n'existe pas.");
                                    return;
                                }

                            }
                        }


                            int id_prof;
                            string querySelectProf = "SELECT Professeur.id_prof FROM Professeur WHERE (Professeur.cin = @prof_cin AND EXISTS (SELECT 1 FROM Matiere WHERE Matiere.id_prof = Professeur.id_prof AND libelle = @libelle)) OR (Professeur.cin = @prof_cin AND Professeur.cin NOT IN (SELECT cin FROM Professeur JOIN Matiere ON Professeur.id_prof = Matiere.id_prof));";
                            using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                            {
                                cmdSelectProf.Parameters.AddWithValue("@prof_cin", prof_cin);
                                cmdSelectProf.Parameters.AddWithValue("@libelle", libelle);

                                using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                                {
                                    if (!readerProf.Read())
                                    {
                                        MessageBox.Show("Professeur non disponible.");
                                        return;
                                    }
                                   else
                                   {
                                        string insertQuery = "UPDATE Matiere SET coefficient = @coefficient, id_prof = @id_prof  WHERE libelle = @libelle;";
                                        id_prof = readerProf.GetInt32(0);
                                        readerProf.Close();
                                        using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                                        {
                                            cmdInsertProf.Parameters.AddWithValue("@coefficient", coefficient);
                                            cmdInsertProf.Parameters.AddWithValue("@id_prof", id_prof);
                                            cmdInsertProf.Parameters.AddWithValue("@libelle", libelle);
                                            cmdInsertProf.ExecuteNonQuery();
                                        }

                                        LoadGrid();
                                        reset();
                            }
                                }
                            }
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

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string libelle = libelle_in.Text;

                if (!string.IsNullOrEmpty(libelle))
                {
                    string querySelectProf = "SELECT libelle FROM Matiere WHERE libelle = @libelle";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@libelle", libelle);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("La matière choisie n'existe pas.");
                                return;
                            }
                        }
                    }

                    string insertQuery = "SELECT libelle, coefficient, Professeur.cin FROM Matiere JOIN Professeur ON Matiere.id_prof = Professeur.id_prof WHERE libelle = @libelle;";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@libelle", libelle);
                        using (SqlDataReader readerProf = cmdInsertProf.ExecuteReader())
                        {
                            readerProf.Read();
                            libelle_in.Text = readerProf.GetString(0);
                            coefficient_in.Text = readerProf.GetDouble(1).ToString();
                            cin_in.Text = readerProf.GetString(2);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Entrez le libellé à rechercher.");
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
