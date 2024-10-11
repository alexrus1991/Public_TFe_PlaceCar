using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class FormuleRepository : IFormuleRepository
    {
        private readonly PlaceCarDbContext _context;

        public FormuleRepository(PlaceCarDbContext context)
        {
            _context = context;
        }


        public async Task<FormuleDePrix> AddFurmule(FormuleDePrix formule)
        {
            try
            {
                if (formule == null) { throw new ArgumentNullException(nameof(formule)); }
                else 
                { 
                    _context.FormuleDePrix.Add(formule); 
                    _context.SaveChanges();
                }
                return formule;
            }
            catch (Exception ex)
            {
                var errorMessage = ex;

                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<FormuleDePrix> GetFormuleById(int formId)
        {
            var form = await _context.FormuleDePrix
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.FORM_Id == formId);
            return form;
        }

        public async Task<FormuleDePrix> GetFormuleByIdWithType(int formId)
        {
            var form = await _context.FormuleDePrix
               .AsNoTracking()
               .Include(fp=> fp.FormuleDePrixType)
               .FirstOrDefaultAsync(p => p.FORM_Id == formId);
            return form;
        }

        public async Task<FormuleDePrix> GetFormuleByParkIdandFormuleType(int parkId)
        {
            var formulesPrixHeure = await _context.FormuleDePrix
                .Include(fp => fp.FormuleDePrixType).Where(ft => ft.FormuleDePrixType.FORM_Title == "Heure")
                .FirstOrDefaultAsync(fp => fp.ParkingId == parkId);

            return formulesPrixHeure;
        }

        public async Task<List<FormuleDePrix>> GetFormulesPrixByParkingId(int parkId)
        {
            try
            {
                var priceFormulas = await _context.FormuleDePrix
                    .AsNoTracking()
                    .Where(fp => fp.ParkingId == parkId)
                    .Include(fp => fp.FormuleDePrixType) 
                    .ToListAsync();

                return priceFormulas;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> UpdateFormule(FormuleDePrix formule)
        {
            bool Ok = false;
            try
            {
                if (formule == null) { throw new ArgumentException("L'objet formule est vide !!!"); }
                else
                {
                  FormuleDePrix fromdb = _context.FormuleDePrix.SingleOrDefault(m=>m.FORM_Id == formule.FORM_Id);
                    if(fromdb != null) 
                    {
                        fromdb.FORM_Prix = formule.FORM_Prix;
                        //mapping
                        fromdb.FormuleDePrixType = formule.FormuleDePrixType;


                        _context.FormuleDePrix.Update(fromdb);
                        Ok = true;
                        await _context.SaveChangesAsync();
                        return Ok;
                    }
                    else
                    {
                        throw new ArgumentException("L'objet formule est invalide !!!");
                    }
                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
