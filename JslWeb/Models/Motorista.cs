using System.ComponentModel.DataAnnotations;

namespace JslWeb.Models
{
    public class Motorista
    {
        [Display(Name = "Código")]
        public long Id { get; set; }

        [StringLength(60, ErrorMessage = "O Nome deve possuir no máximo 60 caracteres"),
            Required(ErrorMessage = "O Nome é obrigatório")]
        public string? Nome { get; set; }

        [StringLength(60, ErrorMessage = "O Sobrenome deve possuir no máximo 60 caracteres"),
            Required(ErrorMessage = "O Sobrenome é obrigatório")]
        public string? Sobrenome { get; set; }

        [StringLength(25, MinimumLength = 2, ErrorMessage = "A Marca do Caminhão deve possuir ao menos 2 e no máximo 25 caracteres"),
            Required(ErrorMessage = "A Marca do Caminhão é obrigatória"),
            Display(Name = "Marca - Caminhão")]
        public string? CaminhaoMarca { get; set; }

        [StringLength(25, MinimumLength = 2, ErrorMessage = "O Modelo do Caminhão deve possuir ao menos 2 e no máximo 25 caracteres"),
            Required(ErrorMessage = "O Modelo do Caminhão é obrigatório"),
            Display(Name = "Modelo - Caminhão")]
        public string? CaminhaoModelo { get; set; }

        [StringLength(7, MinimumLength = 7, ErrorMessage = "A Placa do Caminhão deve possuir 7 caracteres"),
            RegularExpression(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", ErrorMessage = "A Placa deve possuir a seguinte estrutura AAA9X99, sendo A qualquer letra de A-Z, 9 qualquer dígito de 0 a 9 e X qualquer letra ou digito"),
            Required(ErrorMessage = "A Placa do Caminhão é obrigatória"),
            Display(Name = "Placa - Caminhão")]
        public string? CaminhaoPlaca { get; set; }

        [Range(2, 5, ErrorMessage = "Os eixos devem estar entre 2 e 5"),
            Display(Name = "Eixos - Caminhão")]
        public int CaminhaoEixos { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Logradouro deve possuir ao menos 5 e no máximo 50 caracteres"),
            Required(ErrorMessage = "O Logradouro é obrigatório"),
            Display(Name = "Logradouro")]
        public string? EndLogradouro { get; set; }

        [StringLength(10, ErrorMessage = "O Número deve possuir no máximo 10 caracteres"),
            Required(ErrorMessage = "O Número é obrigatório"),
            Display(Name = "Número")]
        public string? EndNumero { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "O Bairro deve possuir ao menos 3 e no máximo 50 caracteres"),
            Required(ErrorMessage = "O Bairro é obrigatório"),
            Display(Name = "Bairro")]
        public string? EndBairro { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "A Cidade deve possuir ao menos 3 e no máximo 50 caracteres"),
            Required(ErrorMessage = "A Cidade é obrigatória"),
            Display(Name = "Cidade")]
        public string? EndCidade { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve possuir 9 caracteres"),
            RegularExpression(@"^[0-9]{5}-[0-9]{3}$", ErrorMessage = "O CEP deve possuir a seguinte estrutura 99999-999, sendo 9 qualquer dígito de 0 a 9"),
            Required(ErrorMessage = "O CEP é obrigatório"),
            Display(Name = "CEP")]
        public string? EndCep { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve possuir 2 caracteres"),
                    Required(ErrorMessage = "A UF é obrigatória"),
                    Display(Name = "UF")]
        public string? EndUf { get; set; }

        public List<Viagem>? Viagens { get; set; }

    }
}
