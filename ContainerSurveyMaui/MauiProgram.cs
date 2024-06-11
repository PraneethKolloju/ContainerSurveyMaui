using Syncfusion.Maui.Core.Hosting;
using ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Services;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Biometric;
using UraniumUI;

namespace ContainerSurveyMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
            .ConfigureSyncfusionCore()
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
            builder.Services.AddTransient<LoadingPage>();

            return builder.Build();
        }
    }
}
