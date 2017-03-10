using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Xianqu.Web.Models.Services
{
    public class Goods
    {
        [Required]
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(80,ErrorMessage ="商品名称非空")]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Producer { get; set; }


    }
}