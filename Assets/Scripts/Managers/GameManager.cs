using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Attributes")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private GameObject playerPrefab;

    public PickupManager pickupSpawner;
    public ScoreManager scoreManager;
    public CurrencyManager currencyManager;

    public Action onGameStart;
    public Action onGameOver;

    public Action<bool> onGamePause;

    private GameObject tempEnemy;
    private bool isEnemySpawning;
    private int eType;

    private Player player;
    private bool isPlaying;

    private bool paused;

    [SerializeField] private int difficultyThreshold = 10; // every time the score increases by 10 (or whatever), the difficulty increases
    private int difficultyCoefficient = 1;
    private int maxDifficultyCoefficient = 5;

    // enemy weapon data
    private Weapon meleeWeapon = new Weapon("Melee", 1, 0);
    private Weapon exploderWeapon = new Weapon("Exploder", 30, 0);
    private Weapon mgWeapon = new Weapon("Machine Gun", 0.5f, 2.5f);
    private Weapon shooterWeapon = new Weapon("Shooter", 10, 10);

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
    void Start()
    {
        isEnemySpawning = true;
        //StartCoroutine(EnemySpawner());
    }

    void FindPlayer() {
        try {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        } catch (NullReferenceException e) {
            Debug.Log("Player not found, " + e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            CreateEnemy();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    void CreateEnemy() {
        /*
        tempEnemy = Instantiate(enemyPrefab);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
        tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(2, 0.25f);

        int index = UnityEngine.Random.Range(0, spawnPositions.Length);
        tempEnemy.transform.position = spawnPositions[index].position;
        */

        // selects random enemy to spawn
        // 0 = melee, 1 = exploder, 2 = machine gun, 3 = shooter
        eType = UnityEngine.Random.Range(0, enemyPrefabs.Length);

        tempEnemy = Instantiate(enemyPrefabs[eType]);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        if (eType==0) {
            tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
            tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(2, 0.25f);
        }
        else if(eType==1) {
            tempEnemy.GetComponent<Enemy>().weapon = exploderWeapon;
            tempEnemy.GetComponent<ExploderEnemy>().SetExploderEnemy(0.5f, 0.5f);
        }
        else if (eType==2) {
            tempEnemy.GetComponent<Enemy>().weapon = mgWeapon;
            tempEnemy.GetComponent<MachineGunEnemy>().SetMachineGunEnemy(5, 0.2f);
        } 
        else if(eType==3) {
            tempEnemy.GetComponent<Enemy>().weapon = shooterWeapon;
            tempEnemy.GetComponent<ShooterEnemy>().SetShooterEnemy(7, 3);
        }
    }

    IEnumerator EnemySpawner() {
        while (isEnemySpawning) {
            yield return new WaitForSeconds(1 / (enemySpawnRate * difficultyCoefficient));
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
        yield return new WaitForSeconds(2);

        // Delete all enemies
        foreach (Enemy item in FindObjectsOfType(typeof(Enemy))) {
            Destroy(item.gameObject);
        }

        // Delete all pickups
        foreach (Pickup item in FindObjectsOfType(typeof(Pickup))) {
            Destroy(item.gameObject);
        }

        isPlaying = false;
        PlayerDied();
    }

    private void PauseGame() {
        if (!paused) {
            paused = true;
            onGamePause?.Invoke(true);
            Time.timeScale = 0;
        } else {
            paused = false;
            onGamePause?.Invoke(false);
            Time.timeScale = 1;
        }
    }

    public void PlayerDied() {
        isEnemySpawning = false;
        onGameOver?.Invoke();
    }

    public void SetEnemySpawnStatus(bool enemystatus) {
        isEnemySpawning = enemystatus;
    }

    public void KillAll(bool includePlayer){
        // Kill all enemies
        foreach (Enemy item in FindObjectsOfType(typeof(Enemy))) {
            item.Die();
        }

        // Get all pickups
        foreach (Pickup item in FindObjectsOfType(typeof(Pickup))) {
            item.OnPickedUp();
        }

        if (includePlayer) {
            player.Die();
        }
    }

    public void IncreaseDifficulty() {
        difficultyCoefficient = Mathf.Min(difficultyCoefficient + 1, maxDifficultyCoefficient);
    }

    public int GetDifficultyCoefficient() {
        return difficultyCoefficient;
    }
    
    public int GetDifficultyThreshold() {
        return difficultyThreshold;
    }
}
