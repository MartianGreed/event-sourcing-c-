using Xunit;

namespace Es.Domain
{
    public class DomainMessageTest
    {
        [Fact]
        public void TestItCanRecordAnEvent()
        {
            string id = Identity.Generate().ToString();
            int playhead = 0;
            string payload = "Dummy payload";
            DateTimeImmutable date = DateTimeImmutable.Now();
            DomainMessage dm = DomainMessage.RecordNow(id, playhead, payload, date);

            Assert.IsType<DomainMessage>(dm);
            Assert.Equal(id, dm.Id);   
            Assert.Equal(playhead, dm.Playhead);   
            Assert.Equal(payload, dm.Payload);   
            Assert.Equal(date, dm.RecordedAt);
        }
    }
}