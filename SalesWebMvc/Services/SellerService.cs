using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {

        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Retorno minha lista de vendedores cadastradas
        public List<Seller> FindAdll()
        {
            return _context.Seller.ToList();
        }

        //Insiro um vendedeor no banco
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Retorna o vendedor com o id passado por parametro
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        /*Remove vendedor passando o id como parametro, removo o obj do dbset e depois
          confirmo a alteração para o entity framework efetivar no banco de dados*/
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        //Update para o botão Edit na tela de vendedores
        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
