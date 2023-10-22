using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Duancuoiki
{
    class DocFile
    {
        //hàm ghi file
        public static void FileLuu(NhanVien nv)
        {
            try
            {
                //đối tượng ghi file, true là có ghi đè, Encoding.UTF8 là tiếng Việt
                StreamWriter streamWriter = new StreamWriter("data.txt", true, Encoding.UTF8);
                using (streamWriter)
                {
                    //chuẩn hóa để lưu vào file, các phần tử phân cách nhau bởi dấu ;
                    string line = nv.ID + ";" + nv.HoTen + ";" + nv.NgaySinh + ";" + nv.LuongCoBan + ";" + nv.SoNgayCong + ";" + nv.TienThuong + ";" + nv.PhuCap + ";" + nv.ChucVu + ";" + nv.PhongBan;
                    //ghi từng dòng vào file
                    streamWriter.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }
        public static void FileSapXep(NhanVien nv)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter("sorted_data.txt", true, Encoding.UTF8);
                using (streamWriter)
                {
                    string line = nv.ID + ";" + nv.HoTen + ";" + nv.NgaySinh + ";" + nv.LuongCoBan + ";" + nv.SoNgayCong + ";" + nv.TienThuong + ";" + nv.PhuCap + ";" + nv.ChucVu + ";" + nv.PhongBan;
                    streamWriter.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }
        //hàm đọc file
        public static List<NhanVien> FileDoc()
        {
            //tạo list lưu Nhân Viên trả ra khi đọc
            List<NhanVien> dsnv = new List<NhanVien>();
            try
            {
                //đối tượng đọc file
                StreamReader streamReader = new StreamReader("data.txt", Encoding.UTF8);
                using (streamReader)
                {
                    //đầu đọc trỏ đến dòng đầu tiên
                    string line = streamReader.ReadLine();
                    while (line != null)
                    {
                        //tách chuỗi string ra
                        string[] arr = line.Split(";");
                        //nếu mảng có đủ 9 phần tử sẽ lưu vào đối tượng Nhân Viên
                        if (arr.Length == 9)
                        {
                            NhanVien nv = new NhanVien();
                            nv.ID = int.Parse(arr[0]);
                            nv.HoTen = arr[1];
                            nv.NgaySinh = arr[2];
                            nv.LuongCoBan = int.Parse(arr[3]);
                            nv.SoNgayCong = int.Parse(arr[4]);
                            nv.TienThuong = int.Parse(arr[5]);
                            nv.PhuCap = int.Parse(arr[6]);
                            nv.ChucVu = arr[7];
                            nv.PhongBan = arr[8];
                            //thêm đối tượng Nhân Viên vào list
                            dsnv.Add(nv);
                        }
                        //chuyển đầu đọc sang dòng tiếp theo
                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
            //trả về list Nhân Viên
            return dsnv;
        }
    }
}