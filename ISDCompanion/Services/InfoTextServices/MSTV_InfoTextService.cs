using StudyCompanion.Resources.Strings;
using Italbytz.Networking.Abstractions;
using Italbytz.Graph.Abstractions;

namespace StudyCompanion.Services.InfoTextServices
{
    internal static class MSTV_InfoTextService
    {
        public static string GetInfoText(ITaggedEdge<string, double> values)
        {
            var InfoText = "";
            InfoText += AppResources.MSTV_Info_1 + "'" + values.Source + "'" + AppResources.MSTV_Info_2 + "\n";
            InfoText += "'" + values.Target + "'" + AppResources.MSTV_Info_3 + "\n";
            InfoText += AppResources.MSTV_Info_4 + "'" + values.Tag + "'.\n";

            return InfoText;
        }
    }
}
