using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    private enum SpawnState
    {
        Spawning, Waiting, Counting
    }
    
    [System.Serializable]
        
    public class Wave
    {
        public string waveName;
        public Transform enemy;
        public int spawnCount;
        public float spawnRate;
    }

    public Wave[] waves;
    private int _nextWave;

    public Transform[] spawnPoints; // create array of random spawnpoints

    public float timeBetweenWaves = 5f;
    private float _waveCountdown;

    private float _searchCountdown = 1f;

    private SpawnState _state = SpawnState.Counting;

    private void Start()
    {
        
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point referenced");
        }
        _waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (_state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        if (_waveCountdown <= 0)
        {
            if (_state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[_nextWave]));
            }
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        _state = SpawnState.Counting;
        _waveCountdown = timeBetweenWaves;

        if (_nextWave + 1 > waves.Length - 1)
        {
            _nextWave = 0;
            Debug.Log("All Waves Completed Looping...");
        }
        else
        {
            _nextWave++;
        }
    }

    private bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;

        if (_searchCountdown <= 0f)
        {
            _searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave " + wave.waveName);
        
        _state = SpawnState.Spawning;

        for (int i = 0; i < wave.spawnCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds( 1f / wave.spawnRate );
        }
        _state = SpawnState.Waiting;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning enemy: " + enemy.name);

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Choose random spawn point
        Instantiate(enemy, sp.position, sp.rotation); // changed Transform to _sp
    }
}
