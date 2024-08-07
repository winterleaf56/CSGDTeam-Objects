using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Attributes")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private GameObject playerPrefab;

    public PickupManager pickupSpawner;
    public ScoreManager scoreManager;

    public Action onGameStart;
    public Action onGameOver;

    private GameObject tempEnemy;
    private bool isEnemySpawning;
    private Weapon meleeWeapon = new Weapon("Melee", 1, 0);
    private Player player;
    private bool isPlaying;

    private static GameManager instance;

    public static GameManager GetInstance() {
        return instance;
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
            
        instance = this;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        isEnemySpawning = true;
        StartCoroutine(EnemySpawner());
    }

    void FindPlayer() {
        try {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        } catch (NullReferenceException e) {
            Debug.Log("Player not found, " + e);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            CreateEnemy();
        }
    }

    void CreateEnemy() {
        tempEnemy = Instantiate(enemyPrefab);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
        tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(2, 0.25f);

        /*int index = UnityEngine.Random.Range(0, spawnPositions.Length);
        tempEnemy.transform.position = spawnPositions[index].position;*/
    }

    IEnumerator EnemySpawner() {
        while (isEnemySpawning) {
            yield return new WaitForSeconds(1 / enemySpawnRate);
            CreateEnemy();
        }
    }

    public void NotifyDeath(Enemy enemy) {
        pickupSpawner.SpawnPickup(enemy.transform.position);
    }

    public Player GetPlayer() {
        return player;
    }

    public bool IsPlaying() {
        return isPlaying;
    }

    public void StartGame() {
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        player.onDeath += StopGame;
        isPlaying = true;

        onGameStart?.Invoke();
        StartCoroutine(GameStarter());
    }

    IEnumerator GameStarter() {
        yield return new WaitForSeconds(2.0f);
        isEnemySpawning = true;
        StartCoroutine(EnemySpawner());
    }

    public void StopGame() {
        scoreManager.SetHighScore();

        StartCoroutine(GameStopper());
    }

    IEnumerator GameStopper() {
        isEnemySpawning = false;
        //SetEnemySpawnStatus(false);
        yield return new WaitForSeconds(0.5f);

        // Delete all enemies
        foreach (Enemy item in FindObjectsOfType(typeof(Enemy))) {
            Destroy(item.gameObject);
        }

        // Delete all pickups
        foreach (Pickup item in FindObjectsOfType(typeof(Pickup))) {
            Destroy(item.gameObject);
        }

        isPlaying = false;
    }

    public void PlayerDied() {
        isEnemySpawning = false;
        onGameOver?.Invoke();
    }

    public void SetEnemySpawnStatus(bool enemystatus) {
        isEnemySpawning = enemystatus;
    }
}
