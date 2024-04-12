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

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for Admin_page.xaml
    /// </summary>
    public partial class Admin_page : Window
    {
        public Admin_page()
        {
            InitializeComponent();
        }

        private void logout_btn(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Visibility = Visibility.Collapsed;
        }

        private void prof_managment_btn(object sender, RoutedEventArgs e)
        {
            prf_managment prfm = new prf_managment();
            prfm.Show();
            Visibility= Visibility.Collapsed;
        }

        private void students_managment_btn(object sender, RoutedEventArgs e)
        {
            student_managment stum = new student_managment();
            stum.Show();
            Visibility = Visibility.Collapsed;
        }

        private void matieres_managment_btn(object sender, RoutedEventArgs e)
        {
            matieres_managment stum = new matieres_managment();
            stum.Show();
            Visibility = Visibility.Collapsed;
        }
    }
}
