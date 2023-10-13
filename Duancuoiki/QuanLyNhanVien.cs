using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duancuoiki
{
    class QuanLyNhanVien
    {
        private List<NhanVien> ListNhanVien = new List<NhanVien>();
        //Hàm tạo ID tăng dần - DONE
        private int MaNhanVien() 
        {
            int max = 1;
            if (ListNhanVien != null && ListNhanVien.Count > 0)
            {
                max = ListNhanVien[0].ID;
                foreach (NhanVien nv in ListNhanVien)
                {
                    if (max < nv.ID)
                    {
                        max = nv.ID;
                    }
                }
                max++;
            }
            return max;
        }
        //Hàm hiển thị số lượng nhân viên hiện tại - DONE
        public int SoLuongNhanVien()
        {
            int Count = 0;
            if (ListNhanVien != null)
            {
                Count = ListNhanVien.Count;
            }
            return Count;
        }
        //Hàm chuẩn hóa tên nhân viên - DONE
        static string chuanhoaten(string FullName)
        {
            string Result = "";
            FullName = FullName.Trim();
            while (FullName.IndexOf("  ") != -1)
            {
                FullName = FullName.Replace("  ", " ");
            }
            string[] SubName = FullName.Split(' ');

            for (int i = 0; i < SubName.Length; i++)
            {
                string FirstChar = SubName[i].Substring(0, 1);
                string OtherChar = SubName[i].Substring(1);
                SubName[i] = FirstChar.ToUpper() + OtherChar.ToLower();
                Result += SubName[i] + " ";
            }

            return Result;
        }
        //Hàm chuẩn ngày sinh - DONE
        static string chuanhoangaysinh(string NgaySinh)
        {
            StringBuilder birth = new StringBuilder(NgaySinh);
            NgaySinh = NgaySinh.Trim();
            while (NgaySinh.IndexOf("  ") != -1)
            {
                NgaySinh = NgaySinh.Replace("  ", " ");
            }
            if (birth[1] == '/')
            {
                birth.Insert(0, '0');
            }
            if (birth[4] == '/')
            {
                birth.Insert(3, '0');
            }
            return birth.ToString();
        }
        //Hàm khởi tạo nhân viên mới - DONE
        public void NhapNhanVien()
        {
            NhanVien nv = new NhanVien();
            nv.ID = MaNhanVien();
            Console.Write("Nhập họ tên nhân viên: ");
            string name = Console.ReadLine();
            nv.HoTen = chuanhoaten(name); //dùng hàm chuẩn hóa tên
            Console.Write("Nhập ngày sinh nhân viên: ");
            string birth = Console.ReadLine(); 
            nv.NgaySinh = chuanhoangaysinh(birth); //dùng hàm chuẩn hóa ngày sinh
            Console.Write("Nhập lương cơ bản mỗi ngày công: ");
            nv.LuongCoBan = int.Parse(Console.ReadLine());
            Console.Write("Nhập số ngày công: ");
            nv.SoNgayCong = int.Parse(Console.ReadLine());
            Console.Write("Nhập chức vụ của nhân viên: ");
            nv.ChucVu = Console.ReadLine();
            ListNhanVien.Add(nv);
        }
        // Hàm cập nhật nhân viên - DONE
        public void SuaThongTinNhanVien(int ID)
        {
            // Tìm kiếm nhân viên trong danh sách ListNhanVien
            NhanVien nv = TimKiemTheoID(ID);
            // Nếu nhân viên tồn tại thì cập nhập thông tin nhân viên
            if (nv != null)
            {
                Console.Write("Nhập họ tên nhân viên mới: ");
                string name = Convert.ToString(Console.ReadLine());
                // Nếu không nhập gì thì không cập nhật họ tên
                if (name != null && name.Length > 0)
                {
                    nv.HoTen = chuanhoaten(name);
                }
                Console.Write("Nhập ngày sinh mới: ");
                string NgaySinh = Console.ReadLine();
                // Nếu không nhập gì thì không cập nhật ngày sinh
                if (NgaySinh != null && NgaySinh.Length > 0)
                {
                    nv.NgaySinh = chuanhoangaysinh(NgaySinh);
                }
                Console.Write("Nhập lương cơ bản mỗi ngày công mới: ");
                int LuongCoBan = int.Parse(Console.ReadLine());
                // Nếu không nhập gì thì không cập nhật tuổi
                nv.LuongCoBan = LuongCoBan;
                Console.Write("Nhập số ngày công mới: ");
                int SoNgayCong = int.Parse(Console.ReadLine());
                // Nếu không nhập gì thì không cập nhật tuổi
                nv.SoNgayCong = SoNgayCong;
                Console.Write("Nhập chức vụ mới: ");
                string ChucVu = Console.ReadLine();
                // Nếu không nhập gì thì không cập nhật ngày sinh
                if (ChucVu != null && ChucVu.Length > 0)
                {
                    nv.ChucVu = ChucVu;
                }
            }
            else
            {
                Console.WriteLine("Nhân viên có ID = {0} không toàn tại", ID);
            }
        }
        //Hàm xóa nhân viên - DONE
        public bool XoaNhanVien(int ID)
        {
            // Tìm nhân viên có mã nhân viên nhập từ người dùng trong danh sách nhân viên
            NhanVien nhanVien = ListNhanVien.FirstOrDefault(nv => nv.ID == ID);

            // Kiểm tra nếu nhân viên tồn tại trong danh sách
            if (nhanVien != null)
            {
                // Nếu tồn tại, xóa nhân viên khỏi danh sách nhân viên
                ListNhanVien.Remove(nhanVien);
                return true;
            }
            else
            {
                return false;
            }
        }
        //Hàm tìm kiếm nhân viên theo ID - DONE
        public NhanVien TimKiemTheoID(int ID)
        {
            NhanVien searchResult = null;
            if (ListNhanVien != null && ListNhanVien.Count > 0)
            {
                foreach (NhanVien nv in ListNhanVien)
                {
                    if (nv.ID == ID)
                    {
                        searchResult = nv;
                    }
                }
            }
            return searchResult;
        }
        //Hàm tìm kiếm nhân viên theo tên - DONE
        public List<NhanVien> TimNhanVienTheoTenNV(string HoTen)
        {
            List<NhanVien> searchResult = new List<NhanVien>();
            //Truyen vao bien ten can tim, tao mot danh sach Ket qua tra ve
            if (ListNhanVien != null && ListNhanVien.Count > 0)
            {
                foreach (NhanVien nv in ListNhanVien)
                {
                    if (nv.HoTen.ToUpper().Contains(HoTen.ToUpper()))
                    {
                        searchResult.Add(nv);
                    }
                }
            }
            return searchResult;
        }
        //Hàm tìm nhân viên có ngày sinh bé nhất - DONE
        public NhanVien TimNhanVienLonTuoiNhat()
        {
            DateTime currentDate = DateTime.Now;
            NhanVien oldestPerson = null;
            TimeSpan maxAge = TimeSpan.Zero;
            foreach (NhanVien nv in ListNhanVien)
            {
                DateTime NgaySinh = Convert.ToDateTime(nv.NgaySinh);
                TimeSpan age = currentDate - NgaySinh;

                if (age > maxAge)
                {
                    maxAge = age;
                    oldestPerson = nv;
                }
            }
            return oldestPerson;
        }
        //Hàm sắp xếp theo ID giảm dần - DONE
        public void SapXepTheoID()
        {
            ListNhanVien.Sort(delegate (NhanVien nv1, NhanVien nv2) {
                return nv2.ID.CompareTo(nv1.ID);
            });
        }
        //Hàm sắp xếp theo tên tăng dần - DONE
        public void SapXepTheoTen()
        {
            ListNhanVien.Sort(delegate (NhanVien nv1, NhanVien nv2) {
                return nv1.HoTen.CompareTo(nv2.HoTen);
            });
        }
        // Hàm tính lương nhân viên


        //Hàm hiển thị danh sách nhân viên - DONE
        public void HienThiNhanVien(List<NhanVien> listNV)
        {
            // Hiển thị tiêu đề cột
            Console.WriteLine("{0, -5} {1, -20} {2, -5} {3, 5} {4, 5} {5, 5}",
                  "ID", "Họ tên", "Ngày Sinh", "Lương cơ bản", "Số ngày công", "Chức vụ");
            // hiển thị danh sách nhân viên nhân viên
            if (listNV != null && listNV.Count > 0)
            {
                foreach (NhanVien nv in listNV)
                {
                    Console.WriteLine("{0, -5} {1, -20} {2, -5} {3, 5} {4, 5} {5, 5}",
                                      nv.ID, nv.HoTen, nv.NgaySinh, nv.LuongCoBan, nv.SoNgayCong, nv.ChucVu);
                }
            }
            Console.WriteLine();
        }
        //Hàm trả về danh sách nhân viên - DONE
        public List<NhanVien> getListNhanVien()
        {
            return ListNhanVien;
        }
    }
}
