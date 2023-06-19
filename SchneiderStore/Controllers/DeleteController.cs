using Microsoft.AspNetCore.Mvc;
using SchneiderStore.DAL;

namespace SchneiderStore.Controllers
{
    public class DeleteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> DoDelete(string SalesOrder, string SalesOrderItem)
        {
            Console.WriteLine("hihi");

            bool result = SStoreDAL.getInstance().deleteOrder(SalesOrder, SalesOrderItem);
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
