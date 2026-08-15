using System;
using Echo.Domain._Project.Domain.Events;
using NUnit.Framework;

namespace Echo.Tests.DomainEvents;

public sealed class EventBusTests
{
    private readonly struct TestEvent
    {
        public readonly int Value;
        public TestEvent(int value) => Value = value;
    }

    [Test]
    public void Publish_CallsSubscriberHandler()
    {
        var bus = new EventBus();
        int received = -1;
        
        bus.Subscribe<TestEvent>(e => received = e.Value);
        bus.Publish(new TestEvent(42));
        
        Assert.AreEqual(42, received);
    }
    
    [Test]
    public void Publish_WithNoSubscribers_DoesNotThrow()
    {
        var bus = new EventBus();
        Assert.DoesNotThrow(() => bus.Publish(new TestEvent(42)));
    }

    [Test]
    public void Dispose_StopsReceivingEvents()
    {
        var bus = new EventBus();
        int callCount = 0;
        var sub = bus.Subscribe<TestEvent>(_ => callCount++);
        
        bus.Publish(new TestEvent(1));
        sub.Dispose();
        bus.Publish(new TestEvent(2));
        
        Assert.AreEqual(1, callCount);
    }

    [Test]
    public void MultipleSubscribers_AllReceiveEvent()
    {
        var bus = new EventBus();
        int callCount = 0;
        int received = 0;
        
        bus.Subscribe<TestEvent>(_ => callCount++);
        bus.Subscribe<TestEvent>(e => received = e.Value);
        
        bus.Publish(new TestEvent(52));
        
        Assert.AreEqual(52, received);
        Assert.AreEqual(1, callCount);
    }

    [Test]
    public void DifferentEventTypes_DoNotCrossFire()
    {
        var bus = new EventBus();
        int value = 0;
        
        bus.Subscribe<int>(_ => value++);
        bus.Publish(new TestEvent(42));
        
        Assert.AreEqual(0, value);
    }

    [Test]
    public void HandlerUnsubscribingDuringPublish_DoesNotThrow()
    {
        var bus = new EventBus();
        IDisposable sub = null;
        sub = bus.Subscribe<TestEvent>(_ => sub.Dispose());
        
        Assert.DoesNotThrow(() => bus.Publish(new TestEvent(1)));
    }
}
