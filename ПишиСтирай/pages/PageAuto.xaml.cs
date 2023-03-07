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

namespace ПишиСтирай.pages
{
    /// <summary>
    /// Логика взаимодействия для PageAuto.xaml
    /// </summary>
    public partial class PageAuto : Page
    {
        public PageAuto()
        {
            InitializeComponent();
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

                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {

        }

        public void generationChapcha() //генерация капчи
        {
            Random random = new Random();
            for (int n = 0; n < 5; n++)
            {
                Line l1 = new Line()
                {
                    X1 = random.Next(201),
                    Y1 = random.Next(101),
                    X2 = random.Next(201),
                    Y2 = random.Next(101)
                };
                can.Children.Add(l1);
            }
            string str = "";
            for (int n = 0; n < 4; n++)
            {
                int i = random.Next(2);
                switch(i)
                {
                    case 0:
                        str += random.Next('a', 'z');
                        break;
                    case 1:
                        str += random.Next(10);
                        break;
                }
            }
            TextBlock tb = new TextBlock()
            {
                Text = str,
                TextDecorations = TextDecorations.Strikethrough
            };
            can.Children.Add(tb);
        }
    }
}
