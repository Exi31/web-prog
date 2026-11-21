using Microsoft.AspNetCore.Mvc;
using NTM_QLTS_De2.Models;
using System.Diagnostics;

namespace NTM_QLTS_De2.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLTaiSanContext db;

        public HomeController(QLTaiSanContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int? maLoai)
        {
            var ds = db.Set<TaiSan>().AsQueryable();
            ViewBag.TieuDe = "NGUYỄN TUẤN MINH";

            if (maLoai.HasValue)
            {
                var loai = db.Set<LoaiTaiSan>().Find(maLoai.Value);

                if (loai == null) return NotFound();

                ViewBag.TieuDe = loai.TenLoai;
                ds = ds.Where(x => x.MaLoai == maLoai.Value);
            } else
            {
                ds = ds.Where(x => x.NguyenGia >= 20);
            }

            return View(ds.ToList());
        }

        public IActionResult Loc(int maLoai)
        {
            var loai = db.Set<LoaiTaiSan>().Find(maLoai);

            ViewBag.TieuDe = loai.TenLoai;

            var ds = db.Set<TaiSan>().Where(x => x.MaLoai == maLoai).ToList();

            return PartialView("NguyenTuanMinh_MainContent", ds);
        }
    }
}
