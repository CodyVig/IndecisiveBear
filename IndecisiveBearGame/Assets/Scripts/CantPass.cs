using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantPass : MonoBehaviour
{
    GameObject[] walls;
    public int placementX = 0;
    public int width = 1;
    public int height = 1;
    public GameObject mostLeft;
    public GameObject mostTop;
    public GameObject nextX;
    public GameObject nextY;

    float spriteWidth;
    float spriteHeight;

    public void updateLeft()
    {
        if (nextX != null)
        {
            nextX.GetComponent<CantPass>().mostLeft = mostLeft;
            nextX.GetComponent<CantPass>().updateLeft();
        }
    }

    public void updateTop()
    {
        if (nextY != null)
        {
            nextY.GetComponent<CantPass>().mostTop = mostTop;
            nextY.GetComponent<CantPass>().updateTop();
        }
    }

    public int updateWidth()
    {
        if (nextX != null)
        {
            return 1 + nextX.GetComponent<CantPass>().updateWidth();
        }
        return 1;
    }

    public int updateHeight()
    {
        if (nextY != null)
        {
            return 1 + nextY.GetComponent<CantPass>().updateHeight();
        }
        return 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        mostLeft = gameObject;
        mostTop = gameObject;

        walls = GameObject.FindGameObjectsWithTag("CantPass");
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        foreach (GameObject wall in walls)
        {
            if (wall != gameObject)
            {
                // Y COLLISION
                if (wall.transform.position.y < transform.position.y &&
                    wall.transform.position.y > transform.position.y - spriteHeight * 2 &&
                    wall.transform.position.x == transform.position.x)
                {
                    nextY = wall;
                    wall.GetComponent<CantPass>().mostTop = mostTop;
                    wall.GetComponent<CantPass>().updateTop();
                    if (mostTop == null)
                    {
                        height = GetComponent<CantPass>().updateHeight();
                    }
                    else
                    {
                        height = mostTop.GetComponent<CantPass>().updateHeight();
                    }
                }
                if (wall.transform.position.y > transform.position.y &&
                    wall.transform.position.y < transform.position.y + spriteHeight * 2 &&
                    wall.transform.position.x == transform.position.x)
                {

                    mostTop = wall.GetComponent<CantPass>().mostTop;
                    updateTop();

                    wall.GetComponent<CantPass>().nextY = gameObject;
                    if (mostTop == null)
                    {
                        height = wall.GetComponent<CantPass>().updateHeight();
                    }
                    else
                    {
                        height = mostTop.GetComponent<CantPass>().updateHeight();
                    }


                }


                // X COLLISION
                if (wall.transform.position.x > transform.position.x &&
                    wall.transform.position.x < transform.position.x + spriteWidth * 2 &&
                    wall.transform.position.y == transform.position.y)
                {
                    nextX = wall;
                    wall.GetComponent<CantPass>().mostLeft = mostLeft;
                    wall.GetComponent<CantPass>().updateLeft();
                    if (mostLeft == null)
                    {
                        width = GetComponent<CantPass>().updateWidth();
                    }
                    else
                    {
                        width = mostLeft.GetComponent<CantPass>().updateWidth();
                    }
                }

                if (wall.transform.position.x < transform.position.x &&
                    wall.transform.position.x > transform.position.x - spriteWidth * 2 &&
                    wall.transform.position.y == transform.position.y)
                {

                    mostLeft = wall.GetComponent<CantPass>().mostLeft;
                    updateLeft();

                    wall.GetComponent<CantPass>().nextX = gameObject;
                    if (mostLeft == null)
                    {
                        width = wall.GetComponent<CantPass>().updateWidth();
                    }
                    else
                    {
                        width = mostLeft.GetComponent<CantPass>().updateWidth();
                    }


                }
            }
        }
    }
}