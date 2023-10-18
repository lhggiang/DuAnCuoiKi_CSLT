using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Duancuoiki
{
    class QuanLyNhanVien
    {   
        private List<NhanVien> ListNhanVien = new List<NhanVien>();
        //Hàm tạo ID tăng dần 
        private int MaNhanVien() 
        {
            int max = 1;
            //lấy danh sách nhân viên trong file data.txt lưu vào biến listNV
            List<NhanVien>  listNV = DocFile.FileDoc("data.txt");
            if (listNV != null && listNV.Count > 0)
            {
                //lấy ra ID của nhân viên vị trí 0 lưu vào biến max
                max = listNV[0].ID;
                foreach (NhanVien nv in listNV)
                {
                    //duyệt qua nếu biến nào lớn hơn max thì cập nhật max
                    if (max < nv.ID)
                    {
                        max = nv.ID;
                    }
                }
                max++;
            }
            return max;
        }
        //Hàm hiển thị số lượng nhân viên hiện tại
        public int SoLuongNhanVien()
        {
            int Count = 0;
            //lấy danh sách nhân viên trong file data.txt lưu vào biến listNV
            List<NhanVien> listNV = DocFile.FileDoc("data.txt");
            //nếu danh sách khác null thì dùng phương thức count để đếm nhân viên trong listNV
            if (listNV != null)
            {
                Count = listNV.Count;
            }
            return Count;
        }
        //Hàm chuẩn hóa tên nhân viên
        public string ChuanHoaTen(string FullName)
        {
            string Result = "";
            //dùng phương thức Trim để xóa khoảng trẳng ở đầu và cuối của chuỗi
            FullName = FullName.Trim();
            /*dùng phương thức IndexOf để kiểm tra xem giữa các từ có bị dư khoảng 
            trắng hay không, nếu có trả về giá trị khác -1 thì dùng phương thức 
            replace để thay thế 2 khoảng trắng thành 1 khoảng trắng*/
            while (FullName.IndexOf("  ") != -1)
            {
                FullName = FullName.Replace("  ", " ");
            }
            //dùng phương thức Split để tách các từ trong file ra sau đó lưu vào mảng SubName
            string[] SubName = FullName.Split(' ');
            //duyệt qua các từ trong tên bằng vòng lặp for
            for (int i = 0; i < SubName.Length; i++)
            {
                //lấy kí tự đầu của mỗi chữ
                string FirstChar = SubName[i].Substring(0, 1);
                //lấy các kí tự còn lại của mỗi chữ
                string OtherChar = SubName[i].Substring(1);
                //in hoa kí tự đầu và in thường các kí tự còn lại
                SubName[i] = FirstChar.ToUpper() + OtherChar.ToLower();
                //nối vào biến Result
                Result += SubName[i] + " ";
            }
            //loại bỏ dấu " " cuối tên
            return Result.TrimEnd();
        }
        //Hàm chuẩn hóa ngày sinh 
        public string ChuanHoaNgaySinh(string NgaySinh)
        {
            //khởi tạo lớp StringBuilder để tạo chuỗi có thể thay đổi được
            StringBuilder birth = new StringBuilder(NgaySinh);
            //dùng phương thức Trim để xóa khoảng trẳng ở đầu và cuối của chuỗi
            NgaySinh = NgaySinh.Trim();
            /*dùng phương thức IndexOf để kiểm tra xem giữa các từ có bị dư khoảng 
            trắng hay không, nếu có trả về giá trị khác -1 thì dùng phương thức 
            replace để thay thế 2 khoảng trắng thành 1 khoảng trắng*/
            while (NgaySinh.IndexOf("  ") != -1)
            {
                NgaySinh = NgaySinh.Replace("  ", " ");
            }
           //kiểm tra xem nếu birth vị trí 1 là dấu / chứng tỏ nó đang thiếu số 0  
            if (birth[1] == '/')
            {
                //dùng insert thêm vào birth vị trí 0 số 0 để chuẩn dạng dd/mm/yyyy
                birth.Insert(0, '0');
            }
            //kiểm tra xem nếu birth vị trí 4 là dấu / chứng tỏ nó đang thiếu số 0  
            if (birth[4] == '/')
            {
                //dùng insert thêm vào birth vị trí 3 số 0 để chuẩn dạng dd/mm/yyyy
                birth.Insert(3, '0');
            }
            //dùng ToString() để chuyển lớp StringBuilder thành kiểu dữ liệu string
            return birth.ToString();
        }
        //Hàm khởi tạo nhân viên mới 
        public void NhapNhanVien()
        {
            //khởi tạo lớp nhân viên
            try
            {
                NhanVien nv = new NhanVien();
                nv.ID = MaNhanVien();
                Console.Write("Nhập họ tên nhân viên: ");
                string name = Console.ReadLine();
                //dùng hàm chuẩn hóa tên quy tên thành một dạng
                name = ChuanHoaTen(name);
                //xử lý tên nhập sai chỉ có một chữ
                bool check = false;
                while (check == false)
                {
                    string[] SubName = name.Split(' ');
                    if (SubName.Length == 1)
                    {
                        Console.Write("Vui lòng nhập lại họ tên nhân viên: ");
                        name = Console.ReadLine();
                        name = ChuanHoaTen(name);
                    }
                    else check = true;
                }
                nv.HoTen = name;
                Console.Write("Nhập ngày sinh nhân viên dạng dd/mm/yyyy: ");
                string birth = Console.ReadLine();
                //dùng hàm chuẩn hóa ngày sinh
                birth = ChuanHoaNgaySinh(birth);
                //xử lý nhập ngày sinh ngày, tháng bị sai
                while (int.Parse(birth.Substring(0, 2)) > 31 || int.Parse(birth.Substring(0, 2)) <= 0 || int.Parse(birth.Substring(3, 2)) > 12 || int.Parse(birth.Substring(3, 2)) <= 0)
                {
                    Console.Write("Vui lòng nhập ngày sinh nhân viên theo dạng dd/mm/yyyy: ");
                    birth = Console.ReadLine();
                    birth = ChuanHoaNgaySinh(birth);
                }
                //xử lý nhập ngày sinh năm bị sai
                while (int.Parse(birth.Substring(6, 4)) < 1900 || int.Parse(birth.Substring(6, 4)) > 2100)
                {
                    Console.Write("Vui lòng nhập ngày sinh nhân viên theo dạng dd/mm/yyyy: ");
                    birth = Console.ReadLine();
                    birth = ChuanHoaNgaySinh(birth);
                }
                nv.NgaySinh = birth;
                Console.Write("Nhập lương cơ bản mỗi ngày công: ");
                int LuongCoBan;
                //nếu nhập sai thì cho nhập lại
                while (int.TryParse(Console.ReadLine(), out LuongCoBan) == false)
                {
                    Console.Write("Vui lòng nhập lại lương cơ bản mỗi ngày công: ");
                }
                nv.LuongCoBan = LuongCoBan;
                Console.Write("Nhập số ngày công: ");
                int SoNgayCong;
                //nếu nhập sai thì cho nhập lại
                while (int.TryParse(Console.ReadLine(), out SoNgayCong) == false)
                {
                    Console.Write("Vui lòng nhập lại số ngày công: ");
                }
                nv.SoNgayCong = SoNgayCong;
                Console.Write("Nhập chức vụ của nhân viên: ");
                nv.ChucVu = Console.ReadLine();
                //lưu file
                DocFile.FileLuu(nv);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        // Hàm cập nhật nhân viên
        public void SuaThongTinNhanVien(int ID)
        {
            // Tìm kiếm nhân viên trong danh sách ListNhanVien
            NhanVien nv = TimKiemTheoID(ID);
            if (nv != null)
            {
                // Yêu cầu nhập họ tên nhân viên mới
                Console.Write("Nhập họ tên nhân viên mới: ");
                // Đọc và loại bỏ khoảng trắng ở đầu và cuối chuỗi nhập liệu
                string name = Console.ReadLine().Trim();
                // Kiểm tra xem chuỗi nhập liệu có tồn tại và không rỗng
                if (!string.IsNullOrEmpty(name))
                {
                    // Nếu họ tên hợp lệ, cập nhật vào thuộc tính HoTen của nhân viên
                    nv.HoTen = ChuanHoaTen(name);
                }
                // Yêu cầu nhập ngày sinh mới
                Console.Write("Nhập ngày sinh mới thep dạng dd/mm/yyyy: ");
                // Đọc ngày sinh từ người dùng
                string NgaySinh = Console.ReadLine();
                // Kiểm tra xem chuỗi nhập liệu có tồn tại và không rỗng
                if (!string.IsNullOrEmpty(NgaySinh))
                {
                    DateTime ngaySinh;
                    // Sử dụng TryParseExact để kiểm tra và chuyển đổi định dạng ngày tháng
                    if (DateTime.TryParseExact(NgaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
                    {
                        // Nếu ngày tháng hợp lệ, cập nhật vào thuộc tính NgaySinh của nhân viên
                        nv.NgaySinh = NgaySinh;
                    }
                    else
                    {
                        Console.WriteLine("Ngày sinh không hợp lệ. Đã bỏ qua cập nhật ngày sinh.");
                    }
                }
                // Yêu cầu nhập lương cơ bản mỗi ngày công mới
                Console.Write("Nhập lương cơ bản mỗi ngày công mới: ");
                // Đọc và chuyển đổi lương cơ bản từ chuỗi nhập liệu sang kiểu int
                if (int.TryParse(Console.ReadLine(), out int LuongCoBan))
                {
                    // Nếu lương cơ bản hợp lệ, cập nhật vào thuộc tính LuongCoBan của nhân viên
                    nv.LuongCoBan = LuongCoBan;
                }
                else
                {
                    Console.WriteLine("Lương cơ bản không hợp lệ. Đã bỏ qua cập nhật lương cơ bản.");
                }
                // Yêu cầu nhập số ngày công mới
                Console.Write("Nhập số ngày công mới: ");
                // Đọc và chuyển đổi số ngày công từ chuỗi nhập liệu sang kiểu int
                if (int.TryParse(Console.ReadLine(), out int SoNgayCong))
                {
                    // Nếu số ngày công hợp lệ, cập nhật vào thuộc tính SoNgayCong của nhân viên
                    nv.SoNgayCong = SoNgayCong;
                }
                else
                {
                    Console.WriteLine("Số ngày công không hợp lệ. Đã bỏ qua cập nhật số ngày công.");
                }
                // Yêu cầu nhập chức vụ mới
                Console.Write("Nhập chức vụ mới: ");
                // Đọc và loại bỏ khoảng trắng ở đầu và cuối chuỗi nhập liệu
                string ChucVu = Console.ReadLine().Trim();
                // Kiểm tra xem chuỗi nhập liệu có tồn tại và không rỗng
                if (!string.IsNullOrEmpty(ChucVu))
                {
                    // Nếu chức vụ hợp lệ, cập nhật vào thuộc tính ChucVu của nhân viên
                    nv.ChucVu = ChucVu;
                }
            }
            else
            {
                Console.WriteLine("Nhân viên có ID = {0} không tồn tại", ID);
            }
        }
        //Hàm xóa nhân viên 
        public bool XoaNhanVien(int ID)
        {
            if (ID == -1)
            {
                // Xóa toàn bộ nhân viên
                if (ListNhanVien.Count > 0)
                {
                    ListNhanVien.Clear();
                    Console.WriteLine("Đã xóa toàn bộ nhân viên.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Danh sách nhân viên đã trống, không có gì để xóa.");
                    return false;
                }
            }
            else
            {
                // Tìm nhân viên có mã nhân viên nhập từ người dùng trong danh sách nhân viên
                NhanVien nhanVien = null;

                // Duyệt qua danh sách nhân viên để tìm nhân viên cần xóa
                foreach (NhanVien nv in ListNhanVien)
                {
                    if (nv.ID == ID)
                    {
                        nhanVien = nv; // Gán nhân viên tìm thấy vào biến nhanVien
                        break; // Thoát khỏi vòng lặp sau khi tìm thấy nhân viên cần xóa
                    }
                }

                // Kiểm tra nếu nhân viên tồn tại trong danh sách
                if (nhanVien != null)
                {
                    // Nếu tồn tại, xóa nhân viên khỏi danh sách nhân viên
                    ListNhanVien.Remove(nhanVien);
                    Console.WriteLine("Nhân viên có ID = {0} đã được xóa.", ID);
                    return true;
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có ID = {0}. Xóa không thành công.", ID);
                    return false;
                }
            }
        }

        //Hàm tìm kiếm nhân viên theo ID 
        public NhanVien TimKiemTheoID(int ID)
        {
            NhanVien searchResult = null;
            //Duyệt Nhân viên trong Danh sách Nhân viên
            List<NhanVien> listNV = DocFile.FileDoc("data.txt");
            if (listNV != null && listNV.Count > 0)
            {
                foreach (NhanVien nv in listNV)
                {
                    //Nhân viên có ID giống với ID người dùng cần tìm sẽ trả về nhân viên đó.
                    if (nv.ID == ID)
                    {
                        searchResult = nv;
                    }
                }
            }
            //Trả về nhân viên đó
            return searchResult;
        }
        //Hàm tìm kiếm nhân viên theo tên
        public List<NhanVien> TimNhanVienTheoTenNV(string HoTen)
        {
            //Tạo một Danh sách người dùng trả về hợp lệ
            List<NhanVien> searchResult = new List<NhanVien>();
            List<NhanVien> listNV = DocFile.FileDoc("data.txt");
            if (listNV != null && listNV.Count > 0)
            {
                foreach (NhanVien nv in listNV)
                {
                    /*Duyệt từng Nhân viên trong danh sách nhân viên.
                    Kiểm tra sự tồn tại của chuỗi Hoten mà người dùng nhập vào có trong danh sách Nhân viên hay không*/
                    if (nv.HoTen.ToUpper().Contains(HoTen.ToUpper()))
                    {
                        //Nếu có, sẽ thêm nhân viên đó vào chuỗi searchResult
                        searchResult.Add(nv);
                    }
                }
            }
            //Trả về chuỗi nhân viên cần tìm
            return searchResult;
        }
        //Hàm tìm nhân viên có ngày sinh bé nhất 
        public NhanVien TimNhanVienLonTuoiNhat()
        {
            List<NhanVien> listNV = DocFile.FileDoc("data.txt");
            // Lấy ngày hiện tại
            DateTime currentDate = DateTime.Now;
            // Khởi tạo biến để lưu trữ nhân viên có tuổi lớn nhất
            NhanVien oldestPerson = null;
            // Khởi tạo biến để lưu trữ độ chênh lệch thời gian lớn nhất
            TimeSpan maxAge = TimeSpan.Zero;
            // Duyệt qua danh sách nhân viên
            foreach (NhanVien nv in ListNhanVien)
            {
                // Chuyển ngày sinh của nhân viên thành kiểu DateTime
                DateTime NgaySinh = Convert.ToDateTime(nv.NgaySinh);
                // Tính độ chênh lệch thời gian giữa ngày hiện tại và ngày sinh
                TimeSpan age = currentDate - NgaySinh;
                // So sánh với độ chênh lệch lớn nhất đã biết
                if (age > maxAge)
                {
                    //Nếu tìm thấy người có độ chênh lệch lớn hơn, cập nhật thông tin
                    maxAge = age;
                    oldestPerson = nv;
                }
            }
            // Trả về nhân viên có tuổi lớn nhất
            return oldestPerson;
        }
        //Hàm sắp xếp theo ID giảm dần 
        public void SapXepTheoID()
        {
            List<NhanVien> listNV = DocFile.FileDoc("data.txt");
            listNV.Sort(delegate (NhanVien nv1, NhanVien nv2) {
                return nv2.ID.CompareTo(nv1.ID);
            });
        }
        //Hàm sắp xếp theo tên tăng dần
        public void SapXepTheoTen()
        {
            ListNhanVien.Sort(delegate (NhanVien nv1, NhanVien nv2) {
                return nv1.HoTen.CompareTo(nv2.HoTen);
            });
        }
        // Hàm tính lương nhân viên

        //Hàm hiển thị danh sách nhân viên 
        public void HienThiNhanVien(List<NhanVien> listNV)
        {
            //Hiển thị tiêu đề cột
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

        //Hàm hiển thị toàn bộ danh sách nhân viên 
        public void HienThiToanBoNhanVien()
        {
            //Hiển thị tiêu đề cột
            List<NhanVien> listNV = DocFile.FileDoc("data.txt");
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
