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
        List<Product> products;
        public WindowShowOrder(List<Product> products)
        {
            InitializeComponent();
            lvProduct.ItemsSource = products;
            this.products = products;
            int countOrders = classes.ClassBase.Base.Order.Count();
            tbNumberOrder.Text = "Заказ "+(countOrders + 1);
            double summaEnd = 0, discount = 0, summaStart = 0;
            foreach(Product pr in products)
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
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Button btn = (Button)sender;
                    string index = btn.Uid;
                    Product product = products.FirstOrDefault(z => z.ProductArticleNumber == index);
                    products.Remove(product);
                    //обновить
                    break;
                default:
                    break;
            }
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //при 0 удалить продукт
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
