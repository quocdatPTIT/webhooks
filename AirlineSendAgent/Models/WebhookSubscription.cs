using System.ComponentModel.DataAnnotations;

namespace AirlineSendAgent.Models
{
    public class WebhookSubscription
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }
        [Required]
        public virtual string WebhookURI { get; set; }
        [Required]
        public virtual string Secret { get; set; }
        [Required]
        public virtual string WebhookType { get; set; }
        [Required]
        public virtual string WebhookPublisher { get; set; }
    }
}