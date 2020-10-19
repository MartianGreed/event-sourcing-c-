using System;
using System.Globalization;

namespace Es.Domain
{
    public class DateTimeImmutable
    {
        private const string DATE_FORMAT = "dd/MM/yyyy HH:mm:ss";
        private DateTime _date;
        
        public int Day => _date.Day;
        public int Month => _date.Month;
        public int Year => _date.Year;
        public int Hour => _date.Hour;
        public int Minute => _date.Minute;
        public int Second  => _date.Second;
        
        private DateTimeImmutable(DateTime date)
        {
            _date = date;
        }
        
        public static DateTimeImmutable Now()
        {
            return new DateTimeImmutable(DateTime.Now);
        }

        public override string ToString()
        {
            return _date.ToString(DATE_FORMAT);
        }

        public static DateTimeImmutable FromString(string date)
        {
            return new DateTimeImmutable(
                DateTime.ParseExact(
                    date,
                    DATE_FORMAT,
                    new CultureInfo("fr-FR")
                )
            );
        }
    }
}