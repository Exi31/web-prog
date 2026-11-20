using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NTM_DeThiMau2026.Models;

namespace NTM_DeThiMau2026.Controllers
{
    public class SachController : Controller
    {
        private readonly QLNhaSachContext _db;

        public SachController(QLNhaSachContext db)
        {
            _db = db;
        }

        private async Task LoadTheLoaiAsync(int selectedId = 0)
        {
            var dsTheLoai = await _db.Set<TheLoai>().AsNoTracking()
                .OrderBy(x => x.MaTl)
                .ToListAsync();

            ViewBag.TheLoaiOptions = new SelectList(dsTheLoai, "MaTl", "TenTl", selectedId);
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {
            await LoadTheLoaiAsync();

            return View(new SachCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SachCreateViewModel model)
        {
            bool theLoaiTonTai = await _db.Set<TheLoai>().AnyAsync(x => x.MaTl == model.MaTl);

            if (!ModelState.IsValid)
            {
                await LoadTheLoaiAsync(model.MaTl);
                return View(model);
            }

            var sachMoi = new Sach
            {
                MaTl = model.MaTl,
                TenSach = model.TenSach.Trim(),
                Gia = model.Gia,
                Anh = string.IsNullOrWhiteSpace(model.Anh) ? null : model.Anh.Trim()
            };

            _db.Set<Sach>().Add(sachMoi);

            await _db.SaveChangesAsync();

            TempData["ThongBao"] = "Thêm sách mới thành công!";

            return RedirectToAction("Index", "Home", new { maTl = model.MaTl });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
