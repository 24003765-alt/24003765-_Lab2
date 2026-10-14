using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab02_OOPBasics
{
    // Bài 1-5: Lớp SinhVien, constructor, đóng gói, nghiệp vụ và danh sách.
    public class SinhVien
    {
        private string _hoTen;
        private double _diemTB;

        public string MaSV { get; set; }
        public string HoTen
        {
            get => _hoTen;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Họ tên không được để trống.");
                _hoTen = value.Trim();
            }
        }

        public DateTime NgaySinh { get; set; }
        public double DiemTB
        {
            get => _diemTB;
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(DiemTB), "Điểm trung bình phải nằm trong khoảng 0-10.");
                _diemTB = value;
            }
        }

        // Constructor không tham số
        public SinhVien()
        {
            MaSV = "Chua co";
            HoTen = "Chua co";
            NgaySinh = DateTime.Today;
            DiemTB = 0;
        }

        // Constructor có tham số
        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, double diemTB)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Mã sinh viên không được để trống.");
            MaSV = maSV.Trim();
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiemTB = diemTB;
        }

        public int TinhTuoi()
        {
            int tuoi = DateTime.Today.Year - NgaySinh.Year;
            if (NgaySinh.Date > DateTime.Today.AddYears(-tuoi)) tuoi--;
            return tuoi;
        }

        public string XepLoai()
        {
            if (DiemTB >= 9) return "Xuất sắc";
            if (DiemTB >= 8) return "Giỏi";
            if (DiemTB >= 6.5) return "Khá";
            if (DiemTB >= 5) return "Trung bình";
            return "Yếu";
        }

        public void HienThi()
        {
            Console.WriteLine("{0,-10} | {1,-22} | {2:dd/MM/yyyy} | {3,5:N2} | {4,-12} | {5,3}",
                MaSV, HoTen, NgaySinh, DiemTB, XepLoai(), TinhTuoi());
        }
    }

    // Bài 6: Lớp SanPham
    public class SanPham
    {
        private decimal _donGia;
        private int _soLuong;

        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia
        {
            get => _donGia;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(DonGia), "Đơn giá không được âm.");
                _donGia = value;
            }
        }
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(SoLuong), "Số lượng không được âm.");
                _soLuong = value;
            }
        }

        public SanPham(string maSP, string tenSP, decimal donGia, int soLuong)
        {
            if (string.IsNullOrWhiteSpace(maSP)) throw new ArgumentException("Mã sản phẩm không được trống.");
            if (string.IsNullOrWhiteSpace(tenSP)) throw new ArgumentException("Tên sản phẩm không được trống.");
            MaSP = maSP.Trim();
            TenSP = tenSP.Trim();
            DonGia = donGia;
            SoLuong = soLuong;
        }

        public decimal ThanhTien() => DonGia * SoLuong;
        public void HienThi() =>
            Console.WriteLine($"{MaSP,-10} | {TenSP,-24} | {DonGia,14:N0} | {SoLuong,8} | {ThanhTien(),16:N0}");
    }

    // Bài 7: Lớp TaiKhoanNganHang
    public class TaiKhoanNganHang
    {
        public string SoTaiKhoan { get; }
        public string ChuTaiKhoan { get; }
        public decimal SoDu { get; private set; }

        public TaiKhoanNganHang(string soTaiKhoan, string chuTaiKhoan, decimal soDuBanDau = 0)
        {
            if (string.IsNullOrWhiteSpace(soTaiKhoan)) throw new ArgumentException("Số tài khoản không được trống.");
            if (string.IsNullOrWhiteSpace(chuTaiKhoan)) throw new ArgumentException("Chủ tài khoản không được trống.");
            if (soDuBanDau < 0) throw new ArgumentOutOfRangeException(nameof(soDuBanDau), "Số dư ban đầu không được âm.");
            SoTaiKhoan = soTaiKhoan.Trim();
            ChuTaiKhoan = chuTaiKhoan.Trim();
            SoDu = soDuBanDau;
        }

        public void NapTien(decimal soTien)
        {
            if (soTien <= 0) throw new ArgumentOutOfRangeException(nameof(soTien), "Số tiền nạp phải lớn hơn 0.");
            SoDu += soTien;
        }

        public bool RutTien(decimal soTien)
        {
            if (soTien <= 0) throw new ArgumentOutOfRangeException(nameof(soTien), "Số tiền rút phải lớn hơn 0.");
            if (soTien > SoDu) return false;
            SoDu -= soTien;
            return true;
        }

        public bool ChuyenTien(TaiKhoanNganHang dich, decimal soTien)
        {
            if (dich == null) throw new ArgumentNullException(nameof(dich));
            if (soTien <= 0) throw new ArgumentOutOfRangeException(nameof(soTien), "Số tiền chuyển phải lớn hơn 0.");
            if (soTien > SoDu) return false;
            SoDu -= soTien;
            dich.SoDu += soTien;
            return true;
        }

        public void HienThi() =>
            Console.WriteLine($"STK: {SoTaiKhoan} | Chủ TK: {ChuTaiKhoan} | Số dư: {SoDu:N0} VNĐ");
    }

    // Bài 8: Lớp NhanVien
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public decimal LuongCoBan { get; set; }
        public double HeSoLuong { get; set; }
        public decimal PhuCap { get; set; }

        public NhanVien(string maNV, string hoTen, decimal luongCoBan, double heSoLuong, decimal phuCap)
        {
            if (string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(hoTen))
                throw new ArgumentException("Mã và họ tên nhân viên không được trống.");
            if (luongCoBan < 0 || heSoLuong < 0 || phuCap < 0)
                throw new ArgumentException("Lương, hệ số và phụ cấp không được âm.");
            MaNV = maNV.Trim();
            HoTen = hoTen.Trim();
            LuongCoBan = luongCoBan;
            HeSoLuong = heSoLuong;
            PhuCap = phuCap;
        }

        public decimal TinhLuong() => LuongCoBan * (decimal)HeSoLuong + PhuCap;
        public void HienThi() =>
            Console.WriteLine($"{MaNV,-10} | {HoTen,-22} | {LuongCoBan,14:N0} | {HeSoLuong,7:N2} | {PhuCap,12:N0} | {TinhLuong(),15:N0}");
    }

    // Bài 9: Lớp HinhChuNhat
    public class HinhChuNhat
    {
        public double ChieuDai { get; set; }
        public double ChieuRong { get; set; }

        public HinhChuNhat(double chieuDai, double chieuRong)
        {
            if (chieuDai <= 0 || chieuRong <= 0)
                throw new ArgumentException("Chiều dài và chiều rộng phải lớn hơn 0.");
            ChieuDai = chieuDai;
            ChieuRong = chieuRong;
        }

        public double TinhChuVi() => 2 * (ChieuDai + ChieuRong);
        public double TinhDienTich() => ChieuDai * ChieuRong;
    }

    public class Program
    {
        private static readonly List<SinhVien> DanhSachSinhVien = new List<SinhVien>();

        public static void Main()
        {
            TaoDuLieuMau();
            while (true)
            {
                Console.WriteLine("\n========== LAB 2 - LẬP TRÌNH .NET ==========");
                Console.WriteLine("1. Hiển thị danh sách sinh viên (Bài 1-5)");
                Console.WriteLine("2. Quản lý sản phẩm (Bài 6)");
                Console.WriteLine("3. Tài khoản ngân hàng (Bài 7)");
                Console.WriteLine("4. Tính lương nhân viên (Bài 8)");
                Console.WriteLine("5. Tính chu vi, diện tích hình chữ nhật (Bài 9)");
                Console.WriteLine("6. Quản lý sinh viên: thêm/sửa/xóa/tìm/sắp xếp/thống kê (Bài 10)");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");
                string chon = Console.ReadLine() ?? "";

                try
                {
                    switch (chon)
                    {
                        case "1": MenuSinhVienCoBan(); break;
                        case "2": DemoSanPham(); break;
                        case "3": MenuTaiKhoan(); break;
                        case "4": DemoNhanVien(); break;
                        case "5": DemoHinhChuNhat(); break;
                        case "6": MenuQuanLySinhVien(); break;
                        case "0": return;
                        default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }
        }

        private static void TaoDuLieuMau()
        {
            DanhSachSinhVien.Add(new SinhVien("SV01", "Nguyễn An", new DateTime(2005, 5, 12), 8.2));
            DanhSachSinhVien.Add(new SinhVien("SV02", "Trần Bình", new DateTime(2005, 9, 3), 6.8));
            DanhSachSinhVien.Add(new SinhVien("SV03", "Lê Chi", new DateTime(2004, 12, 20), 9.1));
            DanhSachSinhVien.Add(new SinhVien("SV04", "Phạm Dũng", new DateTime(2005, 2, 14), 4.7));
            DanhSachSinhVien.Add(new SinhVien("SV05", "Hoàng Hà", new DateTime(2004, 7, 7), 7.5));
        }

        private static void InTieuDeSinhVien()
        {
            Console.WriteLine("{0,-10} | {1,-22} | {2,-10} | {3,5} | {4,-12} | {5,3}",
                "Mã SV", "Họ tên", "Ngày sinh", "Điểm", "Xếp loại", "Tuổi");
            Console.WriteLine(new string('-', 88));
        }

        private static void HienThiDanhSach(IEnumerable<SinhVien> ds)
        {
            InTieuDeSinhVien();
            foreach (SinhVien sv in ds) sv.HienThi();
        }

        private static void MenuSinhVienCoBan()
        {
            Console.WriteLine("\nDANH SÁCH SINH VIÊN MẪU");
            HienThiDanhSach(DanhSachSinhVien);
            Console.Write("\nNhập mã sinh viên cần tìm: ");
            string ma = Console.ReadLine() ?? "";
            SinhVien svTim = DanhSachSinhVien.FirstOrDefault(x =>
                x.MaSV.Equals(ma.Trim(), StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(svTim == null ? "Không tìm thấy sinh viên." : "Kết quả:");
            if (svTim != null) { InTieuDeSinhVien(); svTim.HienThi(); }
        }

        private static void MenuQuanLySinhVien()
        {
            while (true)
            {
                Console.WriteLine("\n--- QUẢN LÝ SINH VIÊN ---");
                Console.WriteLine("1. Xem danh sách");
                Console.WriteLine("2. Thêm sinh viên");
                Console.WriteLine("3. Sửa sinh viên");
                Console.WriteLine("4. Xóa sinh viên");
                Console.WriteLine("5. Tìm theo mã");
                Console.WriteLine("6. Sắp xếp điểm giảm dần");
                Console.WriteLine("7. Thống kê xếp loại");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn: ");
                string chon = Console.ReadLine() ?? "";
                switch (chon)
                {
                    case "1": HienThiDanhSach(DanhSachSinhVien); break;
                    case "2": ThemSinhVien(); break;
                    case "3": SuaSinhVien(); break;
                    case "4": XoaSinhVien(); break;
                    case "5": TimSinhVien(); break;
                    case "6": HienThiDanhSach(DanhSachSinhVien.OrderByDescending(x => x.DiemTB)); break;
                    case "7": ThongKeSinhVien(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                }
            }
        }

        private static void ThemSinhVien()
        {
            Console.Write("Mã SV: "); string ma = Console.ReadLine() ?? "";
            if (DanhSachSinhVien.Any(x => x.MaSV.Equals(ma.Trim(), StringComparison.OrdinalIgnoreCase)))
            { Console.WriteLine("Mã sinh viên đã tồn tại."); return; }
            Console.Write("Họ tên: "); string ten = Console.ReadLine() ?? "";
            DateTime ns = NhapNgay("Ngày sinh (dd/MM/yyyy): ");
            double diem = NhapDouble("Điểm trung bình (0-10): ");
            DanhSachSinhVien.Add(new SinhVien(ma, ten, ns, diem));
            Console.WriteLine("Đã thêm sinh viên.");
        }

        private static void SuaSinhVien()
        {
            SinhVien sv = TimTheoMaNhap();
            if (sv == null) { Console.WriteLine("Không tìm thấy sinh viên."); return; }
            Console.Write($"Họ tên mới ({sv.HoTen}): ");
            string ten = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(ten)) sv.HoTen = ten;
            Console.Write($"Ngày sinh mới dd/MM/yyyy (hiện tại {sv.NgaySinh:dd/MM/yyyy}): ");
            string ngay = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(ngay))
                sv.NgaySinh = DateTime.ParseExact(ngay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Console.Write($"Điểm mới (hiện tại {sv.DiemTB}): ");
            string diem = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(diem)) sv.DiemTB = double.Parse(diem);
            Console.WriteLine("Đã cập nhật sinh viên.");
        }

        private static void XoaSinhVien()
        {
            SinhVien sv = TimTheoMaNhap();
            if (sv == null) { Console.WriteLine("Không tìm thấy sinh viên."); return; }
            DanhSachSinhVien.Remove(sv);
            Console.WriteLine("Đã xóa sinh viên.");
        }

        private static void TimSinhVien()
        {
            SinhVien sv = TimTheoMaNhap();
            if (sv == null) Console.WriteLine("Không tìm thấy sinh viên.");
            else { InTieuDeSinhVien(); sv.HienThi(); }
        }

        private static SinhVien TimTheoMaNhap()
        {
            Console.Write("Nhập mã sinh viên: ");
            string ma = Console.ReadLine() ?? "";
            return DanhSachSinhVien.FirstOrDefault(x =>
                x.MaSV.Equals(ma.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static void ThongKeSinhVien()
        {
            Console.WriteLine($"Tổng số sinh viên: {DanhSachSinhVien.Count}");
            foreach (var nhom in DanhSachSinhVien.GroupBy(x => x.XepLoai()).OrderBy(x => x.Key))
                Console.WriteLine($"{nhom.Key}: {nhom.Count()} sinh viên");
            if (DanhSachSinhVien.Count > 0)
                Console.WriteLine($"Điểm trung bình của lớp: {DanhSachSinhVien.Average(x => x.DiemTB):N2}");
        }

        private static void DemoSanPham()
        {
            List<SanPham> ds = new List<SanPham>
            {
                new SanPham("SP01", "Bàn phím", 350000m, 5),
                new SanPham("SP02", "Chuột máy tính", 180000m, 8),
                new SanPham("SP03", "Tai nghe", 520000m, 3)
            };
            Console.WriteLine("\nDANH SÁCH SẢN PHẨM");
            Console.WriteLine($"{"Mã SP",-10} | {"Tên sản phẩm",-24} | {"Đơn giá",14} | {"Số lượng",8} | {"Thành tiền",16}");
            foreach (SanPham sp in ds) sp.HienThi();
            Console.WriteLine($"Tổng giá trị hàng: {ds.Sum(x => x.ThanhTien()):N0} VNĐ");
        }

        private static void MenuTaiKhoan()
        {
            TaiKhoanNganHang a = new TaiKhoanNganHang("10001", "Nguyễn An", 2000000m);
            TaiKhoanNganHang b = new TaiKhoanNganHang("10002", "Trần Bình", 1000000m);
            while (true)
            {
                Console.WriteLine("\nTÀI KHOẢN NGÂN HÀNG");
                Console.WriteLine("1. Xem số dư  2. Nạp tiền  3. Rút tiền  4. Chuyển tiền  0. Quay lại");
                Console.Write("Chọn: ");
                string chon = Console.ReadLine() ?? "";
                if (chon == "0") return;
                if (chon == "1") { a.HienThi(); b.HienThi(); }
                else if (chon == "2")
                {
                    Console.Write("Nạp vào tài khoản 1 số tiền: ");
                    a.NapTien(NhapDecimal());
                    Console.WriteLine("Nạp tiền thành công."); a.HienThi();
                }
                else if (chon == "3")
                {
                    Console.Write("Rút từ tài khoản 1 số tiền: ");
                    bool ok = a.RutTien(NhapDecimal());
                    Console.WriteLine(ok ? "Rút tiền thành công." : "Số dư không đủ.");
                    a.HienThi();
                }
                else if (chon == "4")
                {
                    Console.Write("Chuyển từ TK 1 sang TK 2 số tiền: ");
                    bool ok = a.ChuyenTien(b, NhapDecimal());
                    Console.WriteLine(ok ? "Chuyển tiền thành công." : "Số dư không đủ.");
                    a.HienThi(); b.HienThi();
                }
                else Console.WriteLine("Lựa chọn không hợp lệ.");
            }
        }

        private static void DemoNhanVien()
        {
            List<NhanVien> ds = new List<NhanVien>
            {
                new NhanVien("NV01", "Nguyễn Minh", 5000000m, 2.2, 500000m),
                new NhanVien("NV02", "Trần Lan", 4800000m, 1.8, 350000m),
                new NhanVien("NV03", "Lê Hùng", 6000000m, 2.5, 700000m)
            };
            Console.WriteLine("\nBẢNG LƯƠNG NHÂN VIÊN");
            Console.WriteLine($"{"Mã NV",-10} | {"Họ tên",-22} | {"Lương cơ bản",14} | {"Hệ số",7} | {"Phụ cấp",12} | {"Thực lĩnh",15}");
            foreach (NhanVien nv in ds) nv.HienThi();
            Console.WriteLine($"Tổng lương phải trả: {ds.Sum(x => x.TinhLuong()):N0} VNĐ");
        }

        private static void DemoHinhChuNhat()
        {
            double dai = NhapDouble("Nhập chiều dài: ");
            double rong = NhapDouble("Nhập chiều rộng: ");
            HinhChuNhat h = new HinhChuNhat(dai, rong);
            Console.WriteLine($"Chu vi: {h.TinhChuVi():N2}");
            Console.WriteLine($"Diện tích: {h.TinhDienTich():N2}");
        }

        private static DateTime NhapNgay(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string s = Console.ReadLine() ?? "";
                if (DateTime.TryParseExact(s, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime ngay))
                    return ngay;
                Console.WriteLine("Ngày không hợp lệ. Ví dụ: 12/05/2005");
            }
        }

        private static double NhapDouble(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string s = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
                if (double.TryParse(s, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double n))
                    return n;
                Console.WriteLine("Vui lòng nhập số hợp lệ.");
            }
        }

        private static decimal NhapDecimal()
        {
            while (true)
            {
                string s = (Console.ReadLine() ?? "").Trim().Replace(",", "");
                if (decimal.TryParse(s, out decimal n) && n > 0) return n;
                Console.Write("Vui lòng nhập số tiền lớn hơn 0: ");
            }
        }
    }
}
