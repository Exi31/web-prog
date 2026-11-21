using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTM_QLTS_De2.Models;

namespace NTM_QLTS_De2.Controllers
{
    public class TaiSanController : Controller
    {
        private readonly QLTaiSanContext db;

        public TaiSanController(QLTaiSanContext db)
        {
            this.db = db;
        }

        void LoadLoai(int id = 0)
        {
            ViewBag.Loai = new SelectList(db.Set<LoaiTaiSan>().ToList(), "MaLoai", "TenLoai", id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ts = db.Set<TaiSan>().Find(id);

            if (ts == null) return NotFound();

            var m = new TaiSanFormVM
            {
                MaTs = ts.MaTs,
                MaLoai = ts.MaLoai,
                TenTs = ts.TenTs,
                NguyenGia = ts.NguyenGia,
                Anh = ts.Anh
            };

            LoadLoai(m.MaLoai);
            return View("Form", m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaiSanFormVM m)
        {
            if (!ModelState.IsValid)
            {
                LoadLoai(m.MaLoai);
                return View("Form", m);
            }

            var ts = db.Set<TaiSan>().Find(m.MaTs);

            if (ts == null) return NotFound();

            ts.MaLoai = m.MaLoai;
            ts.TenTs = m.TenTs.Trim();
            ts.NguyenGia = m.NguyenGia;
            ts.Anh = m.Anh;

            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { maLoai = m.MaLoai });
        }
    }
}
