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

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for note_managment.xaml
    /// </summary>
    public partial class note_managment : Window
    {

        int professorId;
        public note_managment()
        {
            InitializeComponent();
            LoadGrid();
            LoadClasses();
        }
        public note_managment(int profid)
        {
            this.professorId = profid;
            InitializeComponent();
            LoadGrid();
            LoadClasses();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PAQPO7P;Initial Catalog=student_management;Integrated Security=True");
        public void LoadGrid()
        {
            //int professorId = 1;
            DataTable dt = new DataTable();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string my_requete = "SELECT Etudiant.cin, Matiere.libelle, Evaluation.note " +
                                    "FROM Evaluation " +
                                    "JOIN Etudiant ON Evaluation.id_etudiant = Etudiant.id_etudiant " +
                                    "JOIN Matiere ON Evaluation.id_matiere = Matiere.id_matiere " +
                                    "WHERE Matiere.id_prof = @ProfessorId";

                using (SqlCommand cmd = new SqlCommand(my_requete, con))
                {
                    cmd.Parameters.AddWithValue("@ProfessorId", professorId);

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
            note_in.Text = "";
            classe_in.Text = "";
        }

        private void classe_in_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ajouter(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cin_in.Text) || string.IsNullOrEmpty(note_in.Text))
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
                float note;
                bool note_controle = float.TryParse(note_in.Text, out note) && note >= 0 && note <= 20;

                if (note_controle)
                {
                    int id_etudiant = 0;
                    int id_matiere = 0;

                    string querySelectEtudiant = "SELECT id_etudiant FROM Etudiant WHERE cin = @cin AND NOT EXISTS (SELECT cin FROM Evaluation WHERE Etudiant.id_etudiant = Evaluation.id_etudiant)";
                    using (SqlCommand cmdSelectEtudiant = new SqlCommand(querySelectEtudiant, con))
                    {
                        cmdSelectEtudiant.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerEtudiant = cmdSelectEtudiant.ExecuteReader())
                        {
                            if (readerEtudiant.Read())
                            {
                                id_etudiant = readerEtudiant.GetInt32(0);
                            }
                            else
                            {
                                MessageBox.Show("Étudiant avec le CIN fourni introuvable ou a déjà une évaluation.");
                                return;
                            }
                        }
                    }

                    string querySelectMatiere = "SELECT id_matiere FROM Matiere WHERE id_prof = @ProfessorId";
                    using (SqlCommand cmdSelectMatiere = new SqlCommand(querySelectMatiere, con))
                    {
                        cmdSelectMatiere.Parameters.AddWithValue("@ProfessorId", professorId);

                        using (SqlDataReader readerMatiere = cmdSelectMatiere.ExecuteReader())
                        {
                            if (readerMatiere.Read())
                            {
                                id_matiere = readerMatiere.GetInt32(0);
                            }
                            else
                            {
                                MessageBox.Show("Matière non trouvée pour l'ID du professeur fourni.");
                                return;
                            }
                        }
                    }

                    string queryInsert = "INSERT INTO Evaluation (id_etudiant, id_matiere, note) VALUES (@id_etudiant, @id_matiere, @note)";
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, con))
                    {
                        cmdInsert.Parameters.AddWithValue("@id_etudiant", id_etudiant);
                        cmdInsert.Parameters.AddWithValue("@id_matiere", id_matiere);
                        cmdInsert.Parameters.AddWithValue("@note", note);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Note invalide veuillez entrer une note valide.");
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

        private void update(object sender, RoutedEventArgs e)
        {
            
            if(string.IsNullOrEmpty(cin_in.Text) || string.IsNullOrEmpty(note_in.Text))
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
                float note;
                bool note_controle = float.TryParse(note_in.Text, out note) && note >= 0 && note <= 20;

                if (note_controle)
                {
                    int id_etudiant = 0;
                    int id_matiere = 0;

                    string querySelectEtudiant = "SELECT id_etudiant FROM Etudiant WHERE cin = @cin AND id_etudiant IN (SELECT id_etudiant FROM Evaluation WHERE Etudiant.id_etudiant = Evaluation.id_etudiant)";
                    using (SqlCommand cmdSelectEtudiant = new SqlCommand(querySelectEtudiant, con))
                    {
                        cmdSelectEtudiant.Parameters.AddWithValue("@cin", cin);

                        using (SqlDataReader readerEtudiant = cmdSelectEtudiant.ExecuteReader())
                        {
                            if (readerEtudiant.Read())
                            {
                                id_etudiant = readerEtudiant.GetInt32(0);
                            }
                            else
                            {
                                MessageBox.Show("Étudiant avec le CIN fourni introuvable ou n'a pas encore d'évaluation." + id_etudiant);
                                return;
                            }
                        } 
                    }

                    string querySelectMatiere = "SELECT id_matiere FROM Matiere WHERE id_prof = @ProfessorId";
                    using (SqlCommand cmdSelectMatiere = new SqlCommand(querySelectMatiere, con))
                    {
                        cmdSelectMatiere.Parameters.AddWithValue("@ProfessorId", professorId);

                        using (SqlDataReader readerMatiere = cmdSelectMatiere.ExecuteReader())
                        {
                            if (readerMatiere.Read())
                            {
                                id_matiere = readerMatiere.GetInt32(0);
                            }
                            else
                            {
                                MessageBox.Show("Matière non trouvée pour l'ID du professeur fourni.");
                                return;
                            }
                        } // The readerMatiere will be automatically closed here
                    }

                    string queryInsert = "UPDATE Evaluation SET note = @note WHERE id_etudiant = @id_etudiant AND id_matiere = @id_matiere;";
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, con))
                    {
                        cmdInsert.Parameters.AddWithValue("@id_etudiant", id_etudiant);
                        cmdInsert.Parameters.AddWithValue("@id_matiere", id_matiere);
                        cmdInsert.Parameters.AddWithValue("@note", note);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Note invalide veuillez entrer une note valide.");
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


        private void logout_btn(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Visibility = Visibility.Collapsed;
        }

        private void filtre(object sender, RoutedEventArgs e)
        {
            //int professorId = 1;
            if(string.IsNullOrEmpty(classe_in.SelectedItem as string))
            {
                MessageBox.Show("selecter le niveau");
                return;
            }
            string libelle = classe_in.SelectedItem as string;
            DataTable dt = new DataTable();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                string my_requete = "SELECT Etudiant.cin, Matiere.libelle, Evaluation.note " +
                                    "FROM Evaluation " +
                                    "JOIN Etudiant ON Evaluation.id_etudiant = Etudiant.id_etudiant " +
                                    "JOIN Matiere ON Evaluation.id_matiere = Matiere.id_matiere " +
                                    "WHERE Matiere.id_prof = @ProfessorId AND Etudiant.id_class = (SELECT id_class FROM Class WHERE libelle = @libelle)";

                using (SqlCommand cmd = new SqlCommand(my_requete, con))
                {
                    cmd.Parameters.AddWithValue("@ProfessorId", professorId);
                    cmd.Parameters.AddWithValue("@libelle", libelle);

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

        private void reset_data_gride(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            reset();
        }
    }
}
