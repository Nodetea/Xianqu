using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Xianqu.Web.Models.Services
{
    public class Order
    {
        [Key]
        [Required]
        public string OrderId { get; set; }
        
        public bool CustomerChecked { get; set; }

        public bool SellerChecked { get; set; }

        public string WareId { get; set; }

        public int WareAmount { get; set; }

        public DateTime OrderTime { get; set; }

        public string OrderAddress { get; set; }

        public string SellerId { get; set; }

        public string CustomerId { get; set; }

        public decimal total { get; set; }

    }
}