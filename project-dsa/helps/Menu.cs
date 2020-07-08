using project_dsa.components;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace project_dsa.helps
{
    class Menu
    {
        User _us = new User();
        Admin _ad = new Admin();
        Support _sp = new Support();

        public int MainMenu()
        {
            int select;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                Console.Write("*");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tSIMULATOR ATM\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t*");
                Console.WriteLine("*********************************");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t1. Dang Nhap Admin");
                Console.WriteLine("\t2. Dang Nhap User");
                Console.WriteLine("\t3. Thong Tin Nhom");
                Console.WriteLine("\t4. Thoat");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                select = InputSelect();
            } while (select <= 0 || select > 4);
            return select;
        }

        public int AdminMenu()
        {
            int select;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                Console.Write("*");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tDA DN - ADMIN\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t*");
                Console.WriteLine("*********************************");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t1. Xem danh sach tai khoan");
                Console.WriteLine("\t2. Them tai khoan");
                Console.WriteLine("\t3. Xoa tai khoan");
                Console.WriteLine("\t4. Mo khoa tai khoan");
                Console.WriteLine("\t5. -> Dang Xuat");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                select = InputSelect();
            } while (select <= 0 || select > 5);
            return select;
        }

        public int UserMenu()
        {
            int select;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                Console.Write("*");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tDA DN - USER\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t*");
                Console.WriteLine("*********************************");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t1. Xem thong tin tai khoan");
                Console.WriteLine("\t2. Rut tien");
                Console.WriteLine("\t3. Chuyen Tien");
                Console.WriteLine("\t4. Xem noi dung giao dich");
                Console.WriteLine("\t5. Doi ma pin");
                Console.WriteLine("\t6. -> Dang Xuat");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                select = InputSelect();
            } while (select <= 0 || select > 6);
            return select;
        }

        public bool LoginAdminMenu(LinkedList<Admin> ListAdmin)
        {
            string user, pass;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tDANG NHAP ADMIN\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            // user
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("User:\t");
            Console.ResetColor();
            user = Console.ReadLine();
            // pass
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Pass:\t");
            Console.ResetColor();
            pass = _sp.HidePass();
            Console.WriteLine();

            return _ad.Login(ListAdmin, user, pass);
        }

        public User LoginUserMenu(LinkedList<TheTu> ListTheTu)
        {
            back:
            long id; int pin;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tDANG NHAP USER\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            // user
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID:\t");
            Console.ResetColor();
            long.TryParse(Console.ReadLine(), out id);
            // pass
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Pin:\t");
            Console.ResetColor();
            int.TryParse(_sp.HidePass(), out pin);

            bool status = _us.Login(ListTheTu, id, pin);
            if (status)
                return _us.GetFile(id);
            else goto back;
        }

        public int InputSelect()
        {
            int select;
            // select
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ban chon: ");
            Console.ResetColor();
            int.TryParse(Console.ReadLine(), out select);
            return select;
        }
    }
}
