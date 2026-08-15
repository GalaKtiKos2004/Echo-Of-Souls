using System;
using System.Collections.Generic;

namespace Echo.Domain._Project.Domain.Events;

public sealed class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public IDisposable Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);

        if (_handlers.TryGetValue(type, out var list) == false)
        {
            list = new();
            _handlers[type] = list;
        }
        
        list.Add(handler);
        return new Subscription(() => list.Remove(handler));
    }

    public void Publish<T>(T eventToPublish)
    {
        if (_handlers.TryGetValue(typeof(T), out var list) == false) return;
        
        var snapshot = list.ToArray();

        foreach (var handler in snapshot)
        {
            ((Action<T>)handler).Invoke(eventToPublish);
        }
    }

    private sealed class Subscription : IDisposable
    {
        private readonly Action _unsubscribe;
        private bool _disposed;
        
        public Subscription(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _disposed = true;
            _unsubscribe();
        }
    }
}
