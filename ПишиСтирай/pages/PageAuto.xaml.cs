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
        string str = "";
        DispatcherTimer dispatcher = new DispatcherTimer();
        int sec = 10;
        public PageAuto()
        {
            InitializeComponent();
        }

        public PageAuto(int i)
        {
            InitializeComponent();
            generationChapcha();
            spCapcha.Visibility = Visibility.Visible;
            btnEnter.IsEnabled = false;
            dispatcher.Interval = new TimeSpan(0, 0, 1);
            dispatcher.Tick += new EventHandler(DisTimer_Tick);
            dispatcher.Start();
            tbTimer.Visibility = Visibility.Visible;
        }

        private void DisTimer_Tick(object sender, EventArgs e)
        {
            tbTimer.Text = "Вы сможете войти через " + sec + " секунд";
            sec--;
            if (sec < 0)
            {
                dispatcher.Stop();
                btnEnter.IsEnabled = true;
                tbTimer.Visibility = Visibility.Collapsed;
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
                if(userAuto == null)
                {
                    MessageBox.Show("Вы ввели неверные данные!!!");
                    generationChapcha();
                    spCapcha.Visibility = Visibility.Visible;
                }
                else
                {
                    if(userAuto.UserRole == 1)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(1));
                    }
                    else if(userAuto.UserRole == 2)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(2));
                    }
                    else if(userAuto.UserRole == 3)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(3));
                    }            
                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(0));
        }

        public void generationChapcha() //генерация капчи
        {
            Random random = new Random();
            for (int n = 0; n < 5; n++)
            {
                Line l1 = new Line()
                {
                    X1 = random.Next(101),
                    Y1 = random.Next(51),
                    X2 = random.Next(101),
                    Y2 = random.Next(51)
                };
                can.Children.Add(l1);
            }
            
            for (int n = 0; n < 4; n++)
            {
                int i = random.Next(2);
                switch(i)
                {
                    case 0:
                        str += (char)random.Next('a', 'z');
                        break;
                    case 1:
                        str += random.Next(10);
                        break;
                }
            }
            TextBlock tb = new TextBlock()
            {
                Text = str,
                TextDecorations = TextDecorations.Strikethrough,
                FontSize = 18,
                Margin = new Thickness(10)
            };
            can.Children.Add(tb);
        }

        private void tbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbText.Text.Length==4)
            {
                if(str == tbText.Text)
                {
                    User userAuto = classes.ClassBase.Base.User.FirstOrDefault(z => z.UserLogin == tbLodin.Text && z.UserPassword == Password.Password);
                    if (userAuto == null)
                    {
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageAuto(2));
                    }
                    else
                    {
                        if (userAuto.UserRole == 1)
                        {
                            classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(1));
                        }
                        else if (userAuto.UserRole == 2)
                        {
                            classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(2));
                        }
                        else if (userAuto.UserRole == 3)
                        {
                            classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(3));
                        }
                    }
                }
                else
                {
                    classes.ClassFrame.mainFrame.Navigate(new pages.PageAuto(2));
                }
            }
        }
    }
}
