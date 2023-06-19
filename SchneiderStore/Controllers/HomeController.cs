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

			return View();
		}

		// 
		// GET: /Home/Welcome/ 

		public async Task<IActionResult> Data()
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

		[HttpPost]
		public async Task<IActionResult> Find(string SalesOrder, string SalesOrderItem)
		{
			Order order = new Order();
			try
			{
				order = SStoreDAL.getInstance().findOrder(SalesOrder, SalesOrderItem);
				if (order != null)
				{
					string json = JsonSerializer.Serialize(order);
					return Content(json, "application/json");
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
	}
}