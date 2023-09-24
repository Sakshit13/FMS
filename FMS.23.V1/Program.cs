
using Castle.Core.Smtp;
using FMS._23.V1.BAL;
using FMS._23.V1.DAL;
using FMS._23.V1.Models;
using FMS._23.V1.REPOSITORY;
using FMS._23.V1.Services;
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IRepository<ItemCategories>, GenericRepository<ItemCategories>>();
builder.Services.AddScoped<IRepository<ItemViewModel>, GenericRepository<ItemViewModel>>();
builder.Services.AddScoped<IRepository<GrntransactionDetail>, GenericRepository<GrntransactionDetail>>();
builder.Services.AddScoped<IRepository<UnitOfMeasurment>, GenericRepository<UnitOfMeasurment>>();
builder.Services.AddScoped<ItemMasterBAL>();
builder.Services.AddScoped<GRNBAL>();
builder.Services.AddScoped<PoBAL>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddSingleton<IEmailSender>(provider =>
{
	var configuration = provider.GetRequiredService<IConfiguration>();

	// Ensure configuration is not null
	if (configuration == null)
	{
		throw new ArgumentNullException(nameof(configuration), "IConfiguration is null.");
	}

	// Retrieve SMTP settings
	var smtpSettings = configuration.GetSection("SmtpSettings");
	if (smtpSettings == null)
	{
		throw new ArgumentNullException(nameof(smtpSettings), "SmtpSettings section is null.");
	}

	return new SEmailSender(
		smtpSettings["SmtpServer"],
		int.Parse(smtpSettings["SmtpPort"]),
		smtpSettings["SmtpUsername"],
		smtpSettings["SmtpPassword"]
	);
});

builder.Services.Configure<IdentityOptions>(options =>
{
	// Configure your password requirements, lockout settings, etc. here
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
		policy.RequireRole("Admin"));
});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("CanEditPage", policy =>
		policy.RequireRole("Admin", "Editor")); // Adjust the policy as needed
});
builder.Services.AddAuthentication("Cookies")
	.AddCookie("Cookies", options =>
	{
		// Configure cookie options here if needed
	});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=AdminAccount}/{action=Login}/{id?}");

// Add an asynchronous Main method to call app.RunAsync()
await app.RunAsync();
