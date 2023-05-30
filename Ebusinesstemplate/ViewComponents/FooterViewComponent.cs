using Ebusinesstemplate.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ebusinesstemplate.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
        Dictionary<string,string> settings= await _context.Settings.ToDictionaryAsync(k=>k.Key,v=>v.Value);
            return View(settings);
        }
    }
}
