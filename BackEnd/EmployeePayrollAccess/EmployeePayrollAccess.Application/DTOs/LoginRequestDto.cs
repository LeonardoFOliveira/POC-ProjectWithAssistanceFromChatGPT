﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollAccess.Application.DTOs
{
    public class LoginRequestDto
    {
        public string Cpf { get; set; }
        public string Password { get; set; }
    }
}
