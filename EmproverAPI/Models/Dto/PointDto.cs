namespace EmproverAPI.Models.Dto
{
    public class PointDto : IValidDto
    {
        public DateTime DateTime { get; set; }
        public int OpenValue { get; set; }
        public int CloseValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public string IsValid()
        {
            return MinValue > MaxValue
                ? $"MinValue {MinValue} must be less then {MaxValue}"
                : string.Empty;
        }
    }
}
