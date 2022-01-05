namespace Data.Domain
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = string.Empty;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
