using System;

namespace Es.Domain
{
    public class DomainMessage
    {
        public string Id { get; }
        public int Playhead { get; }
        public string Payload { get; }
        public DateTimeImmutable RecordedAt { get; }

        public DomainMessage(string id, int playhead, string payload, DateTimeImmutable recordedAt)
        {
            Id = id;
            Playhead = playhead;
            Payload = payload;
            RecordedAt = recordedAt;
        }
        
        public static DomainMessage RecordNow(string id, int playhead, string payload, DateTimeImmutable recordedAt)
        {
            return new DomainMessage(id, playhead, payload, recordedAt);
        }
    }
}