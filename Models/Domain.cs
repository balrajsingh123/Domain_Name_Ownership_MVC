namespace Domain_Name_Ownership_MVC.Models
{//Domain information including domain host
    public class Domain
    {
        public int Id { get; set; }

        public string DomainName { get; set; }

        public string ContactPhone { get; set; }

        public int DomainHostId { get; set; }

        public DomainHost DomainHost { get; set; }

    }
}
