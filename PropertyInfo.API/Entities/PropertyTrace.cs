using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// info of PropertyTrace
    /// </summary>
    public class PropertyTrace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyTrace { get; set; }

        [Required]
        public DateTime DateSale { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Value { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Tax { get; set; }

        [ForeignKey("IdProperty")]
        public Property Property { get; set; }
        public int IdProperty { get; set; }

    }
}
