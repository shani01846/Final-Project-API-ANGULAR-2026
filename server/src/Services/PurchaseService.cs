using a.Dto;
using NET.Models;
using StoreApi.Interfaces;

namespace a.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPresentRepository _presentRepository;
        private IUserRepository _userRepository;
        private readonly ILogger<PurchaseService> _logger;
        public PurchaseService(IUserRepository userRepository,IPurchaseRepository purchaseRepository, IPresentRepository presentRepository,ILogger<PurchaseService> logger)
        {
            _purchaseRepository=purchaseRepository;
            _presentRepository=presentRepository;
            _logger=logger;
            _userRepository=userRepository;
        }
        public async Task<PurchaseDto> CreatePurchaseAsync(PurchaseDto createDto)
        {
            if(! await _userRepository.ExistsAsync(createDto.UserId))
            {
                throw new ArgumentException($"User with ID {createDto.UserId} does not exist.");
            }
            var purchase = new Purchase
            {
                UserId = createDto.UserId,
                PresentId = createDto.PresentId,
                NumOfTickets = createDto.NumOfTickets,
                IsDraft = true,
            };
            var createdPurchase = await _purchaseRepository.CreateAsync(purchase);
            _logger.LogInformation("product created with ID: {ProductId}", createdPurchase);
            return MapToResponseDto(createdPurchase);

        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {
            return await _purchaseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PurchaseDto>> GetAllPurchasesAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return purchases.Select(MapToResponseDto);
        }

        public async Task<PurchaseDto?> GetPurchaseByIdAsync(int id)
        {

            var purchase = await _purchaseRepository.GetByIdAsync(id);
            return purchase == null ? null : MapToResponseDto(purchase);
        }

        public async Task<IEnumerable<PurchaseDto>> GetPurchasesByUserIdAsync(int userId)
        {

            var purchases = await _purchaseRepository.GetByUserIdAsync(userId);
            return purchases.Select(MapToResponseDto);
        }

        public async Task<PurchaseDto?> UpdatePurchaseAsync(int id, UpdatePurchaseDto updateDto)
        {
            var existingPurchase = await _purchaseRepository.GetByIdAsync(id);
            if (existingPurchase == null) return null;
            if (existingPurchase.IsDraft == false) return null;
           


            if (updateDto.IsDraft != null) existingPurchase.IsDraft = updateDto.IsDraft.Value;
            if (updateDto.UserId != null)  existingPurchase.UserId= updateDto.UserId.Value;
            if (updateDto.NumOfTickets.HasValue) existingPurchase.NumOfTickets = updateDto.NumOfTickets.Value;
            if (updateDto.PresentId.HasValue)
            {
                existingPurchase.PresentId = updateDto.PresentId.Value;
                var presennt = await _presentRepository.GetByIdAsync(updateDto.PresentId.Value);
                if (presennt == null)
                {
                    _logger.LogInformation("ca not find present by present Id while updating purchase", updateDto.PresentId);

                    return null;
                }
                existingPurchase.Present = presennt;

            }


            var updatedUser = await _purchaseRepository.UpdateAsync(existingPurchase);
            _logger.LogInformation("{Purchase.Id} updated successfully ", updateDto.Id);

            return updatedUser != null ? MapToResponseDto(updatedUser) : null;
        }

        public async Task<IEnumerable<PurchaseDto>> GetByPresentIdAsync(int id)
        {
            var purchases = await _purchaseRepository.GetByPresentIdAsync(id);
            return purchases!=null ? purchases.Select(MapToResponseDto) : null;
        }

        public async Task<decimal> GetSumForAllAsync()
        {
            return await _purchaseRepository.GetSumForAllAsync();
        }

        public async Task<IEnumerable<PurchaseDto>> GetAllPurchasesIsDraftAsync(int id)
        {
            var purchases = await _purchaseRepository.getAllPurchasesIsDraftAsync(id);
            return purchases.Select(MapToResponseDto);
        }
        private static PurchaseDto MapToResponseDto(Purchase purchase)
        {
            return new PurchaseDto
            {
                Id = purchase.Id,
                UserId = purchase.UserId,
                UserName = purchase.User != null ? $"{purchase.User.FirstName} {purchase.User.LastName}" : string.Empty,
                
                //TotalAmount = purchase.TotalAmount,
                Created_At = purchase.Created_At,
                PresentId = purchase.PresentId,
                IsDraft = purchase.IsDraft,
                NumOfTickets = purchase.NumOfTickets,
                user = purchase.User,
                Present = purchase.Present==null? null : new PresentForPurchaseDto
                {

                    Id = purchase.Present.Id,
                    Name = purchase.Present.Name,
                    Description = purchase.Present.Description,
                    Price = purchase.Present.Price,
                    CategoryName = purchase.Present.Category?.Name ?? string.Empty,
                    DonorName = purchase.Present.Donor?.Name,
                    ImageUrl = purchase.Present.ImageUrl,
                }
            };
        }
    }
}