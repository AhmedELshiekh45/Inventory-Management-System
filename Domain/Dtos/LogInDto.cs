﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dtos
{
    public class LogInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
