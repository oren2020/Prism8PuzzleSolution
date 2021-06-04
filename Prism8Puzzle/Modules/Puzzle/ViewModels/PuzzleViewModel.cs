using Business;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism8Puzzle.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Puzzle.ViewModels
{
    public class PuzzleViewModel : BindableBase
    {
        #region Members

        // enum
        private enum Direction { BACK, FORTH }

        // private
        private const int _timeTick = 250;
        private readonly IPuzzleService _puzzleService;
        readonly IEventAggregator _eventAggregator;
        private Node _solutionNode;
        private Node _tempSolutionNode;
        private readonly DispatcherTimer _timer;
        private int _cnt;
        private int _moveCnt;
        private bool _isHint;
        private bool _isPublishMinCount;
        private bool _isShowSearchMesseage;
        private bool _isLoadFirst;
        private bool _isGameOver;
        private int _crumb;
        private readonly List<int> _breadCrumbsList;
        private readonly ObservableCollection<Tile> _shuffeldTilesList;

        // public
        public DelegateCommand<object> MoveCommand { get; }
        public DelegateCommand<object> HintMoveCommand { get; }
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwardCommand { get; }
        public DelegateCommand<object> IconsWebSiteCommand { get; }

        private Board _mainBoard;
        public Board MainBoard
        {
            get { return _mainBoard; }
            set { SetProperty(ref _mainBoard, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isViewBusy;
        public bool IsViewBusy
        {
            get { return _isViewBusy; }
            set { SetProperty(ref _isViewBusy, value); }
        }

        private string _busyContent;
        public string BusyContent
        {
            get { return _busyContent; }
            set { SetProperty(ref _busyContent, value); }
        }

        private int _hintTileProp;
        public int HintTileProp
        {
            get { return _hintTileProp; }
            set { SetProperty(ref _hintTileProp, value); }
        }

        private bool _isSettings;
        public bool IsSettings
        {
            get { return _isSettings; }
            set { SetProperty(ref _isSettings, value); }
        }

        #endregion

        #region Constructor

        public PuzzleViewModel(IPuzzleService puzzleService, IEventAggregator eventAggregator)
        {
            // ctr prop
            _puzzleService = puzzleService;
            _eventAggregator = eventAggregator;

            // event aggregator subscribe - xxx_EA_SUB
            _ = _eventAggregator.GetEvent<ShuffleClickEvent>().Subscribe(ShufflePuzzle_EA_SUB);
            _ = _eventAggregator.GetEvent<ResetClickEvent>().Subscribe(ResetPuzzle_EA_SUB);
            _ = _eventAggregator.GetEvent<ShowSolutionClickEvent>().Subscribe(ShowSolutionPuzzle_EA_SUB);
            _ = _eventAggregator.GetEvent<NavigationClickEvent>().Subscribe(NavigationSettings_EA_SUB);

            // delegate command
            MoveCommand = new DelegateCommand<object>(Move, CanMove);
            HintMoveCommand = new DelegateCommand<object>(HintClick, CanHintClick);
            GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            IconsWebSiteCommand = new DelegateCommand<object>(IconsWebSite);

            // private
            _cnt = 0;
            _moveCnt = 0;
            _isHint = false;
            _isPublishMinCount = false;
            _isShowSearchMesseage = false;
            _isLoadFirst = false;
            _isGameOver = false;
            _shuffeldTilesList = new ObservableCollection<Tile>();
            _solutionNode = new Node();
            _tempSolutionNode = new Node();
            _crumb = 0;
            _breadCrumbsList = new List<int>();

            // public
            IsBusy = false;
            IsViewBusy = false;
            BusyContent = "";
            MainBoard = new Board();
            HintTileProp = -1;
            IsSettings = false;

            // dispatcher timer - solution
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(_timeTick)
            };
            _timer.Tick += Timer_Tick;

            // load solution puzzle async
            LoadPuzzleService();
        }

        #endregion

        #region Methods

        // private helper methods
        private void GiveHint()
        {
            _isHint = true;
            _isShowSearchMesseage = true;
            SolveService();
        }
        private void HintMove(object param)
        {
            Move(param);
        }
        private void ShowSolution()
        {
            if (_solutionNode.BoardPathList.Count > 0)
            {
                MainBoard.TilesList.Clear();

                foreach (Tile t in _shuffeldTilesList)
                {
                    MainBoard.TilesList.Add(Tile.CreateTile(t.Number, t.XAxis, t.YAxis));
                }
                PublishIsGameOver(false);
                UpdateCanNavigateEvent_EA_PUB(false);

                if (!_timer.IsEnabled)
                {
                    IsViewBusy = true;
                    _timer.Start();
                }
            }
        }
        private void HintTile()
        {
            if (_tempSolutionNode.BoardPathList.Count > 1)
            {
                int idx = 0;
                int zeroIdx = -1;
                foreach (Tile t in _tempSolutionNode.BoardPathList[1].TilesList)
                {
                    if (t.Number == 0)
                    {
                        zeroIdx = idx;
                    }
                    idx++;
                }
                HintTileProp = _tempSolutionNode.BoardPathList[0].TilesList[zeroIdx].Number;
            }
        }
        private void SolutionView()
        {
            if (_cnt < _solutionNode.BoardPathList.Count)
            {
                if (_cnt > 0)
                {
                    TileClickEvent_EA_PUB(_cnt);
                }
                MainBoard = new Board(_solutionNode.BoardPathList[_cnt++].TilesList);
            }
            else
            {
                if (_timer.IsEnabled)
                {
                    _timer.Stop();
                    _cnt = 0;
                    IsViewBusy = false;
                    HintTileProp = -1;
                    PublishIsGameOver(true);
                    IsAllowResetAndSolutionEvent_EA_PUB();
                    UpdateCanNavigateEvent_EA_PUB(true);
                }
            }
        }
        private bool IsGameOver()
        {
            _isGameOver = true;
            for (int i = 0; i < MainBoard.TilesList.Count - 1; i++)
            {
                if (MainBoard.TilesList[i].Number != i + 1)
                {
                    _isGameOver = false;
                    break;
                }
            }
            return _isGameOver;
        }
        private void PublishIsGameOver(bool isOver)
        {
            _isGameOver = isOver;
            MoveCommand.RaiseCanExecuteChanged();
            HintMoveCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
            GoBackCommand.RaiseCanExecuteChanged();
            IsGameOverEvent_EA_PUB(isOver);
        }
        private void MoveHelper(object param, bool isGoBackOrForth, Direction backOrForth)
        {
            try
            {
                var num = Convert.ToInt32(param);
                if (MainBoard.IsAllowSwitch(num))
                {
                    MainBoard.Switch(num);
                }
                else
                {
                    return;
                }

                HintTileProp = -1;
                if (!isGoBackOrForth)
                {
                    _isHint = true;
                    if (_breadCrumbsList.Count > _crumb)
                    {
                        _breadCrumbsList.RemoveRange(_crumb, _breadCrumbsList.Count - _crumb);
                    }
                    _breadCrumbsList.Add(num);
                }

                switch (backOrForth)
                {
                    case Direction.BACK:
                        _crumb--;
                        TileClickEvent_EA_PUB(--_moveCnt);
                        break;
                    case Direction.FORTH:
                        _crumb++;
                        TileClickEvent_EA_PUB(++_moveCnt);
                        break;
                }
            }
            catch (Exception) { }

            if (IsGameOver())
            {
                PublishIsGameOver(true);
            }
            else
            {
                HintMoveCommand.RaiseCanExecuteChanged();
                GoForwardCommand.RaiseCanExecuteChanged();
                GoBackCommand.RaiseCanExecuteChanged();
                MoveCommand.RaiseCanExecuteChanged();
            }
        }

        // dispatcher timer tick event - solution
        private void Timer_Tick(object sender, EventArgs e)
        {
            SolutionView();
        }

        // event aggregation subscribers
        private void ShufflePuzzle_EA_SUB(bool obj)
        {
            _isPublishMinCount = true;
            HintTileProp = -1;
            LoadShuffledPuzzleService();
        }
        private void ResetPuzzle_EA_SUB(bool obj)
        {
            _isPublishMinCount = true;
            HintTileProp = -1;
            _crumb = 0;
            _moveCnt = 0;
            _breadCrumbsList.Clear();
            MainBoard.TilesList.Clear();
            foreach (Tile t in _shuffeldTilesList)
            {
                MainBoard.TilesList.Add(Tile.CreateTile(t.Number, t.XAxis, t.YAxis));
            }
            PublishIsGameOver(false);
        }
        private void ShowSolutionPuzzle_EA_SUB(bool obj)
        {
            ShowSolution();
        }
        private void NavigationSettings_EA_SUB(bool obj)
        {
            IsSettings = !IsSettings;
            if (IsSettings)
            {
                LoadPuzzleService();
            }
            UpdateSettingModeEvent_EA_PUB();
        }

        // event aggregation publishers
        private void TileClickEvent_EA_PUB(int n)
        {
            _eventAggregator.GetEvent<TileClickEvent>().Publish(n);
        }
        private void IsGameOverEvent_EA_PUB(bool isOver)
        {
            _eventAggregator.GetEvent<IsGameOverEvent>().Publish(isOver);
        }
        private void IsAllowResetAndSolutionEvent_EA_PUB()
        {
            _eventAggregator.GetEvent<IsAllowResetAndSolutionEventAg>().Publish(true);
        }
        private void UpdateSettingModeEvent_EA_PUB()
        {
            _eventAggregator.GetEvent<UpdateSettingModeEvent>().Publish(IsSettings);
        }
        private void UpdateCanNavigateEvent_EA_PUB(bool caNavigate)
        {
            _eventAggregator.GetEvent<UpdateCanNavigateEvent>().Publish(caNavigate);
        }

        // delegate commands
        private bool CanHintClick(object arg)
        {
            return !_isGameOver;
        }
        private void HintClick(object param)
        {
            if (HintTileProp > 0)
            {
                HintMove(param);
                HintTileProp = -1;
            }
            else
            {
                GiveHint();
            }
        }
        private bool CanMove(object arg)
        {
            return !_isGameOver;
        }
        private void Move(object param)
        {
            MoveHelper(param, false, Direction.FORTH);
        }
        private bool CanGoForward()
        {
            return _breadCrumbsList.Count > 0 && _crumb < _breadCrumbsList.Count && !_isGameOver;
        }
        private void GoForward()
        {
            MoveHelper(_breadCrumbsList[_crumb], true, Direction.FORTH);
        }
        private bool CanGoBack()
        {
            return _breadCrumbsList.Count > 0 && _crumb > 0 && !_isGameOver;
        }
        private void GoBack()
        {
            MoveHelper(_breadCrumbsList[_crumb - 1], true, Direction.BACK);
        }
        private void IconsWebSite(object url)
        {
            try
            {
                System.Diagnostics.Process.Start(url as string);
            }
            catch (Exception)
            {
            }
        }

        // async services
        private void LoadPuzzleService()
        {
            IsBusy = true;
            BusyContent = "Loading Puzzle...";
            _puzzleService.GetSolutionPuzzleAsync((sender, result) =>
            {
                IsBusy = false;
                MainBoard.TilesList = new ObservableCollection<Tile>(result.Result);
                PublishIsGameOver(true);
            });
        }
        private void LoadShuffledPuzzleService()
        {
            IsBusy = true;
            BusyContent = "Loading Shuffled Puzzle...";
            UpdateCanNavigateEvent_EA_PUB(false);
            _puzzleService.GetRandomPuzzleAsync((sender, result) =>
            {
                MainBoard.TilesList = new ObservableCollection<Tile>(result.Result);
                _shuffeldTilesList.Clear();
                foreach (Tile t in MainBoard.TilesList)
                {
                    _shuffeldTilesList.Add(Tile.CreateTile(t.Number, t.XAxis, t.YAxis));
                }
                _isShowSearchMesseage = false;
                _isHint = false;
                _moveCnt = 0;
                _isLoadFirst = true;
                SolveService();
            });
        }
        private void SolveService()
        {
            _puzzleService.Board = MainBoard;
            IsBusy = true;
            if (_isShowSearchMesseage)
            {
                BusyContent = "Searching Next Move...";
            }

            _puzzleService.GetSolvedPuzzleAsync((sender, result) =>
            {
                IsBusy = false;
                UpdateCanNavigateEvent_EA_PUB(true);
                if (!_isHint)
                {
                    _solutionNode = result.Result;
                    _tempSolutionNode = new Node(_solutionNode.BoardPathList);
                    _crumb = 0;
                    _breadCrumbsList.Clear();
                }
                else
                {
                    _tempSolutionNode = result.Result;
                    HintTile();
                    _isHint = false;
                }

                if (_isPublishMinCount)
                {
                    int minMoves = _solutionNode.BoardPathList.Count - 1;
                    _eventAggregator.GetEvent<MinMovesCountEvent>().Publish(minMoves);
                    _isPublishMinCount = false;
                }

                if (_isLoadFirst)
                {
                    PublishIsGameOver(false);
                    _isLoadFirst = false;
                }

                if (IsGameOver())
                {
                    PublishIsGameOver(true);
                }

                IsAllowResetAndSolutionEvent_EA_PUB();
            });
        }

        #endregion
    }
}
