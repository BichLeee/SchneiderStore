namespace SchneiderStore.Models
{
    public class Order
    {
		public String SalesOrder { get; set; }
		public String SalesOrderItem { get; set; }
		public String WorkOrder { get; set; }
        public String ProductID { get; set; }
        public String ProductDes { get; set; }
        public decimal OrderQty { get; set; }
        public String OrderStatus { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
