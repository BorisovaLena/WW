using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ПишиСтирай
{
    public partial class Product
    {
        public string ManufacturerString
        {
            get
            {
                return "Производитель: " + Manufacturer.ManufacturerName;
            }
        }

        public string CostOld
        {
            get
            {
                return string.Format("{0:C2}", ProductCost);
            }
        }

        public string CostNew
        {
            get
            {
                if(ProductDiscountAmount>0)
                {
                    double cost = (double)((double)ProductCost - (double)ProductCost * (ProductDiscountAmount / 100));
                    return " " + string.Format("{0:C2}", cost);
                }
                else
                {
                    return "";
                }
            }
        }

        public double CostForOrder
        {
            get
            {

                if (ProductDiscountAmount > 0)
                {
                    double cost = (double)((double)ProductCost - (double)ProductCost * (ProductDiscountAmount / 100));
                    return cost;
                }
                else
                {
                    return (double)ProductCost;
                }
            }
        }

        public string Discount
        {
            get
            {
                return ProductDiscountAmount + "%";
            }
        }

        public string Photo
        {
            get
            {
                if(ProductPhoto != null)
                {
                    return "\\pictures\\" + ProductPhoto;
                }
                else
                {
                    return null;
                }
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                if(ProductDiscountAmount>15)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#7fff00"); 
                }
                else
                {
                    return Brushes.White;
                }
            }
        } 
    }
}
