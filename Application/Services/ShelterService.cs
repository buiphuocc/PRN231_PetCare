using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.ShelterDTO;

public class ShelterService : IShelterService
{
    private readonly IShelterRepo _shelterRepo;
    private readonly IMapper _mapper;

    public ShelterService(IShelterRepo shelterRepo, IMapper mapper)
    {
        _shelterRepo = shelterRepo;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ShelterResDTO>> Create(ShelterReqDTO createForm)
    {
        var shelter = _mapper.Map<Shelter>(createForm);
        await _shelterRepo.AddAsync(shelter);

        var responseDto = _mapper.Map<ShelterResDTO>(shelter);
        return new ServiceResponse<ShelterResDTO>
        {
            Data = responseDto,
            Message = "Shelter created successfully."
        };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var shelter = await _shelterRepo.GetByIdAsync(id);
        if (shelter == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Error = "Shelter not found.",
                Hint = "Ensure the shelter ID is correct."
            };
        }

        await _shelterRepo.Remove(shelter);
        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Shelter deleted successfully."
        };
    }

    public async Task<ServiceResponse<PaginationModel<ShelterResDTO>>> GetAll(int pageNumber, int pageSize)
    {
        var shelters = await _shelterRepo.GetAllAsync();
        var totalRecords = shelters.Count;

        var paginatedShelters = shelters
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var responseDto = _mapper.Map<List<ShelterResDTO>>(paginatedShelters);

        var pagination = new PaginationModel<ShelterResDTO>
        {
            Page = pageNumber,
            TotalPage = (int)Math.Ceiling((double)totalRecords / pageSize),
            TotalRecords = totalRecords,
            ListData = responseDto
        };

        return new ServiceResponse<PaginationModel<ShelterResDTO>>
        {
            Data = pagination,
            Message = "Shelters retrieved successfully."
        };
    }

    public async Task<ServiceResponse<ShelterResDTO>> GetById(int id)
    {
        var shelter = await _shelterRepo.GetByIdAsync(id);
        if (shelter == null)
        {
            return new ServiceResponse<ShelterResDTO>
            {
                Success = false,
                Error = "Shelter not found.",
                Hint = "Check the shelter ID and try again."
            };
        }

        var responseDto = _mapper.Map<ShelterResDTO>(shelter);
        return new ServiceResponse<ShelterResDTO>
        {
            Data = responseDto,
            Message = "Shelter retrieved successfully."
        };
    }

    public async Task<ServiceResponse<ShelterResDTO>> Update(ShelterReqDTO updateForm, int id)
    {
        var shelter = await _shelterRepo.GetByIdAsync(id);
        if (shelter == null)
        {
            return new ServiceResponse<ShelterResDTO>
            {
                Success = false,
                Error = "Shelter not found.",
                Hint = "Make sure the shelter ID is correct."
            };
        }

        _mapper.Map(updateForm, shelter);
        await _shelterRepo.Update(shelter);

        var responseDto = _mapper.Map<ShelterResDTO>(shelter);
        return new ServiceResponse<ShelterResDTO>
        {
            Data = responseDto,
            Message = "Shelter updated successfully."
        };
    }
}
