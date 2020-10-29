using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallFactory : FallingObjectFactory
{

    public GameObject spikedBall;
    public float spikedBallRegenSpeed = 2f;
    public float adjustTime = 1f;

    private float _defaultSpikedBallRegenSpeed;    
    private float _spikedBallMinRegenTime = 0.035f; // was 0.05f
    private float _spikedBallRegenSpeedReducer = 0.05f;

    void Start() {
        _defaultSpikedBallRegenSpeed = spikedBallRegenSpeed;
        base.SetSpawnPoints();
        StartCoroutine("SpawnSpikedBall");
        StartCoroutine("AdjustRegenTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //generate at either left or right
    public void GenerateAtRandomPosition()
    {        
        float spawnXPos = Random.Range(_spawnXPosMin, _spawnXPosMax);
        Instantiate(spikedBall, new Vector2(spawnXPos, _spawnYPos), Quaternion.identity);
    }

    public void ResetSpikedBallRegenSpeed(){
        spikedBallRegenSpeed = _defaultSpikedBallRegenSpeed;
    }

    IEnumerator SpawnSpikedBall()
    {
        while (true) {            
            GenerateAtRandomPosition();
            yield return new WaitForSeconds(spikedBallRegenSpeed);
        }
    }

    IEnumerator AdjustRegenTime(){
        while (true) {
            spikedBallRegenSpeed -= _spikedBallRegenSpeedReducer;
            if(spikedBallRegenSpeed < _spikedBallMinRegenTime){
                spikedBallRegenSpeed = _spikedBallMinRegenTime;
            }
            if (spikedBallRegenSpeed < 0.35f) {
                _spikedBallRegenSpeedReducer = 0.003f; // was 0.01f
            } else {
                _spikedBallRegenSpeedReducer = 0.05f;
            }
            yield return new WaitForSeconds(adjustTime);
        }
    }
}
