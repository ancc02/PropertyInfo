using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// info of PropertyTrace
    /// </summary>
    public class PropertyTrace
    {
        /// <summary>
        /// Primary Key Table Property trace
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyTrace { get; set; }

        /// <summary>
        /// Property date of sale
        /// </summary>
        [Required]
        public DateTime DateSale { get; set; }

        /// <summary>
        /// Name of property
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Value of sale
        /// </summary>
        [Required]
        [Range(0, 9999999999.99)]
        public decimal Value { get; set; }

        /// <summary>
        /// Tax of sale
        /// </summary>
        [Required]
        [Range(0, 9999999999.99)]
        public decimal Tax { get; set; }

        /// <summary>
        /// Foeign Key Table Property
        /// </summary>
        [ForeignKey("IdProperty")]
        public Property Property { get; set; }
        public int IdProperty { get; set; }

    }
}
