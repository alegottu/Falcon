using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _minSwipeDistance = 0;
    public float minSwipeDistance { get { return _minSwipeDistance; } }

    [SerializeField] private float _flightPower = 0;
    public float flightPower { get { return _flightPower; } }

    [SerializeField] private float _longestSwipe = 0;
    public float longestSwipe { get { return _longestSwipe; } }

    [SerializeField] private float _maxTimeMultiplier = 0;
    public float maxTimeMultiplier { get { return _maxTimeMultiplier; } }

    [SerializeField] private float _timeMultiplierBase = 1;
    public float timeMultiplierBase { get { return _timeMultiplierBase; } }

    [SerializeField] private float _farthestSwipe = 0;
    public float farthestSwipe { get { return _farthestSwipe; } }

    [SerializeField] private float _maxDistanceMultiplier = 0;
    public float maxDistanceMultiplier { get { return _maxDistanceMultiplier; } }

    [SerializeField] private float _distanceMultiplierBase = 1;
    public float distanceMultiplierBase { get { return _distanceMultiplierBase; } }

    [SerializeField] private Vector2 _gustSize = Vector2.zero;
    public Vector2 gustSize { get { return _gustSize; } }

    [SerializeField] private float _gustPower = 1;
    public float gustPower { get { return _gustPower; } }

    [SerializeField] private LayerMask _ignoreRaycast = new LayerMask();
    public LayerMask ignoreRaycast { get { return _ignoreRaycast; } }

    [SerializeField] private int _attackPower = 1;
    public int attackPower { get { return _attackPower; } }

    [SerializeField] private int _minDamage = 1;
    public int minDamage { get { return _minDamage; } }

    [SerializeField] private int _bounceForce = 0;
    public int bounceForce { get { return _bounceForce; } }
}
