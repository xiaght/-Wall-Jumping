using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public GameObject[] Prefab = null;
    public Queue<GameObject> objectQueue = new Queue<GameObject>();
    public Queue<GameObject> objectOutQueue = new Queue<GameObject>();
    public Transform parent;
    public Transform boardparent;

    public Player player;


    public int temp;
    public int objectWall = 0;
    private void Start()
    {
        for (int i = 0; i < 40; i++)
        {
            GameObject objectPool = Instantiate(Prefab[Random.Range(2, 4)], Vector3.zero, Quaternion.identity, parent);
            objectQueue.Enqueue(objectPool);
            objectPool.SetActive(false);
        }
        temp = 0;
    }

    public GameObject GetQueue()
    {
        GameObject t_Object = objectQueue.Dequeue();
        t_Object.SetActive(true);

        objectOutQueue.Enqueue(t_Object);
        return t_Object;

    }

    public void InsertQueue(GameObject p_object)
    {
        objectQueue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    private void Update()
    {
        objectWall = (int)(player.transform.position.y + 20) / 5;
        if (temp != objectWall)
        {
            int ran = Random.Range(0, 4);
            GameObject tempWall = GetQueue();
            tempWall.transform.position = new Vector2(ran*2-3, objectWall * 5);
            if (ran == 0) {

                GameObject objectPool =Instantiate(Prefab[0],new Vector2(tempWall.transform.position.x, tempWall.transform.position.y-0.5f),
                    Quaternion.identity, boardparent);
                SpriteRenderer sr = objectPool.GetComponent<SpriteRenderer>();
                sr.flipX = true;
            }
            else if (ran == 3)
            {
                GameObject objectPool = Instantiate(Prefab[0], new Vector2(tempWall.transform.position.x, tempWall.transform.position.y - 0.5f),
    Quaternion.identity, boardparent);
                SpriteRenderer sr = objectPool.GetComponent<SpriteRenderer>();
                sr.flipX = false;
            }
            else
            {

                GameObject objectPool = Instantiate(Prefab[1], new Vector2(tempWall.transform.position.x, tempWall.transform.position.y - 0.4f),
                    Quaternion.identity, boardparent);

            }

            temp = objectWall;
            
            if (temp > 10)
            {
                GameObject t_Object = objectOutQueue.Dequeue();
                InsertQueue(t_Object);
            }

        }



    }
}
