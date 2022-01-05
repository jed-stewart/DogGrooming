namespace Data.Domain
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Prefix { get; set; } = "";
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public DateTimeOffset DateOfBirth { get; set; }


    }
}
