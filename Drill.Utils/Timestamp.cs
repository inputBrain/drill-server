namespace Drill.Utils
{
    public static class Timestamp
    {
        public static long Current => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        public static long CurrentDate => new DateTimeOffset(DateTime.UtcNow.Date).ToUnixTimeSeconds();
        
        
        public static long ToUnixTime(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToUnixTimeSeconds();
        }

        
        public static DateTime ToUtcDateTime(long unixTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
        }


        public static DateTime ToLocalDateTime(long unixTimestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
        }
    }
}
