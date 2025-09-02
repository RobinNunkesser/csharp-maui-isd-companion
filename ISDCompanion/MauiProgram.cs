using Microsoft.Maui.Controls;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Italbytz.Adapters.Meal.STWPB;
using System.Globalization;
using Italbytz.Ports.Meal;
using Mensa.Core;
using StudyCompanion.Ports;
using ISDCompanion.Core;
using Italbytz.Adapters.Meal.Mock;


namespace StudyCompanion;

internal enum Environment
{
    Development,
    Staging,
    Production
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        AppContext.SetSwitch(
            "System.Reflection.NullabilityInfoContext.IsSupported", true);
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
                break;
            case Environment.Staging:
            case Environment.Production:
            default:
                break;
        }

        //builder.Services.AddSingleton<IGetMealsService, MockGetMealsService>();
        builder.Services.AddSingleton<IGetMealsService>(
            new GetMealsService(new MealRepository(Secrets.id,
                CultureInfo.CurrentCulture.TwoLetterISOLanguageName)));
        builder.Services.AddSingleton<IGetCoursesService, GetCoursesService>();

        return builder.Build();
    }

    private static MauiAppBuilder RegisterServices(
        this MauiAppBuilder mauiAppBuilder
    )
    {
        mauiAppBuilder.Services.AddSingleton<MensaPage>();
        mauiAppBuilder.Services.AddSingleton<MensaViewModel>();
        mauiAppBuilder.Services.AddSingleton<CoursesPage>();
        mauiAppBuilder.Services.AddSingleton<CoursesViewModel>();
        return mauiAppBuilder;
    }
}