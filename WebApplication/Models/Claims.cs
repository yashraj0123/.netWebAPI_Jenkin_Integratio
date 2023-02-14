using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    [Table("ClaimsTable")]
    public class Claims
    {
        [Key]
        public int id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int RValue { get; set; }
        public int Avalue { get; set; }
        [Required]
        public string CurrType { get; set; }
        [Required]
        public string Phase { get; set; }
        [Required]
        public string Link { get; set; }
        [ForeignKey("UserId")]
        public User Users { get; set; }
        public int UserId { get; set; }
    }
}
