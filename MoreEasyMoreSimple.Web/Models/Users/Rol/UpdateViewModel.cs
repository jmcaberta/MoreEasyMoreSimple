﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Users.Rol
{
    public class UpdateViewModel
    {
        public int rolId { get; set; }
        [Required]
        public string rolname { get; set; }
        [Required]
        public string roldesc { get; set; }
    }
}
