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
        public async Task<DonorDto> CreateDonorAsync(CreateDonorDto donor)
        {
            var createDonor = new Donor
            {
                Name = donor.Name,
                Email = donor.Email
            };

            var createdDonor = await _donorRepository.CreateDonorAsync(createDonor);
            return MapToResponseDto(createdDonor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Donor deleted with ID: {DonorId}", id);

            return await _donorRepository.DeleteAsync(id);       
        }

        public async Task<IEnumerable<DonorDto>> GetAllAsync()
        {
            var donors = await _donorRepository.GetAllAsync();
           return donors.Select(MapToResponseDto);
        }

        public async Task<DonorDto> GetByEmailAsync(string email)
        {
            var donor = await _donorRepository.GetByEmailAsync(email);
            return donor != null ? MapToResponseDto(donor) : null;
        }

        public async Task<IEnumerable<DonorDto>> getByNameAsync(string name)
        {
            var donors = await _donorRepository.GetByNameAsync(name);
            return donors.Select(MapToResponseDto);
        }
        public async Task<DonorDto> GetByPresentIdAsync(int presentId)
        {
            var donor = await _donorRepository.GetByPresentIdAsync(presentId);
            return donor!=null? MapToResponseDto(donor):null;
        }
        public async Task<DonorDto> UpdateAsync(UpdateDonorDto donor)
        {
            var existDonor = await _donorRepository.GetByIdAsync(donor.Id);
            if (existDonor == null) return null;

            if (donor.Email != null) existDonor.Email = donor.Email;
            if (donor.Name!=null) existDonor.Name = donor.Name;

            var aupdatedDonor = await _donorRepository.UpdateAsync(existDonor);
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