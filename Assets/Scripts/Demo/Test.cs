using UnityEngine;

namespace Demo
{
    public class Test : MonoBehaviour
    {
        public Material MaterialUp;
        public Material MaterialDown;
        public float Speed = 1.0f;
        private Vector3 startPosition;
        private float startTime;
        private MeshRenderer _renderer;
    
        void Start()
        {
            startPosition = transform.position;
            startTime = Time.time;
            _renderer = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            float t = (Time.time - startTime) / 2.0f; // 2 seconds total duration
            float y = Mathf.Sin(t * Mathf.PI * Speed) * 10.0f; // oscillation between -5 and 5 units
            transform.position = startPosition + new Vector3(0.0f, y, 0.0f); // move object up and down

            if (y >= 0.0f) {
                _renderer.material = MaterialUp; // use MaterialUp when going up
            } else {
                _renderer.material = MaterialDown; // use MaterialDown when going down
            }
        }
    }
}