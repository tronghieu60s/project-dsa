using project_dsa.components;
using project_dsa.helps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace project_dsa.methods
{
    class AdminMethods
    {
        public void RenderAccount(LinkedList<TheTu> ListTheTu)
        {
            User _user = new User();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tID");
            Console.Write($"\t\t\tName");
            Console.Write($"\t\t\tSo Du");
            Console.Write($"\t\t\tLoai Tien");
            Console.WriteLine($"\tTrang Thai");
            Console.ResetColor();
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                string[] arr = _user.GetFileID(p.Value.Id);
                string status = Convert.ToBoolean(p.Value.Locked) ? "Bi Khoa" : "Su Dung";
                Console.Write($"\t{arr[0]}");
                Console.Write($"\t\t{arr[1]}");
                Console.Write($"\t\t{arr[2]}");
                Console.Write($"\t\t\t{arr[3]}");
                Console.WriteLine($"\t\t{status}");
            }
        }

        public void CreateAccount(LinkedList<TheTu> ListTheTu)
        {
            TheTu _cd = new TheTu();
            Support _sp = new Support();
            User _user = new User();

            int pin = 123456;
            User user = _user.CreateUser(ListTheTu);
            TheTu card = new TheTu(user.Id, pin, false);
            ListTheTu.AddLast(card);

            _cd.SaveFile(ListTheTu);
            _sp.Await(true, $"Tao tai khoan {user.Id} - " +
                $"{user.Name} - " +
                $"{user.Balance} {user.Currency} thanh cong!", "");
        }

        public void DeleteAccount(LinkedList<TheTu> ListTheTu)
        {
            TheTu _cd = new TheTu();
            Support _sp = new Support();
            RenderAccount(ListTheTu);

            long id; bool status = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nhap ID can xoa:\t");
            Console.ResetColor();
            long.TryParse(Console.ReadLine(), out id);

            // delete
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == id)
                {
                    status = true;
                    ListTheTu.Remove(p);
                    // delete file
                    File.Delete($"D:/{p.Value.Id}.txt");
                    File.Delete($"D:/LichSu{p.Value.Id}.txt");
                    _cd.SaveFile(ListTheTu);
                    break;
                }
            }
            _sp.Await(status, "Xoa Thanh Cong!", "Xoa That Bai!");
        }

        public void UnLockAccount(LinkedList<TheTu> ListTheTu)
        {
            TheTu _cd = new TheTu();
            Support _sp = new Support();
            RenderAccount(ListTheTu);

            long id; bool status = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nhap ID can unlock:\t");
            Console.ResetColor();
            long.TryParse(Console.ReadLine(), out id);

            // unlock
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == id)
                {
                    status = true;
                    p.Value.Locked = false;
                    _cd.SaveFile(ListTheTu);
                    break;
                }
            }
            _sp.Await(status, "Mo khoa Thanh Cong!", "Mo khoa That Bai!");
        }
    }
}
