using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingGear.Models.Account
{
    public class DoiMatKhau
    {
        
        public string matKhauCu { get; set; }    
        public string matKhauMoi { get; set; } 
        public string xacNhanMatKhau { get; set; }
        public string anhDaiDien { get; set; }
        public string ten { get; set; }
    }
}