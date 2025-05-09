using System;
using System.Collections.Generic;
using System.Linq;
using DetectionModule;
using PlayerLogics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class RoadConstructor : MonoBehaviour
{
    [SerializeField] private RoadDetector _roadDetector;
    [SerializeField] private List<RoadSegment> _roadSegmentVariants;
    [SerializeField] private RoadSegment _startSegmentPrefab;
    [SerializeField] private Transform _startRoadPoint;
    [SerializeField] private int _maxRoads;

    private Queue<RoadSegment> _roadSegments;
    private int _roadCounter;

    [Inject]
    private void Construct(Player player)
    {
        player.AssignRoadDetector(_roadDetector);
    }
    
    private void OnEnable()
    {
        _roadDetector.OnTargetExit += OnSegmentExit;
    }

    private void OnDisable()
    {
        _roadDetector.OnTargetExit -= OnSegmentExit;
    }

    public void ConstructRoad()
    {
        if (_maxRoads <= 0) return;
        
        if (_roadSegments is not null && _roadSegments.Count > 0)
        {
            foreach (var road in _roadSegments)
            {
                Destroy(road.gameObject);
            }
            _roadSegments.Clear();
        }
        
        _roadSegments = new Queue<RoadSegment>();

        _roadCounter = 0;
        var startSegment = Instantiate(
            _startSegmentPrefab, _startRoadPoint.position, _startRoadPoint.rotation, transform);
        _roadSegments.Enqueue(startSegment);
        while (_roadSegments.Count < _maxRoads)
        {
            SpawnRandomRoad();
        }
    }
    
    private void OnSegmentExit(RoadSegment segment)
    {
        if (!segment || !_roadSegments.Contains(segment)) return;
        
        var oldRoad = _roadSegments.Dequeue();
        if (oldRoad != segment) throw new Exception("ERROR!");
        Destroy(oldRoad.gameObject);
        
        SpawnRandomRoad();
    }

    private void SpawnRandomRoad()
    {
        RoadSegment randomSegment = _roadSegmentVariants[Random.Range(0, _roadSegmentVariants.Count)];
        RoadSegment lastRoad = _roadSegments.Last();
        var offset = lastRoad.HalfLength + randomSegment.HalfLength;
        Vector3 newRoadPosition = lastRoad.transform.position + new Vector3(0, 0, offset);
        RoadSegment newRoad = Instantiate(randomSegment, newRoadPosition, Quaternion.identity, transform);
        _roadSegments.Enqueue(newRoad);
        newRoad.name = "RoadSegment_" + _roadCounter;
        _roadCounter++;
    }
}
