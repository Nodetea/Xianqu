using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Xianqu.Web.Models.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Xianqu.Web.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。
    public class ApplicationUser : IdentityUser
    {
        public string UserOnlineName { get; set; }
        
        public virtual List<Ware> Goods { get; set; }

        public virtual List<Order> OwnOrders { get; set; }
        [StringLength(10)]
        public string UserSex { get; set; }

        [Column("text")]
        public string ShippingAddress { get; set; }

        public decimal AccountBalance { get;set; }

        public DateTime? BirthDate { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        //public string Descripton { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Order> Orders { get; set; }
        //public DbSet<WarePicture> WareImages { get; set; }
        public DbSet<Ware> Wares { get; set; }
       // public DbSet<WarePicture> Pictures { get; set; }
        public DbSet<ApplicationRole> UserRoles { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}