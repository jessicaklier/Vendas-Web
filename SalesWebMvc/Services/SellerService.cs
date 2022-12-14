using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _contex;

        public SellerService(SalesWebMvcContext contex)
        {
            _contex = contex;
        }

        public  async Task<List<Seller>> FindAllAsync()
        {
            return await _contex.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller obj)
        {
            _contex.Add(obj);
           await _contex.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _contex.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _contex.Seller.FindAsync(id);
                _contex.Seller.Remove(obj);
                await _contex.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException("Não é permitido deletar o vendedor(a) pois tem vendas.");
            }

        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _contex.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new KeyNotFoundException("Id not found");
            }
            try
            {
                _contex.Update(obj);
               await _contex.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
