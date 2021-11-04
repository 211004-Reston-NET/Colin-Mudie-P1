namespace Models
{
    public class LineItemOrder
    {
        public int LineItemOrderId { get; set; }
        public int OrderId { get; set; }
        public int LineItemsId { get; set; }
        public LineItems LineItem { get; set; }
        public Order Order { get; set; }
    }
}