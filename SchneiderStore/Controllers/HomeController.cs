using Microsoft.AspNetCore.Mvc;
using SchneiderStore.DAL;
using SchneiderStore.Models;
using System;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SchneiderStore.Controllers
{
	public class HomeController : Controller
	{
		//private readonly ILogger<HomeController> _logger;

		public IActionResult Index()
		{
			//         List<Order> orders = new List<Order>();
			//try
			//         {
			//	orders = storeDAO.GetAllOrders();
			//	if (orders == null)
			//	{
			//		return NotFound();
			//	}
			//}
			//         catch (Exception ex) 
			//         {
			//             TempData["errorMessage"] = ex.Message;
			//         }
			//return View(orders);

			return View();
		}

		// 
		// GET: /Home/Welcome/ 

		public IActionResult Data()
		{
			List<Order> orders = new List<Order>();
			try
			{
				orders = SStoreDAL.getInstance().GetAllOrders();
				if (orders != null)
				{
					string json = JsonSerializer.Serialize(orders);
					return Content(json, "application/json");
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}