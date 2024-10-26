namespace Book_api.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Addres { get; set; }
        public List<Book> Items { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
