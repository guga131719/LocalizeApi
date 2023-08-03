using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[TestClass]
public class TarefaControllerTests
{
    private readonly HttpClient _client;

    public TarefaControllerTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44370/") // Coloque a URL da sua API aqui
        };
    }

    [TestMethod]
    public async Task BuscarTarefaPorId_DeveRetornarTarefa()
    {
        // Arrange
        var idTarefa = 1;
        var expectedStatus = System.Net.HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync($"api/Tarefa/{idTarefa}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }
}