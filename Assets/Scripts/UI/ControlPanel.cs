using InteractiveSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// ��������� ������ ��� ���������� �������
public class ControlPanel : UIBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [field: SerializeField] public UnityEvent<Vector3> OnMoveTo { get; private set; }
    [field: SerializeField] public UnityEvent<InteractableObject> OnInteractiveWith { get; private set; }
    [field: SerializeField] public UnityEvent<Vector2> OnDragDelta { get; private set; }

    public Camera MainCamera => _mainCamera ??= Camera.main;

    private bool _isDraging;
    private Camera _mainCamera;

    public void OnDrag(PointerEventData eventData)
    {
        _isDraging = true;
        OnDragDelta?.Invoke(eventData.delta);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDraging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �������� �� ������������ �������/�������� �� ������
        if (_isDraging == false)
        {
            // ���� �� ���� ������������, �� ����������� ���� �� ������
            Click(eventData.pointerCurrentRaycast.screenPosition);
        }
    }

    private void Click(Vector2 screenPosition)
    {
        Ray ray = MainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit = default;
        Physics.Raycast(ray, out hit);
        if (hit.transform != null)
        {
            // �������� �� ������� �� �������������� �������, � ���� ������ ����������� ������������ � ����� �� �����
            if (hit.transform.TryGetComponent(out InteractableObject interactiveObject))
            {
                OnInteractiveWith?.Invoke(interactiveObject);
            }
            else
            {
                OnMoveTo?.Invoke(hit.point);
            }
        }
    }

}
