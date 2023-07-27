using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlunosApi.Models
{
    public partial class PedidoDto
    {
       
        [Required]
        [StringLength(80, ErrorMessage = "")]
        public string CNPJ { get; set; }


    }
}
