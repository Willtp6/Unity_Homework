using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinite_Road : MonoBehaviour
{
    float roadY = 270;
    Vector3 newObjectPos;
    int lastnum = 3, roadData = 0;
    //gameobject while in the game
    public GameObject roadWithoutEnd, leftCorner, rightCorner, hole, oneSide;
    
    // Start is called before the first frame update
    void Start()
    {
        newObjectPos = new Vector3(-72f, 0f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void autoGenerate()
    {
        Vector3 direction = getNewDirection(roadY);
        int num;
        do
        {
            num = Random.Range(0, 5);
        } while (!checkroad(num, lastnum));
        lastnum = num;
        switch (num)
        {
            case 0:
                Instantiate(hole, newObjectPos, Quaternion.Euler(0, roadY + 90, 0));
                newObjectPos += direction * 12;
                break;
            case 1:
                Instantiate(leftCorner, newObjectPos, Quaternion.Euler(0, roadY + 90, 0));
                roadY -= 90;
                newObjectPos += direction * 11 + Quaternion.AngleAxis(-90, Vector3.up) * direction * 11;
                break;
            case 2:
                Instantiate(rightCorner, newObjectPos, Quaternion.Euler(0, roadY + 90, 0));
                roadY += 90;
                newObjectPos += direction * 11 + Quaternion.AngleAxis(90, Vector3.up) * direction * 11;
                break;
            case 3:
                Instantiate(roadWithoutEnd, newObjectPos, Quaternion.Euler(0, roadY + 90, 0));
                newObjectPos += direction * 30;
                break;
            case 4:
                Instantiate(oneSide, newObjectPos + Quaternion.AngleAxis(90, Vector3.up) * direction * Random.Range(-1, 2) * 3, Quaternion.Euler(0, roadY + 180, 0));
                newObjectPos += direction * 12;
                break;
        }
        roadY %= 360;
    }
    public bool checkroad(int num1, int num2)
    {
        if (num1 == num2)
        {
            if (num1 == 3)//only continuous straight is leagal
                return true;
            else
                return false;
        }
        else
        {
            if (num1 == 1)
            {
                if (roadData == 3)
                    return false;
                roadData += 1;
            }
            if (num1 == 2)
            {
                if (roadData == -3)
                    return false;
                roadData -= 1;
            }
        }
        return true;
    }

    public Vector3 getNewDirection(float y)
    {
        int dir = (int)y;
        Vector3 tmp = Vector3.zero;
        switch (dir)
        {
            case 0:
            case 360: tmp = new Vector3(0, 0, 1); break;
            case -270:
            case 90: tmp = new Vector3(1, 0, 0); break;
            case -180:
            case 180: tmp = new Vector3(0, 0, -1); break;
            case -90:
            case 270: tmp = new Vector3(-1, 0, 0); break;
        }
        return tmp;
    }

}
