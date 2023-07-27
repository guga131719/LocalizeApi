using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[TestClass]
public class PedidoControllerTests
{
    private readonly HttpClient _client;

    public PedidoControllerTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44370/") // Coloque a URL da sua API aqui
        };
    }

    [TestMethod]
    public async Task BuscarPedidoPorId_DeveRetornarPedido()
    {
        // Arrange
        var idPedido = 1;
        var expectedStatus = System.Net.HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync($"api/pedido/{idPedido}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }
}