using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCtrl : MonoBehaviour

{
    public static ArrowCtrl Instance { get; protected set; }
    public GameObject ClockWiseArrow;
    public GameObject CounterClockWiseArrow;

    //keep this even
    public int maxArrows = 10;
    public int ClockArrows;
    public int CounterArrows;

    public float TimeOutThreshold = 30f;

    public float ElapsedTime = 0f;


    // Start is called before the first frame update
    void Start () {
        if (Instance != null) {
            Debug.LogError ("There should never be two world controllers");
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= TimeOutThreshold/maxArrows){
            UpdateArrows();
            ElapsedTime = 0f;
        }
    }

    public void UpdateArrows () {

        Vector2 bottomLeft = GameCtrl.Instance.bottomLeft;
        Vector2 topRight = GameCtrl.Instance.topRight;

        //GameObject[] gos = transform.GetComponentsInChildren<GameObject>();

        if(transform.childCount >= maxArrows){
            Destroy(transform.GetChild(0).gameObject);
        }

        int coinFlip = Random.Range(0,2);
        float x = Random.Range(bottomLeft.x + 2, topRight.x -2);
        float y = Random.Range(bottomLeft.y + 2 , topRight.y -2);

        Debug.Log("Arrow Spawn at " + x + " " + y);

        if( coinFlip == 0){
            GameObject go1 = Instantiate (ClockWiseArrow, new Vector3 (x, y , 0 ), 
            ClockWiseArrow.transform.rotation);
            go1.transform.SetParent(transform,true);

        }
        else if (coinFlip == 1){
            GameObject go2 = Instantiate (CounterClockWiseArrow,  new Vector3 (x, y , 0 ), 
            CounterClockWiseArrow.transform.rotation);
            go2.transform.SetParent(transform, true);
        }
        else{
            Debug.LogError("Check the arrow spawing max");
        }


        /*
        if (HorizontalMode) {
            GameObject goUp = Instantiate (UpArrow, new Vector2 (PlayerPosition.x + 10, PlayerPosition.y ), UpArrow.transform.rotation);
            goUp.transform.SetParent(transform);
            GameObject goDown = Instantiate (DownArrow, new Vector2 (PlayerPosition.x - 10, PlayerPosition.y ), DownArrow.transform.rotation);
            goDown.transform.SetParent(transform);

        } else {
            GameObject goLeft = Instantiate (LeftArrow, new Vector2 (PlayerPosition.x, PlayerPosition.y - 5), LeftArrow.transform.rotation);
            goLeft.transform.SetParent(transform);
            GameObject goRight = Instantiate (RightArrow, new Vector2 (PlayerPosition.x , PlayerPosition.y + 5), RightArrow.transform.rotation);
            goRight.transform.SetParent(transform);

        }
        */

    }
}