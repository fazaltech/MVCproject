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
 
    
    public partial class tblproduct
    {
        public int id { get; set; }
        public string product_id { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string unit_id { get; set; }
        public string category_id { get; set; }
        public Nullable<decimal> unit_in_stock { get; set; }
        public Nullable<decimal> unit_price { get; set; }
        public decimal discount_percentage { get; set; }
        public Nullable<decimal> recorder_level { get; set; }
        public string user_id { get; set; }
        public string flag { get; set; }
    }
}
