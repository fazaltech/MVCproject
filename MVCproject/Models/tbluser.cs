//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCproject.Models
{

    
    public partial class tbluser
    {
        [Key]
        public int id { get; set; }
        public Nullable<decimal> user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public string role { get; set; }
        public Nullable<int> account_type { get; set; }
        public string flag { get; set; }
        public string email_id { get; set; }
        
    }
}
