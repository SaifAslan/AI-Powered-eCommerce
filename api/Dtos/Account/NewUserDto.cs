using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }        
    }
}