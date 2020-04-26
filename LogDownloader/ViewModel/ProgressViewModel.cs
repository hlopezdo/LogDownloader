namespace LogDownloader.ViewModel
{
    public class ProgressViewModel:BaseViewModel
    {
        public ProgressViewModel()
        {
            Initialize();
        }

        public double Value { get; set; }

        public double Maximum { get; set; }

        public void Initialize()
        {
            Maximum = 100;
            Value = 0;
        }

        public void SetMaximum(double value)
        {
            Maximum = value;
        }

        public void Increment()
        {
            Value += 1;
        }
    }
}
