using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace BufferSystem
{
    public class BufferManager
    {
        private Dictionary<string, Object> _objects = new();
        private Dictionary<string, List<Action<Object>>> _callbacks = new();

        public void AddObject(string id, Object obj)
        {
            if (_objects.ContainsKey(id)) return;
            
            _objects.Add(id, obj);
            if (_callbacks.ContainsKey(id))
            {
                foreach (var callback in _callbacks[id])
                {
                     callback?.Invoke(obj);
                }
            }
        }

        public void RemoveObject(string id)
        {
            if (!_objects.ContainsKey(id)) return;
            
            _objects.Remove(id);
        }

        public T GetObject<T>(string id, Action<Object> callback = null) where T : Object
        {
            if (callback is not null)
            {
                if (!_callbacks.ContainsKey(id))
                {
                    _callbacks.Add(id, new List<Action<Object>>());
                }
                _callbacks[id].Add(callback);
            }

            if (_objects.ContainsKey(id)) return _objects[id] as T;
            
            return null;
        }
    }
}