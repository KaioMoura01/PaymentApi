using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Context
{
    public class VendaContext: DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options){}

        DbSet<NovaVenda> NovaVendas {get; set;}
        DbSet<Vendedor> Vendedores {get; set;}
        DbSet<Venda> Vendas {get; set;}
    }
}