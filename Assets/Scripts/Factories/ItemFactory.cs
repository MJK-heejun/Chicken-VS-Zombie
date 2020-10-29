using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FallingObjectFactory
{

    public GameObject star;
    public GameObject stopSign;
    public GameObject lightning;

    public float itemRegenSpeed;

    void Start() {
        base.SetSpawnPoints();
        StartCoroutine("SpawnItem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateAtRandomPosition()
    {        
        ItemType itemType = (ItemType)Mathf.FloorToInt(Random.Range(0f, 4f));
        GameObject gameObj = null;
        switch(itemType){
            case ItemType.STAR:
                gameObj = star;
                break;
            case ItemType.STOP_SIGN:
                gameObj = stopSign;
                break;
            case ItemType.LIGHTNING:
                gameObj = lightning;    
                break;
            default:
                // item generation skipped
                break;
        }        

        float spawnXPos = Random.Range(_spawnXPosMin, _spawnXPosMax);
        Instantiate(gameObj, new Vector2(spawnXPos, _spawnYPos), Quaternion.identity);
    }

    IEnumerator SpawnItem()
    {
        while (true) {
            GenerateAtRandomPosition();
            yield return new WaitForSeconds(itemRegenSpeed);
        }
    }
}
