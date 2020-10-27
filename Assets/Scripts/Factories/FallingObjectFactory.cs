using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectFactory : MonoBehaviour
{
    private GameObject _leftWall;
    private GameObject _rightWall;
    protected float _spawnYPos;
    protected float _spawnXPosMin;
    protected float _spawnXPosMax;

    void Start()
    {
        SetSpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SetSpawnPoints(){
        _leftWall = GameObject.Find("LeftWall");
        _rightWall = GameObject.Find("RightWall");

        _spawnYPos = _leftWall.transform.position.y + (_leftWall.transform.localScale.y / 2);
        _spawnXPosMin = _leftWall.transform.position.x + (_leftWall.transform.localScale.x / 2);
        _spawnXPosMax = _rightWall.transform.position.x - (_leftWall.transform.localScale.x / 2);
    }
}