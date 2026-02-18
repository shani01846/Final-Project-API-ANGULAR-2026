using System.ComponentModel.DataAnnotations;

namespace NET.Models
{
    public class Purchase
    {
         public int Id { get; set; }
        [Required(ErrorMessage = "יש להזין מזהה משתמש")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "יש להזין מזהה מתנה")]
        public int PresentId { get; set; }
        public Present Present { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "מספר הכרטיסים חייב להיות גדול מ־0")]
        public int NumOfTickets { get; set; } 
        public bool IsDraft { get; set; } = false;
        public string Created_At { get; set; } = string.Empty;
        //public ICollection<Present> presents { get; set; } = new List<Present>();
    }
}
