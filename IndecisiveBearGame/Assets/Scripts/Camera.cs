using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Camera camera;

    public GameObject blackboxTop;
    public GameObject blackboxBottom;
    public GameObject blackboxLeft;
    public GameObject blackboxRight;
    SpriteRenderer blackboxTopRenderer;
    SpriteRenderer blackboxBottomRenderer;
    SpriteRenderer blackboxLeftRenderer;
    SpriteRenderer blackboxRightRenderer;

    float opacitySpeed = 0.005f;
    float gainOpacityTop = 0f;
    bool topIsHere = false;
    float gainOpacityBottom = 0f;
    bool bottomIsHere = false;
    float gainOpacityLeft = 0f;
    bool leftIsHere = false;
    float gainOpacityRight = 0f;
    bool rightIsHere = false;

    GameObject player;
    GameObject[] walls;

    float width;
    float height;
    float widthDiv = 4f;
    float heightDiv = 4f;

    float yDif = 0f;
    float xDif = 0f;

    Vector3 pos;

    bool setUpCantPass = false;

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

        blackboxBottom.transform.localScale = new Vector3(blackboxBottom.transform.localScale.x, height, blackboxBottom.transform.localScale.z);
        blackboxBottomRenderer = blackboxBottom.GetComponent<SpriteRenderer>();

        blackboxLeft.transform.localScale = new Vector3(width, blackboxLeft.transform.localScale.y, blackboxLeft.transform.localScale.z);
        blackboxLeftRenderer = blackboxLeft.GetComponent<SpriteRenderer>();

        blackboxRight.transform.localScale = new Vector3(width, blackboxRight.transform.localScale.y, blackboxRight.transform.localScale.z);
        blackboxRightRenderer = blackboxRight.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setUpCantPass == false)
        {
            foreach (GameObject wall in walls)
            {
                wall.GetComponent<CantPass>().updateLeft();
                wall.GetComponent<CantPass>().width = wall.GetComponent<CantPass>().updateWidth();
                wall.GetComponent<CantPass>().updateTop();
                wall.GetComponent<CantPass>().height = wall.GetComponent<CantPass>().updateHeight();
            }
            setUpCantPass = true;
        }

        if (topIsHere == false)
        {
            gainOpacityTop -= opacitySpeed;
            if (gainOpacityTop < 0f)
            {
                gainOpacityTop = 0f;
            }
        }

        topIsHere = false;



        if (bottomIsHere == false)
        {
            gainOpacityBottom -= opacitySpeed;
            if (gainOpacityBottom < 0f)
            {
                gainOpacityBottom = 0f;
            }
        }

        bottomIsHere = false;


        if (leftIsHere == false)
        {
            gainOpacityLeft -= opacitySpeed;
            if (gainOpacityLeft < 0f)
            {
                gainOpacityLeft = 0f;
            }
        }

        leftIsHere = false;



        if (rightIsHere == false)
        {
            gainOpacityRight -= opacitySpeed;
            if (gainOpacityRight < 0f)
            {
                gainOpacityRight = 0f;
            }
        }

        rightIsHere = false;

        // UNCOMMENT THIS CODE WHEN WE IMPLEMENT THE CAMERA PLAYER PUSHING CODE

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
        pos = player.transform.position - transform.forward * 10 - transform.up * yDif - transform.right * xDif;

        if (yDif != 0f)
        {
            if (player.transform.position.y > transform.position.y)
            {
                if (pos.y < transform.position.y)
                {
                    pos = new Vector3(pos.x, transform.position.y, pos.z);
                    yDif = player.transform.position.y - transform.position.y;
                }
            } else
            {
                if (pos.y > transform.position.y)
                {
                    pos = new Vector3(pos.x, transform.position.y, pos.z);
                    yDif = player.transform.position.y - transform.position.y;
                }
            }
        }
        if (xDif != 0f)
        {
            if (player.transform.position.x > transform.position.x)
            {
                if (pos.x < transform.position.x)
                {
                    pos = new Vector3(transform.position.x, pos.y, pos.z);
                    xDif = player.transform.position.x - transform.position.x;
                }
            }
            else
            {
                if (pos.x > transform.position.x)
                {
                    pos = new Vector3(transform.position.x, pos.y, pos.z);
                    xDif = player.transform.position.x - transform.position.x;
                }
            }
        }


        foreach (GameObject wall in walls)
        {
            float width2 = wall.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float height2 = wall.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            // X COLLISION
            if (player.transform.position.y > wall.transform.position.y - height2 &&
                player.transform.position.y < wall.transform.position.y + height2)
            {
                if (pos.x + width / 2 > wall.transform.position.x + width2)
                {
                    if (pos.x + width / 2 < wall.transform.position.x + width2 + width / 100)
                    {
                        pos = new Vector3(wall.transform.position.x + width2 - width / 2, pos.y, pos.z);
                        xDif = player.transform.position.x - transform.position.x;
                    }
                    else
                    {
                        if (player.transform.position.x < wall.transform.position.x)
                        {
                            float bbwidth = wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width * (width2 * 2) + width;
                            float bbheight = wall.GetComponent<CantPass>().mostTop.GetComponent<CantPass>().height * (height2 * 2);
                            blackboxRight.transform.localScale = new Vector3(bbwidth, wall.transform.localScale.y * wall.GetComponent<CantPass>().mostTop.GetComponent<CantPass>().height, blackboxRight.transform.localScale.z);

                            blackboxRight.transform.position = transform.right * (wall.GetComponent<CantPass>().mostLeft.transform.position.x + (bbwidth/2 + width2) + (bbwidth - width) - width2*2) + transform.up * (wall.GetComponent<CantPass>().mostTop.transform.position.y - (bbheight / 2) + height2) + transform.forward * wall.transform.position.z;

                            gainOpacityRight += opacitySpeed;
                            if (gainOpacityRight > 1f)
                            {
                                gainOpacityRight = 1f;
                            }
                            rightIsHere = true;
                        }
                    }
                }
                if (pos.x - width / 2 < wall.transform.position.x - width2)
                {
                    if (pos.x - width / 2 > wall.transform.position.x - width2 - width / 100)
                    {
                        pos = new Vector3(wall.transform.position.x - width2 + width / 2, pos.y, pos.z);
                        xDif = player.transform.position.x - transform.position.x;
                    }
                    else
                    {
                        if (player.transform.position.x > wall.transform.position.x)
                        {
                            float bbwidth = wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width * (width2 * 2) + width;
                            float bbheight = wall.GetComponent<CantPass>().mostTop.GetComponent<CantPass>().height * (height2 * 2);
                            blackboxLeft.transform.localScale = new Vector3(bbwidth, wall.transform.localScale.y * wall.GetComponent<CantPass>().mostTop.GetComponent<CantPass>().height, blackboxLeft.transform.localScale.z);

                            blackboxLeft.transform.position = transform.right * (wall.GetComponent<CantPass>().mostLeft.transform.position.x - (bbwidth / 2 + width2)) + transform.up * (wall.GetComponent<CantPass>().mostTop.transform.position.y - (bbheight / 2) + height2) + transform.forward * wall.transform.position.z;

                            gainOpacityLeft += opacitySpeed;
                            if (gainOpacityLeft > 1f)
                            {
                                gainOpacityLeft = 1f;
                            }
                            leftIsHere = true;
                        }
                    }
                }
            }
            
            // Y COLLISION
            if (player.transform.position.x > wall.transform.position.x - width2 &&
                player.transform.position.x < wall.transform.position.x + width2)
            {
                if (pos.y + height / 2 > wall.transform.position.y + height2)
                {
                    if (pos.y + height / 2 < wall.transform.position.y + height2 + height / 100)
                    {
                        pos = new Vector3(pos.x, wall.transform.position.y + height2 - height / 2, pos.z);
                        yDif = player.transform.position.y - transform.position.y;
                    }
                    else
                    {
                        if (player.transform.position.y < wall.transform.position.y)
                        {
                            
                            float bbwidth = wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width * (width2*2);
                            float bbheight = wall.GetComponent<CantPass>().mostTop.GetComponent<CantPass>().height * (height2*2) + height;
                            blackboxTop.transform.localScale = new Vector3(wall.transform.localScale.x * wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width, bbheight, blackboxTop.transform.localScale.z);

                            blackboxTop.transform.position = transform.right * (wall.GetComponent<CantPass>().mostLeft.transform.position.x + (bbwidth/2 - width2)) + transform.up * (wall.GetComponent<CantPass>().mostTop.transform.position.y + (bbheight/2) + height2) + transform.forward * wall.transform.position.z; 
                            
                            gainOpacityTop += opacitySpeed;
                            if (gainOpacityTop > 1f)
                            {
                                gainOpacityTop = 1f;
                            }
                            topIsHere = true;
                        }
                    }
                }
                if (pos.y - height / 2 < wall.transform.position.y - height2)
                {
                    if (pos.y - height / 2 > wall.transform.position.y - height2 - height / 100)
                    {
                        pos = new Vector3(pos.x, wall.transform.position.y - height2 + height / 2, pos.z);
                        yDif = player.transform.position.y - transform.position.y;
                    } else
                    {
                        if (player.transform.position.y > wall.transform.position.y)
                        {
                            float bbwidth = wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width * (width2 * 2);
                            float bbheight = wall.GetComponent<CantPass>().mostTop.GetComponent<CantPass>().height * (height2 * 2) + height;
                            blackboxBottom.transform.localScale = new Vector3(wall.transform.localScale.x * wall.GetComponent<CantPass>().mostLeft.GetComponent<CantPass>().width, bbheight, blackboxBottom.transform.localScale.z);

                            blackboxBottom.transform.position = transform.right * (wall.GetComponent<CantPass>().mostLeft.transform.position.x + (bbwidth / 2 - width2)) + transform.up * (wall.GetComponent<CantPass>().mostTop.transform.position.y - (bbheight / 2) + height2 - (bbheight - height)) + transform.forward * wall.transform.position.z;
                            gainOpacityBottom += opacitySpeed;
                            if (gainOpacityBottom > 1f)
                            {
                                gainOpacityBottom = 1f;
                            }
                            bottomIsHere = true;
                        }
                   }
                }
            }
        }

        blackboxTopRenderer.color = new Color(0f, 0f, 0f, gainOpacityTop);
        blackboxBottomRenderer.color = new Color(0f, 0f, 0f, gainOpacityBottom);

        blackboxLeftRenderer.color = new Color(0f, 0f, 0f, gainOpacityLeft);
        blackboxRightRenderer.color = new Color(0f, 0f, 0f, gainOpacityRight);


        transform.position = pos;
    }
}
