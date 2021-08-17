using Microsoft.AspNetCore.Mvc;

namespace FlexHR.Web.ViewComponents
{
    public class Wrapper:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
