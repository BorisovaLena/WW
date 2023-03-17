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
    /// Логика взаимодействия для PageTovar.xaml
    /// </summary>
    public partial class PageTovar : Page
    {
        List<Product> listFilter;
        int role;
        User user;
        public PageTovar(int role, User user)
        {
            InitializeComponent();
            this.role = role;
            this.user = user;
            lvTovar.ItemsSource = classes.ClassBase.Base.Product.ToList();
            cmbSortCount.SelectedIndex = 0;
            cmbSortDiscount.SelectedIndex = 0;
        }

        public PageTovar(List<Product> products, int role)
        {
            InitializeComponent();
            this.role = role;
            lvTovar.ItemsSource = classes.ClassBase.Base.Product.ToList();
            cmbSortCount.SelectedIndex = 0;
            cmbSortDiscount.SelectedIndex = 0;
            classes.ClassBase.ProductsUser = products;
        }

        private void cmbSortCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        public void Filter() //метод для фильтрации данных
        {
            listFilter = classes.ClassBase.Base.Product.ToList();
            if(!string.IsNullOrWhiteSpace(tbSearch.Text)) // поиск по названию
            {
                listFilter = listFilter.Where(z => z.TitleProduct.TitleProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            switch(cmbSortCount.SelectedIndex) // фильтрация по стоимости
            {
                case 1:
                    listFilter.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                    break;
                case 2:
                    listFilter.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                    listFilter.Reverse();
                    break;
            }
            switch(cmbSortDiscount.SelectedIndex) // фильтрация по размеру скидки
            {
                case 1:
                    listFilter = listFilter.Where(x => x.ProductDiscountAmount < 10).ToList();
                    break;
                case 2:
                    listFilter = listFilter.Where(x => x.ProductDiscountAmount > 9.99 && x.ProductDiscountAmount<15 ).ToList();
                    break;
                case 3:
                    listFilter = listFilter.Where(x => x.ProductDiscountAmount > 14.99).ToList();
                    break;
            }

            lvTovar.ItemsSource = listFilter;
            
            int countNow = listFilter.Count;
            int countProduct = classes.ClassBase.Base.Product.Count();
            tbCountData.Text = countNow + " из " + countProduct;

            if (listFilter.Count == 0)
            {
                MessageBox.Show("Ничего не найдено");
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            windows.WindowOrders windowOrders = new windows.WindowOrders();
            windowOrders.ShowDialog();
        }

        private void btnOrders_Loaded(object sender, RoutedEventArgs e)
        {
            if(role==2 || role==3)
            {
                btnOrders.Visibility = Visibility.Visible;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Вы уверены, что хотите удалить?", "", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Button btn = (Button)sender;
                    string index = btn.Uid;

                    List<OrderProduct> products = classes.ClassBase.Base.OrderProduct.Where(z => z.ProductArticleNumber == index).ToList();
                    if (products.Count == 0)
                    {
                        Product prod = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == index);
                        classes.ClassBase.Base.Product.Remove(prod);
                        classes.ClassBase.Base.SaveChanges();
                        classes.ClassFrame.mainFrame.Navigate(new pages.PageTovar(role, user));
                    }
                    else
                    {
                        MessageBox.Show("Удаление запрещено!!! ");
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnShowOrder_Click(object sender, RoutedEventArgs e)
        {
            windows.WindowShowOrder windowShowOrder = new windows.WindowShowOrder(user);
            windowShowOrder.ShowDialog();
            if(classes.ClassBase.ProductsUser.Count==0)
            {
                btnShowOrder.Visibility= Visibility.Collapsed;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string index = mi.Uid;
            Product prod = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == index);
            classes.ClassBase.ProductsUser.Add(prod);
            btnShowOrder.Visibility = Visibility.Visible;
        }

        private void btnDelete_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(role == 2 || role==3)
            {
                btn.Visibility = Visibility.Visible;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            classes.ClassFrame.mainFrame.Navigate(new pages.PageAuto());
        }
    }
}
