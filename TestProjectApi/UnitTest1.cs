using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[TestClass]
public class TarefaControllerTests
{
    private readonly HttpClient _client;

    public TarefaControllerTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44370/")
        };
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarGetTarefas()
    {
        // Arrange
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync("api/Tarefas");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarGetTarefaPorCNPJ()
    {
        // Arrange
        var cnpj = "06034513000108";
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync($"api/Tarefas?cnpj={cnpj}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarGetTarefaPorId()
    {
        // Arrange
        var idTarefa = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.GetAsync($"api/Tarefas/{idTarefa}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarPutTarefaPorCNPJ()
    {
        // Arrange
        var cnpj = "06034513000108";
        var Tarefa = new Tarefa
        {
            Id = 1,
            CNPJ = "06034513000108",
            Resultado = "string de resultado"
        };
        var jsonTarefa = JsonConvert.SerializeObject(Tarefa);
        var content = new StringContent(jsonTarefa, Encoding.UTF8, "application/json");
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.PutAsync($"api/Tarefas/{cnpj}", content);
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarPostTarefa()
    {
        // Arrange
        var Tarefa = new Tarefa
        {
            Id = 1,
            CNPJ = "06034513000108",
            Resultado = "string de resultado"
        };
        var jsonTarefa = JsonConvert.SerializeObject(Tarefa);
        var content = new StringContent(jsonTarefa, Encoding.UTF8, "application/json");
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.PostAsync("api/Tarefas", content);
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }

    [TestMethod]
    public async Task DeveRetornarStatusCodeOK_QuandoChamarDeleteTarefaPorId()
    {
        // Arrange
        var idTarefa = 1;
        var expectedStatus = HttpStatusCode.OK;

        // Act
        var response = await _client.DeleteAsync($"api/Tarefas/{idTarefa}");
        var actualStatus = response.StatusCode;

        // Assert
        Assert.AreEqual(expectedStatus, actualStatus);
    }
}

public class Tarefa
{
    public int Id { get; set; }
    public string CNPJ { get; set; }
    public string Resultado { get; set; }
}