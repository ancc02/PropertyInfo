using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// Info of Owner
    /// </summary>
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOwner { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
