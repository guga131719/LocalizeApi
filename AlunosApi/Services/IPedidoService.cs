using AlunosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetPedidos();
        Task<Pedido> GetPedido(int id);
        Task<IEnumerable<Pedido>> GetPedidoByCnpj(string cnpj);
        Task CreatePedido(Pedido aluno);
        Task UpdatePedido(Pedido aluno);
        Task DeletePedido(Pedido aluno);

        Task CreatePedidoByCnpj(string cnpj);
    }
}
