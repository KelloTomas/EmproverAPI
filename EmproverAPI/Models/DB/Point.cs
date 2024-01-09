using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace EmproverAPI.Models.DB
{
    [PrimaryKey(nameof(DayStatisticsSymbolCode), nameof(DayStatisticsDateTime), nameof(Time))]
    public class Point
    {
        private DayStatistics _dayStatistics;

        public DateTime Time { get; set; }

        public int OpenValue { get; set; }

        public int CloseValue { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        // Foreign key
        [JsonIgnore]
        public string DayStatisticsSymbolCode { get; set; }

        [JsonIgnore]
        public DateTime DayStatisticsDateTime { get; set; }

        [JsonIgnore]
        public DayStatistics DayStatistics
        {
            get => _dayStatistics;
            set
            {
                DayStatisticsSymbolCode = value.SymbolCode;
                DayStatisticsDateTime = value.DateTime;
                _dayStatistics = value;
            }
        }
    }
}
