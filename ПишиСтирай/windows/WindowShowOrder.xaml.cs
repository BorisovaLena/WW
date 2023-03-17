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
            int countOrders = classes.ClassBase.Base.Order.Count();
            tbNumberOrder.Text = "Заказ "+(countOrders + 1);
            double summaEnd = 0, discount = 0, summaStart = 0;
            foreach(Product pr in classes.ClassBase.ProductsUser)
            {
                summaEnd += (double)pr.CostForOrder;
                summaStart += (double)pr.ProductCost;
            }
            discount = 100 - 100*summaEnd/summaStart;
            tbDiscount.Text = "Скидка: " + discount + "%";
            tbSumma.Text = "Стоимость: " + string.Format("{0:C2}", summaEnd);

            List<PickupPoint> pickupPoints = classes.ClassBase.Base.PickupPoint.ToList();
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
                    lvProduct.Items.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string index = tb.Uid;       
            if (tb.Text=="0") 
            {
                Product product = classes.ClassBase.ProductsUser.FirstOrDefault(z => z.ProductArticleNumber == index);
                classes.ClassBase.ProductsUser.Remove(product);
                lvProduct.Items.Refresh();
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<PickupPoint> pickupPoints = classes.ClassBase.Base.PickupPoint.ToList(); //получаю выбранный пункт выдачи
            int PP = cmbPickupPoint.SelectedIndex;

            Random random = new Random();

            bool morethree = true;
            DateTime dateDelivery;
            foreach (Product product in classes.ClassBase.ProductsUser)
            {
                if(product.ProductQuantityInStock<3)
                {
                    morethree = false;
                }
            }
            if(morethree==false)
            {
                dateDelivery = DateTime.Now.AddDays(6);
            }
            else
            {
                dateDelivery = DateTime.Now.AddDays(3);
            }
            Order order = new Order()
            {
                OrderStatus = 1,
                OrderDeliveryDate = dateDelivery,
                OrderPickupPoint = PP,
                OrderDate = DateTime.Now,
                OrderClient = user.UserID,
                OrderCode = random.Next(100,1000),
            };

            foreach (Product product in classes.ClassBase.ProductsUser)
        }
    }
}
