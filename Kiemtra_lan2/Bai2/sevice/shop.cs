using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Bai2.sevice
{
    class shop
    {
        public int odid ;
        public static string path;
        public static string nameFileData;
        public shop(string pat,string namedata)
        {
            path = pat;
            nameFileData = namedata;
            ReadData();
            odid = listorder.ListOrder.Count;
        }
        public Listorder listorder = new Listorder()
        {
            ListOrder = new List<Order>()
        };

       

        public void ReadData()
        {
            string fulllink = $@"{path}\{nameFileData}";
            using (StreamReader sr = File.OpenText(fulllink))
            {
                var data = sr.ReadToEnd();
                listorder = JsonConvert.DeserializeObject<Listorder>(data);
            }
        }

        public void UpdateOrder(int orderid,Product pd)
        {
            Order od = Check(orderid);
            if (od != null && od.Status==1)
            {
                od.Products.Add(pd);
            }
        }

        public void PrintBill(Order od)
        {
            string billname = $"{DateTime.Now.ToString("ddMMyyyy")}_Id{od.OrderId}";
            readwriteFile<Order>.WriteData($@"{path}\{billname}", od);
            string fulllink = $"{path}{nameFileData}";
            readwriteFile<Listorder>.WriteData(fulllink, listorder);
        }

        public void Add(Order od)
        {
            odid++;
            od.OrderId = odid;
            listorder.ListOrder.Add(od);
            string fulllink = $"{path}{nameFileData}";
            readwriteFile<Listorder>.WriteData(fulllink, listorder);
        }

        public void UpdateStatus(Order od,int stt)
        {
            od.Status = stt;
        }

        public void SearchKey(string key)
        {
            key = key.ToLower();
            bool result = false;
            foreach (Order order in listorder.ListOrder)
            {
                if (order.NameCustomer.ToLower().Contains(key)||order.Add.ToLower().Contains(key))
                {
                    Console.WriteLine(order.ToString());
                    result = true;
                }
            }
            if (!result)
            {
                Console.WriteLine("Không tìm thấy");
            }
        }

        public Order Check(int orderid)
        {
            foreach (Order order in listorder.ListOrder)
            {
                if (order.OrderId.Equals(orderid))
                {
                    return order;
                }
            }
            return null;
        }

    }
    class Listorder
    {
        public List<Order> ListOrder;
    }
}
