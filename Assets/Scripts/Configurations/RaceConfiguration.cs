using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "RaceConfiguration", menuName = "Configurations/Race Configuration")]
    public class RaceConfiguration : ScriptableObject
    {
        [SerializeField] private float _raceDistance;

        public float RaceDistance => _raceDistance;
    }
}