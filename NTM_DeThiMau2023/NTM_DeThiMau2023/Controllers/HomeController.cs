using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTM_DeThiMau2023.Models;
using System.Diagnostics;

namespace NTM_DeThiMau2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QLHangHoaContext _db;

        public HomeController(ILogger<HomeController> logger, QLHangHoaContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var danhSach = await _db.Set<HangHoa>().AsNoTracking().Where(x => x.Gia >= 100).OrderBy(x => x.MaHang).ToListAsync();

            return View(danhSach);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> LocTheoLoai(int maLoai)
        {
            var danhSach = await _db.Set<HangHoa>().AsNoTracking()
                .Where(x => x.MaLoai == maLoai && x.Gia >= 100)
                .OrderBy(x => x.MaHang)
                .ToListAsync();

            return PartialView("_DanhSachHangHoa", danhSach);
        }
    }
}
