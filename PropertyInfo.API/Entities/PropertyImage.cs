using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyInfo.API.Entities
{
    /// <summary>
    /// info of PropertyImage
    /// </summary>
    public class PropertyImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPropertyImage { get; set; }

        [Required]
        public string File { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [ForeignKey("IdProperty")]
        public Property Property { get; set; }
        public int IdProperty { get; set; }
    }
}
