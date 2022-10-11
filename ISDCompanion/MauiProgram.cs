﻿namespace ISDCompanion;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                /*fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");*/
                fonts.AddFont("SourceCodePro-Regular.ttf");
            });

        builder.Services.AddLocalization();

        return builder.Build();
    }
}

