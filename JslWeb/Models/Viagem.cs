using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JslWeb.Models
{
    public class Viagem
    {
        public int Id { get; set; }

        [Range(0.0001, 99999.9999, ErrorMessage = "Peso deve estar ente 0,0001 e 99999,9999"),
            Required(ErrorMessage = "O Peso é obrigatório"),
            Display(Name = "Peso da Carga")]
        public double PesoCarga { get; set; }

        [DataType(DataType.Date),
            Display(Name = "Data/Hora da Viagem"),
            Required(ErrorMessage = "A Data da Viagem é obrigatória")]
        public DateTime DtHrViagem { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "O Local de Entrega deve possuir no mínimo 2 e no máximo 50 caracteres"),
            Required(ErrorMessage = "O Local de Entrega é obrigatório"),
            Display(Name = "Local de Entrega")]
        public string? LocalEntrega { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "O Local de Saída deve possuir no mínimo 2 e no máximo 50 caracteres"),
            Required(ErrorMessage = "O Local de Saída é obrigatório"),
            Display(Name = "Local de Saída")]
        public string? LocalSaida { get; set; }

        [Range(0.0001, 999999.9999, ErrorMessage = "Km Total deve estar ente 0,0001 e 999999,9999"),
            Required(ErrorMessage = "O Km Total é obrigatório"),
            Display(Name = "KM Total")]
        public double TotalKm { get; set; }

        public Motorista? Motorista { get; set; }
        [Required(ErrorMessage = "O Motorista é obrigatório"),
            Display(Name = "Motorista")]
        public long MotoristaId { get; set; }
    }
}
