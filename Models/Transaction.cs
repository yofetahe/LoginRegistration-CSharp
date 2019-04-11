using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Display(Name="Deposit/Withdrow")]
        [Required(ErrorMessage="Amount is required.")]
        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public User user { get; set; }

    }
}