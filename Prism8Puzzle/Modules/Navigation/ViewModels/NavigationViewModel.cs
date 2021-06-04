using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism8Puzzle.Core;

namespace Navigation.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        #region Members

        private readonly IEventAggregator _eventAggregator;
        private bool _canNavigate;
        public DelegateCommand NavigateCommand { get; }

        #endregion

        #region Ctor

        public NavigationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand(Navigate, CanNavigate);
            _ = _eventAggregator.GetEvent<UpdateCanNavigateEvent>().Subscribe(UpdateCanNavigateEvent_EA_SUB);
            _canNavigate = true;
        }

        #endregion

        #region Methods

        private void UpdateCanNavigateEvent_EA_SUB(bool caNavigate)
        {
            _canNavigate = caNavigate;
            NavigateCommand.RaiseCanExecuteChanged();
        }

        private bool CanNavigate()
        {
            return _canNavigate;
        }

        private void Navigate()
        {
            _eventAggregator.GetEvent<NavigationClickEvent>().Publish(true);
        }

        #endregion
    }
}
