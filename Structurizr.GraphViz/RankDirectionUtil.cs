namespace Structurizr.GraphViz
{
    using System;

    public static class RankDirectionUtil
    {
        public static string GetCode(RankDirection direction)
        {
            switch (direction)
            {
                case RankDirection.TopBottom:
                    return "TB";
                case RankDirection.BottomTop:
                    return "BT";
                case RankDirection.LeftRight:
                    return "LR";
                case RankDirection.RightLeft:
                    return "RL";
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
