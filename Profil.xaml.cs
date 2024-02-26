using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace PRAKTIKA
{
    /// <summary>
    /// Логика взаимодействия для Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {
        int count = 0;
        public Profil()
        {
            InitializeComponent();

            using (var context = new AppDbContext())
            {
                var currentUser = context.Users.FirstOrDefault();

                if (currentUser != null)
                {
                    Log.Text = currentUser.Login;
                }
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Catalog window1 = new Catalog();
            window1.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();
            this.Close();
        }
    }
}
