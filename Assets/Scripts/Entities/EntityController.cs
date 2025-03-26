using InteractiveSystem;
using UnityEngine;

public class EntityController : MovableEntity
{

    [SerializeField] private Animator m_animator;

    private InteractableObject _interactableObject;

    protected override void Start()
    {
        base.Start();
        OnTargetAchive.AddListener(OnTargetAchived);
    }

    protected override void Update()
    {
        base.Update();
        m_animator.SetFloat("Blend", _navAgent.velocity.magnitude / _navAgent.speed * 0.6f); // Установка переменной скорости в анимтор, макс. скорость - 0.6
    }

    public void InteractiveWithObject(InteractableObject interactableObject)
    {
        // !!! - Возможно в будущем заменить на StateMachine, если появлятся ещё состояния
        // Передвижение к интерактивному объекту и его запись в переменную для дальнейшего взаимодействия
        GoTo(interactableObject.transform.position + (transform.position - interactableObject.transform.position).normalized * 0.1f);
        _interactableObject = interactableObject;
    }

    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        // Обнуление переменной интерактивного объекта, если установленная точка была сброшена
        _interactableObject = null;
    }

    private void OnTargetAchived(Vector3 position)
    {
        if (_interactableObject == null)
            return;
        _interactableObject.Interact(gameObject);
        _interactableObject = null;
    }

}
