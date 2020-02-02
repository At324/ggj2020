using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    public GameObject objects;

    public Vector3 speedVector;

    public List<GameObject> objectsToMove = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        objectsToMove.Add(GameObject.FindGameObjectWithTag("ObjectsToMove"));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject thing in objectsToMove)
        {
            thing.transform.Translate(speedVector * Time.deltaTime);
        }
    }

    public void SpawnObject()
    {
        Vector3 spawnLoc;

        spawnLoc = this.gameObject.transform.position;
        spawnLoc += new Vector3(-30f, 0f, 0f);

        GameObject newObject = Instantiate<GameObject>(objects, spawnLoc, Quaternion.identity);
        objectsToMove.Add(newObject);

    }
}
