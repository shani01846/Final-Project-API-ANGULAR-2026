using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace a.Dto;

    public class PresentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Price { get; set; }
        public string DonorName { get; set; } = string.Empty;
        public string CategoryName {  get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int NumOfPurchases { get; set; }
    }

    public class CreatePresentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Price { get; set; }
        public int CategoryId { get; set;}
        public int DonorId { get; set; } 
    }
    public class UpdatePresentDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int? Price { get; set; }
        public int? CategoryId { get; set; }
        public int DonorId {  get; set; }
    }
    public class PresentWithDonorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
}
public class PresentForPurchaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Price { get; set; }
    public string DonorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}

