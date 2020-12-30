using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCproject.Models
{
    [Table("tblproductcategory")]
    public class tblproductcategory
    {
        public int id { get; set; }
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string flag { get; set; }
  

    }
}