using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherCtrl : MonoBehaviour {

    public GameObject NormalPusher;

    public float PusherSpawnThreshold = 5f;
    public float ElapsedTime;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= PusherSpawnThreshold) {
            ElapsedTime = 0f;
            int spawnSide = Random.Range (0, 4);

            float radius = Mathf.Max (GameCtrl.Instance.topRight.x + 5, GameCtrl.Instance.topRight.y + 5);
            Vector2 spawnPos = Random.insideUnitCircle.normalized * radius;
            
            switch (spawnSide) {
                case 0:
                    GameObject go0 = Instantiate (NormalPusher, spawnPos , NormalPusher.transform.rotation);
                    //go0.transform.Rotate (0, 0, 0);
                    break;
                case 1:
                    GameObject go1 = Instantiate (NormalPusher, spawnPos, NormalPusher.transform.rotation);
                    go1.transform.Rotate (0, 0, 45);
                    break;
                case 2:
                    GameObject go2 = Instantiate (NormalPusher, spawnPos, NormalPusher.transform.rotation);
                    go2.transform.Rotate (0, 0, 90);
                    break;
                case 3:
                    GameObject go3 = Instantiate (NormalPusher, spawnPos, NormalPusher.transform.rotation);
                    go3.transform.Rotate (0, 0, -45);
                    break;
            }

        }
    }
}