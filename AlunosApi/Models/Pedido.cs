using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocalizeApi.Models
{
    public partial class Pedido
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(80, ErrorMessage = "")]
        public string CNPJ { get; set; }

        [Required]
        [StringLength(10000, ErrorMessage = "")]
        public string Resultado { get; set; }


    }
}
