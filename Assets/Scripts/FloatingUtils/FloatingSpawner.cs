using UnityEngine;

namespace FloatingUtils
{

    public class FloatingSpawner : MonoBehaviour
    {

        public static FloatingSpawner Instance { get; private set; }

        [SerializeField] private Transform m_objectsContainer;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public FloatingObject SpawnObject(FloatingObject prefab)
        {
            FloatingObject newObject = Instantiate(prefab, m_objectsContainer);
            return newObject;
        }

    }

}