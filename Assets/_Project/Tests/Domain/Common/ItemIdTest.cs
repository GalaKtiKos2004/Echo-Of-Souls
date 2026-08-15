using System;
using Echo.Domain.Common;
using NUnit.Framework;

namespace Echo.Tests.Domain.Common;

public sealed class ItemIdTest
{
    [Test]
    public void SameValue_AreEqual()
    {
        Assert.AreEqual(new ItemId("sword"),  new ItemId("sword"));
    }

    [Test]
    public void DifferentValue_AreNotEqual()
    {
        Assert.AreNotEqual(new ItemId("sword"),  new ItemId("shield"));
    }
        
    [Test]
    public void SameValue_ShareHashCode()
    {
        Assert.AreEqual(new ItemId("sword").GetHashCode(),
            new ItemId("sword").GetHashCode());
    }

    [Test]
    public void Empty_Throws()
    {
        Assert.Throws<ArgumentException>(() => new ItemId(""));
        Assert.Throws<ArgumentException>(() => new ItemId("   "));
        Assert.Throws<ArgumentException>(() => new ItemId(null));
    }
        
    [Test]
    public void Default_IsNone()
    {
        Assert.IsTrue(default(ItemId).IsNone);
        Assert.IsFalse(new ItemId("sword").IsNone);
    }
}
