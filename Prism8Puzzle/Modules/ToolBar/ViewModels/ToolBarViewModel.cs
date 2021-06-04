using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism8Puzzle.Core;

namespace ToolBar.ViewModels
{
    public class ToolBarViewModel : BindableBase
    {
        #region Members

        private readonly IEventAggregator _eventAggregator;
        private bool _isAllowResetAndSolution;
        private bool _isAllowShuffel;
        public DelegateCommand ShuffleCommand { get; }
        public DelegateCommand ResetCommand { get; }
        public DelegateCommand ShowSolutionCommand { get; }

        #endregion

        #region Constructor

        public ToolBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _isAllowResetAndSolution = false;
            _isAllowShuffel = true;
            ShuffleCommand = new DelegateCommand(Shuffle, CanShuffle);
            ResetCommand = new DelegateCommand(Reset, CanReset);
            ShowSolutionCommand = new DelegateCommand(ShowSolution, CanShowSolution);
            _ = _eventAggregator.GetEvent<UpdateSettingModeEvent>().Subscribe(UpdateSettingModeEvent_EA_SUB);
            _ = _eventAggregator.GetEvent<IsAllowResetAndSolutionEventAg>().Subscribe(IsAllowResetAndSolutionEvent_EA_SUB);
        }

        #endregion

        #region Methods

        private bool CanShuffle()
        {
            return _isAllowShuffel;
        }

        private void Shuffle()
        {
            _isAllowShuffel = false;
            _isAllowResetAndSolution = false;
            ShuffleCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
            ShowSolutionCommand.RaiseCanExecuteChanged();
            _eventAggregator.GetEvent<ResetMovesCountEvent>().Publish(true);
            _eventAggregator.GetEvent<ShuffleClickEvent>().Publish(true);
        }

        private bool CanReset()
        {
            return _isAllowResetAndSolution;
        }

        private void Reset()
        {
            _eventAggregator.GetEvent<ResetClickEvent>().Publish(true);
            _eventAggregator.GetEvent<ResetMovesCountEvent>().Publish(true);
        }

        private bool CanShowSolution()
        {
            return _isAllowResetAndSolution;
        }

        private void ShowSolution()
        {
            _isAllowShuffel = false;
            _isAllowResetAndSolution = false;
            ShuffleCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
            ShowSolutionCommand.RaiseCanExecuteChanged();
            _eventAggregator.GetEvent<ShowSolutionClickEvent>().Publish(true);
        }

        private void IsAllowResetAndSolutionEvent_EA_SUB(bool obj)
        {
            _isAllowResetAndSolution = true;
            _isAllowShuffel = true;
            ResetCommand.RaiseCanExecuteChanged();
            ShowSolutionCommand.RaiseCanExecuteChanged();
            ShuffleCommand.RaiseCanExecuteChanged();
        }

        private void UpdateSettingModeEvent_EA_SUB(bool isSettings)
        {
            _isAllowShuffel = !isSettings;
            _isAllowResetAndSolution = !isSettings && _isAllowResetAndSolution;
            ResetCommand.RaiseCanExecuteChanged();
            ShowSolutionCommand.RaiseCanExecuteChanged();
            ShuffleCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
