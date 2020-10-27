using System;
using System.Collections.Generic;
using System.Text;

namespace Takisnmore
{
    public class PaymentMethod
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ExpirationDate { get; set; }
        public string TypeOfPayment { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
