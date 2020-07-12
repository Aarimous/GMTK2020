using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 6f;
    public Rigidbody2D rigidbody;
    Vector2 movement;
    Vector2 mousePos;
    public Animator animator;
    public GameObject go;
    public Camera camera;
    public GameObject firePoint;
    public static Player Instance;
    public float BulletAngleChangeTime = 5f;
    public float PlayerRotateIn;

    public int rotateIdex = 0;

    public bool HorizontalMode = false;

    float RotatePlayerTime = 7.5f;

    public float Health = 3f;

    void Start () {

        Instance = this;
        PlayerRotateIn = RotatePlayerTime;

        //Ignore the nodes
        List<GameObject> ignoreObjects = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Node"));

        foreach (GameObject obj in ignoreObjects) {
            Physics2D.IgnoreCollision (obj.GetComponent<Collider2D> (), transform.GetComponent<Collider2D> ());
        }
    }

    // Update is called once per frame
    void Update () {
        //handle input
        movement.x = Input.GetAxisRaw ("Horizontal");

        movement.y = Input.GetAxisRaw ("Vertical");

        if (HorizontalMode) {
            movement.x = Input.GetAxisRaw ("Horizontal");
            movement.y = 0f;
        } else {
            movement.x = 0f;
            movement.y = Input.GetAxisRaw ("Vertical");
        }

        PlayerRotateIn -= Time.deltaTime;
        if (PlayerRotateIn <= 0) {
            //ChangeCannonAngle();
            rotatePlayer ();
            PlayerRotateIn = RotatePlayerTime;
        }

        if (Mathf.Abs (transform.position.x) > GameCtrl.Instance.topRight.x || Mathf.Abs (transform.position.y) > GameCtrl.Instance.topRight.y) {
            Health--;
            transform.position = new Vector2 (0, 0);
            StartCoroutine (TakeDamageEffect ());
        }

        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate () {

        rigidbody.MovePosition (rigidbody.position + movement * moveSpeed * Time.smoothDeltaTime);

    }

    void rotatePlayer () {
        rotateIdex++;       
        if (rotateIdex == 4) {
            rotateIdex = 0;
        }
        transform.rotation = Quaternion.Euler (0, 0, (90 * rotateIdex));
        if (rotateIdex == 0 || rotateIdex == 2) {
            HorizontalMode = false;
        } else {
            HorizontalMode = true;
        }

    }

    IEnumerator TakeDamageEffect () {
        GameObject sr = transform.GetComponentInChildren<SpriteRenderer> ().gameObject;
        bool next = false;
        float blinkTime = 2f;
        float elapsedTime = 0f;
        while (blinkTime > 0f) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= .2f) {
                elapsedTime = 0f;
                blinkTime -= .2f;
                sr.SetActive (next);
                if (next) {
                    next = false;
                } else {
                    next = true;
                }
            }
            yield return null;
        }
    }

    void ChangeCannonAngle () {

        firePoint.transform.Rotate (0, 0, Random.Range (-180f, 180f));

    }

    void OnCollisionEnter2D (Collision2D collision) {


        if (collision.gameObject.tag == "Enemy") {
            this.Health -= 1f;
            collision.gameObject.GetComponentInParent<SimpleEnemy> ().Die ();
            StartCoroutine (TakeDamageEffect ());
        }
    }

}