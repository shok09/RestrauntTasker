﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class RefreshTokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
