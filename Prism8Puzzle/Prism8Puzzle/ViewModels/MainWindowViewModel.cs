using Prism.Mvvm;

namespace Prism8Puzzle.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get => _title;
            set => _ = SetProperty(ref _title, value);
        }

        public MainWindowViewModel()
        {
            Title = "8Puzzle";
        }
    }
}
