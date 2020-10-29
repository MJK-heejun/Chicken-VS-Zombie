using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenController : MonoBehaviour
{    
    public ZombieFactory zombieFactory;
    public SpikedBallFactory spikedBallFactory;
    public SoundBoxController soundBoxController;

    public float moveSpeed;
    public float jumpSpeed;

    public Transform chickenFeet;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public LayerMask zomebieLayer;

    public Text EggCountText;
    public Text StarCountText;
    public Text GameOverText;
    public Button RestartButton;

    public bool onGround;
    public bool onZombie;

    private Rigidbody2D _myRigidbody;
    private Animator _myAnim;
    private DirectionType _flyDirection = DirectionType.LEFT;

    private bool _dead = false;
    private int _numEggs = 0;
    private int _numStars = 0;
    private bool _hit = false;
    private float _leftBoundary;
    private float _rightBoundary;

    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnim = GetComponent<Animator>();


        GameObject leftWall = GameObject.Find("LeftWall");
        _leftBoundary = leftWall.transform.position.x + leftWall.transform.localScale.x;

        GameObject rightWall = GameObject.Find("RightWall");
        _rightBoundary = rightWall.transform.position.x - rightWall.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_hit && !_dead){
            onGround = Physics2D.OverlapCircle(chickenFeet.position, groundCheckRadius, groundLayer);
            onZombie = Physics2D.OverlapCircle(chickenFeet.position, groundCheckRadius, zomebieLayer);

            if( MovingRight()) {
                _myRigidbody.velocity = new Vector3(moveSpeed, _myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else if( MovingLeft()) {
                _myRigidbody.velocity = new Vector3(-moveSpeed, _myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            } else {
                _myRigidbody.velocity = new Vector3(0, _myRigidbody.velocity.y, 0f);
            }

            if( Jumping() && (onGround || onZombie)){
                _myRigidbody.velocity = new Vector3(_myRigidbody.velocity.x, jumpSpeed, 0f);
            }            
        }
    }   

    void FixedUpdate() {
        UpdateUIText();
        if (_dead) {
            FlyAway();
        } else {
            _myAnim.SetFloat("Speed", Mathf.Abs(_myRigidbody.velocity.x));
            _myAnim.SetBool("Hit", _hit);
        }
        KeepInsideBoundary();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Egg") {
            _numEggs++;
            soundBoxController.PlaySwallow();
            Destroy(other.gameObject);
        } 
        if (other.tag == "Star") {
            _numStars++;
            soundBoxController.PlayStar();
            Destroy(other.gameObject);
        }
        if (other.tag == "StopSign") {
            zombieFactory.ResetZombieMoveSpeed();
            soundBoxController.PlayCarHonk();
            Destroy(other.gameObject);
        }
        if (other.tag == "Lightning") {
            spikedBallFactory.ResetSpikedBallRegenSpeed();
            soundBoxController.PlayLightning();
            Destroy(other.gameObject);
        }
        if (other.tag == "SpikedBall") {
            _hit = true;
            _myAnim.SetBool("Hit", _hit);
            StartCoroutine("RecoverFromHit");
            soundBoxController.PlayMetalHit();
            Destroy(other.gameObject);
        }
        
        if (other.name == "ZombieBody") {
            _numStars--;
            var zombieCont = other.transform.parent.GetComponent<ZombieController>();
            zombieCont.KillIt();
            soundBoxController.PlayZombieDead();
            if(_numStars < 0) {
                ChickenDies(zombieCont);
            }
        }
        

        Debug.Log($"collided to {other.name}");
    }

    private IEnumerator DestorySoon()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void FlyAway() {
        float xVector = _flyDirection == DirectionType.RIGHT ? 5f : -5f;        
        _myRigidbody.velocity = new Vector3(xVector, 5f, 0f);
        float currentRotation = gameObject.transform.rotation.eulerAngles.z;    
        transform.Rotate(Vector3.forward * 45);
    }

    private void UpdateUIText() {
        StarCountText.text = "x " + _numStars;
        EggCountText.text = "x " + _numEggs;
    }

    private IEnumerator RecoverFromHit() {
        yield return new WaitForSeconds(1f);
        _hit = false;
    }

    private void KeepInsideBoundary() {    
        float distance = moveSpeed * Time.deltaTime;        

        if (_myRigidbody.transform.position.x <= _leftBoundary){
            transform.position = new Vector2(transform.position.x + distance, transform.position.y);
        } else if (_myRigidbody.transform.position.x >= _rightBoundary){
            transform.position = new Vector2(transform.position.x - distance, transform.position.y);
        }
    }

    private void ChickenDies(ZombieController zombieCont) {
        _dead = true;
        DirectionType _flyDirection = zombieCont.direction;
        GetComponent<BoxCollider2D>().enabled = false;      
        soundBoxController.PlayChickenDead();
        StartCoroutine("DestorySoon");

        GameOverText.text = $"You collected {_numEggs} eggs";
        GameOverText.gameObject.SetActive(true);        
        RestartButton.gameObject.SetActive(true);
    }

    private bool MovingRight() => Input.GetAxisRaw("Horizontal") > 0f;
    private bool MovingLeft() => Input.GetAxisRaw("Horizontal") < 0f;
    private bool Jumping() => Input.GetButtonDown("Jump");

}