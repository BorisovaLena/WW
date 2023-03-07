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
        public PageTovar()
        {
            InitializeComponent();
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
            listFilter = new List<Product>();
            if(!string.IsNullOrWhiteSpace(tbSearch.Text)) // поиск
            {
                listFilter = listFilter.Where(z => z.TitleProduct.TitleProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

        }
    }
}
