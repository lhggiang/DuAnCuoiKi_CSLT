using Duancuoiki;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
class Program
{
    static void Main()
    {
        //nhập và in ra được chữ tiếng Việt
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.Unicode;
        //khởi tạo đối tượng QuanLyNhanVien
        QuanLyNhanVien quanLyNhanVien = new QuanLyNhanVien();
        //dùng mảng 2 chiều để in ra số thứ tự của menu
        int[,] A =
            {
                {0, 1, 2, 3 },
                {4, 5, 6, 7 },
                {8, 9, 10, 11 },
            };
        //dùng vòng lặp while (true) lặp đến khi nào người dùng nhấn phím 0 thì sẽ thoát chương trình
        while (true)
        {
            Console.WriteLine("\n***********CHƯƠNG TRÌNH QUẢN LÝ NHÂN VIÊN C#***********");
            Console.WriteLine("*******************************************************");
            Console.WriteLine($"**  {A[0,1]}. Thêm nhân viên.                               **"); //1
            Console.WriteLine($"**  {A[0,2]}. Cập nhật thông tin nhân viên theo ID.         **"); //2
            Console.WriteLine($"**  {A[0,3]}. Xóa nhân viên theo ID.                        **"); //3
            //chức năng tìm kiếm
            Console.WriteLine($"**  {A[1,0]}. Tìm kiếm nhân viên theo ID.                   **"); //4
            Console.WriteLine($"**  {A[1,1]}. Tìm kiếm nhân viên theo tên.                  **"); //5
            Console.WriteLine($"**  {A[1,2]}. Tìm kiếm nhân viên có ngày sinh bé nhất.      **"); //6
            //chức năng sắp xếp
            Console.WriteLine($"**  {A[1,3]}. Sắp xếp nhân viên theo ID.                    **"); //7
            Console.WriteLine($"**  {A[2,0]}. Sắp xếp nhân viên theo tên.                   **"); //8
            Console.WriteLine($"**  {A[2,1]}. In ra tổng số lượng nhân viên.                **"); //9
            Console.WriteLine($"** {A[2,2]}. Hiển thị danh sách nhân viên.                 **"); //10
            Console.WriteLine($"** {A[2,3]}. Tính lương tháng nhân viên theo ID            **"); //11
            //thoát chương trình
            Console.WriteLine($"**  {A[0,0]}. Thoát chương trình.                           **"); //0
            //dùng vòng for lồng nhau để in ra dấu *
            for (int i = 1; i <= 2; i++) 
            {
                for (int j = 1; j <= 55; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.Write("Nhập tùy chọn: ");
            int luachon;
            //xử lý tryparse
            while (int.TryParse(Console.ReadLine(), out luachon) == false)
            {
                Console.Write("\nVui lòng nhập lại lựa chọn: ");
            }
            //dùng switch case để chọn các lựa chọn nhập vào
            switch (luachon)
            {
                case 1:
                    Console.WriteLine("\n1. Thêm nhân viên.");
                    quanLyNhanVien.NhapNhanVien();
                    Console.WriteLine("\nThêm nhân viên thành công!");
                    break;
                case 2: 
                    //check xem danh sách có nhân viên hay không
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        int id;
                        Console.WriteLine("\n2. Cập nhật thông tin nhân viên theo ID.");
                        Console.Write("\nNhập ID để cập nhật thông tin: ");
                        //xử lý tryparse
                        while (int.TryParse(Console.ReadLine(), out id) == false)
                        {
                            Console.Write("\nVui lòng nhập lại ID để cập nhật thông tin: ");
                        }
                        quanLyNhanVien.SuaThongTinNhanVien(id);
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 3: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        int id;
                        Console.WriteLine("\n3. Xóa nhân viên.");
                        Console.Write("\nNhập ID cần xóa. Nếu nhập -1 mặc định xóa hết nhân viên: ");
                        while (int.TryParse(Console.ReadLine(), out id) == false)
                        {
                            Console.Write("\nVui lòng nhập lại ID để xóa. Nếu nhập -1 mặc định xóa hết nhân viên: ");
                        }
                        quanLyNhanVien.XoaNhanVien(id);
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 4: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        int id;
                        Console.WriteLine("\n4. Tìm kiếm nhân viên theo ID.");
                        Console.Write("\nNhập ID để tìm kiếm: ");
                        while (int.TryParse(Console.ReadLine(), out id) == false)
                        {
                            Console.Write("\nVui lòng nhập lại ID để tìm kiếm: ");
                        }
                        /*tạo searchResult để lưu giá trị trả về, mặc dù ID chỉ trả về 1 Nhan Vien
                        nhưng tạo list thì để tiện khi truyền tham số vào hàm HienThiNhanVien */
                        List<NhanVien> searchResult = new List<NhanVien>();
                        searchResult.Add(quanLyNhanVien.TimKiemTheoID(id));
                        if (quanLyNhanVien.TimKiemTheoID(id) == null)
                        {
                            Console.WriteLine("Không tìm thấy nhân viên có mã số id là {0}", id);
                        }
                        else
                        {
                            quanLyNhanVien.HienThiNhanVien(searchResult);

                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;

                case 5: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.WriteLine("\n5. Tìm kiếm nhân viên theo tên.");
                        Console.Write("\nNhập tên để tìm kiếm: ");
                        string name = Console.ReadLine();
                        List<NhanVien> searchResult = quanLyNhanVien.TimNhanVienTheoTenNV(name);
                        if (searchResult.Count != 0)
                        {
                            quanLyNhanVien.HienThiNhanVien(searchResult);
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy nhân viên có tên trên");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 6: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.WriteLine("\n6. Tìm kiếm nhân viên có ngày sinh bé nhất.");
                        List<NhanVien> searchResult = new List<NhanVien>();
                        searchResult.Add(quanLyNhanVien.TimNhanVienLonTuoiNhat());
                        quanLyNhanVien.HienThiNhanVien(searchResult);
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;         
                case 7:
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.WriteLine("\n7. Sắp xếp nhân viên theo ID.");
                        quanLyNhanVien.SapXepTheoID();
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;

                case 8: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.WriteLine("\n8. Sắp xếp nhân viên theo tên.");
                        quanLyNhanVien.SapXepTheoTen();
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 9: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.Write("\n9. In ra tổng số lượng nhân viên.");
                        Console.WriteLine(quanLyNhanVien.SoLuongNhanVien());
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 10: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.WriteLine("\n10. Hiển thị danh sách nhân viên.");
                        quanLyNhanVien.HienThiToanBoNhanVien();
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 11: 
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        int id;
                        Console.WriteLine("\n11. Tính lương tháng nhân viên theo ID ");
                        Console.Write("\nNhập ID để tính lương: ");
                        while (int.TryParse(Console.ReadLine(), out id) == false)
                        {
                            Console.Write("\nVui lòng nhập lại ID để tính lương: ");
                        }
                        Console.WriteLine(quanLyNhanVien.TinhLuong(id));
                    }
                    else
                    {
                        Console.WriteLine("\nDanh sách nhân viên trống!");
                    }
                    break;
                case 0:
                    Console.WriteLine("\nBạn đã chọn thoát chương trình!");
                    return;
                //chạy vào khi nhập số khác lựa chọn 1-11
                default:
                    Console.WriteLine("\nKhông có chức năng bạn tìm kiếm!");
                    Console.WriteLine("Hãy chọn chức năng khác trong chương trình.");
                    break;
            }
        }
    }
}

