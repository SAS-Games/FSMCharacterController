using SAS.Utilities.TagSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControllerRelativeMovement : MonoBehaviour
{
    [FieldRequiresSelf] private Rigidbody _rigidbody;
    [SerializeField] private string m_Tag;

    private CharacterController _characterController;

    private void Awake()
    {
        this.Initialize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_Tag))
            other.TryGetComponent(out _characterController);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_characterController != null)
            _characterController.Move(_rigidbody.velocity * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_Tag))
        {
            if (other.TryGetComponent(out CharacterController characterController))
            {
                if (_characterController == characterController)
                    _characterController = null;
            }
        }
    }
}
