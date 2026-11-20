using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using NTM_DeThiMau2023.Models;

namespace NTM_DeThiMau2023.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly QLHangHoaContext _db;

        public HangHoaController(QLHangHoaContext db)
        {
            _db = db;
        }

        private async Task LoadLoaiHangAsync(int selectedId = 0)
        {
            var loaiHangs = await _db.Set<LoaiHang>().AsNoTracking()
                .OrderBy(x => x.MaLoai)
                .ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(loaiHangs, "MaLoai", "TenLoai", selectedId);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLoaiHangAsync();

            return View(new HangHoaCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HangHoaCreateViewModel model)
        {
            bool loaiHopLe = await _db.Set<LoaiHang>().AnyAsync(x => x.MaLoai == model.MaLoai);

            if (!loaiHopLe)
            {
                ModelState.AddModelError(nameof(model.MaLoai), "Loại hàng không tồn tại");
            }

            if (!ModelState.IsValid)
            {
                await LoadLoaiHangAsync(model.MaLoai);
                return View(model);
            }

            var hangHoa = new HangHoa
            {
                MaLoai = model.MaLoai,
                TenHang = model.TenHang.Trim(),
                Gia = model.Gia,
                Anh = model.Anh.Trim()
            };

            _db.Set<HangHoa>().Add(hangHoa);

            await _db.SaveChangesAsync();

            TempData["Success"] = "Thêm hàng hóa thành công!";

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
