using System;
using Echo.Domain.Common;
using NUnit.Framework;

namespace Echo.Tests.Domain.Common;

public sealed class EntityIdTests
{
    [Test]
    public void SameValue_AreEqual()
    {
        Assert.AreEqual(new EntityId("player"),  new EntityId("player"));
    }

    [Test]
    public void DifferentValue_AreNotEqual()
    {
        Assert.AreNotEqual(new EntityId("player"),  new EntityId("other"));
    }
        
    [Test]
    public void SameValue_ShareHashCode()
    {
        Assert.AreEqual(new EntityId("player").GetHashCode(),
            new EntityId("player").GetHashCode());
    }

    [Test]
    public void Empty_Throws()
    {
        Assert.Throws<ArgumentException>(() => new EntityId(""));
        Assert.Throws<ArgumentException>(() => new EntityId("   "));
        Assert.Throws<ArgumentException>(() => new EntityId(null));
    }
        
    [Test]
    public void Default_IsNone()
    {
        Assert.IsTrue(default(EntityId).IsNone);
        Assert.IsFalse(new EntityId("player").IsNone);
    }
}