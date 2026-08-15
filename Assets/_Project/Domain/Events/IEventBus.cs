using System;

namespace Echo.Domain._Project.Domain.Events;

public interface IEventBus
{
   public IDisposable Subscribe<T>(Action<T> handler);
   public void Publish<T>(T eventToPublish);
}
