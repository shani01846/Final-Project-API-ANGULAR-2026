using NET.Models;

namespace a.Dto;

    public class PurchaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int PresentId { get; set; }
        public PresentForPurchaseDto Present {  get; set; }
        public int NumOfTickets { get; set; }
        public string Created_At { get; set; } = string.Empty;
        public bool IsDraft { get; set; } = false;
    }
public class UpdatePurchaseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PresentId { get; set; }
    public PresentForPurchaseDto Present { get; set; }
    public int NumOfTickets { get; set; }
    public string Created_At { get; set; } = string.Empty;
    public bool IsDraft { get; set; } = false;
}


