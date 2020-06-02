using System;
using System.Collections.Generic;
using System.Text;

namespace Bai2.sevice
{
    class Order
    {
        public int OrderId { get; set; }
        public string NameCustomer { get; set; }
        public string Add { get; set; }
        public string TimeOrder { get; set; }
        public int Status { get; set; }
        public int TotalAmount => totalamount();
        public List<Product> Products = new List<Product>();
        private int totalamount()
        {
            int total = 0;
            foreach (var pd in Products)
            {
                total += pd.Amount;
            };
            return total;
        }

        public override string ToString()
        {
            string str = $"\nOrderId: {OrderId}\nCustomer: {NameCustomer}\nAddress: {Add}\n" +
                $"Time Order: {TimeOrder}\n" +
                $"Status: {((Status == 1) ? "Nhận đơn" : (Status == 2) ? "Đã thanh toán" : "Hủy đơn")}" +
                $"\nProducts:\nName\t\tPrice\tCount\tAmount";
            foreach(var pd in Products)
            {
                str+=$"\n{pd.ToString()}";
            }
            str += $"\nTotal Amount: {TotalAmount}\n\t\t----------&&&------------";
            return str;
        }
    }
}
