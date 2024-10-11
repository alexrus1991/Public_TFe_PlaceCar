using Azure;
using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class FormuleTypeRepository : IFormuleTypeRepository
    {
        private readonly PlaceCarDbContext _context;

        public FormuleTypeRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddFormType(FormuleDePrixType formulType)
        {
            try 
            {
                if (formulType == null) { throw new ArgumentNullException(nameof(formulType)); }
                else { _context.FormulesPrixType.Add(formulType); }
            }
            catch(DbUpdateException ex) 
            {
                var err = ex.InnerException;
            }
            

            //await _context.SaveChangesAsync();
        }

        public async Task<List<FormuleDePrixType>> GetAllTypeFormules()
        {
           return await _context.FormulesPrixType
                .AsNoTracking()
                .ToListAsync();
        }

        

        public async Task<FormuleDePrixType> GetFormTypeById(int typeId)
        {
            try
            {
                if(typeId > 0)
                {
                     
                    var f = await _context.FormulesPrixType
                           .AsNoTracking()
                           .FirstOrDefaultAsync(p => p.FORM_Type_Id == typeId);
                    return f;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFormuleTypeDescription(int id, string description)
        {
            try
            {
                int reponce = _context.FormulesPrixType
                    .Where(f => f.FORM_Type_Id == id)
                    .ExecuteUpdate(f => f
                    .SetProperty(f => f.FORM_Type_Description, description));
                if (reponce > 0) { return true; }
                else
                {
                    throw new InvalidOperationException("FormileType does not exist!!!");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateFormuleTypeTitre(int id, string titre)
        {
            try
            {
                int reponce = _context.FormulesPrixType
                    .Where(f => f.FORM_Type_Id == id)
                    .ExecuteUpdate(f => f
                    .SetProperty(f => f.FORM_Title, titre));
                if (reponce > 0) { return true; }
                else
                {
                    throw new InvalidOperationException("FormileType does not exist!!!");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
