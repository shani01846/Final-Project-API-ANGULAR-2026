namespace NET.Models
{
    public class Purchase
    {
         public int Id { get; set; }
         public int UserId { get; set; }
        public User User { get; set; }
         public int PresentId { get; set; }
        public Present Present { get; set; }
         public int NumOfTickets { get; set; }
        public bool IsDraft { get; set; } = false;
        public string Created_At { get; set; } = string.Empty;
        //public ICollection<Present> presents { get; set; } = new List<Present>();
    }
}
