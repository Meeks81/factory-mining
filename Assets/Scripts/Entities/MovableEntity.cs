using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class MovableEntity : MonoBehaviour, IMovable
{

    public UnityEvent<Vector3> OnTargetAchive { get; private set; } = new UnityEvent<Vector3>();

    public bool IsMoving { get; protected set; } = false;

    protected NavMeshAgent _navAgent { get; private set; }

    protected virtual void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        // Вызов коллбэка при достижении установленной точки
        if (IsMoving && !_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            if (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude == 0f)
            {
                IsMoving = false;
                OnTargetAchive?.Invoke(_navAgent.destination);
            }
        }
    }

    public virtual void GoTo(Vector3 position)
    {
        _navAgent.SetDestination(position);
        IsMoving = true;

        // Если сразу установили точку, но персонаж уже там, вызваем коллбэк немедленно
        if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            IsMoving = false;
            OnTargetAchive?.Invoke(_navAgent.destination);
        }
    }

}
