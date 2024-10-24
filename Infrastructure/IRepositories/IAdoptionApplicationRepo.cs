using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
	public interface IAdoptionApplicationRepo : IGenericRepo<AdoptionApplication>
	{
		Task<IEnumerable<AdoptionApplication>> FindApplicationsByAdopterId(int id);
		Task<IEnumerable<AdoptionApplication>> FindApplicationsByCatId(int id);
		Task<AdoptionApplication> FindApplicationByAdopterAndCatId(int adopterId, int catId);
	}
}
