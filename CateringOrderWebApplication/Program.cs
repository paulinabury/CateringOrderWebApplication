using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Repositories;
using CateringOrderWebApplication.Repositories.BlogPosts;
using CateringOrderWebApplication.Repositories.Caterings;
using CateringOrderWebApplication.Repositories.Tags;
using CateringOrderWebApplication.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// AddBlogPost services to the container.
builder.Services.AddControllersWithViews();

// Database context - dependency injection
builder.Services.AddDbContext<CateringOrderDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// AuthDb context - dependency injection
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AuthDbConnection")
    )
);

builder.Services.Configure<PasswordHasherOptions>(options =>
    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
);

// Configure password requirements
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

// TUser, TRole - dependency injection
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

// Repositories - dependency injection
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICateringRepository, CateringRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();