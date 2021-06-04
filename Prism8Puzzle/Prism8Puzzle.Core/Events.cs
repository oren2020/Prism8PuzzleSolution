using Prism.Events;

namespace Prism8Puzzle.Core
{
    public class ShuffleClickEvent : PubSubEvent<bool> { }
    public class ShowSolutionClickEvent : PubSubEvent<bool> { }
    public class TileClickEvent : PubSubEvent<int> { }
    public class ResetClickEvent : PubSubEvent<bool> { }
    public class ResetMovesCountEvent : PubSubEvent<bool> { }
    public class MinMovesCountEvent : PubSubEvent<int> { }
    public class IsGameOverEvent : PubSubEvent<bool> { }
    public class IsAllowResetAndSolutionEventAg : PubSubEvent<bool> { }
    public class NavigationClickEvent : PubSubEvent<bool> { }
    public class UpdateSettingModeEvent : PubSubEvent<bool> { }
    public class UpdateCanNavigateEvent : PubSubEvent<bool> { }
}
