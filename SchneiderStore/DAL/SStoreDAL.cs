using Microsoft.AspNetCore.Connections;
using SchneiderStore.Models;
using System.Data;
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
						order.Timestamp = Convert.ToDateTime(reader["Timestamp"]);
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
					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = "spCreateOrder";

					command.Parameters.AddWithValue("@SalesOrder", order.SalesOrder);
					command.Parameters.AddWithValue("@SalesOrderItem", order.SalesOrderItem);
					command.Parameters.AddWithValue("@WorkOrder", order.WorkOrder);
					command.Parameters.AddWithValue("@ProductID", order.ProductID);
					command.Parameters.AddWithValue("@ProductDes", order.ProductDes);
					command.Parameters.AddWithValue("@OrderQty", order.OrderQty);
					command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
					command.Parameters.AddWithValue("@Timestamp", Order.getTimeStr(order.Timestamp) );

					result = command.ExecuteNonQuery();
				}
			}
			if (result == 0) { return false; }
			return true;
		}

		public bool deleteOrder(string SalesOrder, string SalesOrderItem)
		{
			int result = 0;

			string ConnectionString = getConnectionString();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = $"Delete From [Order] Where SalesOrder='{SalesOrder}' and SalesOrderItem='{SalesOrderItem}'";

					result = command.ExecuteNonQuery();
				}
			}
			if (result == 0) { return false; }
			return true;
		}

		public Order findOrder(string SalesOrder, string SalesOrderItem)
		{
			string ConnectionString = getConnectionString();

			Order order = new Order();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = $"Select * From [Order] Where SalesOrder='{SalesOrder}' And SalesOrderItem='{SalesOrderItem}'";

					var reader = command.ExecuteReader();
					reader.Read();

					order.SalesOrder = reader["SalesOrder"].ToString();
					order.SalesOrderItem = reader["SalesOrderItem"].ToString();
					order.WorkOrder = reader["WorkOrder"].ToString();
					order.ProductID = reader["ProductID"].ToString();
					order.ProductDes = reader["ProductDes"].ToString();
					order.OrderQty = decimal.Parse(reader["OrderQty"].ToString());
					order.OrderStatus = reader["OrderStatus"].ToString();
					order.Timestamp = Convert.ToDateTime(reader["Timestamp"]);

				}
			}
			return order;
		}

		public bool editOrder(Order order)
		{
			int result = 0;

			string ConnectionString = getConnectionString();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				

				using (var command = connection.CreateCommand())
				{
					command.CommandText =
						$"Update [Order] " +
						$"Set WorkOrder = '{order.WorkOrder}'," +
						$"ProductID = '{order.ProductID}'," +
						$"ProductDes = '{order.ProductDes}'," +
						$"OrderQty = {order.OrderQty}," +
						$"OrderStatus = '{order.OrderStatus}'," +
						$"Timestamp = '{Order.getTimeStr(order.Timestamp)}' " +
						$"Where SalesOrder='{order.SalesOrder}' And SalesOrderItem='{order.SalesOrderItem}';";

					result = command.ExecuteNonQuery();
				}
			}
			if (result == 0) { return false; }
			return true;
		}
	}
}
