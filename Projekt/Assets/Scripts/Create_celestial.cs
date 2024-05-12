using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_celestial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject celestial;
    private int plane;
    
    public float min_launchforce = 0f;
    public float max_launchforce = 1000f;
    public float charge_speed = 100f;
    private Vector3 initial_coord; 
 
    private bool fired;
    private float current_launchforce;

    private void Awake()
    {
        plane = LayerMask.GetMask("Plane");
        
    }

   

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            fired = false;
            current_launchforce = min_launchforce;

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if(Physics.Raycast(camRay, out hit, 1000f, plane))
            {
                initial_coord = hit.point;
            }

        } else if (Input.GetMouseButton(0) && !fired)
        {
            current_launchforce += charge_speed * Time.deltaTime * 100f;
        } else if (Input.GetMouseButtonUp(0) && !fired)
        {
            MouseClick();
        }
    } 
    private void MouseClick()
    {
        
        fired = true;
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        
        if(Physics.Raycast(camRay, out hit, 1000f, plane))
        {

            GameObject planet = Instantiate(celestial, hit.point, Quaternion.identity);
            planet.GetComponent<Rigidbody>().velocity = new Vector3( initial_coord.x - hit.point.x, initial_coord.y - hit.point.y, initial_coord.z - hit.point.z).normalized * current_launchforce;
            planet.GetComponent<Rigidbody>().mass = current_launchforce;
        }
    }
}
