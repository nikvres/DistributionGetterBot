using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistributionGetterBot.Models
{
    public class StatusMessage
    {
        [Key]
        public int IdStatus { get; set; }
        [ForeignKey(nameof(IdUser))]
        public string IdUser { get; set; }
        public int CurrentStatus { get; set; }
        
    }
}
