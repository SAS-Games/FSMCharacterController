using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAS.Waypoints
{
    interface IPath
    {
        IEnumerator<Transform> GetPathEnumerator();
    }

    public abstract class Path : MonoBehaviour, IPath
    {
        [SerializeField] protected Transform[] m_Points;

        public abstract IEnumerator<Transform> GetPathEnumerator();

        private void OnDrawGizmos()
        {
            if (m_Points.Length < 2)
                return;

            for (var i = 1; i < m_Points.Length; ++i)
                Gizmos.DrawLine(m_Points[i - 1].position, m_Points[i].position);
        }
    }
}
