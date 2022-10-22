using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Models;
using tech_test_payment_api.Context;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController: ControllerBase
    {
        private readonly VendaContext _context;

        public VendaController(VendaContext context)
        {
            _context = context;
        }

        [HttpPost("Cadastra-venda")]
        public IActionResult CadastraVenda(NovaVenda novaVenda)
        {
            if(novaVenda.Venda.ItensComprados == null)
                return BadRequest("Deve haver ao menos um item comprado.");
            
            _context.Add(novaVenda);
            novaVenda.Venda.DataVenda = DateTime.Now.ToShortDateString();
            novaVenda.Venda.Status = "Aguardando pagamento";
            _context.SaveChanges();

            return Ok(novaVenda);
        }

        [HttpGet("Buscar Venda{id}")]
        public IActionResult BuscarVenda(int id)
        {
            var venda = _context.Find<Venda>(id);
            if(venda == null)
                return BadRequest($"O ID: {id} não possui compra registrada. Busque um ID válido.");
            return Ok(venda);
        }

        [HttpPut("AlterarStatus{id}")]
        public IActionResult Editar(int id, string status)
        {
            string[] mudaStatus = new string[2];
            string statusDisponiveis = "";

            var vendaBanco = _context.Find<Venda>(id);
            if(vendaBanco == null)
                return NotFound();

            if(vendaBanco.Status == "Entregue" || vendaBanco.Status == "Cancelado")
                return BadRequest($"O status de da compra não pode ser alterado, pois ele já foi {vendaBanco.Status.ToUpper()}");


            if(vendaBanco.Status == "Aguardando pagamento")
                statusDisponiveis = "Pagamento aprovado,Cancelado";
            if(vendaBanco.Status =="Pagamento aprovado")
                statusDisponiveis = "Entregue a transportadora,Cancelado";
            if(vendaBanco.Status == "Entregue a transportadora")
                statusDisponiveis = "Entregue,Entregue";

            mudaStatus = statusDisponiveis.Split(",");
            mudaStatus[0].Trim();
            mudaStatus[1].Trim();
            if(status.ToUpper() == mudaStatus[0].ToUpper() || status.ToUpper() == mudaStatus[1].ToUpper())
            {
                vendaBanco.Status = status;
                _context.Update(vendaBanco);
                _context.SaveChanges();
                return Ok(vendaBanco);
            }

            if(vendaBanco.Status == "Entregue a transportadora")
                    return BadRequest($"O status aceitos pelo pedido é: {mudaStatus[0]}");
                
                return BadRequest($"Os status aceitos pelo pedido são: {mudaStatus[0]} e {mudaStatus[1]}");
            
        }
    }
}