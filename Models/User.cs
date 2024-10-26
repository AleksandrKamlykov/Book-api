namespace Book_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
