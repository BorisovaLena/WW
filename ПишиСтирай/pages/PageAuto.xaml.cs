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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ПишиСтирай.pages
{
    /// <summary>
    /// Логика взаимодействия для PageAuto.xaml
    /// </summary>
    public partial class PageAuto : Page
    {
        string str="";
        DispatcherTimer dispatcher = new DispatcherTimer();
        int sec = 10;
        User user;
        public PageAuto()
        {
            InitializeComponent();
            classes.ClassBase.ProductsUser = new List<Product>();
        }

        public PageAuto(int i)
        {
            InitializeComponent();
            generationChapcha();
            spCapcha.Visibility = Visibility.Visible;
        }

        private void DisTimer_Tick(object sender, EventArgs e)
        {
            tbTimer.Text = "Вы сможете войти через " + sec + " секунд";
            sec--;
            if (sec < 0)
            {
                dispatcher.Stop();  
                tbTimer.Visibility = Visibility.Collapsed;
                tbLodin.IsEnabled = true;
                Password.IsEnabled = true;
                tbText.IsEnabled = true;
                classes.ClassFrame.mainFrame.Navigate(new pages.PageAuto(2));
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if(tbLodin.Text=="" && Password.Password=="")
            {
                MessageBox.Show("Введите все данные!!!");
            }
            else
            {
                User userAuto = classes.ClassBase.Base.User.FirstOrDefault(z => z.UserLogin == tbLodin.Text && z.UserPassword == Password.Password);
                user = userAuto;
                if(userAuto == null)
                {
                    MessageBox.Show("Вы ввели неверные данные!!!");
                    btnEnter.IsEnabled = false;
                    generationChapcha();
                    spCapcha.Visibility = Visibility.Visible;
                }
                else
                {
                    if(userAuto.UserRole == 1)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(1, userAuto));
                    }
                    else if(userAuto.UserRole == 2)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(2, userAuto));
                    }
                    else if(userAuto.UserRole == 3)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(3, userAuto));
                    }            
                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(0, user));
        }

        public void generationChapcha() //генерация капчи
        {
            str = "";
            can.Children.Clear();
            Random random = new Random();
            for (int n = 0; n < 5; n++)
            {
                SolidColorBrush Brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
                Line l1 = new Line()
                {
                    X1 = random.Next(151),
                    Y1 = random.Next(101),
                    X2 = random.Next(151),
                    Y2 = random.Next(101),
                    Stroke = Brush
                };
                can.Children.Add(l1);
            }

            int margin = 0;
            for (int n = 0; n < 4; n++)
            {
                int i = random.Next(3);
                switch(i)
                {
                    case 0:
                        char c = (char)random.Next('a', 'z');
                        TextBlock tb = new TextBlock()
                        {
                            Text = c.ToString(),
                            TextDecorations = TextDecorations.Strikethrough,
                            FontSize = 18,
                            Margin = new Thickness(margin, random.Next(65), 0, 0)
                        };
                        can.Children.Add(tb);
                        margin += 25;
                        str += c;
                        break;
                    case 1:
                        int c2 = random.Next(10);
                        TextBlock tb2 = new TextBlock()
                        {
                            Text = c2.ToString(),
                            TextDecorations = TextDecorations.Strikethrough,
                            FontSize = 18,
                            Margin = new Thickness(margin, random.Next(65), 0, 0)
                        };
                        can.Children.Add(tb2);
                        margin += 25;
                        str += c2;
                        break;
                    case 2:
                        char c1 = (char)random.Next('A', 'Z');
                        TextBlock tb3 = new TextBlock()
                        {
                            Text = c1.ToString(),
                            TextDecorations = TextDecorations.Strikethrough,
                            FontSize = 18,
                            Margin = new Thickness(margin, random.Next(65), 0, 0)
                        };
                        can.Children.Add(tb3);
                        margin += 25;
                        str += c1;
                        break;
                }
            }
            
        }

        private void tbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbText.Text.Length==4)
            {
                if(str == tbText.Text)
                {
                    btnEnter.IsEnabled = true;
                }
                else
                {
                    dispatcher.Interval = new TimeSpan(0, 0, 1);
                    dispatcher.Tick += new EventHandler(DisTimer_Tick);
                    dispatcher.Start();
                    tbTimer.Visibility = Visibility.Visible;
                    btnEnter.IsEnabled = false;
                    tbLodin.Text = "";
                    Password.Password = "";
                    tbLodin.IsEnabled = false;
                    Password.IsEnabled = false;
                    tbText.Text = "";
                    tbText.IsEnabled = false;
                }
            }
        }
    }
}
