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
using System.Globalization;

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for student_managment.xaml
    /// </summary>
    public partial class student_managment : Window
    {
        public student_managment()
        {
            InitializeComponent();
            LoadGrid();
            LoadClasses();
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
                using (SqlCommand cmd = new SqlCommand("SELECT id_etudiant,cin,nom,prenom,adresse,date_naissance,Class.libelle FROM Etudiant JOIN Class ON Etudiant.id_class = Class.id_class", con))
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
                MessageBox.Show("Entrez le CIN à rechercher: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void return_btn(object sender, RoutedEventArgs e)
        {
            Admin_page aP = new Admin_page();
            aP.Show();
            Visibility = Visibility.Collapsed;
        }

        private void logout_btn(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Visibility = Visibility.Collapsed;
        }

        private void LoadClasses()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string query = "SELECT libelle FROM Class";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string libelle = reader.GetString(0);
                    classe_in.Items.Add(libelle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des classes: {ex.Message}");
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
            adresse_in.Text = "";
            classe_in.Text = "";
            classe_in.Text = "";
            date_in.SelectedDate = null;
        }

        private void classe_in_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           /* string selectedLibelle = classe_in.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedLibelle))
            {
                MessageBox.Show($"Selected libelle: {selectedLibelle}");
            }*/
        }

        private void ajouter_btn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(cin_in.Text) ||
                string.IsNullOrEmpty(nom_in.Text) ||
                string.IsNullOrEmpty(prenom_in.Text) ||
                string.IsNullOrEmpty(adresse_in.Text) ||
                string.IsNullOrEmpty(classe_in.SelectedItem as string) ||
                string.IsNullOrEmpty(date_in.SelectedDate?.ToString()))
            {
                MessageBox.Show("Remplissez tous les champs.");
                return;
            }


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string cin = cin_in.Text;
                string querySelectEtudiant = "SELECT id_etudiant FROM Etudiant WHERE cin = @cin;";
                using (SqlCommand cmdSelectEtudiant = new SqlCommand(querySelectEtudiant, con))
                {
                    cmdSelectEtudiant.Parameters.AddWithValue("@cin", cin);

                    using (SqlDataReader readerEtudiant = cmdSelectEtudiant.ExecuteReader())
                    {

                        if (readerEtudiant.Read())
                        {
                            MessageBox.Show("Le professeur avec le CIN saisi existe déjà.");
                            return;
                        }
                    }
                }

                string querySelectClasse = "SELECT id_class FROM Class WHERE libelle = @libelle;";
                string selectedClasse = classe_in.SelectedItem as string;
                int idClass;
                using (SqlCommand cmdSelectClasse = new SqlCommand(querySelectClasse, con))
                {
                    cmdSelectClasse.Parameters.AddWithValue("@libelle", selectedClasse);

                    using (SqlDataReader readerClasse = cmdSelectClasse.ExecuteReader())
                    {
                        readerClasse.Read();
                        idClass = readerClasse.GetInt32(0);

                    }
                }


                string nom = nom_in.Text;
                string prenom = prenom_in.Text;
                string adresse = adresse_in.Text;
                DateTime DN = (DateTime)date_in.SelectedDate;
                string insertQuery = "INSERT INTO Etudiant (cin, nom, prenom, adresse, date_naissance, id_class) VALUES (@cin, @nom, @prenom, @adresse, @date_naissance, @id_class);";
                using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                {
                    cmdInsertProf.Parameters.AddWithValue("@cin", cin);
                    cmdInsertProf.Parameters.AddWithValue("@nom", nom);
                    cmdInsertProf.Parameters.AddWithValue("@prenom", prenom);
                    cmdInsertProf.Parameters.AddWithValue("@adresse", adresse);
                    cmdInsertProf.Parameters.AddWithValue("@date_naissance", DN);
                    cmdInsertProf.Parameters.AddWithValue("@id_class", idClass);
                    cmdInsertProf.ExecuteNonQuery();
                }

                LoadGrid();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'accès à la base de données: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

        }

        private void supprimer_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cin_in.Text))
            {
                MessageBox.Show("Donnez le CIN à supprimer.");
                return;
            }


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string cin = cin_in.Text;
                string querySelectEtudiant = "SELECT id_etudiant FROM Etudiant WHERE cin = @cin;";
                using (SqlCommand cmdSelectEtudiant = new SqlCommand(querySelectEtudiant, con))
                {
                    cmdSelectEtudiant.Parameters.AddWithValue("@cin", cin);

                    using (SqlDataReader readerEtudiant = cmdSelectEtudiant.ExecuteReader())
                    {

                        if (!readerEtudiant.Read())
                        {
                            MessageBox.Show("Étudiant avec le CIN fourni n'existe pas.");
                            return;
                        }
                    }
                }
                string insertQuery = "DELETE FROM Etudiant WHERE CIN = @cin;";
                using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                {
                    cmdInsertProf.Parameters.AddWithValue("@cin", cin);
                    cmdInsertProf.ExecuteNonQuery();
                }

                LoadGrid();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'accès à la base de données: {ex.Message}");
            }
            finally
            {
                con.Close();
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

                if (!string.IsNullOrEmpty(cin_in.Text) &&
                    !string.IsNullOrEmpty(nom_in.Text) &&
                    !string.IsNullOrEmpty(prenom_in.Text) &&
                    !string.IsNullOrEmpty(adresse_in.Text) &&
                    !string.IsNullOrEmpty(classe_in.SelectedItem as string) &&
                    !string.IsNullOrEmpty(date_in.SelectedDate?.ToString()))
                {
                    string cin = cin_in.Text;
                    string prenom = prenom_in.Text;
                    string nom = nom_in.Text;
                    string adresse = adresse_in.Text;
                    DateTime DN = (DateTime)date_in.SelectedDate;
                    string selectedClasse = classe_in.SelectedItem as string;

                    string querySelectProf = "SELECT cin FROM Etudiant WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("Étudiant avec le CIN fourni n'existe pas.");
                                return;
                            }
                        }
                    }

                    string querySelectClasse = "SELECT id_class FROM Class WHERE libelle = @libelle;";
                    int idClass;
                    using (SqlCommand cmdSelectClasse = new SqlCommand(querySelectClasse, con))
                    {
                        cmdSelectClasse.Parameters.AddWithValue("@libelle", selectedClasse);

                        using (SqlDataReader readerClasse = cmdSelectClasse.ExecuteReader())
                        {
                            readerClasse.Read();
                            idClass = readerClasse.GetInt32(0);

                        }
                    }

                    string insertQuery = "UPDATE Etudiant SET nom = @nom, prenom = @prenom, adresse = @adresse, date_naissance = @DN, id_class = @idClass WHERE cin = @cin;";
                    using (SqlCommand cmdInsertProf = new SqlCommand(insertQuery, con))
                    {
                        cmdInsertProf.Parameters.AddWithValue("@nom", nom);
                        cmdInsertProf.Parameters.AddWithValue("@prenom", prenom);
                        cmdInsertProf.Parameters.AddWithValue("@adresse", adresse);
                        cmdInsertProf.Parameters.AddWithValue("@DN", DN);
                        cmdInsertProf.Parameters.AddWithValue("@idClass", idClass);
                        cmdInsertProf.Parameters.AddWithValue("@cin", cin);
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

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {

                if (!string.IsNullOrEmpty(cin_in.Text))
                {
                    string cin = cin_in.Text;
                    string querySelectProf = "SELECT * FROM Etudiant WHERE cin = @cin";
                    using (SqlCommand cmdSelectProf = new SqlCommand(querySelectProf, con))
                    {
                        cmdSelectProf.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerProf = cmdSelectProf.ExecuteReader())
                        {
                            if (!readerProf.Read())
                            {
                                MessageBox.Show("Étudiant avec le CIN fourni n'existe pas.");
                                return;
                            }
                            nom_in.Text = readerProf[2].ToString();
                            prenom_in.Text = readerProf[3].ToString();
                            adresse_in.Text = readerProf[4].ToString();
                            DateTime selectedDate = (DateTime)readerProf[5];
                            date_in.SelectedDate = selectedDate;
                            date_in.DisplayDate = selectedDate;

                        }
                    }


                    string querySelectClasse = "SELECT libelle FROM Class WHERE id_class = (SELECT id_class FROM Etudiant WHERE cin = @cin);";
                    string libelleClass;

                    using (SqlCommand cmdSelectClasse = new SqlCommand(querySelectClasse, con))
                    {
                        cmdSelectClasse.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerClasse = cmdSelectClasse.ExecuteReader())
                        {
                            readerClasse.Read();
                            libelleClass = readerClasse[0].ToString();
                            classe_in.Text = libelleClass;


                        }
                    }




                    LoadGrid();
                }
                else
                {
                    MessageBox.Show("Donnez le CIN à rechercher");
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
