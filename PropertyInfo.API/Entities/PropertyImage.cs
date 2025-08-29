using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// info of PropertyImage
    /// </summary>
    public class PropertyImage
    {
        /// <summary>
        /// Primary Key Table Property Image
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyImage { get; set; }

        /// <summary>
        /// Property Image
        /// </summary>
        [Required]
        public string File { get; set; }

        /// <summary>
        /// Property availability
        /// </summary>
        [Required]
        public bool Enabled { get; set; }

        /// <summary>
        /// Foreign Key Table Property
        /// </summary>
        [ForeignKey("IdProperty")]
        public Property Property { get; set; }

        public int IdProperty { get; set; }
    }
}
