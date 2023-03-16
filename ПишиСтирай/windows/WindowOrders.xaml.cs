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
    /// Логика взаимодействия для WindowOrders.xaml
    /// </summary>
    public partial class WindowOrders : Window
    {
        public WindowOrders()
        {
            InitializeComponent();
            lvOrders.ItemsSource = classes.ClassBase.Base.Order.ToList();
            cmbSortDiscount.SelectedIndex = 0;
            cmbSortCount.SelectedIndex = 0;
        }

        public void Filter()
        {
            List<Order> listFilter = classes.ClassBase.Base.Order.ToList();
            switch (cmbSortDiscount.SelectedIndex)
            {
                case 1:
                    listFilter = listFilter.Where(x => x.SummaDiscountSort < 11).ToList();
                    break;
                case 2:
                    listFilter = listFilter.Where(x => x.SummaDiscountSort > 10 && x.SummaDiscountSort < 15).ToList();
                    break;
                case 3:
                    listFilter = listFilter.Where(x => x.SummaDiscountSort > 15).ToList();
                    break;
            }

            switch(cmbSortCount.SelectedIndex)
            {
                case 1:
                    listFilter.Sort((x,y)=> x.SummaOrder.CompareTo(y.SummaOrder));
                    break;
                case 2:
                    listFilter.Sort((x, y) => x.SummaOrder.CompareTo(y.SummaOrder));
                    listFilter.Reverse();
                    break;
            }
            lvOrders.ItemsSource = listFilter;
            if (listFilter.Count == 0)
            {
                MessageBox.Show("Ничего не найдено");
            }
        }

        private void cmbSortCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
