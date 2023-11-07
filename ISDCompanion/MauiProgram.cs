using Microsoft.Maui.Controls;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace StudyCompanion;

enum Environment
{
    Development,
    Staging,
    Production
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var env = Environment.Production;
#if DEBUG
        env = Environment.Development;
#endif
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("SourceCodePro-Regular.ttf");
            })
            .RegisterServices();

        switch (env)
        {
            case Environment.Development:
                builder.Logging.AddDebug();
                /*                builder.Services.AddSingleton<IGetMealsService, MockGetMealsService>();
                                builder.Services.AddSingleton<IGetCoursesService, MockGetCoursesService>();*/
                break;
            case Environment.Staging:
            case Environment.Production:
                /*builder.Services.AddSingleton<IGetMealsService>(new OpenMensaGetMealsService(new OpenMensaMealDataSource(35, DateTime.Now)));
                builder.Services.AddSingleton<IGetCoursesService, MockGetCoursesService>();*/
                break;
            default:
                break;
        }

        return builder.Build();
    }

    private static MauiAppBuilder RegisterServices(
        this MauiAppBuilder mauiAppBuilder
    )
    {
        /*mauiAppBuilder.Services.AddSingleton<QuizPage>();
        mauiAppBuilder.Services.AddSingleton<QuizViewModel>();*/
        mauiAppBuilder.Services.AddSingleton<MensaPage>();
        mauiAppBuilder.Services.AddSingleton<MensaViewModel>();
        /*mauiAppBuilder.Services.AddSingleton<CoursesPage>();
        mauiAppBuilder.Services.AddSingleton<CourseViewModel>();*/
        /*mauiAppBuilder.Services.AddSingleton<ProfsPage>();
        mauiAppBuilder.Services.AddSingleton<ProfsViewModel>();*/
        /*mauiAppBuilder.Services.AddSingleton<SettingsPage>();
        mauiAppBuilder.Services.AddSingleton<SettingsViewModel>();*/
        return mauiAppBuilder;
    }
}

