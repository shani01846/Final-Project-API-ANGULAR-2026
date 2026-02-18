using a.Dto;
using a.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using NET.Models;
using StoreApi.DTOs;
using StoreApi.Interfaces;
using System.Threading.Tasks;

namespace a.Services
{
    public class PresentService : IPresentService
    {
        private readonly IPresentRepository _presentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ILogger<PresentService> _logger;

        public PresentService(IPresentRepository presentRepository, ICategoryRepository categoryRepository,IPurchaseRepository purchaseRepository,
            ILogger<PresentService> logger)
        {
            _purchaseRepository = purchaseRepository;
            _categoryRepository = categoryRepository;
            _presentRepository = presentRepository;
            _logger = logger;
        }
        public async Task<PresentDto> CreatePresentAsync(CreatePresentDto createDto)
        {
            if (! await _categoryRepository.ExistsAsync(createDto.CategoryId))
            {
                _logger.LogError("error while creating present, category Id doesn't exist", createDto.CategoryId);

                throw new ArgumentException($"Category with ID {createDto.CategoryId} does not exist.");
            }
            var present = new Present
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                CategoryId = createDto.CategoryId,
                DonorId = createDto.DonorId,
                ImageUrl = createDto.ImageUrl
                
            };
            var createdPresent = await _presentRepository.CreateAsync(present);
            _logger.LogInformation("Product created with ID: {ProductId}", createdPresent.Id);
            return MapToResponseDto(createdPresent);

        }
        public async Task<bool> DeletePresentAsync(int id)
        {
            return await _presentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PresentDto>> GetAllPresentsAsync()
        {
            var products = await _presentRepository.GetAllAsync();
            return products.Select(MapToResponseDto);
        }

        public async Task<PagedResult<PresentDto>> GetAllPresentsPagedAsync(PaginationParams paginationParams)
        {
            var (items, totalCount) = await _presentRepository.GetAllPagedAsync(paginationParams.PageNumber,
                paginationParams.PageSize);
            var presentDtos = items.Select(MapToResponseDto);
            return new PagedResult<PresentDto>
            {
                Items = presentDtos,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)paginationParams.PageSize)
            };
        }

        public async Task<PresentDto?> GetPresentByIdAsync(int id)
        {

            var present =await _presentRepository.GetByIdAsync(id);
            return present != null ? MapToResponseDto(present) : null;
        }

        public async Task<IEnumerable<PresentDto>> GetPresentsByCategoryAsync(int categoryId)
        {
            var presents = await _presentRepository.GetByCategoryAsync(categoryId);
            return presents.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<PresentDto>> SearchPresentsByNameAsync(string name)
        {
            var presents = await _presentRepository.SearchByNameAsync(name);
            return presents.Select(MapToResponseDto);

        }



        public async Task<PresentDto?> UpdatePresentAsync(int id, UpdatePresentDto updateDto)
        {
            var existingProduct = await _presentRepository.GetByIdAsync(id);
            if (existingProduct == null) return null;
            if (!string.IsNullOrWhiteSpace(updateDto.Name)) existingProduct.Name = updateDto.Name;
            if (!string.IsNullOrWhiteSpace(updateDto.ImageUrl)) existingProduct.ImageUrl = updateDto.ImageUrl;
            if (!string.IsNullOrWhiteSpace(updateDto.Description)) existingProduct.Description = updateDto.Description;
            if (updateDto.IsLotteryDone != null) existingProduct.IsLotteryDone = updateDto.IsLotteryDone;
            if (updateDto.Price.HasValue) existingProduct.Price = updateDto.Price.Value;
            if (updateDto.CategoryId.HasValue)
            {
                if (!await _categoryRepository.ExistsAsync(updateDto.CategoryId.Value))
                {
                    _logger.LogError("error while updating present, category Id doesn't exist", updateDto.CategoryId);

                    throw new ArgumentException($"Category with ID {updateDto.CategoryId} does not exist.");
                }
                existingProduct.CategoryId = updateDto.CategoryId.Value;
            }

            var updatedPresent = await _presentRepository.UpdateAsync(existingProduct);
            return updatedPresent != null ? MapToResponseDto(updatedPresent) : null;
        }

        private static  PresentDto MapToResponseDto(Present present)
        {
            //int num = await _purchaseRepository.getNumOfPurchasesForPresentAsync(present.Id);
            return new PresentDto
            {
                Id = present.Id,
                Name = present.Name,
                Description = present.Description,
                Price = present.Price,
                CategoryName = present.Category?.Name ?? string.Empty,
                DonorName = present.Donor?.Name ?? string.Empty,
                ImageUrl = present.ImageUrl,
                IsLotteryDone = present.IsLotteryDone,
                CategoryId = present.CategoryId,
                NumOfPurchases = present.Purchases.Count
                //Purchases = present.Purchases?.Select(p => new PurchaseDtoForPresent
                //{
                //    Id = p.Id,
                //    NumOfTickets = p.NumOfTickets,
                //    UserId = p.UserId,
                //    UserName = p.User.FirstName + " "+ p.User.LastName,
                    
                    
                //}).ToList() ?? new List<PurchaseDtoForPresent>()
                
            }; 
        }

        public async Task<IEnumerable<PresentDto>> SearchByDonorNameAsync(string name)
        {
            var presents = await _presentRepository.SearchByDonorNameAsync(name);
            return presents.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<PresentDto>> SearchByNumOfPurchasesAsync(string name)
        {
            var presents = await _presentRepository.SearchByDonorNameAsync(name);
            return presents.Select(MapToResponseDto);
        }
    }
}
