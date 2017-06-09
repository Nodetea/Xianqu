using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace Xianqu.Web.Models.Services
{
    public class Ware

    {
        [Required]
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(80,ErrorMessage ="商品名称非空")]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Column(TypeName ="text")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Inventory { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }
        //public virtual List<Order> CustomerOrders { get; set; }
        //public ApplicationUser Owner { get; set; }
        //public ApplicationUser Master { get; set; }
       /* public Ware(string name,string category,string description,decimal price,string imageurl)
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
            ImageUrl = imageurl;
            ID = Guid.NewGuid().ToString();
        }
        */
        public static Ware FindWare(string wareId)
        {
            Ware temp = new Ware();
            List<Ware> templist = new List<Ware>();
            using (var dbcontext=new ApplicationDbContext())
            {
               
                var ResultSet = from w in dbcontext.Wares where w.ID == wareId select w;
                foreach(var i in ResultSet)
                {
                    templist.Add(i);
                }
            }
            if (templist.Count == 1)
            {
                temp = templist.First();
            }
            return temp;
        }
    }
}