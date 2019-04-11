using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LoginRegistration.Models
{
    public class UserTransaction
    {
        public User user { get; set; }
        public Transaction transaction { get; set; }
    }
}