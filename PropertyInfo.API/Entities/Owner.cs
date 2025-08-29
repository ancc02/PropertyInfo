using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// Info of Owner
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// Primary key Table Owner
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOwner { get; set; }

        /// <summary>
        /// Name of owner
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Owner address
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// Owner photo
        /// </summary>
        [Required]
        public string Photo { get; set; }

        /// <summary>
        /// Owner Birthday
        /// </summary>
        [Required]
        public DateTime Birthday { get; set; }
    }
}
