
namespace OpenAIApp;

public class Program
{
    public static void Main(string[] args)
    {

        Modules.SignUp.SignUpService.SignUp(
            new Modules.SignUp.SignUpBody
            {
                Email = "",
                Password = "password123"
            }
        );

        Config.Environment.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
