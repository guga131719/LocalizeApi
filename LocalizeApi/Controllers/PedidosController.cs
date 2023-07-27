
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizeApi.Models;
using LocalizeApi.Services;
using System.Linq;
using LocalizesApi.Services;

namespace LocalizeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _PedidoService;

        public PedidosController(IPedidoService PedidoService)
        {
            _PedidoService = PedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> Index()
        {
            try
            {
                var Pedidos = await _PedidoService.GetPedidos();
                return Ok(Pedidos);
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao acessar dados de Pedidos");
            }
        }

        [HttpGet("{id:int}", Name = "GetPedido")]
        public async Task<ActionResult<Pedido>> Details(int id)
        {
            var Pedido = await _PedidoService.GetPedido(id);

            if (Pedido == null)
                return NotFound($"Pedido com id= {id} não encontrado");

            return Ok(Pedido);
        }

        [HttpGet("PedidosPorCnpj")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidoPorCnpj([FromQuery] string cnpj)
        {
            var Pedidos = await _PedidoService.GetPedidoByCnpj(cnpj);

            if (Pedidos == null)
                return NotFound($"Não existem Pedidos com cnpj = {cnpj}");

            return Ok(Pedidos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdate(Pedido pedido)
        {
            try
            {

                var PedidoRertorno= await _PedidoService.GetPedido(pedido.Id);
                var PedidosByCnpj = await _PedidoService.GetPedidoByCnpj(pedido.CNPJ);



                if (PedidoRertorno == null && PedidosByCnpj.Count() == 0)
                {
                    await _PedidoService.CreatePedido(pedido);
                    return CreatedAtRoute("GetPedido", new { id = pedido.Id }, pedido);
                }
                if (PedidoRertorno == null && PedidosByCnpj.Count() > 0)
                {                          
                    return Ok($"O valor do  id={PedidosByCnpj.FirstOrDefault().Id.ToString() } tem que ser para atualizar");
                }
                if (PedidoRertorno != null && PedidosByCnpj.Count() == 0)
                {               
                    return Ok($"O valor do  cpnj={PedidoRertorno.CNPJ.ToString()} tem que ser para atualizar");
                }
                else {

                    await _PedidoService.UpdatePedido(pedido);
                    return Ok($"Pedido com id={pedido.Id.ToString()} atualizado com sucesso");

                }

              

            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{cnpj}")]
        public async Task<ActionResult> ImplementaServicoSerasa(string cnpj, [FromBody] Pedido Pedido)
        {
            try
            {

                var pedidos = await _PedidoService.GetPedidoByCnpj(cnpj.ToString());

              
                if (pedidos.Count() == 0)
                {
                    await _PedidoService.CreatePedidoByCnpj(cnpj.ToString());
                    return Ok($"Pedido com id={cnpj} atualizado com sucesso");
                 
                }
                else
                {
                    return Ok($"Esse cnpj {cnpj} ja realizou a implementacao Serasa");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var Pedido = await _PedidoService.GetPedido(id);
                if (Pedido != null)
                {
                    await _PedidoService.DeletePedido(Pedido);
                    return Ok($"Pedido com id={id} excluído com sucesso");
                }
                else
                {
                    return NotFound($"Pedido com id= {id} não encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
