using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Xianqu.Web.Models;
using Xianqu.Web.Models.Services;
namespace Xianqu.Web.Controllers
{
    public class WareManageController : Controller
    {
        public WareManageController()
        {

        }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Finished()
        {
            return View();
        }

        public ActionResult Book()
        {
            ViewBag.Message = "闲书区";
            var model = new WareViewModel();
            List<WareViewModel> goods = new List<WareViewModel>();
            using(var dbcontext=new ApplicationDbContext())
            {
                var books = from b in dbcontext.Wares where b.Category == "书籍" select b;
                foreach(var book in books)
                {
                    goods.Add(new WareViewModel {Id=book.ID, Name = book.Name, Category = book.Category, Description = book.Description, Price = book.Price, Inventory = book.Inventory, ImageUrl = book.ImageUrl });
                }
            }
            return View(goods.ToList());
        }

        public ActionResult Electronic()
        {
            ViewBag.Message = "闲置电子区";
            return View();
        }

        public ActionResult Life()
        {
            ViewBag.Message = "生活专区";
            return View();
        }

       /* public ActionResult WareDetail(string id)
        {
            WareViewModel wareshown = new WareViewModel();
            using (var dbcontext = new ApplicationDbContext())
            {
                var ware = (from w in dbcontext.Wares where w.ID == id select w).Single();
                //var wareshown = new WareViewModel { Id = ware.ID, Name = ware.Name, Category = ware.Category, Description = ware.Description, ImageUrl = ware.ImageUrl, Price = ware.Price, Inventory = ware.Inventory };
                wareshown.Id = ware.ID;
                wareshown.Name = ware.Name;
                wareshown.Category = ware.Category;
                wareshown.Description = ware.Description;
                wareshown.Price = ware.Price;
                wareshown.Inventory = ware.Inventory;
                wareshown.ImageUrl = ware.ImageUrl;
            }
            return View(wareshown);
        }*/

        public ActionResult BuyWare(string id)
        {
            BuyWareViewModel model = new BuyWareViewModel();
            ApplicationUser merchant = new ApplicationUser();
            using(var dbcontext=new ApplicationDbContext())
            {
                string val = id;
                var currectuser = UserManager.FindByName(User.Identity.GetUserName());
                var customer = (from u in dbcontext.Users where u.Id == currectuser.Id select u).Single();
                dbcontext.Entry(customer).Collection(u => u.Goods).Load();
                var ware = (from w in dbcontext.Wares.Local where w.ID == val select w).Single();
                //ApplicationUser seller = dbcontext.Entry(ware).Reference(w => w.Owner).Load();
                merchant = BuyWareViewModel.FindSeller(ware.Owner.Id);
                model.Owne = customer.UserName;
                model.WareId = ware.ID;
                model.ShipAddress = customer.ShippingAddress;
                model.WareName = ware.Name;
                model.Seller = merchant.UserName;
                model.Price = ware.Price;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult BuyWare(BuyWareViewModel model)
        {
            using(var dbcontext=new ApplicationDbContext())
            {
                var customer = (from u in dbcontext.Users where u.UserName == model.Owne select u).Single();
                customer.OwnOrders.Add(new Order { OrderId = Guid.NewGuid().ToString(), CustomerChecked = true, SellerChecked = false, WareId = model.WareId, WareAmount = model.WareNumber, OrderAddress = model.ShipAddress, OrderTime = System.DateTime.Now,SellerId=model.Seller,CustomerId=customer.Id});
                dbcontext.SaveChanges();
            }

            return RedirectToAction("OrderSucceed");
        }

        public ActionResult Comment()
        {
            return View();
        }

        public ActionResult CommentSucceed()
        {
            return View();
        }

        public ActionResult Succeed()
        {
            return View();
        }

        public ActionResult OrderSucceed()
        {
            return View();
        }


        public ActionResult EntryWare()
        {
            ViewBag.Message = "产品录入";
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EntryWare(WareViewModel model,FormCollection form)
        {
            //using(var currentuser=UserManager.FindByNameAsync(User.Identity.G))
            using(var dbcontext=new ApplicationDbContext())
            {
                var currectuser =await UserManager.FindByNameAsync(User.Identity.GetUserName());
                var files = Request.Files;
                
                if (files.Count > 0)
                {
                    //List<WarePicture> temppictures=new List<WarePicture>();
                        var file = files[0];

                    var filename = Path.Combine(Server.MapPath("~/Content/Photos/"), Path.GetFileName(file.FileName));
                    file.SaveAs(filename);
                    string ImageUrl = "~/Resources/" + Path.GetFileName(file.FileName);
                    
                    var master = (from u in dbcontext.Users where u.UserName == currectuser.UserName select u).Single();
                    master.Goods.Add(new Ware { Name = model.Name, Category = model.Category, Price = model.Price, ID = Guid.NewGuid().ToString(), ImageUrl = ImageUrl,Inventory=model.Inventory });
                    dbcontext.SaveChanges();
                }
                    
                }
                /*foreach (HttpPostedFileBase file in fileToUpload)
                {
                    string path = System.IO.Path.Combine(Server.MapPath("~/Image"), System.IO.Path.GetFileName(file.FileName));
                    WarePicture pic = new WarePicture { Id = Guid.NewGuid().ToString(), ImageUrl = path };
                    dbcontext.Pictures.Add(pic);
                    ware.Images.Add(pic);
                    file.SaveAs(path);
                }*/
                //dbcontext.Wares.Add(ware);
                //dbcontext.SaveChanges();
            
            //ViewBag.Message = "成功发布";
            return RedirectToAction("Succeed");
        }

        public ActionResult OrderHand()
        {
            ApplicationUser customer = new ApplicationUser();
            ApplicationUser seller = new ApplicationUser();
            Ware targetware = new Ware();
            List<OrderHandViewModel> models = new List<OrderHandViewModel>();
            using(var dbcontext =new ApplicationDbContext())
            {
                var currectuser = UserManager.FindByName(User.Identity.GetUserName());
                var OrderHander = from o in dbcontext.Orders where o.SellerId == currectuser.Id select o;
                foreach(var singleorder in OrderHander)
                {
                    var model = new OrderHandViewModel();
                    customer = OrderHandViewModel.GetApplicationUser(singleorder.CustomerId);
                    seller = OrderHandViewModel.GetApplicationUser(singleorder.SellerId);
                    targetware = Ware.FindWare(singleorder.WareId);
                    model.OrderId = singleorder.OrderId;
                    model.SellerNmae = seller.UserName;
                    model.CustomerName = customer.UserName;
                    model.WareName = targetware.Name;
                    model.SellerChecked = singleorder.SellerChecked;
                    model.Sum = singleorder.total;
                    models.Add(model);
                }
            }
            return View(models);
        }

        public ActionResult DetailView()
        {
            return View();
        }
       
        public ActionResult OrderSee()
        {
            return View();
        }
       
    }
}