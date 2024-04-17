using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SAS.Waypoints
{
    public class PingPongPath : Path
    {
        public override IEnumerator<Transform> GetPathEnumerator()
        {
            if (m_Points.Length < 1)
                yield break;
            var direction = 1;
            var index = 0;

            while (true)
            {
                yield return m_Points[index];

                if (m_Points.Length == 1)
                    continue;

                if (index <= 0)
                    direction = 1;
                else if (index >= m_Points.Length - 1)
                    direction = -1;

                index += direction;
            }
        }
    }
}
