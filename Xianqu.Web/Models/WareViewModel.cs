using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
namespace Xianqu.Web.Models
{
    public class WareViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "商品名称非空")]
        [Display(Name ="商品名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "商品种类")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "商品描述")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name ="库存量")]
        public int Inventory { get; set; }

        public string ImageUrl { get; set; }
    }

    public class BuyWareViewModel
    {
        public string WareId { get; set; }

        [Display(Name ="商品名称")]
        public string WareName { get; set; }

        [Display(Name ="商品单价")]
        public decimal Price { get; set; }

        [Display(Name ="商品数量")]
        public int WareNumber { get; set; }

        [Display(Name ="收货地址")]
        public string ShipAddress { get; set; }

        [Display(Name ="收货人")]
        public string Owne { get; set; }

        [Display(Name ="卖家")]
        public string Seller { get; set; }

        [Display(Name ="总价")]
        public decimal Total { get; set; }

        public static ApplicationUser FindSeller(string id)
        {
            var temp = new ApplicationUser();
            List<ApplicationUser> merchants = new List<ApplicationUser>();
            using (var context=new ApplicationDbContext())
            {
                
                var ResultSet = from u in context.Users where u.Id == id select u;
                foreach(var i in ResultSet)
                {
                    merchants.Add(i);
                }
            }
            if (merchants.Count == 1)
                temp = merchants.Single();
            return temp;
        }
    }

    public class CommentViewModel
    {
        [Required]
        [Display(Name = "评论人")]
        public string CommentOwner { get; set; }

        [Required]
        [Display(Name = "评价订单号")]
        public string CommentOrderId { get; set; }
    }

    public class OrderHandViewModel
    {
        [Display(Name ="商品名")]
        public string WareName { get; set; }

        [Display(Name ="订单号")]
        public string OrderId { get; set; }

        [Display(Name = "卖家")]
        public string SellerNmae { get; set; }

        [Display(Name = "买家")]
        public string CustomerName { get; set; }

        [Display(Name ="订单金额")]
        public decimal Sum { get; set; }

        [Display(Name = "确认交易")]
        public bool SellerChecked { get; set; }


        public static ApplicationUser GetApplicationUser(string userId)
        {
            ApplicationUser temp = new ApplicationUser();
            using (var context = new ApplicationDbContext())
            {
                var result = from user in context.Users where user.Id == userId select user;
                List<ApplicationUser> list = new List<ApplicationUser>();
                foreach (var i in result)
                {
                    list.Add(i);
                }
                if (list.Count == 1)
                {
                    temp = list.First();
                }
            }
            return temp;
        }
    }
}