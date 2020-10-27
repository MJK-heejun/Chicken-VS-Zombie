using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieController : MonoBehaviour
{
    public GameObject zombieHead;
    public GameObject zombieBody;
    public DirectionType direction;
    public float moveSpeed;
    public float jumpSpeed;


    private bool _dead = false;
    private Rigidbody2D _myRigidbody;
    private GameObject _chickenObj; // not needed.
    private Animator _myAnim;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnim = GetComponent<Animator>();
        _chickenObj = GameObject.Find("Chicken"); // not needed.
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        // ChangeDirection();
        FaceDirection();
        SetAnimation();
        Move();        
    }

    public void KillIt(){
        _dead = true;
        zombieBody.SetActive(false);
        zombieHead.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine("DestorySoon");
    }

    private IEnumerator DestorySoon()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void Move() {
        if(!_dead){
            var animInfo = _myAnim.GetCurrentAnimatorStateInfo(0);
            if(animInfo.IsName("MaleZombieRun") || animInfo.IsName("MaleZombieWalk")){
                float moveX = direction == DirectionType.LEFT ? -moveSpeed : moveSpeed;
                _myRigidbody.velocity = new Vector3(moveX, _myRigidbody.velocity.y, 0f);
            }
        } else {
            FlyAway();
        }
    }

    private void FlyAway() {        
        float xVector = direction == DirectionType.LEFT ? 5f : -5f;
        _myRigidbody.velocity = new Vector3(xVector, 5f, 0f);
        float currentRotation = gameObject.transform.rotation.eulerAngles.z;    
        transform.Rotate(Vector3.forward * 45);
    }

    private void FaceDirection() {
        float xVector = direction == DirectionType.LEFT ? -0.5f : 0.5f;
        transform.localScale = new Vector3(xVector, 0.5f, 0.5f);
    }

    private void ChangeDirection() { // not needed for this game
        direction = _chickenObj.transform.position.x > transform.position.x ? DirectionType.RIGHT : DirectionType.LEFT;
    }

    private void SetAnimation() {
        bool idle = false;
        bool walking = false;
        bool running = false;

        if(moveSpeed > 5f) {
            running = true;
        } else if (moveSpeed > 0f) {
            walking = true;
        } else {
            idle = true;
        }

        _myAnim.SetBool("Idle", idle);
        _myAnim.SetBool("Walking", walking);
        _myAnim.SetBool("Running", running);        
    }
}
