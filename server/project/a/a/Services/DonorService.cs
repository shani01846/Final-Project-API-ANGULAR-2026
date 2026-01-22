using a.Dto;
using a.Interfaces;
using a.Models;
using NET.Models;

namespace a.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepository;

        private readonly ILogger<DonorService> _logger;

        public DonorService(ILogger<DonorService> logger, IDonorRepository repository )
        {
            _logger = logger;
            _donorRepository = repository; 
        }
        public async Task<DonorDto> CreateDonor(CreateDonorDto donor)
        {
            var createDonor = new Donor
            {
                Name = donor.Name,
                Email = donor.Email
            };

            var createdDonor = await _donorRepository.CreateDonor(createDonor);
            return MapToResponseDto(createdDonor);
        }

        public async Task<bool> Delete(int id)
        {
            return await _donorRepository.Delete(id);       
        }

        public async Task<IEnumerable<DonorDto>> GetAllAsync()
        {
            var donors = await _donorRepository.GetAllAsync();
           return donors.Select(MapToResponseDto);
        }

        public async Task<DonorDto> GetByEmail(string email)
        {
            var donor = await _donorRepository.GetByEmail(email);
            return donor != null ? MapToResponseDto(donor) : null;

        }

        public async Task<IEnumerable<DonorDto>> getByName(string name)
        {
            var donors = await _donorRepository.getByName(name);
            return donors.Select(MapToResponseDto);
        }
        public async Task<DonorDto> GetByPresentId(int presentId)
        {
            var donor = await _donorRepository.GetByPresentId(presentId);
            return donor!=null? MapToResponseDto(donor):null;
        }
        public async Task<DonorDto> Update(UpdateDonorDto donor)
        {
            var existDonor = await _donorRepository.GetByIdAsync(donor.Id);
            if (existDonor == null) return null;

            if (donor.Email != null) existDonor.Email = donor.Email;
            if (donor.Name!=null) existDonor.Name = donor.Name;

            var aupdatedDonor = await _donorRepository.Update(existDonor);
            return aupdatedDonor != null ? MapToResponseDto(aupdatedDonor) : null;

        }
        private static DonorDto MapToResponseDto(Donor donor)
        {
            return new DonorDto
            {
                Id = donor.Id,
                Name = donor.Name,
                Email = donor.Email,
                Presents = donor.Presents?.Select(p => new PresentWithDonorDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                }).ToList() ?? new List<PresentWithDonorDto>()
            };
        }


        public async Task<DonorDto?> GetByIdAsync(int id)
        {
            var donor = await _donorRepository.GetByIdAsync(id);
            return donor != null ? MapToResponseDto(donor) : null;
        }
    }
}