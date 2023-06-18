using Microsoft.AspNetCore.Connections;
using SchneiderStore.Models;
using System.Data.SqlClient;
using System.Numerics;

namespace SchneiderStore.DAL
{
	public class SStoreDAL
	{
		IConfiguration Configuration = null;

		private static SStoreDAL instance = null;
		public static SStoreDAL getInstance()
		{
			if (instance == null)
			{
				instance = new SStoreDAL();
			}
			return instance;
		}

		public string getConnectionString()
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

			Configuration = builder.Build();

			return Configuration.GetConnectionString("DefaultConnection");
		}

		public List<Order> GetAllOrders()
		{
			string ConnectionString = getConnectionString();

			List<Order> orders = new List<Order>();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "Select * From [Order]";

					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Order order = new Order();
						order.SalesOrder = reader["SalesOrder"].ToString();
						order.SalesOrderItem = reader["SalesOrderItem"].ToString();
						order.WorkOrder = reader["WorkOrder"].ToString();
						order.ProductID = reader["ProductID"].ToString();
						order.ProductDes = reader["ProductDes"].ToString();
						order.OrderQty = decimal.Parse(reader["OrderQty"].ToString());
						order.OrderStatus = reader["OrderStatus"].ToString();
						order.Timestamp = Convert.ToDateTime(reader["Timestamp"].ToString());
						orders.Add(order);
					}
				}
			}
			return orders;
		}

		public bool addOrder(Order order)
		{
			int result = 0;

			string ConnectionString = getConnectionString();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "spCreateOrder";

					command.Parameters.AddWithValue("@SalesOrder", order.SalesOrder);
					command.Parameters.AddWithValue("@SalesOrderItem", order.SalesOrderItem);
					command.Parameters.AddWithValue("@WorkOrder", order.WorkOrder);
					command.Parameters.AddWithValue("@ProductID", order.ProductID);
					command.Parameters.AddWithValue("@ProductDes", order.ProductDes);
					command.Parameters.AddWithValue("@OrderQty", order.OrderQty);
					command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
					command.Parameters.AddWithValue("@Timestamp", order.Timestamp);
					
					result = command.ExecuteNonQuery();
				}
			}
			if (result == 0) { return false; }
			return true;
		}

	}
}
