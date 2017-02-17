using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Xianqu.Models
{
    class Goods
    {
        [Key]
        [Required]
        public string GoodsId { get; set; }
        [Required]
        public string GoodsCategory { get; set; }
        [Required]
        public string GoodsName { get; set; }
        [Required]
        public string GoodsDescription { get; set; }
    }
}
