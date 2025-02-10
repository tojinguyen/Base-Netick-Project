using UnityEngine;

public class MapSpawnPosition : MonoBehaviour
{
    [SerializeField] private Transform[] _leftSpawnPositions;
    [SerializeField] private Transform[] _rightSpawnPositions;
    
    public Vector3 GetSpawnPosition(TeamSide teamSide)
    {
        var spawnPositions = teamSide == TeamSide.Team1 ? _leftSpawnPositions : _rightSpawnPositions;
        return spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }
}
