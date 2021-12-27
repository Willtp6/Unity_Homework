using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fence, rock, coin;
    bool[,] map = new bool[5, 3];
    int fenceXPos = -1, posX = 0, posZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 3; j++)
                map[i, j] = true;
        //instantiate new fence
        if (Random.Range(0, 4) < 3)
        {
            fenceXPos = Random.Range(0, 5);
            for (int j = 0; j < 3; j++)
                map[fenceXPos, j] = false;
            generateFence(fenceXPos);
        }
        int numOfRock = Random.Range(3, 5);
        for(int i = 0; i < numOfRock; ++i)
        {
            while (true)
            {
                posX = Random.Range(0, 5);
                posZ = Random.Range(0, 3);
                if (map[posX, posZ])
                    break;
            }
            map[posX, posZ] = false;
            generateRock(posX, posZ);
        }
        for (int i = 0; i < Random.Range(3, 6); ++i)
        {
            while (true)
            {
                posX = Random.Range(0, 5);
                posZ = Random.Range(0, 3);
                if (map[posX, posZ])
                    break;
            }
            map[posX, posZ] = false;
            generateCoin(posX, posZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateFence(int fanceXPos)
    {
        Instantiate(fence, transform.transform);
        fence.transform.localPosition = Vector3.zero - new Vector3(fenceXPos * 6 + 3, 0, 0);
        fence.transform.localEulerAngles = new Vector3(0, 90, 0);
        fence.transform.localScale = new Vector3(1.5f, 1.5f, 2f);
    }
    void generateRock(int posX, int posZ)
    {
        Instantiate(rock, transform.transform);
        rock.transform.localPosition = Vector3.zero - new Vector3(posX * 6 + 3, 0, (posZ - 1) * 3);
        rock.transform.localEulerAngles = new Vector3(0, 90, 0);
        rock.transform.localScale = new Vector3(1f, 1.5f, 1f);
    }
    void generateCoin(int poX, int posZ)
    {
        Instantiate(coin, transform.transform);
        coin.transform.localPosition = Vector3.zero - new Vector3(posX * 6 + 3, -1, (posZ - 1) * 3);
        coin.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
