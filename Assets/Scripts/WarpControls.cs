using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class WarpControls : MonoBehaviour
{
    bool canWarp = false;
    Camera camera;

    [SerializeField] GameObject firstPedestalMount;

    // Start is called before the first frame update
    void Start()
    {
        // move player to assigned pedestal
        gameObject.transform.position = firstPedestalMount.transform.position;
        firstPedestalMount.GetComponent<Renderer>().enabled = false;

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward);
        if(Physics.Raycast(ray, out hit, 10f))
        {
            if(hit.transform.tag == "Pedestal")
            {
                print("I'm looking at " + hit.transform.name);
            }
        }
        else
        {
            print("I'm looking at nothing!");
        }
    }
    
}
