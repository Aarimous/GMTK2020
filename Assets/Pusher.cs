using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    public float MoveSpeed = 1f;

    public Vector2 spwan;
    public Vector2 target;

    // Start is called before the first frame update
    void Start () {


        spwan = transform.position;
        if(Mathf.Abs(spwan.x) >= Mathf.Abs(spwan.y)){
            target = new Vector2 (-spwan.x, spwan.y);
        }
        else{
            target = new Vector2 (spwan.x, -spwan.y);
        }
        

    }

    // Update is called once per frame
    void Update () {
       // transform.Translate (transform.right * MoveSpeed * Time.smoothDeltaTime);
    }

    void FixedUpdate () {

        transform.position = Vector3.MoveTowards (transform.position, target, .03f);
        FixRotation (target);

        if(new Vector2(transform.position.x, transform.position.y) == target){
            Destroy(gameObject);
        }
    }
    

    void FixRotation(Vector3 targ){
                 targ.z = 0f;
 
             Vector3 objectPos = transform.position;
             targ.x = targ.x - objectPos.x;
             targ.y = targ.y - objectPos.y;
 
             float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90f;
             transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
}
}