using Xunit;

namespace Es.Domain
{
    public class DateTimeImmutableTest
    {
        [Fact]
        public void TestItGenerateAProperDate()
        {
            DateTimeImmutable date = DateTimeImmutable.Now();
            
            Assert.IsType<DateTimeImmutable>(date);
        }
        
        [Fact]
        public void TestItCanFormatDateForHuman()
        {
            DateTimeImmutable date = DateTimeImmutable.Now();
            
            Assert.Matches(@"^[0-9]{2}\/[0-9]{2}\/[0-9]{4}\s[0-9]{2}:[0-9]{2}:[0-9]{2}$", date.ToString());
        }

        [Fact]
        public void TestItCanCreateADateTimeObjectFromString()
        {
            string d = "18/10/2020 12:41:37";
            DateTimeImmutable date = DateTimeImmutable.FromString(d);
            
            Assert.Equal(18, date.Day);
            Assert.Equal(10, date.Month);
            Assert.Equal(2020, date.Year);
            Assert.Equal(12, date.Hour);
            Assert.Equal(41, date.Minute);
            Assert.Equal(37, date.Second);
        }
    }
}