﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Model
{
    public class Marketers
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }
}