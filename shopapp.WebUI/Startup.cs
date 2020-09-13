using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shopapp.Core.Abstract;
using shopapp.Core.Concrete.EFCore;
using shopapp.Web.EmailService;
using shopapp.Web.Identity;
using shopapp.WebUI.Middlewares;
using shoppapp.Services.Abstract;
using shoppapp.Services.Concrete;

namespace shopapp.WebUI
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; } //appsettings içerisinden bilgi almak için tanımlanır.

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true;//Parola içerisinde sayısal bir değer ister
                options.Password.RequireLowercase = true; // Parola içerisinde küçük bir karakter ister
                options.Password.RequiredLength = 6;//min 6 karakter
                options.Password.RequireNonAlphanumeric = true;//özel karakter ister
                options.Password.RequireUppercase = true;//Büyük harf ister

                options.Lockout.MaxFailedAccessAttempts = 5;//en fazla 5 kere yanlış girebilir.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//5dk boyunca kullnıcı parola girişi yapamaz
                options.Lockout.AllowedForNewUsers = true;//yeni bir kullanıcı içinde geçerli olur lockout işlemi

                options.User.RequireUniqueEmail = true;//aynı mail adresi ile başkası giriş yaptıysa üyelik oluşturmaz.

                options.SignIn.RequireConfirmedEmail = true;//Onay yapmadan uygulamaya login olmamaz.
                options.SignIn.RequireConfirmedPhoneNumber = false;//telefon onayı için kullanılır.
            });

            services.ConfigureApplicationCookie(options =>
            {
                //cookie ayarları burada yapılır
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";//kullanıcı yetkisi olmayan bir sayfaya girdiğinde yöneleceği sayfa
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);//Cookie'nin varsayılan(20dk) değerini 60 olarak güncelledik.
                options.SlidingExpiration = true;//kullanıcı uygulamaya bir request gönderdiğinde cookie süresi tekrardan 60 dk olarak güncellenir.
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,//scriptler cookielere ulaşamaz.Güvenlik açısından ulaşamaması gerekir.
                    Name = "ShopApp.v2.Security.Cookie",
                    SameSite = SameSiteMode.Strict//Croos Site Attackslarını engellemek için oluşturuldu.
                };

            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderManager>();

            services.AddScoped<IEmailSender, EmailSender>(i =>
                new EmailSender(_configuration["EmailSender:Host"], _configuration.GetValue<int>("EmailSender:Port"), _configuration.GetValue<bool>("EmailSender:EnableSSL"), _configuration["EmailSender:UserName"], _configuration["EmailSender:Password"]));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.CustomStaticFiles();
            app.UseAuthentication();//usemvc'den önce olmak zorunda.
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "adminProducts",
                    template: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }
                    );

                routes.MapRoute(
                    name: "adminProducts",
                    template: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "EditProduct" }
                    );

                routes.MapRoute(
                    name: "products",
                    template: "products/{category?}",
                    defaults: new { controller = "Shop", action = "List" }
                    );
                routes.MapRoute(
                    name: "cart",
                    template: "cart",
                    defaults: new { controller = "Cart", action = "Index" }
                    );
                routes.MapRoute(
                    name: "orders",
                    template: "orders",
                    defaults: new { controller = "Cart", action = "GetOrders" }
                    );
                routes.MapRoute(
                    name: "checkout",
                    template: "checkout",
                    defaults: new { controller = "Cart", action = "Checkout" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );

            });

            SeedIdentity.Seed(userManager, roleManager, _configuration).Wait();


        }
    }
}
