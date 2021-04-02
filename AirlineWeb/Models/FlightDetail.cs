using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineWeb.Model
{
    public class FlightDetail
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }
        [Required]
        public virtual string FlightCode { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public virtual decimal Price { get; set; }
    }
}