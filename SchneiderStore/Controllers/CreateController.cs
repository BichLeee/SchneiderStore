using Microsoft.AspNetCore.Mvc;
using SchneiderStore.DAL;
using SchneiderStore.Models;
using System.Text.Json;

namespace SchneiderStore.Controllers
{
	public class CreateController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DoCreate(Order order)
		{
			order.Timestamp= DateTime.Now;

			bool result = false;
			try
			{
				result = SStoreDAL.getInstance().addOrder(order);
			}
			catch(Exception ex){}
			if (result)
			{
				TempData["resultMessage"] = true;
			}
			else
			{
				TempData["resultMessage"] = false;
			}
			return RedirectToAction("Index","Home",new {add_result= result });
		}

		
	}
}
