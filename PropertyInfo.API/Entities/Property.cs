using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// Info of Property
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Primary Key Table Property
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProperty { get; set; }

        /// <summary>
        /// Name of property
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Property address
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// Property price
        /// </summary>
        [Required]
        [Range(0, 9999999999.99)]
        public decimal Price { get; set; }

        /// <summary>
        /// Property code internal
        /// </summary>
        [Required]
        public string CodeInternal { get; set; }

        /// <summary>
        /// Property year
        /// </summary>
        [Required]
        public DateTime Year { get; set; }

        /// <summary>
        /// Foreign Key Table Owner
        /// </summary>
        [ForeignKey("IdOwner")]
        public Owner? Owner { get; set; }

        public int IdOwner { get; set; }
    }
}
