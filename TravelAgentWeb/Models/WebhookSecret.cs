using System.ComponentModel.DataAnnotations;

namespace TravelAgentWeb.Models
{
    public class WebhookSecret
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Secret { get; set; }  

        [Required]
        public virtual string Publisher { get; set; }
    }
}