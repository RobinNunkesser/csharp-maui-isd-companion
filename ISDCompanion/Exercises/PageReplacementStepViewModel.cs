namespace StudyCompanion
{
    public class PageReplacementStepViewModel : ExerciseStepViewModel
    {
        public String Element { get; set; } = String.Empty;
        public String[] Frames { get; set; } = new string[4];
        public String[] FrameInformation { get; set; } = new string[4];
        public String AdditionalInfo { get; set; } = String.Empty;
    }
}

