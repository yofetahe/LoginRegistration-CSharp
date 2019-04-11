using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

       [Required(ErrorMessage="First Name is required.")]
       [Display(Name="First Name")]
       [MinLength(2, ErrorMessage="First Name should have at least 2 character")]
       public string FristName { get; set; }
       
       [Required(ErrorMessage="Last Name is required.")]
       [Display(Name="Last Name")]
       [MinLength(2, ErrorMessage="Last Name should have at least 2 character")]
       public string LastName { get; set; }
       
       [Required(ErrorMessage="Email is required")]
       [Display(Name="Email")]
       [DataType(DataType.EmailAddress)]
       [EmailAddress]
       public string Email { get; set; }
       
       [Required(ErrorMessage="Password is required")]
       [Display(Name="Password")]
       [MinLength(8, ErrorMessage="Password must be a min of 8 character")]
       [DataType(DataType.Password)]
       public string Password { get; set; }

       public DateTime CreatedAt { get; set; } = DateTime.Now;
       public DateTime UpdatedAt { get; set; } = DateTime.Now;

       [NotMapped]
       [Compare("Password", ErrorMessage="Password do not match.")]
       [DataType(DataType.Password)]
       [Display(Name="Confirm Password")]
       public string Confirm {get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}