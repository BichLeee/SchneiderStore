using Microsoft.AspNetCore.Mvc;
using SchneiderStore.DAL;

namespace SchneiderStore.Controllers
{
    public class EditController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> DoEDit(Models.Order order)
		{
			Console.WriteLine("hihi");

			bool result = SStoreDAL.getInstance().editOrder(order);
			if (result)
			{
				TempData["resultMessage"] = true;
			}
			else
			{
				TempData["resultMessage"] = false;
			}
			return RedirectToAction("Index", "Home", new { add_result = result });

		}
	}
}
