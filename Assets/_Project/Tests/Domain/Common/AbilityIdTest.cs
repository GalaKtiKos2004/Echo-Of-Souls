using System;
using Echo.Domain.Common;
using NUnit.Framework;

namespace Echo.Tests.Domain.Common;

public sealed class AbilityIdTest
{
    [Test]
    public void SameValue_AreEqual()
    {
        Assert.AreEqual(new AbilityId("fireball"),  new AbilityId("fireball"));
    }

    [Test]
    public void DifferentValue_AreNotEqual()
    {
        Assert.AreNotEqual(new AbilityId("fireball"),  new AbilityId("snowball"));
    }
        
    [Test]
    public void SameValue_ShareHashCode()
    {
        Assert.AreEqual(new AbilityId("fireball").GetHashCode(),
            new AbilityId("fireball").GetHashCode());
    }

    [Test]
    public void Empty_Throws()
    {
        Assert.Throws<ArgumentException>(() => new AbilityId(""));
        Assert.Throws<ArgumentException>(() => new AbilityId("   "));
        Assert.Throws<ArgumentException>(() => new AbilityId(null));
    }
        
    [Test]
    public void Default_IsNone()
    {
        Assert.IsTrue(default(AbilityId).IsNone);
        Assert.IsFalse(new AbilityId("fireball").IsNone);
    }
}
