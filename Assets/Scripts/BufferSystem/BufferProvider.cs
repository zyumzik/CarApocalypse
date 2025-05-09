using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace BufferSystem
{
    public class BufferProvider<T> : MonoBehaviour where T : Object
    {
        [SerializeField] private string _id;
        [SerializeField] private T _object;

        private BufferManager _bufferManager;
        
        [Inject]
        private void Construct(BufferManager manager)
        {
            _bufferManager = manager;
            
            _bufferManager.AddObject(_id, _object);
        }

        private void OnDestroy()
        {
            _bufferManager.RemoveObject(_id);
        }
    }
}