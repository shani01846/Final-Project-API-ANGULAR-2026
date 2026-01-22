using a.Models;

namespace NET.Models
{
    public class Present
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        public bool IsLotteryDone { get; set; } = false;
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();


    }
}
