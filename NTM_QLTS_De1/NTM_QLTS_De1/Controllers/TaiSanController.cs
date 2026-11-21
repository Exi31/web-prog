using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTM_QLTS_De1.Models;

namespace NTM_QLTS_De1.Controllers
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
        public IActionResult Create()
        {
            LoadLoai();
            return View("Form", new TaiSanFormVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaiSanFormVM m)
        {
            if (!ModelState.IsValid)
            {
                LoadLoai(m.MaLoai);
                return View("Form", m);
            }

            var ts = new TaiSan
            {
                MaLoai = m.MaLoai,
                TenTs = m.TenTs.Trim(),
                NguyenGia = m.NguyenGia,
                Anh = m.Anh
            };

            db.Set<TaiSan>().Add(ts);
            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { maLoai = m.MaLoai });
        }
    }
}
