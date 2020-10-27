using UnityEngine;
using System.Collections;

public class ZombieFactory : MonoBehaviour {

    public ZombieController maleZombie;
    public float zombieMoveSpeed = 2f;
    public float zombieRegenSpeed = 0.5f;

    private GameObject _leftWall;
    private GameObject _rightWall;
    private float _defaultZombieMoveSpeed;

    void Start(){
        _defaultZombieMoveSpeed = zombieMoveSpeed;
        
        _leftWall = GameObject.Find("LeftWall");
        _rightWall = GameObject.Find("RightWall");

        StartCoroutine("SpawnZombie");
    }

    //generate at either left or right
    public void GenerateAtRandomPosition(float moveSpeed)
    {        
        float rNum = Random.Range(0.0F, 2F);
        GenerateAt((DirectionType)Mathf.FloorToInt(rNum), moveSpeed);
    }

    public void SpeedChange(float speedVal) {
        maleZombie.GetComponent<ZombieController>().moveSpeed = speedVal;
    }

    public void GenerateAt(DirectionType direction, float moveSpeed) {
        maleZombie.direction = direction;
        SpeedChange(moveSpeed);

        float startPosX = direction == DirectionType.LEFT
            ? _rightWall.transform.position.x
            : _leftWall.transform.position.x;

        switch (direction)
        {
            case DirectionType.LEFT:                
                Instantiate(maleZombie, new Vector2(startPosX, -2.5f), Quaternion.identity);
                break;
            default:
                Instantiate(maleZombie, new Vector2(startPosX, -2.5f), Quaternion.identity);
                break;
        }
    }

    public void ResetZombieMoveSpeed() {
        zombieMoveSpeed = _defaultZombieMoveSpeed;
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        if(zombies.Length > 0) {
            ZombieController zombie = zombies[0].GetComponent<ZombieController>();
            zombie.moveSpeed = _defaultZombieMoveSpeed;
        }
    }

    IEnumerator SpawnZombie()
    {
        while (true) {
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            if(zombies.Length < 1) {
                GenerateAtRandomPosition(zombieMoveSpeed);
                zombieMoveSpeed += 0.5f;
            }
            yield return new WaitForSeconds(zombieRegenSpeed);
        }
    }

}
