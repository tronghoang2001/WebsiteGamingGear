using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingGear.Models.Account
{
    public class KhoiPhucMatKhau
    {
        
        [StringLength(100)]
      
        [DataType(DataType.Password)]
        public string matKhauMoi { get; set; }

        
        [DataType(DataType.Password)]
       

        public string xacNhanMatKhau { get; set; }

        [Required] public string resetCode { get; set; }
    }
}