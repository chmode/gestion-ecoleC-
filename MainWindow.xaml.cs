using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string connectType = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CnnectChoix(object sender, RoutedEventArgs e)
        {
            connectType = (sender as RadioButton).Content.ToString();
        }

        private void connecter_btn(object sender, RoutedEventArgs e)
        {
            if(connectType == "" || string.IsNullOrEmpty(user_tb.Text) || string.IsNullOrEmpty(pwd_tb.Password))
            {
                MessageBox.Show("Remplissez tous les champs et choisissez le type de connexion.");
            }
            else
            {
                /*string t = $"{connectType}";
                MessageBox.Show(t+user_tb.Text+pwd_tb.Password);
                Admin_page ap = new Admin_page();
                ap.Show();
                Visibility = Visibility.Collapsed;*/
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PAQPO7P;Initial Catalog=student_management;Integrated Security=True");
                DataTable dt = new DataTable();

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    if (connectType == "Admin")
                    {
                        string username = user_tb.Text;
                        string pwd = pwd_tb.Password;

                        string my_requete = "SELECT id FROM compte_admin WHERE userName = @username AND pwd = @pwd;";
                        using (SqlCommand cmd = new SqlCommand(my_requete, con))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@pwd", pwd);

                            using (SqlDataReader admin = cmd.ExecuteReader())
                            {
                                if (admin.Read())
                                {
                                    Admin_page ap = new Admin_page();
                                    ap.Show();
                                    Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                                }
                            }
                        }

                    }
                    else
                    {
                        string username = user_tb.Text;
                        string pwd = pwd_tb.Password;

                        string my_requete = "SELECT id_prof FROM compte_Professeur WHERE userName = @username AND pwd = @pwd;";
                        using (SqlCommand cmd = new SqlCommand(my_requete, con))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@pwd", pwd);

                            using (SqlDataReader prof = cmd.ExecuteReader())
                            {
                                if (prof.Read())
                                {
                                    int profId = Convert.ToInt32(prof["id_prof"]);
                                    note_managment ap = new note_managment(profId);
                                    ap.Show();
                                    Visibility = Visibility.Collapsed;
                                    //MessageBox.Show(profId.ToString());

                                }
                                else
                                {
                                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                                }
                            }
                        }
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
}
