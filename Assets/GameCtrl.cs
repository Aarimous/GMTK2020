using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour {

    public static GameCtrl Instance { get; protected set; }

    public Camera camera;

    public Vector2 bottomLeft;
    public Vector2 topRight;
    public GameObject Health;
    public GameObject RotateIn;
    public float BaseSpawnRate = 8f;
    public int NodesCaptured = 0;

    public GameObject Wave;
    public GameObject Enemies;

    public int[] Waves;
    public int currentWave = 0;

    float betweenWaveTime = 5f;
    float waitTime = 0f;

    // Start is called before the first frame update
    void Start () {

        if (Instance != null) {
            Debug.LogError ("There should never be two world controllers");
        }
        Instance = this;
        SetCameraInfo ();
        NextWave ();

    }

    // Update is called once per frame
    void Update () {

        Health.GetComponent<TextMeshProUGUI> ().text = "Health: " + Player.Instance.Health + "";
        RotateIn.GetComponent<TextMeshProUGUI> ().text = "Rotate In: " + Mathf.Floor (Player.Instance.PlayerRotateIn) + "";
        Wave.GetComponent<TextMeshProUGUI> ().text = "Wave: " + currentWave + " of " + Waves.Length;
        Enemies.GetComponent<TextMeshProUGUI> ().text = "Viruses Remaining: " + EnemyCtrl.Instance.numEnemiesLeftInWave;

        if (Player.Instance.Health == 0 || NodesCaptured == 4) {
            Debug.Log ("GAME OVER!!");
            SceneManager.LoadScene ("Loser");
        }
    }

    public void NextWave () {
        Debug.Log ("WAVE " + currentWave + " ENDED !!!");
        if (currentWave + 1 > Waves.Length) {
            Debug.Log ("YOU WON");
            SceneManager.LoadScene ("Winner");
        } else {

            currentWave++;
            Debug.Log ("WAVE " + currentWave + " STARTING !!! Spawning " + Waves[currentWave - 1] + " Enemies");
            EnemyCtrl.Instance.StarteWave (Waves[currentWave - 1], BaseSpawnRate - currentWave);

        }

    }

    void SetCameraInfo () {

        float distanceFromCamera = camera.nearClipPlane; // Change this value if you want
        Vector3 topLeft = camera.ViewportToWorldPoint (new Vector3 (0, 1, distanceFromCamera));
        Vector3 topRight3 = camera.ViewportToWorldPoint (new Vector3 (1, 1, distanceFromCamera));

        topRight = new Vector2 (topRight3.x, topRight3.y);
        bottomLeft = new Vector2 (topLeft.x, -topLeft.y);
        Debug.Log ("bottomLeft " + bottomLeft);
        Debug.Log ("topRight" + topRight);
    }
    

}