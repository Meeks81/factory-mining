using UnityEngine;
using UnityEngine.Events;

namespace InteractiveSystem
{

    public class InteractableObject : MonoBehaviour, IInteractable
    {

        [field: SerializeField] public UnityEvent OnInteractive { get; private set; }

        public virtual void Interact(GameObject interactor)
        {
            OnInteractive?.Invoke();
        }

    }

}
