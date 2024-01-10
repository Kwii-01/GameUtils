using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

namespace Events {
    public static class EventManager {
        private static readonly Dictionary<Type, Delegate> m_events = new Dictionary<Type, Delegate>();

        public static void AddListener<T>(Action<T> listener) {
            Type type = typeof(T);
            if (m_events.ContainsKey(type)) {
                m_events[type] = Delegate.Combine(m_events[type], listener);
            } else {
                m_events[type] = listener;
            }
        }

        public static void RemoveListener<T>(Action<T> listener) {
            Type type = typeof(T);
            if (m_events.ContainsKey(type)) {
                m_events[type] = Delegate.Remove(m_events[type], listener);
            }
        }

        public static void RemoveAllListeners<T>() {
            Type type = typeof(T);
            if (m_events.ContainsKey(type)) {
                m_events.Remove(type);
            }
        }

        public static void Raise<T>(T eventData) {
            if (m_events.TryGetValue(typeof(T), out Delegate action)) {
                (action as Action<T>).Invoke(eventData);
            }
        }
    }
}