namespace SlimWaist.Extentions
{
    public class ChangeDirections
    {
        public FlowDirection this[string key] => FlowDirection;

        public FlowDirection FlowDirection { get; set; } = FlowDirection.RightToLeft;

        public static ChangeDirections instance { get; set; } = new ChangeDirections();
    }
}
