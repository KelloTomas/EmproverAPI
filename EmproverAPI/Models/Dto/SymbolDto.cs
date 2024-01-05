namespace EmproverAPI.Models.Dto
{
    public class SymbolDto : IValidDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // can be EUR/USD, Gold, Silver, Cupprum
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public List<DayStatisticsDto> DayStatistics { get; set; } = new List<DayStatisticsDto>();

        public string IsValid()
        {
            string errorMsg = string.Empty;
            int StringLength = 15; // ToDo make as constant
            if (Code.Length > StringLength)
            {
                errorMsg += $"Max Code lenght is {StringLength} but get {Code.Length}; ";
            }
            if (Name.Length > StringLength)
            {
                errorMsg += $"Max Name lenght is {StringLength} but get {Name.Length}; ";
            }
            if (ValidFrom > ValidTo)
            {
                errorMsg += $"ValidFrom {ValidFrom} is more then ValidTo {ValidTo}; ";
            }
            errorMsg += DayStatistics.Select(d => d.IsValid());

            return errorMsg;
        }
    }
}
