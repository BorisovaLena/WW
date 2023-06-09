﻿using System;
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

namespace ПишиСтирай.windows
{
    /// <summary>
    /// Логика взаимодействия для WindowShowOrder.xaml
    /// </summary>
    public partial class WindowShowOrder : Window
    {
        User user;
        public WindowShowOrder(User user)
        {
            InitializeComponent();
            this.user = user;
            lvProduct.ItemsSource = classes.ClassBase.ProductsUser;

            List<Order> countOrders = classes.ClassBase.Base.Order.ToList(); //вывод номера заказа
            int count = 0;
            foreach(Order order in countOrders)
            {
                count = order.OrderID;
            }
            tbNumberOrder.Text = "Заказ "+(count + 1);

            CalculationSumma();

            List<PickupPoint> pickupPoints = classes.ClassBase.Base.PickupPoint.ToList(); //заполнение comboBox адресами пунктов выдачи
            cmbPickupPoint.Items.Add("Выберите пункт выдачи");
            foreach(PickupPoint pickup in pickupPoints)
            {
                cmbPickupPoint.Items.Add(pickup.City.CityName + ", " + pickup.Street.StreetName + ", " + pickup.PickupPointHouse);
            }
            cmbPickupPoint.SelectedIndex = 0;

            if(user!=null)
            {
                tbFIO.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            else
            {
                tbFIO.Text = "Гость";
            }
        }

        public void CalculationSumma() //подсчет и вывод стоимости и скидки выбранных товаров
        {
            double summaEnd = 0, discount, summaStart = 0;
            foreach (Product pr in classes.ClassBase.ProductsUser)
            {
                summaEnd += (double)pr.CostForOrder;
                summaStart += (double)pr.ProductCost;
            }
            discount = 100 - 100 * summaEnd / summaStart;
            tbDiscount.Text = "Скидка: " + discount + "%";
            tbSumma.Text = "Стоимость: " + string.Format("{0:C2}", summaEnd);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Button btn = (Button)sender;
                    string index = btn.Uid;
                    Product product = classes.ClassBase.ProductsUser.FirstOrDefault(z => z.ProductArticleNumber == index);
                    classes.ClassBase.ProductsUser.Remove(product);
                    if (classes.ClassBase.ProductsUser.Count == 0)
                    {
                        Close();
                    }
                    else
                    {
                        lvProduct.Items.Refresh();
                        CalculationSumma();
                    }
                    break;
                default:
                    break;
            }
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string index = tb.Uid;
            if (tb.Text == "0")
            {
                switch (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButton.YesNo))
                {
                    case MessageBoxResult.Yes:
                        Product product = classes.ClassBase.ProductsUser.FirstOrDefault(z => z.ProductArticleNumber == index);
                        classes.ClassBase.ProductsUser.Remove(product);
                        if(classes.ClassBase.ProductsUser.Count==0)
                        {
                            Close();
                        }
                        else
                        {
                            lvProduct.Items.Refresh();
                            CalculationSumma();
                        }
                        break;
                    default:
                        break;
                }
            }
                
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                
                bool morethree = true; //определение, выбранных продуктов больше 3 на складе или нет
                foreach (Product product in classes.ClassBase.ProductsUser)
                {
                    if (product.ProductQuantityInStock < 3)
                    {
                        morethree = false;
                    }
                }
               
                Order order = new Order(); //создание нового заказа
                order.OrderStatus = 1;
                if (morethree == false)
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                }
                else
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                }
                if(cmbPickupPoint.SelectedIndex!=0)
                {
                    order.OrderPickupPoint = cmbPickupPoint.SelectedIndex;
                    order.OrderDate = DateTime.Now;
                    if (user != null)
                    {
                        order.OrderClient = user.UserID;
                    }
                    order.OrderCode = random.Next(100, 1000); //генерация кода

                    classes.ClassBase.Base.Order.Add(order);

                    foreach (Product product in classes.ClassBase.ProductsUser) //создание новых элементов таблицы OrderProduct
                    {
                        OrderProduct orderProduct = new OrderProduct()
                        {
                            OrderID = order.OrderID,
                            ProductArticleNumber = product.ProductArticleNumber,
                            ProductCount = 1
                        };
                        classes.ClassBase.Base.OrderProduct.Add(orderProduct);
                    }
                    classes.ClassBase.Base.SaveChanges();
                    MessageBox.Show("Успешное оформление заказа!!!");
                    classes.ClassBase.ProductsUser.Clear();
                    Close();
                }
                else
                {
                    MessageBox.Show("Выберите пункт выдачи!!!");
                }                  
            }
            catch
            {
                MessageBox.Show("Ошибочка!!!");
            }
        }

        private void tbCount_PreviewTextInput(object sender, TextCompositionEventArgs e) //запрет ввода символов в textBox для количества
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
