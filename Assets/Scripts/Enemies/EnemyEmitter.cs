using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour
{
    enum EnemyType
    {
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy4,
        Enemy5,
        Enemy6,
        Enemy7
    }

    [Header("Enemy Selection")]
    [SerializeField] GameObject[] enemyPrefab = null;

    [Header("Emitter Limits")]
    [SerializeField] Transform emissionStartPoint = null;
    [SerializeField] Transform emissionEndPoint = null;

    [Header("Emitter Orientation")]
    [SerializeField] string emissionOrientationType = null;
    [SerializeField] string emitterType = null;
    [SerializeField] int directionOfEmission = 0;

    GameObject enemy7Holder = null;

    struct Interval
    {
        public int Min;
        public int Max;
    }

    List<Interval> _spawnEnemyIntervalChances = new List<Interval> {
        new Interval { Min = 0, Max = 1 },
        new Interval { Min = 2, Max = 3 },
        new Interval { Min = 4, Max = 6 },
        new Interval { Min = 7, Max = 10 },
        new Interval { Min = 11, Max = 20 },
        new Interval { Min = 21, Max = 40 },
        new Interval { Min = 41, Max = 100 }
    };

    Dictionary<Interval, EnemyType> enemyTypeIntervalChances = new Dictionary<Interval, EnemyType> {
        { new Interval { Min = 0, Max = 1 }, EnemyType.Enemy5},
        { new Interval { Min = 2, Max = 3 }, EnemyType.Enemy6},
        { new Interval { Min = 4, Max = 6 },  EnemyType.Enemy7},
        { new Interval { Min = 7, Max = 10 }, EnemyType.Enemy4},
        { new Interval { Min = 11, Max = 20 }, EnemyType.Enemy3},
        { new Interval { Min = 21, Max = 40 }, EnemyType.Enemy1},
        { new Interval { Min = 41, Max = 100 }, EnemyType.Enemy2},
    };

    Interval GetIntervalChance(float chance) => _spawnEnemyIntervalChances.SingleOrDefault(x => chance <= x.Max && chance >= x.Min);

    void SpawnEnemyInIntervalChance(Interval intervalChance)
    {
        var enemyType = enemyTypeIntervalChances[intervalChance];

        if (enemyType == EnemyType.Enemy7)
        {
            SpawnEnemySeven();
        }
        else
        {
            SpawnEnemy(this.enemyPrefab[(int)enemyType], this.emissionOrientationType, this.directionOfEmission);
        }
    }

    void Awake()
    {
        //Find a way of balacing the game difficulty, instead of using hard coded % chances for the enemy type spawn
        InvokeRepeating(nameof(SpawnEnemyRoutine), 1f, 3f);
    }

    void SpawnEnemyRoutine()
    {
        float chance = Random.Range(0, 100);
        var interval = GetIntervalChance(chance);
        SpawnEnemyInIntervalChance(interval);
    }

    Vector2 RandomEmissionPointInEmitterLimits(string emitterType)
    {
        Vector2 worldEmissionStartPoint = emissionStartPoint.transform.TransformPoint(emissionStartPoint.position);
        Vector2 worldEmissionEndPoint = emissionEndPoint.transform.TransformPoint(emissionEndPoint.position);
        float randomXPos = 0, randomYPos = 0;

        if (emitterType == "Side")
        {
            randomXPos = Random.Range(worldEmissionStartPoint.x / 2, worldEmissionEndPoint.x / 2);
            randomYPos = Random.Range(emissionStartPoint.position.y, emissionEndPoint.position.y);
        }
        else if (emitterType == "Top")
        {
            randomXPos = Random.Range(emissionStartPoint.position.x, emissionEndPoint.position.x);
            randomYPos = Random.Range(worldEmissionStartPoint.y / 2, worldEmissionEndPoint.y / 2);
        }

        Vector2 randomEmissionPoint = new Vector2(randomXPos, randomYPos);

        return randomEmissionPoint;
    }

    void SpawnEnemy(GameObject enemyPrefab, string orientation, int direction)
    {
        GameObject enemySpawned;
        switch (enemyPrefab.gameObject.tag)
        {
            case "Enemy1":
                enemySpawned = InstantiateStandardMovementEnemy(enemyPrefab, orientation, direction);
                break;
            case "Enemy2":
                enemySpawned = InstantiateStandardMovementEnemy(enemyPrefab, orientation, direction);
                break;
            case "Enemy3":
                enemySpawned = Instantiate(enemyPrefab, RandomEmissionPointInEmitterLimits(this.emitterType), Quaternion.identity);
                break;
            case "Enemy4":
                enemySpawned = InstantiateStandardMovementEnemy(enemyPrefab, orientation, direction);
                break;
            case "Enemy5":
                enemySpawned = Instantiate(enemyPrefab, RandomEmissionPointInEmitterLimits(this.emitterType), transform.rotation);
                break;
            case "Enemy6":
                enemySpawned = Instantiate(enemyPrefab, RandomEmissionPointInEmitterLimits(this.emitterType), transform.rotation);
                Enemy6Movement enemyTypeSixSpawnedMovementController = enemySpawned.GetComponent<Enemy6Movement>();
                enemyTypeSixSpawnedMovementController.SetDirectionAndOrientation(orientation, direction);
                break;
        }
    }

    GameObject InstantiateStandardMovementEnemy(GameObject enemyPrefab, string orientation, int direction)
    {
        GameObject enemySpawned = Instantiate(enemyPrefab, RandomEmissionPointInEmitterLimits(this.emitterType), transform.rotation);
        EnemyStandardMovement enemyTypeSpawnedMovementController = enemySpawned.GetComponent<EnemyStandardMovement>();
        enemyTypeSpawnedMovementController.SetDirectionAndOrientation(orientation, direction);

        return enemySpawned;
    }

    void SpawnEnemySeven()
    {
        if (this.emitterType == "Side" && !IsOneEnemyTypeSevenOnTheScene())
        {
            enemy7Holder = Instantiate(this.enemyPrefab[(int)EnemyType.Enemy7], this.emissionEndPoint.position, Quaternion.identity);
        }
    }

    bool IsOneEnemyTypeSevenOnTheScene() => GameObject.FindGameObjectWithTag("Enemy7") != null;
}
