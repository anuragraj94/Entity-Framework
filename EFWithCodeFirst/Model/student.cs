﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWithCodeFirst.Model
{
    public class student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
    }
}