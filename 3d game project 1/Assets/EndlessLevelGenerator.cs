using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelGenerator : MonoBehaviour
{

    public GameObject[] tilePreFabs;
    public Transform player;
    public float tileLength;
    public int tilesOnScreen = 3;
    public float spawnZ = 404.5f;

    private int lastPrefabIndex = 0;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float safeZone = 340f; // Distance before deleting a tile

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i < 2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
        }
    }

    void SpawnTile(int preFabIndex = -1)
    {

        GameObject go;
        if (preFabIndex == -1)
        {
            preFabIndex = RandomPrefabIndex();
        }

        go = Instantiate(tilePreFabs[preFabIndex], Vector3.forward * spawnZ, Quaternion.identity);
        Debug.Log(Vector3.forward * spawnZ);
        activeTiles.Add(go);
        spawnZ += tileLength;
    }

    int RandomPrefabIndex()
    {
        if (tilePreFabs.Length <= 1) return 0;
        int randomIndex;
        do
        {
            randomIndex = Random.Range(2, tilePreFabs.Length);
        }

        while (randomIndex == lastPrefabIndex);

        lastPrefabIndex = randomIndex;
        return randomIndex;


    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}