using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ПишиСтирай
{
    public partial class Order
    {
        public string FOIClient
        {
            get
            {
                if(OrderClient!=null)
                {
                    return "Заказчик: " + OrderClient;
                }
                return null;
            }
        }

        public string NumberOrder
        {
            get
            {
                return "Заказ " + OrderID;
            }
        }

        public string OrPr
        {
            get
            {
                string str = "Состав заказа: ";
                List<OrderProduct> orderProducts = classes.ClassBase.Base.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach(OrderProduct prod in orderProducts)
                {
                    Product product = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    str+= product.TitleProduct.TitleProductName + " ("+ product.ProductArticleNumber+"), ";
                }
                str = str.Substring(0, str.Length - 2);
                return str;
            }
        }

        public string SummaOrderPrint
        {
            get
            {
                double summa = 0;
                List<OrderProduct> orderProducts = classes.ClassBase.Base.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    summa += product.CostForOrder;
                }
                return "Стоимость заказа: "+string.Format("{0:C2}", summa);
            }
        }

        public double SummaOrder
        {
            get
            {
                double summa = 0;
                List<OrderProduct> orderProducts = classes.ClassBase.Base.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    summa += product.CostForOrder;
                }
                return summa;
            }
        }

        public string SummaDiscount
        {
            get
            {
                double summa = 0;
                List<OrderProduct> orderProducts = classes.ClassBase.Base.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    summa += product.DiscountForOrder;
                }
                return "Общая скидка: " + summa+"%";
            }
        }

        public double SummaDiscountSort
        {
            get
            {
                double summa = 0;
                List<OrderProduct> orderProducts = classes.ClassBase.Base.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    summa += product.DiscountForOrder;
                }
                return summa;
            }
        }

        public SolidColorBrush ColorOrder
        {
            get
            {
                bool moreThree = false;
                bool zero = false;
                List<OrderProduct> orderProducts = classes.ClassBase.Base.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = classes.ClassBase.Base.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    if(product.ProductQuantityInStock>3)
                    {
                        moreThree = true;
                    }
                    else
                    {
                        moreThree = false;
                        if (product.ProductQuantityInStock == 0)
                        {
                            moreThree = false;
                            zero = true;
                            goto met;
                        }
                    }
                }
                met: if(moreThree == true)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#20b2aa");
                }
                else if(zero == true)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#ff8c00");
                }
                else
                {
                    return Brushes.White;
                }
            }
        }
    }
}
