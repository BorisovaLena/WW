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
        public PageTovar(int role)
        {
            InitializeComponent();
            this.role = role;
            lvTovar.ItemsSource = classes.ClassBase.Base.Product.ToList();
            cmbSortCount.SelectedIndex = 0;
            cmbSortDiscount.SelectedIndex = 0;
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
                case 0:
                    listFilter.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                    break;
                case 1:
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
    }
}
