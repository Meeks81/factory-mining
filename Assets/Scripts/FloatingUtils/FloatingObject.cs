using UnityEngine;

namespace FloatingUtils
{

    public class FloatingObject : MonoBehaviour
    {

        [Tooltip("������ ��� �������� FloatingObject.\n(Can be null)")]
        public Transform ConnectObject;

        [Tooltip("���������� �� ConnectObject �� FloatingObject �� ������ (Y Axis)")]
        public float HeightSpace;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (ConnectObject != null)
            {
                transform.position = _mainCamera.WorldToScreenPoint(ConnectObject.transform.position + new Vector3(0, HeightSpace, 0));
            }
        }

    }

}