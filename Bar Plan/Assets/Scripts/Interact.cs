using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameManager.InteractType interactType;
    private Movement playerCharacter;
    private GameManager gameManager;

    private Color c_startColor;

    private Vector3 v3_startPos;
    private Vector3 v3_endPos;

    private Vector3 v3_moveTowards;
    private bool bl_moveTowards;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.Find("Player").GetComponent<Movement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        c_startColor = GetComponent<Renderer>().material.color;
        v3_startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HightlightObject();
        if (bl_moveTowards) MoveTowards();
    }

    void HightlightObject()
    {
        if (playerCharacter.objectSelected == gameObject)
        {
            GetComponent<Renderer>().material.color = gameManager.c_highlight;
            if (Input.GetMouseButtonDown(0)) ObjectInteract();
        }
        else
        {
            GetComponent<Renderer>().material.color = c_startColor;
        }
    }

    void ObjectInteract()
    {
        switch(interactType)
        {
            case GameManager.InteractType.Table:
                {

                }
                break;

            case GameManager.InteractType.Stage:
                {
                    if (v3_endPos == new Vector3(0, 0, 0))
                    {
                        v3_endPos = v3_startPos;
                        v3_endPos.z -= 4;
                    }

                    if (v3_moveTowards == v3_endPos)
                    {
                        v3_moveTowards = v3_startPos;
                    }
                    else
                    {
                        v3_moveTowards = v3_endPos;
                    }

                    bl_moveTowards = true;
                }
                break;

            case GameManager.InteractType.Door:
                {
                    if (transform.rotation.y == 0)
                    {
                        Quaternion doorOpen = Quaternion.Euler(transform.rotation.x,
                                                               90,
                                                               transform.rotation.z);
                        transform.rotation = doorOpen;
                    }
                    else
                    {
                        Quaternion doorClosed = Quaternion.Euler(transform.rotation.x,
                                                                 0,
                                                                 transform.rotation.z);
                        transform.rotation = doorClosed;
                    }
                }
                break;
        }
    }

    void MoveTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, v3_moveTowards, 1f * Time.deltaTime);
        if (transform.position == v3_moveTowards)
        {
            bl_moveTowards = false;
        }
    }
}
