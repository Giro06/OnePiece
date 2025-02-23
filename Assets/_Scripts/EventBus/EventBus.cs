using System.Collections.Generic;
using _Scripts.EventBus.Interface;
using UnityEngine;

namespace _Scripts.EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Subscribe(IEventBinding<T> binding) => bindings.Add(binding);
        public static void Unsubscribe(IEventBinding<T> binding) => bindings.Remove(binding);

        public static void Publish(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear()
        {
            Debug.Log($"Clearing {typeof(EventBus<T>).Name}");
            bindings.Clear();
        }
    }
}