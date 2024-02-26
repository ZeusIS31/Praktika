using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
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

    public partial class Korzina : Window
    {
        int count = 0;
        int columns = 0;
        int row = 0;
        public Korzina()
        {
            InitializeComponent();

            
                var contex = new AppDbContext();
                var q = contex.pomoika.Count(); var l = contex.pomoika.Where(x => x.Id > 0).ToList();
                int ss = l.Sum(x => Convert.ToInt32(x.kol_vo));
                var w = contex.Products.Where(x => x.Id > 0).ToList();
                
                while (count < q)
                {
                    if (columns == 5)
                    {
                        columns = 0; row += 1;
                    }

                Image image = new Image();
                string a = w[count].Kartinka.ToString(); image.Source = new BitmapImage(new Uri($"{a}", UriKind.RelativeOrAbsolute));
                TextBlock textBlock = new TextBlock(); textBlock.Text = w[count].Nazvanie;
                textBlock.TextWrapping = TextWrapping.Wrap; Button button = new Button();
                button.Content = w[count].price.ToString() + " руб."; button.Width = 150;
                button.Height = 35; button.Template = (ControlTemplate)Resources["овальная кнопка"];
                button.CommandParameter = a; button.Click += Button_Click;

                var summa = contex.pomoika.Select(x => x.Price).ToList();
                var sum = summa.Sum();
                tb.Text = "Сумма заказа: " + sum + " руб";



                Grid.SetColumn(image, columns);
                Grid.SetRow(image, row); 
                Grid.SetColumn(textBlock, columns + 1);
                Grid.SetRow(textBlock, row); 
                Grid.SetColumn(button, columns + 2);
                Grid.SetRow(button, row);


                Button plusButton = new Button();
                plusButton.Content = "+";
                plusButton.FontSize = 20;
                plusButton.Height = 40;
                plusButton.Width = 40;
                plusButton.Click += PlusButton_Click;

                Button minusButton = new Button();
                minusButton.Content = "-";
                minusButton.FontSize = 20;
                minusButton.Height = 40;
                minusButton.Width = 40;
                minusButton.Click += MinusButton_Click;




                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = w[count].price.ToString() + "руб.";
                textBlock1.TextWrapping = TextWrapping.Wrap;
                textBlock1.VerticalAlignment = VerticalAlignment.Center;
                textBlock1.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock1.FontSize = 20;



                Grid.SetRow(plusButton, row);
                Grid.SetColumn(plusButton, columns + 2);
                Grid.SetColumn(textBlock1, columns + 5);
                Grid.SetRow(textBlock1, row);
                Grid.SetRow(minusButton, row);
                Grid.SetColumn(minusButton, columns + 4);

                myGrid.Children.Add(plusButton);
                myGrid.Children.Add(minusButton);
                myGrid.Children.Add(textBlock1);
                myGrid.Children.Add(image);
                myGrid.Children.Add(textBlock); 
                myGrid.Children.Add(button);
                row++;
                count++;
                }
        }
        public void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateDisplayedQuantity()
        {
            var context = new AppDbContext();
            var l = context.pomoika.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.kol_vo));

            // Обновление отображаемого количества товара в вашем интерфейсе
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                Button button = sender as Button;
                var context = new AppDbContext();

                string par = button.CommandParameter as string;

                var q = context.Products.Where(x => x.Kartinka == par).ToList();
                var r = context.pomoika.Where(x => x.kartinka == par).ToList();

                if (r.Count > 0)
                {
                    if (q[0].Id == r[0].Id)
                    {
                        int pricee = (Convert.ToInt32(r[0].kol_vo) + 1);
                        var product = context.pomoika.FirstOrDefault(x => x.Id == r[0].Id);
                        if (product != null)
                        {
                            product.kol_vo = pricee;
                            context.SaveChanges();

                            // Обновление отображаемого количества товара
                            UpdateDisplayedQuantity();
                        }
                    }
                }
                else
                {
                    var tov = new Pomoika { Id = q[0].Id, kartinka = q[0].Kartinka, nazvanie = q[0].Nazvanie, kol_vo = 1, Price = q[0].price };
                    context.pomoika.Add(tov);
                    context.SaveChanges();

                    // Обновление отображаемого количества товара
                    UpdateDisplayedQuantity();
                }
            }
            private void bas_Click(object sender, RoutedEventArgs e)
        {
            Catalog katalog = new Catalog();
            katalog.Show(); 
            this.Close();
        }
    }
}