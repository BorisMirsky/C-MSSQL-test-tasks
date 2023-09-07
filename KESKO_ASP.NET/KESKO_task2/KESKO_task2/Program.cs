var builder = WebApplication.CreateBuilder(args);

// for to use httpcontext.session
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession();               
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();  
//     + app.UseSession();   below


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();  //

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
