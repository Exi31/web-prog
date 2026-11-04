
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTM_TH_Lab01.Data;

namespace NTM_TH_Lab01.ViewComponents;

public class MajorViewComponent(SchoolContext db) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var majors = await db.Majors
            .OrderBy(x => x.MajorName)
            .ToListAsync();

        return View("RenderMajor", majors);
    }
}