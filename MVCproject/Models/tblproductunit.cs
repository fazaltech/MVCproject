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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCproject.Models
{

    [Table("tblproductunits")]
    public partial class tblproductunit
    {
        public int id { get; set; }
        public string unit_id { get; set; }
        [DisplayName("Unit Name")]
        public string unit_name { get; set; }
        public string flag { get; set; }
    }
}
