using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
