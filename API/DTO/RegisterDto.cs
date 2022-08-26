using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using API.Entities;
namespace API.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string password { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime Created { get; set; }=DateTime.Now;
        [Required]
        public DateTime LastActive { get; set; }=DateTime.Now;
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Introduction { get; set; }
        [Required]
        public string LookingFor { get; set; }
        [Required]
        public string Interests { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public ICollection<Photos> Photos {get;set;}
    }
}