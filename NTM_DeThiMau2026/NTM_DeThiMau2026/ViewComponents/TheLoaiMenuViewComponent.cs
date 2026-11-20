
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTM_DeThiMau2026.Models;

namespace NTM_DeThiMau2026.ViewComponents
{
    public class TheLoaiMenuViewComponent : ViewComponent
    {
        private readonly QLNhaSachContext _db;

        public TheLoaiMenuViewComponent(QLNhaSachContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var theLoais = await _db.Set<TheLoai>().AsNoTracking()
                .OrderBy(x => x.MaTl)
                .ToListAsync();

            return View(theLoais);
        }
    }
}
