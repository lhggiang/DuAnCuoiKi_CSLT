using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Duancuoiki
{
    class QuanLyNhanVien
    {
        //Hàm tạo ID tăng dần 
        private int MaNhanVien()
        {
            int max = 1;
            //lấy danh sách nhân viên trong file data.txt lưu vào biến listNV
            List<NhanVien> listNV = DocFile.FileDoc();
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

        //Hàm in ra tổng số lượng nhân viên
        public int SoLuongNhanVien()
        {
            int Count = 0;
            //lấy danh sách nhân viên trong file data.txt lưu vào biến listNV
            List<NhanVien> listNV = DocFile.FileDoc();
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
                //gán ID nhân viên
                nv.ID = MaNhanVien();
                //nhập tên nhân viên
                Console.Write("Nhập họ tên nhân viên: ");
                string name = Console.ReadLine();
                //xử lý khi nhập dấu enter
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Vui lòng nhập lại họ tên nhân viên: ");
                    name = Console.ReadLine();
                }
                //dùng hàm chuẩn hóa tên quy tên thành một dạng
                name = ChuanHoaTen(name);
                //xử lý tên nhập sai khi chỉ nhập có một chữ
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
                //nhập ngày sinh
                Console.Write("Nhập ngày sinh nhân viên dạng dd/mm/yyyy: ");
                string birth = Console.ReadLine();
                //xử lý khi nhập dấu enter
                while (string.IsNullOrWhiteSpace(birth))
                {
                    Console.Write("Vui lòng nhập lại ngày sinh nhân viên theo dạng dd/mm/yyyy: ");
                    birth = Console.ReadLine();
                }
                //dùng hàm chuẩn hóa ngày sinh
                birth = ChuanHoaNgaySinh(birth);
                bool ok = false;
                while (ok == false)
                {
                    //xử lý nhập ngày sinh ngày khi ngày bé hơn 1 và lớn hơn 31, tháng bị sai khi tháng bé hơn 1 và lớn hơn 12, năm nhập lớn hơn năm hiện tại
                    DateTime dateTime = DateTime.Now;
                    int currentbirth = dateTime.Year;
                    //xử lý để khi chuyển string birth thành DateTime không bị lỗi
                    string BirthFormat = birth.Substring(3, 2) + "/" + birth.Substring(0, 2) + "/" + birth.Substring(6, 4);
                    while ((int.Parse(birth.Substring(0, 2)) > 31) || (int.Parse(birth.Substring(0, 2)) <= 0) ||
                        (int.Parse(birth.Substring(3, 2)) > 12) || (int.Parse(birth.Substring(3, 2)) <= 0)||
                        (int.Parse(birth.Substring(6, 4)) < currentbirth - 60) || (int.Parse(birth.Substring(6, 4)) > currentbirth))
                    {
                        Console.Write("Vui lòng nhập lại ngày sinh nhân viên theo dạng dd/mm/yyyy A: ");
                        birth = Console.ReadLine();
                        birth = ChuanHoaNgaySinh(birth);
                    }
                    // Kiểm tra trường hợp thời gian nhập lớn hơn thời gian hiện tại
                    while (true)
                    {
                        BirthFormat = birth.Substring(3, 2) + "/" + birth.Substring(0, 2) + "/" + birth.Substring(6, 4);
                        if (!DateTime.TryParse(BirthFormat, out DateTime userTime))
                        {
                            Console.Write("Vui lòng nhập lại ngày sinh nhân viên theo dạng dd/mm/yyyy B: ");
                            birth = Console.ReadLine();
                            birth = ChuanHoaNgaySinh(birth);
                        }
                        break;
                    }
                    //vì sau vòng true ở trên giá trị BirthFormatd đã thay đổi nên ta gán lại giá trị mới
                    BirthFormat = birth.Substring(3, 2) + "/" + birth.Substring(0, 2) + "/" + birth.Substring(6, 4);
                    //chuyển ngày sinh của nhân viên thành kiểu DateTime
                    DateTime NgaySinh = Convert.ToDateTime(BirthFormat);
                    //check lại các điều kiện
                    while ((int.Parse(birth.Substring(0, 2)) > 31) || (int.Parse(birth.Substring(0, 2)) <= 0) || 
                        (int.Parse(birth.Substring(3, 2)) > 12) || (int.Parse(birth.Substring(3, 2)) <= 0) || 
                        (int.Parse(birth.Substring(6, 4)) < currentbirth - 60) || (int.Parse(birth.Substring(6, 4)) > currentbirth) || (NgaySinh > dateTime))
                    {
                        Console.Write("Vui lòng nhập lại ngày sinh nhân viên theo dạng dd/mm/yyyy: ");
                        birth = Console.ReadLine();
                        birth = ChuanHoaNgaySinh(birth);
                    }
                    ok = true;
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
                //nhập số ngày công
                int SoNgayCong;
                string soNgayCong;
                do
                {
                    Console.Write("Nhập số ngày công: ");
                    //nếu nhập vào giá trị là null thì gán chuỗi rỗng
                    soNgayCong = Console.ReadLine() ?? "";
                    if (!int.TryParse(soNgayCong, out SoNgayCong) || SoNgayCong < 0 || SoNgayCong > 31)
                    {
                        Console.WriteLine("Vui lòng nhập lại số ngày công: ");
                    }
                } while (SoNgayCong < 0 || SoNgayCong > 31 || !int.TryParse(soNgayCong, out SoNgayCong));
                nv.SoNgayCong = SoNgayCong;
                //tiền thưởng số ngày công đạt điều kiện thưởng
                if (nv.SoNgayCong >= 25)
                {
                    nv.TienThuong = nv.LuongCoBan * 20 / 100;
                }
                else if (nv.SoNgayCong >= 22)
                {
                    nv.TienThuong = nv.LuongCoBan * 10 / 100;
                }
                else
                {
                    nv.TienThuong = 0;
                }
                //lựa chọn chức vụ tương ứng cho người được thêm vào danh sách
                Console.Write("Chọn chức vụ của người được thêm vào danh sách\n" +
                    "   1.Giám đốc\n" +
                    "   2.Phó giám đốc\n" +
                    "   3.Trưởng phòng\n" +
                    "   4.Nhân viên\n" +
                    "Mời nhập lựa chọn: ");
                string nhapluachon = Console.ReadLine() ?? "";
                int soluachoncv;
                while (!int.TryParse(nhapluachon, out soluachoncv) || (soluachoncv < 1 || soluachoncv > 4))
                {
                    Console.Write("Vui lòng nhập lại lựa chọn là số có giá trị từ 1 - 4: ");
                    nhapluachon = Console.ReadLine() ?? "";
                }
                switch (soluachoncv)
                {
                    case 1:
                        nv.ChucVu = "Giám đốc";
                        nv.PhuCap = 250000;
                        break;
                    case 2:
                        nv.ChucVu = "Phó giám đốc";
                        nv.PhuCap = 200000;
                        break;
                    case 3:
                        nv.ChucVu = "Trưởng phòng";
                        nv.PhuCap = 180000;
                        break;
                    case 4:
                        nv.ChucVu = "Nhân viên";
                        nv.PhuCap = 150000;
                        break;
                }
                //Thêm phòng ban 
                Console.Write("Chọn phòng ban của người được thêm vào danh sách\n" +
                    "   1.Phòng Nhân Sự\n" +
                    "   2.Phòng Kế Toán\n" +
                    "   3.Phòng Marketing\n" +
                    "Mời nhập lựa chọn: ");
                string Nhapluachon = Console.ReadLine() ?? "";
                int SoLuaChon;
                while (!int.TryParse(Nhapluachon, out SoLuaChon) || (SoLuaChon < 1 || SoLuaChon > 4))
                {
                    Console.Write("Vui lòng nhập lại lựa chọn là số có giá trị từ 1 - 4: ");
                    nhapluachon = Console.ReadLine() ?? "";
                }
                switch (SoLuaChon)
                {
                    case 1:
                        nv.PhongBan = "Phòng Nhân Sự";
                        break;
                    case 2:
                        nv.PhongBan = "Phòng Kế Toán";
                        break;
                    case 3:
                        nv.PhongBan = "Phòng Marketing";
                        break;
                }
                //lưu file
                DocFile.FileLuu(nv);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        // Hàm cập nhật nhân viên
        public void SuaThongTinNhanVien(int ID)
        {
            try
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
                    //tiền thưởng số ngày công đạt điều kiện thưởng
                    if (nv.SoNgayCong >= 25)
                    {
                        nv.TienThuong = nv.LuongCoBan * 20 / 100;
                    }
                    else if (nv.SoNgayCong >= 22)
                    {
                        nv.TienThuong = nv.LuongCoBan * 10 / 100;
                    }
                    else
                    {
                        nv.TienThuong = 0;
                    }
                    // Yêu cầu nhập chức vụ mới
                    Console.Write("Câp nhật chức vụ của Nhân Viên này: \n" +
                        "   1.Giám đốc\n" +
                        "   2.Phó giám đốc\n" +
                        "   3.Trưởng phòng\n" +
                        "   4.Nhân viên\n" +
                        "Mời nhập lựa chọn: ");
                    string nhapluachon = Console.ReadLine() ?? "";
                    int soluachoncv;
                    while (!int.TryParse(nhapluachon, out soluachoncv) || (soluachoncv < 1 || soluachoncv > 4))
                    {
                        Console.Write("Vui lòng nhập lại lựa chọn là số có giá trị từ 1 - 4: ");
                        nhapluachon = Console.ReadLine() ?? "";
                    }
                    switch (soluachoncv)
                    {
                        default:
                            break;
                        case 1:
                            nv.ChucVu = "Giám đốc";
                            nv.PhuCap = 250000;
                            break;
                        case 2:
                            nv.ChucVu = "Phó giám đốc";
                            nv.PhuCap = 200000;
                            break;
                        case 3:
                            nv.ChucVu = "Trưởng phòng";
                            nv.PhuCap = 180000;
                            break;
                        case 4:
                            nv.ChucVu = "Nhân viên";
                            nv.PhuCap = 150000;
                            break;
                    }
                    //Sua phong ban 
                    Console.Write("Chọn phong ban của người được thêm vào danh sách\n" +
                        "   1.Phong Nhan Su\n" +
                        "   2.Phong Ke Toan\n" +
                        "   3.Phong Marketing\n" +
                        "   4.Giữ Nguyên\n" +
                        "Mời nhập lựa chọn: ");
                    string Nhapluachon = Console.ReadLine() ?? "";
                    int SoLuaChon;
                    while (!int.TryParse(Nhapluachon, out SoLuaChon) || (SoLuaChon < 1 || SoLuaChon > 4))
                    {
                        Console.Write("Vui lòng nhập lại lựa chọn là số có giá trị từ 1 - 4: ");
                        Nhapluachon = Console.ReadLine() ?? "";
                    }
                    switch (SoLuaChon)
                    {
                        case 1:
                            nv.PhongBan = "Phòng Nhân Sự";
                            break;
                        case 2:
                            nv.PhongBan = "Phòng Kế Toán";
                            break;
                        case 3:
                            nv.PhongBan = "Phòng Marketing";
                            break;
                        case 4:
                            Console.WriteLine("Giữ nguyên Phòng ban của nhân viên này");
                            break;
                    }
                    List<NhanVien> listNV = DocFile.FileDoc();
                    //lưu từng phần tử vào file
                    foreach (NhanVien nhanvien in listNV)
                    {
                        if (nhanvien.ID == nv.ID)
                        {
                            nhanvien.ID = nv.ID;
                            nhanvien.HoTen = nv.HoTen;
                            nhanvien.NgaySinh = nv.NgaySinh;
                            nhanvien.LuongCoBan = nv.LuongCoBan;
                            nhanvien.SoNgayCong = nv.SoNgayCong;
                            nhanvien.TienThuong = nv.TienThuong;
                            nhanvien.PhuCap = nv.PhuCap;
                            nhanvien.ChucVu = nv.ChucVu;
                            nhanvien.PhongBan = nv.PhongBan;
                        }
                    }
                    //xóa tất cả dữ liệu trong file
                    File.WriteAllText("data.txt", string.Empty);
                    //lưu từng phần tử vào file
                    foreach (NhanVien nhanvien in listNV)
                    {
                        DocFile.FileLuu(nhanvien);
                    }
                    
                    Console.WriteLine("\nNhân viên có ID = {0} cập nhật dữ liệu thành công", ID);
                }
                else
                {
                    Console.WriteLine("\nNhân viên có ID = {0} không tồn tại", ID);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        //Hàm xóa nhân viên 
        public void XoaNhanVien(int ID)
        {
            List<NhanVien> listNV = DocFile.FileDoc();
            if (ID == -1)
            {
                // Xóa toàn bộ nhân viên
                if (listNV.Count > 0)
                {
                    listNV.Clear();
                    File.WriteAllText("data.txt", string.Empty);
                    Console.WriteLine("\nĐã xóa toàn bộ nhân viên.");
                }
                else
                {
                    Console.WriteLine("Danh sách nhân viên đã trống, không có gì để xóa.");
                }
            }
            else
            {
                // Tìm nhân viên có mã nhân viên nhập từ người dùng trong danh sách nhân viên
                NhanVien nhanVien = null;

                // Duyệt qua danh sách nhân viên để tìm nhân viên cần xóa
                foreach (NhanVien nv in listNV)
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
                    listNV.Remove(nhanVien);
                    //xóa tất cả dữ liệu trong file
                    File.WriteAllText("data.txt", string.Empty);
                    //lưu từng phần tử vào file
                    foreach (NhanVien nv in listNV)
                    {
                        DocFile.FileLuu(nv);
                    }
                    Console.WriteLine("\nNhân viên có ID = {0} đã được xóa.", ID);
                }
                else
                {
                    Console.WriteLine("\nKhông tìm thấy nhân viên có ID = {0}. Xóa không thành công.", ID);
                }
            }
        }

        //Hàm tìm kiếm nhân viên theo ID 
        public NhanVien TimKiemTheoID(int ID)
        {
            NhanVien searchResult = null;
            //Duyệt Nhân viên trong Danh sách Nhân viên
            List<NhanVien> listNV = DocFile.FileDoc();
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
            List<NhanVien> listNV = DocFile.FileDoc();
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
        public List<NhanVien> TimNhanVienLonTuoiNhat()
        {
            try
            {
                List<NhanVien> listNV = DocFile.FileDoc();
                List<NhanVien> result = new List<NhanVien>();
                // Lấy ngày hiện tại
                DateTime currentDate = DateTime.Now;
                // Khởi tạo biến để lưu trữ nhân viên có tuổi lớn nhất
                NhanVien oldestPerson = null;
                // Khởi tạo biến để lưu trữ độ chênh lệch thời gian lớn nhất
                TimeSpan maxAge = TimeSpan.Zero;
                // Duyệt qua danh sách nhân viên
                foreach (NhanVien nv in listNV)
                {
                    string BirthFormat = nv.NgaySinh.Substring(3, 2) + "/" + nv.NgaySinh.Substring(0, 2) + "/" + nv.NgaySinh.Substring(6, 4);
                    // Chuyển ngày sinh của nhân viên thành kiểu DateTime
                    DateTime NgaySinh = Convert.ToDateTime(BirthFormat);
                    // Tính độ chênh lệch thời gian giữa ngày hiện tại và ngày sinh
                    TimeSpan age = currentDate.Subtract(NgaySinh);
                    // So sánh với độ chênh lệch lớn nhất đã biết
                    if (age > maxAge)
                    {
                        //Nếu tìm thấy người có độ chênh lệch lớn hơn, cập nhật thông tin
                        maxAge = age;
                        oldestPerson = nv;
                    }
                }
                foreach (NhanVien nv in listNV)
                {
                    if (nv.NgaySinh == oldestPerson.NgaySinh)
                    {
                        result.Add(nv);
                    }
                }
                // Trả về nhân viên có tuổi lớn nhất
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        //Hàm sắp xếp theo ID giảm dần 
        public void SapXepTheoID()
        {
            List<NhanVien> listNV = DocFile.FileDoc();
            //Nếu chuỗi đầu tiên < chuỗi thứ hai, CompareTo sẽ trả về số âm
            listNV.Sort(delegate (NhanVien nv1, NhanVien nv2) {
                return nv2.ID.CompareTo(nv1.ID);
            });
            //in nhân viên ra màn hình
            HienThiNhanVien(listNV);
            //xóa dữ liệu ban đầu trong file sorted_data
            File.WriteAllText("sorted_data.txt", string.Empty);
            //lưu từng phần tử vào file
            foreach (NhanVien nv in listNV)
            {
                DocFile.FileSapXep(nv);
            }
        }

        //Hàm sắp xếp theo tên tăng dần
        public void SapXepTheoTen()
        {
            List<NhanVien> listNV = DocFile.FileDoc();
            //Nếu chuỗi đầu tiên > chuỗi thứ hai, CompareTo sẽ trả về số dương
            listNV.Sort(delegate (NhanVien nv1, NhanVien nv2) {
                //nếu trùng tên thì sắp theo ID giảm dần
                //tách chuỗi họ tên để tìm ra tên
                string[] name1 = nv1.HoTen.Split(" ");
                string[] name2 = nv2.HoTen.Split(" ");
                //dùng length để truy xuất ra phần tử tên có chỉ số cuối c
                if (name1[name1.Length-1].CompareTo(name2[name2.Length - 1]) == 0)
                {
                    return nv2.ID.CompareTo(nv1.ID);
                }
                return name1[name1.Length - 1].CompareTo(name2[name2.Length - 1]);
            });
            //in nhân viên ra màn hình
            HienThiNhanVien(listNV);
            //xóa dữ liệu ban đầu trong file sorted_data
            File.WriteAllText("sorted_data.txt", string.Empty);
            //lưu từng phần tử vào file
            foreach (NhanVien nv in listNV)
            {
                DocFile.FileSapXep(nv);
            }
        }

        //Hàm tính lương nhân viên
        public string TinhLuong(int ID)
        {
            List<NhanVien> listNV = DocFile.FileDoc();
            NhanVien nv = null;
            //duyệt qua các nhân viên
            foreach (NhanVien nvID in listNV)
            {
                //nếu nhân viên có ID trùng với ID nhập vào thì cập nhật Nhân Viên đó vào biến nv
                if (nvID.ID == ID)
                {
                    nv = nvID;
                    break;
                }
            }
            //nếu đã cập nhật Nhân Viên vào biến nv thì bắt đầu tính lương
            if (nv != null)
            {
                long luong;
                if (nv.SoNgayCong >= 25)
                    luong = nv.LuongCoBan * nv.SoNgayCong + nv.TienThuong + nv.PhuCap;
                else if (nv.SoNgayCong >= 22)
                    luong = nv.LuongCoBan * nv.SoNgayCong + nv.TienThuong + nv.PhuCap;
                else luong = nv.LuongCoBan * nv.SoNgayCong + nv.PhuCap;
                Console.Write($"Lương của nhân viên là: {luong}");
                return "";
            }
            else
            {
                Console.WriteLine("Không tìm thấy nhân viên có ID = {0}", ID);
                return "";
            }
        }

        //Hàm hiển thị danh sách nhân viên 
        public void HienThiNhanVien(List<NhanVien> listNV)
        {
            try
            {
                //Hiển thị tiêu đề cột
                //{0,-5} cột đầu tiên là cột 0 có độ rộng là 5 và căn lề trái.
                //đổi tiêu đề thành màu đỏ
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0, -5} {1, -25} {2, -15} {3, -15} {4, -15} {5, -15} {6,-15}",
                      "ID", "Họ tên", "Ngày Sinh", "Lương cơ bản", "Số ngày công", "Chức vụ", "Phòng ban");
                // hiển thị danh sách nhân viên nhân viên
                //đổi chữ thành màu trắng
                Console.ForegroundColor = ConsoleColor.White;
                if (listNV != null && listNV.Count > 0)
                {
                    foreach (NhanVien nv in listNV)
                    {
                        Console.WriteLine("{0, -5} {1, -25} {2, -17} {3, -18} {4, -10} {5, -15} {6,-15}",
                                          nv.ID, nv.HoTen, nv.NgaySinh, nv.LuongCoBan, nv.SoNgayCong, nv.ChucVu, nv.PhongBan);
                    }
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        //Hàm hiển thị toàn bộ danh sách nhân viên 
        public void HienThiToanBoNhanVien()
        {
            try
            {
                //Hiển thị tiêu đề cột
                List<NhanVien> listNV = DocFile.FileDoc();
                //đổi tiêu đề thành màu đỏ
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0, -5} {1, -25} {2, -15} {3, -15} {4, -15} {5, -15} {6,-15}",
                      "ID", "Họ tên", "Ngày Sinh", "Lương cơ bản", "Số ngày công", "Chức vụ", "Phòng ban");
                //hiển thị danh sách nhân viên nhân viên
                //đổi chữ thành màu trắng
                Console.ForegroundColor = ConsoleColor.White;
                if (listNV != null && listNV.Count > 0)
                {
                    foreach (NhanVien nv in listNV)
                    {
                        Console.WriteLine("{0, -5} {1, -25} {2, -17} {3, -18} {4, -10} {5, -15} {6,-15}",
                                          nv.ID, nv.HoTen, nv.NgaySinh, nv.LuongCoBan, nv.SoNgayCong, nv.ChucVu, nv.PhongBan);
                    }
                }
                Console.WriteLine();
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        // Hàm tìm kiếm theo phòng ban
        public List<NhanVien> TimKiemPhongBan(int id)
        {
        // Tạo một Danh sách người dùng trả về hợp lệ
            List<NhanVien> searchResult = new List<NhanVien>();
            List<NhanVien> listNV = DocFile.FileDoc();
            if (id == 1) //Phòng Nhân Sự
            {
                foreach (NhanVien nv in listNV)
                {
                    if (nv.PhongBan == "Phòng Nhân Sự")
                        searchResult.Add(nv);
                }
            }
            else if (id == 2) //Phòng Kế Toán
            {
                foreach (NhanVien nv in listNV)
                {
                    if (nv.PhongBan == "Phòng Kế Toán")
                        searchResult.Add(nv);
                }
            }
            else //Phòng Marketing
            {
                foreach (NhanVien nv in listNV)
                {
                    if (nv.PhongBan == "Phòng Marketing")
                        searchResult.Add(nv);
                }
            }
            return searchResult;
        }
    }
}