using System;
using StudyCompanion.Resources.Strings;
using Microsoft.Extensions.Localization;

namespace StudyCompanion
{
    [ContentProperty(nameof(Key))]
    public class LocalizeExtension : IMarkupExtension
    {
        IStringLocalizer<AppResources> _localizer;

        public string Key { get; set; } = string.Empty;

        public LocalizeExtension()
        {
            _localizer = ServiceHelper.GetService<IStringLocalizer<AppResources>>();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            string localizedText = _localizer[Key];
            return localizedText;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}

