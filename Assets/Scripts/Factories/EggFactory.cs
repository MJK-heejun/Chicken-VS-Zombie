using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggFactory : FallingObjectFactory
{

    public GameObject egg;
    public float eggRegenSpeed = 2f;

    void Start() {
        base.SetSpawnPoints();
        StartCoroutine("SpawnEgg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //generate at either left or right
    public void GenerateAtRandomPosition()
    {        
        float spawnXPos = Random.Range(_spawnXPosMin, _spawnXPosMax);
        Instantiate(egg, new Vector2(spawnXPos, _spawnYPos), Quaternion.identity);
    }

    IEnumerator SpawnEgg()
    {
        while (true) {
            GenerateAtRandomPosition();
            yield return new WaitForSeconds(eggRegenSpeed);
        }
    }
}
