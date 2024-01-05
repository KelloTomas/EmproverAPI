using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmproverAPI.Models.DB
{
    public class DayStatistics
    {
        [Key]
        public int Id { get; set; }
        public Point Point { get; set; } = new Point();
        public int Kapitalizacia { get; set; }
        public int BuySellVolume { get; set; }
        //public object? Obj1 { get; set; }
        //public object? Obj2 { get; set; }
        //public object? Obj3 { get; set; }
        //public object? Obj4 { get; set; }
        //public object? Obj5 { get; set; }
    }
}
