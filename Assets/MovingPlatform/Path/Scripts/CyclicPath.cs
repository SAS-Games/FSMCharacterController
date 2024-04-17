using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SAS.Waypoints
{
    public class CyclicPath : Path
    {
        public override IEnumerator<Transform> GetPathEnumerator()
        {
            if (m_Points.Length < 1)
                yield break;

            var index = 0;

            while (true)
            {
                yield return m_Points[index];

                if (m_Points.Length == 1)
                    continue;

                if (index == m_Points.Length - 1)
                    index = -1;

                ++index;
            }
        }
    }
}
