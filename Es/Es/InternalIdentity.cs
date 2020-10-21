
using System;
using Es.Domain;

namespace Es
{
    public class InternalIdentity : Identity<InternalIdentity>
    {
        public InternalIdentity(Guid uuid) : base(uuid)
        {
        }
    }
}