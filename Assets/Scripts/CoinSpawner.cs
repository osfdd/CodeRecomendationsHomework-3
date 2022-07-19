using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _spawnDelay;

    private Transform[] _coinPositions;
    private Coin[] _coins;
    private bool[] _isSpawning;
    private Coroutine _spawnCoin;
    private WaitForSeconds _spawnDelayValue;

    private void Start()
    {
        _spawnDelayValue = new WaitForSeconds(_spawnDelay);
        _coins = new Coin[transform.childCount];
        _isSpawning = new bool[transform.childCount];

        for (int i = 0; i  < transform.childCount; i++)
        {
            _coins[i] = Instantiate(_coin, transform.GetChild(i).position, Quaternion.identity);
            _isSpawning[i] = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (_coins[i] == null)
            {
                _spawnCoin = StartCoroutine(SpawnCoin(i));
            }
        }
    }

    private IEnumerator SpawnCoin(int index)
    {
        if (_isSpawning[index] == false)
        {
            _isSpawning[index] = true;
            yield return _spawnDelayValue;
            _coins[index] = Instantiate(_coin, transform.GetChild(index).position, Quaternion.identity);
            _isSpawning[index] = false;
        }
    }
}
