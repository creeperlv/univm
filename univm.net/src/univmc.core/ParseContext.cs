using LibCLCC.NET.TextProcessing;

namespace univmc.core
{
    public class ParseContext
    {
        public Segment HEAD;
        public Segment Current;
        public Segment? End;
        public ParseContext(Segment head)
        {
            HEAD = head;
            Current = head;
        }
        public string GetCurrentContent() => Current.content;
        public MatchResult IsLabel()
        {
            if (Current.Next is null)
                return MatchResult.ReachEnd;
            if (Current == End)
                return MatchResult.ReachEnd;
            if (Current.Next == End)
                return MatchResult.No;
            if (Current.Next.content == "")
                if (Current.Next.Next is null)
                    return MatchResult.ReachEnd;
            return Current.Next.content == ":" ? MatchResult.Match : MatchResult.No;
        }
        public bool IsCurrentLastSegment()
        {
            if (Current.Next is null) return true;
            if (Current.Next.content == "")
                if (Current.Next.Next is null) return true;
            return false;
        }
        public bool GoNext()
        {
            if (Current.Next is null) return false;
            if (Current.Next.content == "")
                if (Current.Next.Next is null) return false;
            Current = Current.Next;
            return true;
        }
    }
}
