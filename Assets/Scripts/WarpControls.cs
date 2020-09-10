using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class WarpControls : MonoBehaviour
{
    bool canWarp = false;
    bool isWarping = false;
    GameObject nextWarpPoint;
    GameObject currentWarpPoint;
    Camera camera;
    Animator anim;

    [SerializeField] GameObject firstPedestalMount;
    [SerializeField] float warpSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // move player to assigned pedestal
        gameObject.transform.position = firstPedestalMount.transform.position;
        firstPedestalMount.GetComponent<Renderer>().enabled = false;
        currentWarpPoint = firstPedestalMount;

        camera = Camera.main;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && canWarp)
        {
            WarpToMountPoint();
        }

        if (isWarping)
        {
            if (!anim.GetBool("isWarping"))
            {
                anim.SetBool("isWarping", true);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextWarpPoint.transform.position, warpSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, nextWarpPoint.transform.position) <= 0.1f)
            {
                isWarping = false;
            anim.SetBool("isWarping", false);
            }
        }

        if(nextWarpPoint != null && !isWarping && canWarp)
        {
            nextWarpPoint.GetComponentInParent<Glow>().GlowOn();
        }
        else if (isWarping)
        {
            nextWarpPoint.GetComponentInParent<Glow>().GlowOff();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Ray ray = new Ray(transform.position + transform.up, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Pedestal")
            {
                canWarp = true;
                nextWarpPoint = hit.transform.gameObject;
                
            }
            else
            {
                canWarp = false;

            }

        }    
    }

    public void WarpToMountPoint()
    {
        if (canWarp)
        {
            print("I'm a warping!");
            //gameObject.transform.position = warpPointLookingAt.transform.position;
            isWarping = true;
            nextWarpPoint.GetComponent<Renderer>().enabled = false;
            currentWarpPoint.GetComponent<Renderer>().enabled = true;
            currentWarpPoint = nextWarpPoint;
            canWarp = false;
        }
    }
}
