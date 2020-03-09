using System;
using System.ComponentModel.DataAnnotations.Schema;
using AuthAPI.Entities.Base;


namespace AuthAPI.Entities
{
    public class User : IEntityBase
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("uid")]
        public string Uid { get; set ;}
        [Column("firstName")]
        public string FirstName { get; set; }
        [Column("lastName")]
        public string LastName { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [Column("email")]
        public string Email { get; set; }
        // [Column("isActive")]
        // public bool isActive { get; set; }
        // [Column("id")]
        // public DateTime createdAt { get; set; }
        // public string Salt { get; set; }
        // public bool isUserConfirmed { get; set; }
    }
}