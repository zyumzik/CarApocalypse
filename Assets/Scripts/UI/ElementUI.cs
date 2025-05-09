using UnityEngine;

namespace UI
{
    public class ElementUI : MonoBehaviour
    {
        public virtual void Show() { gameObject.SetActive(true); }
        public virtual void Hide() { gameObject.SetActive(false); }
    }
}