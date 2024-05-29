namespace PhoneRegister.Models
{
    public class PhoneEntry
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime LastCallDate { get; set; }
        public int PhoneListId { get; set; }
        public PhoneList PhoneList { get; set; }
    }
}
