namespace EmproverAPI.Models.Dto
{
    public class DayStatisticsDto : IValidDto
    {
        public PointDto Point { get; set; } = new PointDto();
        public int Kapitalizacia { get; set; }
        public int BuySellVolume { get; set; }

        public string IsValid()
        {
            return Point.IsValid();
        }
    }
}
