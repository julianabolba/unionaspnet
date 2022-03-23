using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unionmongo.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Column("Id")]
        [Display(Name ="Código")]
        public Guid Id { get; set; }

        [Required]
        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

       
        [Required]
        [Column("Telefone")]
        [Display(Name = "Telefone")]
        public double Telefone { get; set; }

        [EmailAddress]
        [Column("E-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
