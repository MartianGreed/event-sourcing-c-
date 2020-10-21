using System;
using System.Text.RegularExpressions;
using Xunit;

namespace Es.Domain
{
    public class IdentityTest
    {
        [Fact]
        public void TestItCanGenerateUuid()
        {
            InternalIdentity id = Identity<InternalIdentity>.Generate();
            Assert.Matches("^[a-f0-9]{8}-[a-f0-9]{4}-4[a-f0-9]{3}-[89aAbB][a-f0-9]{3}-[a-f0-9]{12}$", id.ToString());
        }

        [Fact]
        public void TestItCanHydrateBackFromString()
        {
            InternalIdentity id = Identity<InternalIdentity>.Generate();
            InternalIdentity id2 = Identity<InternalIdentity>.FromString(id.ToString());
            
            Assert.Equal(id.ToString(), id2.ToString());
        }
    }
}