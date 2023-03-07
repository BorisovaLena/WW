using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПишиСтирай
{
    public partial class Product
    {
        public string ManufacturerString
        {
            get
            {
                return "Производитель: " + ProductManufacturer;
            }
        }

        public string Cost
        {
            get
            {
                return "Цена: "+ ProductCost;
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
                return "\\pictures\\" + ProductPhoto;
            }
        }
    }
}
