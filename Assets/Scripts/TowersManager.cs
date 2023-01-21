using UnityEngine;

public class TowersManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;

    private PlayerStats _playerStats;

    [SerializeField] private float towerCost = 100f;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }
}