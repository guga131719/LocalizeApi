using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public class PedidosService : IPedidoService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;


        public PedidosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetPedidos()
        {
            try
            {
                return await _context.Pedidos.ToListAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task CreatePedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePedido(Pedido pedido)
        {

            var existingPedido = await _context.Pedidos.FindAsync(pedido.Id);
            _context.Entry(existingPedido).CurrentValues.SetValues(pedido);
            _context.Update(existingPedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido> GetPedido(int id)
        {
            Pedido pedido = await _context.Pedidos.FindAsync(id);
            return pedido;
        }

        public async Task DeletePedido(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pedido>> GetPedidoByCnpj(string cnpj)
        {
            if (!string.IsNullOrWhiteSpace(cnpj))
            {
                var alunos = await _context.Pedidos.Where(n => n.CNPJ.Contains(cnpj)).ToListAsync();
                return alunos;
            }
            else
            {
                var alunos = await GetPedidos();
                return alunos;
            }
        }


        public async Task CreatePedidoByCnpj(string cnpj)
        {

            Random random = new Random();
            int randomNumber = random.Next();


            var pedido = new Pedido
            {
                CNPJ = cnpj,
                Id= randomNumber

            };

            // Realizar a solicitação de extração na ReceitaWS
            var options = new RestClientOptions("https://receitaws.com.br")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v1/cnpj/"+ cnpj, Method.Get);
            request.AddHeader("Accept", "application/json");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);


            if (response.IsSuccessStatusCode)
            {
                pedido.Resultado = response.Content;
            }
            else
            {
                // Tratar o caso de erro na chamada à API ReceitaWS
                // Exemplo: logar o erro, lançar uma exceção, etc.
                throw new Exception("Erro ao consultar a ReceitaWS");
            }


            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

    }
}
