using Duancuoiki;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.Unicode;
        QuanLyNhanVien quanLyNhanVien = new QuanLyNhanVien();
        while (true)
        {
            Console.WriteLine("\n***********CHƯƠNG TRÌNH QUẢN LÝ NHÂN VIÊN C#***********");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("**  1. Thêm nhân viên.                               **");
            Console.WriteLine("**  2. Cập nhật thông tin nhân viên theo ID.         **");
            Console.WriteLine("**  3. Xóa nhân viên theo ID.                        **");
            Console.WriteLine("**  4. Tìm kiếm nhân viên theo ID.                   **");
            Console.WriteLine("**  5. Tìm kiếm nhân viên theo tên.                  **");
            Console.WriteLine("**  6. Tìm kiếm nhân viên có ngày sinh bé nhất.      **");
            Console.WriteLine("**  7. Sắp xếp nhân viên theo ID.                    **");
            Console.WriteLine("**  8. Sắp xếp nhân viên theo tên.                   **");
            Console.WriteLine("**  9. Tổng số lượng nhân viên hiện tại.             **");
            Console.WriteLine("** 10. Hiển thị danh sách nhân viên.                 **");
            Console.WriteLine("** 11. Tính lương tháng nhân viên theo ID            **");
            Console.WriteLine("**  0. Thoát chương trình.                            **");
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 55; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.Write("Nhập tùy chọn: ");
            int luachon = int.Parse(Console.ReadLine());
            switch (luachon)
            {
                case 1: //done
                    Console.WriteLine("\n1. Thêm nhân viên.");
                    quanLyNhanVien.NhapNhanVien();
                    Console.WriteLine("\nThêm nhân viên thành công!");
                    break;
                case 2: //done
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        int id;
                        Console.WriteLine("\n2. Cập nhật thông tin nhân viên theo ID.");
                        Console.Write("\nNhập ID để cập nhật thông tin: ");
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
                        Console.Write("\nNhập ID để tìm kiếm: ");
                        while (int.TryParse(Console.ReadLine(), out id) == false)
                        {
                            Console.Write("\nVui lòng nhập lại ID để tìm kiếm: ");
                        }
                        List<NhanVien> searchResult = new List<NhanVien>();
                        searchResult.Add(quanLyNhanVien.TimKiemTheoID(id));
                        if (searchResult[0] == null)
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
                case 6: //done
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.WriteLine("\n6. Tìm kiếm nhân viên có ngày sinh bé nhất.");
                        List<NhanVien> searchResult = new List<NhanVien>();
                        searchResult.Add(quanLyNhanVien.TimNhanVienLonTuoiNhat());
                        quanLyNhanVien.HienThiNhanVien(searchResult);
                    }
                    else
                    {
                        Console.WriteLine("\nNhân viên tìm kiếm không hợp lệ!");
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
                case 9: //done
                    if (quanLyNhanVien.SoLuongNhanVien() > 0)
                    {
                        Console.Write("\n8. Số lượng nhân viên hiện tại: ");
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
                default:
                    Console.WriteLine("\nKhông có chức năng bạn tìm kiếm!");
                    Console.WriteLine("\nHãy chọn chức năng khác trong chương trình.");
                    break;
            }
        }
    }
}

