namespace Domain_Name_Ownership_MVC.Models
{
    //Represents an ownership of a domain
    public class Ownership
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int DomainId { get; set; }

        public Domain Domain { get; set; }
    }
}
