using System;
using System.Collections.Generic;

namespace PTLab_2.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public char? Name { get; set; }
        public char Login { get; set; }
        public int? Purchase { get; set; }
        public char Password { get; set; }
    }
}
