namespace ISDCompanion
{
    public class RealtimeSchedulingStepViewModel : ViewModel
    {
        public int Step { get; set; } = 0;
        public Color A { get; set; } = Colors.Transparent;
        public Color B { get; set; } = Colors.Transparent;
        public Color C { get; set; } = Colors.Transparent;
        public Color EDF { get; set; } = Colors.Transparent;
        public Color RMS { get; set; } = Colors.Transparent;
        private String text = "";
        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
    }
}