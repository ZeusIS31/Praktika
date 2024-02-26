using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PRAKTIKA
{
    /// <summary>
    /// Логика взаимодействия для Registracia.xaml
    /// </summary>
    public partial class Registracia : Window
    {
        public Registracia()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var log = Login1.Text;
            var pas = Parol1.Text;
            var qqq = new AppDbContext();
            bool c = false;
            bool c2 = false;
            bool c3 = false;

            if (log != "Введите почту")
            {
                if (log.Contains("@"))
                {
                    string[] words = log.Split("@");

                    if (Regex.IsMatch(words[0], "^[a-zA-Z0-9]*$"))
                    {
                        c3 = true;
                    }
                    else
                    {
                        Login1.BorderBrush = new SolidColorBrush(Colors.Red);
                        oshibka2.Text += " Почта содержит только буквы английского алфавита и цифры ";
                    }
                    if (words[1].Contains("."))
                    {
                        string[] latters = words[1].Split(".");
                        if (latters[1].Length >= 2)
                        {
                            if (c3 == true)
                            {
                                var userAdd1 = qqq.Users.SingleOrDefault(x => x.Login == Login1.Text);
                                if (userAdd1 == null)
                                {
                                    c = true;
                                }
                                else
                                {
                                    Login1.BorderBrush = new SolidColorBrush(Colors.Red);
                                    oshibka2.Text += "\n Почта уже зарегистрирована";
                                }
                            }
                        }
                        else
                        {
                            Login1.BorderBrush = new SolidColorBrush(Colors.Red);
                            oshibka2.Text += "\n Почта не содерижит .com, .ru, .gmail";
                        }
                    }
                    else
                    {
                        Login1.BorderBrush = new SolidColorBrush(Colors.Red);
                        oshibka2.Text += "\n Почта не содерижит .com, .ru, .gmail";
                    }
                }
                else
                {
                    Login1.BorderBrush = new SolidColorBrush(Colors.Red);
                    oshibka2.Text += "\n Почта не содерижит '@' ";
                }
            }
            foreach (var symbols in pas)
            {
                if (char.IsDigit(symbols))
                {
                    c2 = true;
                    break;
                }
            }
            if (pas != "Введите пароль")
            {
                if (pas.Length >= 6)
                {
                    if (c2 == true)
                    {
                        if (pas.Contains("!") || pas.Contains("@") || pas.Contains("#") || pas.Contains("$") || pas.Contains("*") || pas.Contains("&") || pas.Contains("%"))
                        {
                            var userAdd = qqq.Users.SingleOrDefault(x => x.Login == Login1.Text);
                            if (userAdd is null && c == true)
                            {
                                var user = new User { Login = log, Password = pas, };
                                qqq.Users.Add(user);
                                qqq.SaveChanges();
                                MessageBox.Show("Вы успешно зарегистрировались!");
                                this.Hide();
                                MainWindow mainWindow2 = new MainWindow();
                                mainWindow2.Show();
                            }
                            else
                            {
                                Login1.BorderBrush = new SolidColorBrush(Colors.Red);
                                oshibka2.Text += "\n Такой пользователь уже зарегиcтрирован";
                            }
                        }
                        else
                        {
                            Parol1.BorderBrush = new SolidColorBrush(Colors.Red);

                            oshibka2.Text += "\n Пароль должен содержать специальные символы !,@,#,$,*,&,%";
                        }
                    }
                    else
                    {
                        Parol1.BorderBrush = new SolidColorBrush(Colors.Red);

                        oshibka2.Text += "\n Пароль должен содержать цирфы";
                    }
                }
                else
                {
                    Parol1.BorderBrush = new SolidColorBrush(Colors.Red);

                    oshibka2.Text += "\n Пароль должен содержать 6 или более символов";
                }
            }

            else
            {
                Parol1.BorderBrush = new SolidColorBrush(Colors.Red);

                oshibka2.Text += "\n Пароли не одинаковые";
            }
        }
    }
}
    
