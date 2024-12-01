var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//create branch
//app.Map("/download", app1 =>
//{
//    app1.Run(async (context) =>
//    {
//        await context.Response.WriteAsync("no allow");

//    });
//});
//create nested branch
app.Map("/download", app1 =>
{
    app1.Map("/images", app2 => { 
    app2.Run(async (context) =>
    {
        await context.Response.WriteAsync("no allow");

    });
    });
});

app.Use(async (context, next) =>
{
    var id = context.Request.Query["id"];
    if (id.Count > 0)
        await context.Response.WriteAsync("the id==" + id);//ریسپانس را می دهد و تمام میشود 
    else
        await next.Invoke();
});
//app.Run(async context =>
//{
//   await context.Response.WriteAsync("run is complete");
//});
//app.Use(async (context, next) =>
//{
//    if (context.Request.Query["name"] == "ali")
//        await context.Response.WriteAsync("welwcomm ali");

//    else
//        await next.Invoke();
//});

app.Run();
