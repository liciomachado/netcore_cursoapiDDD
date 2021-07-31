using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoCreate
    {
        [Required(ErrorMessage = "CEP é obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Numero é obrigatório")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        public Guid MunicipioId { get; set; }
    }
}
