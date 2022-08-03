﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.Model
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
