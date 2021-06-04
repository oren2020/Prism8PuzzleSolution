using Business;

namespace Prism8Puzzle.Core
{
    public class NodeServiceResult
    {
        public NodeServiceResult(Node result)
        {
            Result = result;
        }

        public Node Result { get; }
    }
}
