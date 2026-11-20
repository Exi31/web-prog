using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NTM_DeThiMau2023.Models;

namespace NTM_DeThiMau2023.ViewComponents
{
    public class LoaiHangMenuViewComponent : ViewComponent
    {
        private readonly QLHangHoaContext _db;

        public LoaiHangMenuViewComponent(QLHangHoaContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loaiHangs = await _db.Set<LoaiHang>().AsNoTracking().OrderBy(x => x.MaLoai).ToListAsync();

            return View(loaiHangs);
        }
    }
}
