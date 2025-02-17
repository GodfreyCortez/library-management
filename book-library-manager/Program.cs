using book_library_manager.Models;
using book_library_manager.Services;

var MyAllowSpecificOrigins = "AllowSpecificOriginsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<BooksService>();
builder.Services.AddSingleton<OpenLibraryService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5174");
                          policy.WithHeaders(["Content-Type"]);
                          policy.AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

SeedData.Initialize(RavenDbContext.Store);
OpenLibraryService olservice = new OpenLibraryService();
app.Run();
