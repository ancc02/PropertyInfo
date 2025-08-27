using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// Info of Property
    /// </summary>
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProperty { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Price { get; set; }

        [Required]
        public string CodeInternal { get; set; }

        [Required]
        public DateTime Year { get; set; }

        [ForeignKey("IdOwner")]
        public Owner? Owner { get; set; }
        public int IdOwner { get; set; }
    }
}
