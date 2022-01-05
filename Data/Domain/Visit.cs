namespace Data.Domain
{
    public class Visit : BaseEntity
    {
        public string VisitNumber { get; set; } = string.Empty;
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
