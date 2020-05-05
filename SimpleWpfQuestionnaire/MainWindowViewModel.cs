using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;


namespace SimpleWpfQuestionnaire
{
    public class MainWindowViewModel : ViewModelBase
    {
        public int[] Answers { get; set; }
        public string PercentageOfTingles { get; set; }

        public ICommand ResetCommand { get; }
        public ICommand ResultCommand { get; }

        public MainWindowViewModel()
        {
            ResetCommand = new RelayCommand(Reset);
            ResultCommand = new RelayCommand(ShowResult, CanShowResult);

            Reset();
        }

        private void Reset()
        {
            Answers = new int[5] { -1, -1, -1, -1, -1 };
            RaisePropertyChanged("Answers");

            PercentageOfTingles = string.Empty;
            RaisePropertyChanged("PercentageOfTingles");
        }

        private void ShowResult()
        {
            PercentageOfTingles = string.Format("You get {0:d}% of tingles.", SumOfAnswers() * 10);
            RaisePropertyChanged("PercentageOfTingles");
        }

        private int SumOfAnswers()
        {
            int sum = 0;

            foreach (int answer in Answers)
                sum += answer;

            return sum;
        }

        private bool CanShowResult()
        {
            bool questionnaireCompleted = true;

            foreach (int answer in Answers)
                questionnaireCompleted = questionnaireCompleted && (answer >= 0) && (answer <= 2);

            return questionnaireCompleted;
        }
    }
}