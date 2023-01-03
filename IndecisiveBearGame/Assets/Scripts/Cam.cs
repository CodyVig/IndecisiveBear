using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Camera camera;

    public GameObject blackboxTop;
    SpriteRenderer blackboxTopRenderer;

    float opacitySpeed = 0.005f;
    float gainOpacityTop = 0f;
    bool topIsHere = false;

    GameObject player;
    GameObject[] walls;

    float width;
    float height;
    float widthDiv = 4f;
    float heightDiv = 4f;

    float yDif = 0f;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        player = GameObject.FindGameObjectWithTag("Player");
        walls = GameObject.FindGameObjectsWithTag("CantPass");

        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;

        blackboxTop.transform.localScale = new Vector3(blackboxTop.transform.localScale.x, height, blackboxTop.transform.localScale.z);
        blackboxTopRenderer = blackboxTop.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //blackboxTop.transform.position = new Vector3(0, -10000, 0);

        if (topIsHere == false)
        {
            gainOpacityTop -= opacitySpeed;
            if (gainOpacityTop < 0f)
            {
                gainOpacityTop = 0f;
            }
        }

        topIsHere = false;
        
        /*pos = new Vector3(transform.position.x, transform.position.y, player.transform.position.z) - transform.forward * 10;
        if (player.transform.position.x > transform.position.x + width / widthDiv)
        {
            pos = new Vector3(player.transform.position.x - width / widthDiv, pos.y, pos.z);
        }
        if (player.transform.position.x < transform.position.x - width / widthDiv)
        {
            pos = new Vector3(player.transform.position.x + width / widthDiv, pos.y, pos.z);
        }
        if (player.transform.position.y > transform.position.y + height / heightDiv)
        {
            pos = new Vector3(pos.x, player.transform.position.y - height / heightDiv, pos.z);
        }
        if (player.transform.position.y < transform.position.y - height / heightDiv)
        {
            pos = new Vector3(pos.x, player.transform.position.y + height / heightDiv, pos.z);
        }*/
        pos = player.transform.position - transform.forward * 10 - transform.up * yDif;
        
        if (yDif != 0f)
        {
            if (player.transform.position.y > transform.position.y)
            {
                if (pos.y < transform.position.y)
                {
                    pos = new Vector3(pos.x, transform.position.y, pos.z);
                    yDif = player.transform.position.y - transform.position.y;
                }
            }
        }


        foreach (GameObject wall in walls)
        {
            float width2 = wall.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float height2 = wall.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            if (player.transform.position.x > wall.transform.position.x - width2 &&
                player.transform.position.x < wall.transform.position.x + width2)
            {
                if (pos.y + height / 2 > wall.transform.position.y + height2)
                {
                    if (pos.y + height / 2 < wall.transform.position.y + height2 + height / 100)
                    {
                        pos = new Vector3(pos.x, wall.transform.position.y + height2 - height / 2, pos.z);
                        yDif = player.transform.position.y - transform.position.y;
                    } else
                    {
                        blackboxTop.transform.position = wall.GetComponent<CantPass>().mostLeft.transform.position + transform.right * (wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width / 2 - width2) + transform.up * (height2 + height / 2);
                        blackboxTop.transform.localScale = new Vector3(wall.transform.localScale.x * wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width, blackboxTop.transform.localScale.y, blackboxTop.transform.localScale.z);
                        gainOpacityTop += opacitySpeed;
                        if (gainOpacityTop > 1f)
                        {
                            gainOpacityTop = 1f;
                        }
                        topIsHere = true;
                    }
                }
            }
        }

        blackboxTopRenderer.color = new Color(0f, 0f, 0f, gainOpacityTop);


        transform.position = pos;
    }
}
