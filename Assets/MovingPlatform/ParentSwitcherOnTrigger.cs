using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwitcherOnTrigger : MonoBehaviour
{
    [SerializeField] private string m_Tag;

    private Dictionary<GameObject, Transform> _objectParentMap = new Dictionary<GameObject, Transform>();
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_Tag))
        {
            if (!_objectParentMap.ContainsKey(other.gameObject))
                _objectParentMap.Add(other.gameObject, other.transform.parent);

            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_Tag))
        {
            if (_objectParentMap.TryGetValue(other.gameObject, out var parent))
                _objectParentMap.Remove(other.gameObject);
            other.transform.SetParent(parent, true);
        }
    }
}
