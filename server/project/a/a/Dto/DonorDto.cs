using NET.Models;

namespace a.Dto
{
    public class DonorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<PresentWithDonorDto> Presents { get; set; } = new List<PresentWithDonorDto>();
    }

    public class UpdateDonorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CreateDonorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

   
}
