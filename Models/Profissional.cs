using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unionmongo.Models
{
    [Table("Profissional")]
    public class Profissional
    {
        [Column("Id")]
        [Display(Name ="Código")]
        public Guid Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("Telefone")]
        [Display(Name = "Telefone")]
        public int Telefone { get; set; }
    }
}
