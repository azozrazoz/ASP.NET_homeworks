using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsAdmin { get; set; }
    }
}