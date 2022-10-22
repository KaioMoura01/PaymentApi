using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public string DataVenda { get; set; }
        public string ItensComprados { get; set; }
        public int IdVendedor { get; set; }
        public string Status { get; set; }
    }
}