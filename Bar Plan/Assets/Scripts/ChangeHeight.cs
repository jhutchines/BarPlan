using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeight : MonoBehaviour
{
    public float fl_newHeight;
    public float fl_oldHeight;

    // Start is called before the first frame update
    void Start()
    {
        fl_oldHeight = GameObject.Find("Player").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = new Vector3(other.transform.position.x, fl_newHeight, other.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = new Vector3(other.transform.position.x, fl_oldHeight, other.transform.position.z);
        }
    }
}
