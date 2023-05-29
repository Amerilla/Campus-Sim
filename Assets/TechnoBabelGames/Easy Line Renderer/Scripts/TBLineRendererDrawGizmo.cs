using UnityEditor;
using UnityEngine;

namespace TechnoBabelGames
{
    public class TBLineRendererDrawGizmo : MonoBehaviour
    {
        [HideInInspector]
        public Transform targetPoint;
        [HideInInspector]
        public TBLineRendererComponent parent;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (targetPoint != null)
            {
                Gizmos.DrawLine(this.transform.position, targetPoint.transform.position);
            }

            if (!Selection.Contains(parent.gameObject))
            {
                Gizmos.DrawWireSphere(this.transform.position, 0.2f);
            }            

            parent.SetPoints();
        }
    }
}
