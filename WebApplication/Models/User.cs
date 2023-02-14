using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    [Table("UserTable")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
       
        [Required]
        public string PanNumber { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string AccNumber { get; set; }
        public ICollection<Claims> claim { get; set; }
    }
}
