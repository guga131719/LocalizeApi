using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[TestClass]
public class PedidoControllerTests
{
    private readonly HttpClient _client;

    public PedidoControllerTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44370/")
        };
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarGetPedidos()
    {
        // Arrange
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync("api/pedidos");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarGetPedidoPorCNPJ()
    {
        // Arrange
        var cnpj = "06034513000108";
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync($"api/pedidos?cnpj={cnpj}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarGetPedidoPorId()
    {
        // Arrange
        var idPedido = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync($"api/pedidos/{idPedido}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarPutPedidoPorCNPJ()
    {
        // Arrange
        var cnpj = "06034513000108";
        var pedido = new Pedido
        {
            Id = 1,
            CNPJ = "06034513000108",
            Resultado = "string de resultado"
        };
        var jsonPedido = JsonConvert.SerializeObject(pedido);
        var content = new StringContent(jsonPedido, Encoding.UTF8, "application/json");
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.PutAsync($"api/pedidos/{cnpj}", content);
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarPostPedido()
    {
        // Arrange
        var pedido = new Pedido
        {
            Id = 1,
            CNPJ = "06034513000108",
            Resultado = "string de resultado"
        };
        var jsonPedido = JsonConvert.SerializeObject(pedido);
        var content = new StringContent(jsonPedido, Encoding.UTF8, "application/json");
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.PostAsync("api/pedidos", content);
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarDeletePedidoPorId()
    {
        // Arrange
        var idPedido = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.DeleteAsync($"api/pedidos/{idPedido}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }
}

public class Pedido
{
    public int Id { get; set; }
    public string CNPJ { get; set; }
    public string Resultado { get; set; }
}