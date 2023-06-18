using Microsoft.AspNetCore.Mvc;
using SchneiderStore.DAL;
using SchneiderStore.Models;

namespace SchneiderStore.Controllers
{
	public class CreateController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Order order)
		{
			order.Timestamp= DateTime.Now;

			bool result = SStoreDAL.getInstance().addOrder(order);
			if (result)
			{
				TempData["resultMessage"] = "Action Successfull ~~";
			}
			else
			{
				TempData["resultMessage"] = "Action Fail !!!";
			}
			return RedirectToAction("Index","Home",new {add_result= result });
		}
	}
}
