namespace Data.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        private static TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public static DateTime Now()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone);
        }

        public static DateTimeOffset StartOfWeek(this DateTimeOffset dt, DayOfWeek startOfWeek)
        {
            return GetDateFromDayOfTheWeek(dt, startOfWeek);
        }

        public static DateTimeOffset EndOfWeek(this DateTimeOffset dt, DayOfWeek endOfWeek)
        {
            return GetDateFromDayOfTheWeek(dt, endOfWeek);
        }

        private static DateTimeOffset GetDateFromDayOfTheWeek(DateTimeOffset dt, DayOfWeek dayOfTheWeek)
        {
            return DateTimeOffset.Now.AddDays(-(int)DateTimeOffset.Now.DayOfWeek + (int)dayOfTheWeek).Date;
        }
    }
}
