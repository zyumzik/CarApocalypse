using UnityEngine;
using UnityEngine.Splines;

public class RoadSegment : MonoBehaviour
{
    [SerializeField] private BoxCollider _mainCollider;
    [SerializeField] private SplineContainer _spline;
    
    public float HalfLength => _mainCollider.size.z / 2;
    public SplineContainer Spline => _spline;
}
