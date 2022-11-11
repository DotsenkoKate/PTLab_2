using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTLab_2.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required] public string Login { get; set; }
        public int Purchase { get; set; }
        [DataType(DataType.Password)] public string Password { get; set; }
    }
}