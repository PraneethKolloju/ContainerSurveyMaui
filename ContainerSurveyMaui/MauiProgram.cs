using ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Services;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Biometric;
using UraniumUI;
using SkiaSharp;
using Microsoft.Maui.Controls.Hosting;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;


namespace ContainerSurveyMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Georama-Light.tff", "GeoramaLight");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //builder.Services.AddScoped<AuthService>();
            

            builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);
            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<IGetPostService, GetPostSevice>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            return builder.Build();
        }
    }
}
