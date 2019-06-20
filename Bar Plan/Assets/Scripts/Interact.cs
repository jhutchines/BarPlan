using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameManager.InteractType interactType;
    private Movement playerCharacter;
    private GameManager gameManager;

    private Color c_startColor;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.Find("Player").GetComponent<Movement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        c_startColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        HightlightObject();
    }

    void HightlightObject()
    {
        if (playerCharacter.objectSelected == gameObject)
        {
            GetComponent<Renderer>().material.color = gameManager.c_highlight;
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

                }
                break;

            case GameManager.InteractType.Door:
                {

                }
                break;
        }
    }
}
