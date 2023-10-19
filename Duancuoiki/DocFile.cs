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
        public static bool FileLuu(NhanVien nv)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter("data.txt", true, Encoding.UTF8);
                using (streamWriter)
                {
                    string line = nv.ID + ";" + nv.HoTen + ";" + nv.NgaySinh + ";" + nv.LuongCoBan + ";" + nv.SoNgayCong + ";" + nv.ChucVu;
                    streamWriter.WriteLine(line);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }
        public static bool FileSapXep(NhanVien nv)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter("sorted_data.txt", true, Encoding.UTF8);
                using (streamWriter)
                {
                    string line = nv.ID + ";" + nv.HoTen + ";" + nv.NgaySinh + ";" + nv.LuongCoBan + ";" + nv.SoNgayCong + ";" + nv.ChucVu;
                    streamWriter.WriteLine(line);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return false;
            }
        }

        public static List<NhanVien> FileDoc()
        {
            List<NhanVien> dsnv = new List<NhanVien>();
            try
            {
                StreamReader streamReader = new StreamReader("data.txt", Encoding.UTF8);
                using (streamReader)
                {
                    string line = streamReader.ReadLine();
                    while (line != null)
                    {
                        string[] arr = line.Split(";");
                        if (arr.Length == 6)
                        {
                            NhanVien nv = new NhanVien();
                            nv.ID = int.Parse(arr[0]);
                            nv.HoTen = arr[1];
                            nv.NgaySinh = arr[2];
                            nv.LuongCoBan = int.Parse(arr[3]);
                            nv.SoNgayCong = int.Parse(arr[4]);
                            nv.ChucVu = arr[5];
                            dsnv.Add(nv);
                        }
                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
            return dsnv;
        }
    }
}