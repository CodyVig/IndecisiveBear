using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantPass : MonoBehaviour
{
    GameObject[] walls;
    public int placementX = 0;
    public int width = 1;
    public GameObject mostLeft;
    public GameObject nextX;

    float spriteWidth;
    float spriteHeight;

    void updateLeft()
    {
        if (nextX != null)
        {
            nextX.GetComponent<CantPass>().mostLeft = mostLeft;
            nextX.GetComponent<CantPass>().updateLeft();
        }
    }

    int updateWidth()
    {
        if (nextX != null)
        {
            return 1 + nextX.GetComponent<CantPass>().updateWidth();
        }
        return 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        mostLeft = gameObject;

        walls = GameObject.FindGameObjectsWithTag("CantPass");
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        foreach (GameObject wall in walls)
        {
            if (wall != gameObject)
            {
                if (wall.transform.position.x > transform.position.x && 
                    wall.transform.position.x < transform.position.x + spriteWidth * 2 &&
                    wall.transform.position.y == transform.position.y)
                {
                    nextX = wall;
                    //width += wall.GetComponent<CantPass>().width;
                    //wall.GetComponent<CantPass>().width = width;
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
                    //wall.GetComponent<CantPass>().updateWidth();

                    //Debug.Log("hi");
                }

                if (wall.transform.position.x < transform.position.x &&
                    wall.transform.position.x > transform.position.x - spriteWidth * 2 &&
                    wall.transform.position.y == transform.position.y)
                {
                    //Debug.Log("ohno");
                    //width += wall.GetComponent<CantPass>().width;
                    //wall.GetComponent<CantPass>().width = width;

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
