using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using System.Globalization;

namespace PRAKTIKA
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        int count = 0;
        int columns = 0;
        int row = 0;
        int mm = 0;

        public Catalog()
        {
            InitializeComponent();

            var context = new AppDbContext();
            var q = context.Products.Count();
            var l = context.pomoika.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.kol_vo));
            var W = context.Products.Where(x => x.Id > 0).ToList();
            while (count < q)
            {
                if (count == 4)
                {
                    columns = 0;
                    row += 3;
                    if (row == 2)
                    {
                        break;
                    }
                }
                Image image = new Image();
                string imageUrl = W[count].Kartinka.ToString(); // Путь к изображению
                BitmapImage bitmap = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
                image.Source = bitmap;


               
                TextBlock textBlock = new TextBlock();
                textBlock.Text = W[count].Nazvanie;
                textBlock.FontSize = 20;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.TextWrapping = TextWrapping.Wrap;

                Button button = new Button();
                button.Content = W[count].price.ToString() + " руб.";
                button.Width = 150;
                button.Height = 35;
                button.FontSize = 20;
                button.VerticalAlignment = VerticalAlignment.Bottom;
                button.CommandParameter = imageUrl;
                button.Click += new RoutedEventHandler(Button2_Click);

                Button opis = new Button();
                opis.Content ="Описание";
                opis.Width = 150;
                opis.Height = 30;
                opis.FontSize = 20;
                opis.CommandParameter = imageUrl;
                opis.Click += new RoutedEventHandler(Button3_Click);

                Button Info = new Button();
                Info.Template = (ControlTemplate)Resources["кнопка"];
                Info.CommandParameter = opis;
                Info.Click += Info_Click;
                korziina.Text = ss.ToString();



               

                Grid.SetRow(image, row = 0);
                Grid.SetRow(textBlock, row = 0);
                Grid.SetRow(button, row = 0);
                Grid.SetRow(Info, row = 0);
                Grid.SetRow(opis, row = 1);
 

                Grid.SetColumn(image, columns);
                Grid.SetColumn(textBlock, columns);
                Grid.SetColumn(button, columns);
                Grid.SetColumn(opis, columns);

                Grid.SetRowSpan(Info, 2);


                myGrid.Children.Add(image);
                myGrid.Children.Add(textBlock);
                myGrid.Children.Add(button);
                myGrid.Children.Add(Info);
                myGrid.Children.Add(opis);


                columns++;
                count++;

            }
        }



        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string imageUrl = btn.CommandParameter as string;

                var context = new AppDbContext();
                var product = context.Products.FirstOrDefault(x => x.Kartinka == imageUrl);

                if (product != null)
                {
                    MessageBox.Show($"{product.Opisanie}");
                }
                
            }
        }


        public void Info_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(korziina.Text, out int currentValue))
            {

                currentValue++;
                korziina.Text = currentValue.ToString();
            }

            Button button = sender as Button; var context = new AppDbContext();
            string par = button.CommandParameter as string; var q = context.Products.Where(x => x.Kartinka == par).ToList();
            var r = context.pomoika.Where(x => x.kartinka == par).ToList(); if (r.Count > 0)
            {
                if (q[0].Id == r[0].Id)
                {
                    int cost = (Convert.ToInt32(r[0].kol_vo) + 1);
                    var h = context.pomoika.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.kol_vo = cost; return x; }); foreach (var x in h)
                    {
                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            else
            {
                var tov = new Pomoika { kartinka = q[0].Kartinka, nazvanie = q[0].Nazvanie, kol_vo = 1, Price = q[0].price };
                context.pomoika.Add(tov);
            }
            context.SaveChanges();
            var l = context.pomoika.Where(x => x.Id > 0).ToList(); int ss = l.Sum(x => Convert.ToInt32(x.kol_vo));

        }
        
        private void Button1_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Korzina window1 = new Korzina();
            window1.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profil window1 = new Profil();
            window1.Show();
            this.Close();
        }
    }
}



