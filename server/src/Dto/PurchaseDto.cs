using NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace a.Dto;

    public class PurchaseDto
    {
        public int Id { get; set; }

    [Required(ErrorMessage = "יש להזין מזהה משתמש")]
    public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "יש להזין מזהה מתנה")]

    public int PresentId { get; set; }
        public PresentForPurchaseDto Present { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "מספר הכרטיסים חייב להיות גדול מ־0")]

    public int NumOfTickets { get; set; }
        public string Created_At { get; set; } = string.Empty;
        public bool IsDraft { get; set; } = true;
    public User? user { get; set; }
}

public class PurchaseDtoForPresent
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public UserDto user { get; set; }
    public int NumOfTickets { get; set; }
}
public class UpdatePurchaseDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? PresentId { get; set; }
    public int? NumOfTickets { get; set; }
    public string Created_At { get; set; } = string.Empty;
    public bool? IsDraft { get; set; } = true;
}


