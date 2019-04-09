using UnityEngine;

namespace Sort.Scirpts
{
    public class DataStructure : MonoBehaviour
    {
        private Color _color;

        private Transform _transform;

        private Vector3 _pos;
        
        private float data;

        public void DataInit(Color c,Transform t,float d,Vector3 localScale)
        {
            _color = c;
            _transform = t;
            data = d;
            _pos = _transform.position;
            _transform.localScale = localScale;
        }

        public float GetData()
        {
            return data;
        }

        public void SetData(float d)
        {
            data = d;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            _color = GetComponent<SpriteRenderer>().color;
            _transform = transform;
            _pos = _transform.position;
        }
    }
}
