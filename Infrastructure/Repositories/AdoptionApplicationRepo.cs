using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class AdoptionApplicationRepo : IAdoptionApplicationRepo
	{
		private readonly PetCareDbContext _context;

		public AdoptionApplicationRepo(PetCareDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(AdoptionApplication adoptionApplication)
		{
			try
			{
				await _context.AdoptionApplications.AddAsync(adoptionApplication);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task Remove(AdoptionApplication adoptionApplication)
		{
			try
			{
				if (await _context.AdoptionApplications.FindAsync(adoptionApplication) == null)
					throw new Exception("Application with this ID was not found.");
				
				_context.AdoptionApplications.Remove(adoptionApplication);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<AdoptionApplication> FindApplicationByAdopterAndCatId(int adopterId, int catId)
		{
			try
			{
				var application = await _context.AdoptionApplications
												.Where(a => a.AdopterId == adopterId && a.CatId == catId)
												.FirstOrDefaultAsync();
				return application;
			}
			catch
			{
				throw;
			}
		}

		public async Task<IEnumerable<AdoptionApplication>> FindApplicationsByAdopterId(int id)
		{
			try
			{
				var applications = await _context.AdoptionApplications.Where(a => a.AdopterId == id).ToListAsync();
				return applications;
			}
			catch
			{
				throw;
			}
		}

		public async Task<IEnumerable<AdoptionApplication>> FindApplicationsByCatId(int id)
		{
			try
			{
				var applications = await _context.AdoptionApplications.Where(a => a.CatId == id).ToListAsync();
				return applications;
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<AdoptionApplication>> GetAllAsync()
		{
			try
			{
				var adoptionApplication = await _context.AdoptionApplications.ToListAsync();
				return adoptionApplication;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<AdoptionApplication> GetByIdAsync(int id)
		{
			try
			{
				var adoptionApplication = await _context.AdoptionApplications.FindAsync(id) ?? throw new Exception("Application with this ID was not found.");
				
				return adoptionApplication;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task Update(AdoptionApplication adoptionApplication)
		{
			try
			{
				if (await _context.AdoptionApplications.FindAsync(adoptionApplication) == null) throw new Exception("Application does not exist.");
				
				_context.AdoptionApplications.Update(adoptionApplication);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async void UpdateE(AdoptionApplication adoptionApplication)
		{
			try
			{
				if (await _context.AdoptionApplications.FindAsync(adoptionApplication) == null) throw new Exception("Application does not exist.");

				_context.AdoptionApplications.Update(adoptionApplication);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
