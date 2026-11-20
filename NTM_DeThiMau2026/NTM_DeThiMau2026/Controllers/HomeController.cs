using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTM_DeThiMau2026.Models;
using System.Diagnostics;

namespace NTM_DeThiMau2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLNhaSachContext _db;

        public HomeController(QLNhaSachContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int? maTl)
        {
            var query = _db.Set<Sach>().AsNoTracking();
            string tieuDe = "NGUYỄN TUẤN MINH";

            if (maTl.HasValue)
            {
                var theLoai = await _db.Set<TheLoai>().FirstOrDefaultAsync(x => x.MaTl == maTl.Value);

                if (theLoai == null)
                {
                    return NotFound();
                }

                tieuDe = theLoai.TenTl;

                query = query.Where(x => x.MaTl == maTl.Value && x.Gia != null);
            } else
            {
                query = query.Where(x => x.Gia >= 100);
            }

            var danhSach = await query.OrderBy(x => x.MaSach).ToListAsync();

            var model = new SachListViewModel
            {
                TieuDe = tieuDe,
                DanhSach = danhSach
            };

            return View(model);
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

        public async Task<IActionResult> LocTheoTheLoai(int maTl)
        {
            var theLoai = await _db.Set<TheLoai>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.MaTl == maTl);

            if (theLoai == null)
            {
                return NotFound();
            }

            var danhSach = await _db.Set<Sach>().AsNoTracking()
                .Where(x => x.MaTl == maTl && x.Gia != null)
                .OrderBy(x => x.MaSach)
                .ToListAsync();

            var model = new SachListViewModel
            {
                TieuDe = theLoai.TenTl,
                DanhSach = danhSach
            };

            return PartialView("_DanhSachSach", model);
        }
    }
}
