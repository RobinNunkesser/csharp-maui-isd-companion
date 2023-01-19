using StudyCompanion.Exercises.Baseclasses;
using StudyCompanion.Resources.Strings;
using System;

namespace StudyCompanion.Services.InfoTextServices
{
    internal static class ShortestPath_InfoTextService
    {
        public static string GetInfoText(PathSolution pathSolution)
        {
            string InfoText = "";
            //last step infos
            var values = pathSolution;
            InfoText += AppResources.ShortestPath_Info_1 + "'" + values.First + "'" + AppResources.ShortestPath_Info_2;
            InfoText += "'" + values.Last + "'" + AppResources.ShortestPath_Info_3 + "\n";

            //no need for translations
            foreach (var value in values.Values)
            {
                var pathValues = value.Value;
                InfoText += pathValues[0] + " ->(" + pathValues[1] + ") " + pathValues[2] + "\n";
            }
            return InfoText;
        }
    }
}
