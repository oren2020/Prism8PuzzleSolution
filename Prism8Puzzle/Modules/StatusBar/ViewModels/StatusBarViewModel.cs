using Prism.Events;
using Prism.Mvvm;
using Prism8Puzzle.Core;

namespace StatusBar.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {
        #region Members

        private readonly IEventAggregator _eventAggregator;

        private int _movesCount;
        public int MovesCount
        {
            get { return _movesCount; }
            set { SetProperty(ref _movesCount, value); }
        }

        private int _minMoves;
        public int MinMoves
        {
            get { return _minMoves; }
            set { SetProperty(ref _minMoves, value); }
        }

        private bool _isGmeOver;
        public bool IsGmeOver
        {
            get { return _isGmeOver; }
            set { SetProperty(ref _isGmeOver, value); }
        }

        #endregion

        #region Constructor

        public StatusBarViewModel( IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<TileClickEvent>().Subscribe(UpdateMovesCount_EA_SUB);
            _eventAggregator.GetEvent<ResetMovesCountEvent>().Subscribe(ResetMovesCountt_EA_SUB);
            _eventAggregator.GetEvent<MinMovesCountEvent>().Subscribe(MinMovesCountt_EA_SUB);
            _eventAggregator.GetEvent<IsGameOverEvent>().Subscribe(IsGameOvert_EA_SUB);
            _eventAggregator.GetEvent<UpdateSettingModeEvent>().Subscribe(UpdateSettingModeEvent_EA_SUB);
            IsGmeOver = false;
        }

        #endregion

        #region Methods

        private void UpdateSettingModeEvent_EA_SUB(bool obj)
        {
            IsGmeOver = true;
            MinMoves = 0;
            MovesCount = 0;
        }

        private void IsGameOvert_EA_SUB(bool obj)
        {
            IsGmeOver = obj;
        }

        private void MinMovesCountt_EA_SUB(int obj)
        {
            MinMoves = obj;
        }

        private void ResetMovesCountt_EA_SUB(bool obj)
        {
            MovesCount = 0;
        }

        private void UpdateMovesCount_EA_SUB(int obj)
        {
            MovesCount = obj;
        }

        #endregion
    }
}
