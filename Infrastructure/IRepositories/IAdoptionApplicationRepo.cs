using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
	public interface IAdoptionApplicationRepo
	{
		Task<IEnumerable<AdoptionApplication>> GetAllAsync();
		Task<AdoptionApplication> GetByIdAsync(int id);
		Task<IEnumerable<AdoptionApplication>> FindApplicationsByAdopterId(int id);
		Task<IEnumerable<AdoptionApplication>> FindApplicationsByCatId(int id);
		Task<AdoptionApplication> FindApplicationByAdopterAndCatId(int adopterId, int catId);
		Task AddAsync(AdoptionApplication adoptionApplication);
		Task UpdateAsync(AdoptionApplication adoptionApplication);
		Task DeleteAsync(int id);
	}
}
