using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public enum AccountStatus
    {
        enable, disable
    }
    [Table("Account")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(255), EmailAddress]
        public string Email { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
