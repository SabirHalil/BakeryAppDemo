using BakeryAppUI.Controllers;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddSingleton<ITakenCourseService, TakenCourseManager>();

builder.Services.AddHttpClient<ApiService>();




// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "customRoute",
        pattern: "Gün-Sonu-Hesap", // Ýstediðiniz URL'yi buraya yazýn
        defaults: new { controller = "EndOfDayAccount", action = "Index" } // Controller ve action'ý kendi ihtiyaçlarýnýza göre deðiþtirin
    );

    //endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
