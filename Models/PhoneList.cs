namespace PhoneRegister.Models
{
    public class PhoneList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<PhoneEntry> PhoneEntries { get; set; }
    }
}
