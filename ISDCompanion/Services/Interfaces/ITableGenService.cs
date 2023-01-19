namespace StudyCompanion
{
    public interface ITableGenService
    {
        Grid GenerateTable_TableHeader();

        Grid GenerateTable_EmptyTable();

        Grid GenerateTable_NextStep();

        Grid GenerateTable_PreviousStep();

        Grid GenerateTable_ShowSolution();

        String GetInfoText();

        bool InfoAvailable();

        int X_CoordoninatesOfInterest();
        int Y_CoordoninatesOfInterest();
    }
}
