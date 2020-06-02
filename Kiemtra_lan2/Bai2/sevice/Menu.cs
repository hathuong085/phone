using System;
using System.Collections.Generic;
using System.Text;

namespace Bai2.sevice
{
    static class Menu
    {
        public static void CreateMenu()
        {

            int option = 0;
            do
            {
                Console.WriteLine("\nChọn từ 1 đến 5:");
                Console.WriteLine("1. Tất cả đơn hàng");
                Console.WriteLine("2. Tìm kiếm đơn hàng");
                Console.WriteLine("3. Tìm kiếm khách hàng");
                Console.WriteLine("4. Thêm đơn hàng");
                Console.WriteLine("5. Thoát");

                Console.Write("Chọn: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    option = number;
                }
                if (option > 5 || option < 1)
                {
                    Console.Clear();
                }
            }
            while (option > 5 || option < 1);

            Process(option);
        }

        public static void Process(int opt)
        {
            Console.Clear();
            switch (opt)
            {
                case 1:
                    {
                        show();
                        break;
                    }
                case 2:
                    {
                        searchorder();
                        break;
                    }
                case 3:
                    {
                        searchkey();
                        break;
                    }
                case 4:
                    {
                        newoder();
                        break;
                    }
                case 5:
                    {
                        Environment.Exit(Environment.ExitCode);
                        break;
                    }
            }
            CreateMenu();
        }

        public static void CreateMenu2(Order od)
        {

            int option = 0;
            do
            {
                Console.WriteLine("\nChọn từ 1 đến 4:");
                Console.WriteLine("1. Thêm hàng");
                Console.WriteLine("2. Chuyển trạng thái");
                Console.WriteLine("3. Thanh thoán");
                Console.WriteLine("4. Quay lại");

                Console.Write("Chọn: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    option = number;
                }
                if (option > 4 || option < 1)
                {
                    Console.Clear();
                    Console.WriteLine(od.ToString());
                };
            }
            while (option > 4 || option < 1);

            Process2(option, od);
        }

        public static void Process2(int opt, Order od)
        {
            Console.Clear();
            switch (opt)
            {
                case 1:
                    {
                        UpdateProduct(od);
                        break;
                    }
                case 2:
                    {
                        UpdateStatus(od);
                        break;
                    }
                case 3:
                    {
                        Pay(od);
                        break;
                    }
                case 4:
                    {
                        CreateMenu();
                        break;
                    }
            }
            CreateMenu2(od);
        }

        private static void UpdateProduct(Order od)
        {
            if (od.Status == 1)
            {
                do
                {
                    od.Products.Add(newproduct());
                    int ind = Shop.listorder.ListOrder.IndexOf(od);
                    Shop.listorder.ListOrder[ind].Products = od.Products;
                    string fulllink = $"{path}{nameFile}";
                    readwriteFile<Listorder>.WriteData(fulllink, Shop.listorder);
                    Console.Clear();
                    Console.WriteLine(od.ToString());
                    Console.WriteLine("Thêm thành công!");
                    Console.Write("Bạn có muốn thêm sản phẩm nữa hay không? y/n:  ");
                } while (Console.ReadLine().ToLower() == "y");
            }
            else
            {
                Console.WriteLine(od.ToString());
                Console.WriteLine("Đơn hàng đã được thanh toán hoặc đã bị hủy!");
            }

        }

        private static void UpdateStatus(Order od)
        {
            try
            {
                Console.Write("1.Nhận đơn\n2.Đã thanh toán\n3.Hủy đơn\nChọn: ");
                int stt = int.Parse(Console.ReadLine());
                int ind = Shop.listorder.ListOrder.IndexOf(od);
                od.Status = (stt == 1) ? 1 : (stt == 2) ? 2 : 3;
                Shop.listorder.ListOrder[ind].Status = od.Status;
                string fulllink = $"{path}{nameFile}";
                readwriteFile<Listorder>.WriteData(fulllink, Shop.listorder);
                Console.Clear();
                Console.WriteLine(od.ToString());
                Console.WriteLine("Cập nhật thành công!");
            }
            catch (Exception)
            {
                Console.WriteLine("Lựa chọn là 1 con số.Vui lòng nhập lại!");
                Console.WriteLine(od.ToString());
                UpdateStatus(od);
            }
        }
        private static void Pay(Order od)
        {
            if (od.Status == 1)
            {
                od.Status = 2;
                Shop.PrintBill(od);
                Console.Clear();
                Console.WriteLine(od.ToString());
                Console.WriteLine("Thanh toán thành công!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine(od.ToString());
                Console.WriteLine("Đơn đã bị hủy hoặc đã được thanh toán!");
            }
            
        }


        public static string path = $@"C:\Users\Admin\source\repos\Kiemtra_lan2\Bai2\Data\";
        public static string nameFile = "data.json";
        public static shop Shop = new shop(path, nameFile);

        private static void show()
        {
            foreach (Order od in Shop.listorder.ListOrder)
            {
                Console.WriteLine(od.ToString());
            }
        }

        private static void searchorder()
        {
            try
            {
                Console.Write("Nhập mã đơn hàng:  ");
                int id = int.Parse(Console.ReadLine());
                Order od = Shop.Check(id);
                if (od != null)
                {
                    Console.WriteLine(od.ToString());
                    CreateMenu2(od);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy đơn!");
                }
            } catch(Exception)
            {
                Console.Clear();
                Console.WriteLine("Mã đơn hàng là một số nguyên!Vui lòng nhập lại.");
                searchorder();
            }
        }

        private static void searchkey()
        {
            Console.Write("Nhập tên hoặc địa chỉ khách hàng: ");
            Shop.SearchKey(Console.ReadLine());
        }

        private static void newoder()
        {

            Order od = new Order();
            Console.Write("Tên khách hàng: ");
            od.NameCustomer = Console.ReadLine();
            Console.Write("Địa chỉ: ");
            od.Add = Console.ReadLine();
            od.Status = 1;
            string pos = "n";
            do
            {
                od.Products.Add(newproduct());
                Console.Write("Bạn có muốn thêm một mặt hàng vào đơn? y/n:  ");
                pos = Console.ReadLine().ToLower();
            } while (pos.Equals("y"));

            od.TimeOrder = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            Shop.Add(od);
            Console.WriteLine("Thêm thành công");
        }
        public static Product newproduct()
        {
            Product pd = new Product();
            Console.Write("Tên hàng: ");
            pd.name = Console.ReadLine();
            Console.Write("Giá: ");
            pd.price = int.Parse(Console.ReadLine());
            Console.Write("Số lượng: ");
            pd.count = int.Parse(Console.ReadLine());
            return pd;
        }
    }
}
