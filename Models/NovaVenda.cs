using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class NovaVenda
    {
        public int Id {get; set;}
        public int VendedorId { get; set; }
        public Vendedor Vendedor {get; set;}
        public int VendaId { get; set; }
        public Venda Venda {get; set;}
    }
}