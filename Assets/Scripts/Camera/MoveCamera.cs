using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{

    [SerializeField, Range(1f, 10f)] private float m_moveSpeed;
    [SerializeField] private InputActionAsset m_inputActionAsset;
    [Space]
    [SerializeField] private bool m_lockArea;
    [SerializeField] private Vector2 m_minPosition;
    [SerializeField] private Vector2 m_maxPosition;

    private InputAction _inputAction;

    private void Start()
    {
        _inputAction = m_inputActionAsset.FindActionMap("Player").FindAction("Move");
    }

    private void Update()
    {
        Vector2 movement = _inputAction.ReadValue<Vector2>();
        Move(movement * m_moveSpeed * Time.deltaTime);
    }

    public void Move(Vector2 delta)
    {
        transform.Translate(new Vector3(delta.x, 0, delta.y));

        // Ограничение передвижения камеры
        if (m_lockArea)
        {
            Vector3 newPosition = transform.position;

            if (transform.position.x < m_minPosition.x)
                newPosition.x = m_minPosition.x;
            else if (transform.position.x > m_maxPosition.x)
                newPosition.x = m_maxPosition.x;

            if (transform.position.z < m_minPosition.y)
                newPosition.z = m_minPosition.y;
            else if (transform.position.z > m_maxPosition.y)
                newPosition.z = m_maxPosition.y;

            transform.position = newPosition;
        }
    }

}
