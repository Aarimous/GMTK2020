using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {

    public GameObject EnemyPrefab;

    public int numEnemies = 0;
    public float EnemySpawnThreshold = 5f;
    public float ElapsedTime;
    public static EnemyCtrl Instance;
    bool startWave = false;

    public int numEnemiesLeftInWave;


    void Start(){
        Instance = this;
    }

    public void StarteWave (int numEnemies, float spawnRate) {

        this.numEnemies = numEnemies;
        numEnemiesLeftInWave = numEnemies;
        this.EnemySpawnThreshold = spawnRate;
        startWave = true;
    }

    // Update is called once per frame
    void Update () {
        if (startWave && numEnemies > 0) {
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime >= EnemySpawnThreshold) {
                ElapsedTime = 0f;

                float radius = Mathf.Max (GameCtrl.Instance.topRight.x + 5, GameCtrl.Instance.topRight.y + 5);
                Vector2 spawnPos = Random.insideUnitCircle.normalized * radius;

                Debug.Log ("Radius " + radius + " : " + spawnPos);
                GameObject go0 = Instantiate (EnemyPrefab, spawnPos, EnemyPrefab.transform.rotation);
                go0.transform.SetParent(transform);
                numEnemies --;
            }
        }

        if(transform.childCount == 0 && numEnemies == 0){
            startWave = false;
            GameCtrl.Instance.NextWave();
        }
    }
}