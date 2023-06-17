using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
