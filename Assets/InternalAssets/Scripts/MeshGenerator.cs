using UnityEngine;


namespace Assets.Scripts
{
    public class MeshGenerator : MonoBehaviour
    {
        [SerializeField] private int _xSize, _ySize;
        private Vector3[] _vertices;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        
        private void Start()
        {
            GenerateQuad();
        }
        
        private void GenerateQuad()
        {
            _vertices = new Vector3[(_xSize + 1) * (_ySize + 1)];

            for(int i = 0, y = 0; y < _ySize; y++)
            {
                for(int x = 0; x < _xSize; x++, i++)
                {
                    _vertices[i] = new Vector3(x, y);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if(_vertices == null)
            {
                return;
            }

            Gizmos.color = Color.green;

            foreach(var vertex in _vertices)
            {
                Gizmos.DrawSphere(vertex, 0.2f);
            }
        }
    }
}