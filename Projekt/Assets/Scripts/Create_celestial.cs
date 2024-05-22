using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Create_celestial : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject celestial;
    public GameObject Arrow_object;

    private int plane;


    public float min_launchforce = 0f;
    public float max_launchforce = 1000f;
    public float roatation_speed = 100f;
    private Vector3 initial_coord; 
 
    private bool fired;
    private float current_launchforce;

    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Vector3 arrow;
    public Quaternion test = new Quaternion();
   
    private void Awake()
    {
        plane = LayerMask.GetMask("Plane");
    }

    void Start()
    {
       GetComponent<Renderer>().material = mat1;
       GetComponent<Renderer>().material = mat2;
       GetComponent<Renderer>().material = mat3;
       GetComponent<Renderer>().material = mat4;
        Vector3 forward = new Vector3(1f, 0f, 0f);
        Vector3 up = new Vector3(0f, 0f, 0f);
        test.SetLookRotation(forward, up);

        

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
                Arrow_object.SetActive(true);
                Arrow_object.transform.localScale = new Vector3(10f, 10f, 10f);

            }

        } else if (Input.GetMouseButton(0) && !fired)
        {
            current_launchforce += roatation_speed * Time.deltaTime * 100f;
        
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(camRay, out hit, 1000f, plane))
            {
                Vector3 length = (initial_coord - hit.point);
                
                Arrow_object.transform.localScale = new Vector3(1f, length.magnitude/10f, 1f);
                Arrow_object.transform.position = initial_coord + (hit.point-initial_coord)/2;
                Arrow_object.transform.Rotate(new Vector3(0, 0, 0), Space.World);

            }
        } else if (Input.GetMouseButtonUp(0) && !fired)
        {
            MouseClick();

            Arrow_object.SetActive(false);
        }
    } 
    private void MouseClick()
    {
        
        fired = true;
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        
        if(Physics.Raycast(camRay, out hit, 1000f, plane))
        {
            Vector3 forward = new Vector3(1f, 0f, 0f);
            Vector3 up = new Vector3(0f, 0f, 0f);
            test.SetLookRotation(forward, up);

            GameObject planet = Instantiate(celestial, initial_coord, test);
            planet.GetComponent<Rigidbody>().velocity = new Vector3( initial_coord.x - hit.point.x, 0f, initial_coord.z - hit.point.z);
            planet.GetComponent<Rigidbody>().mass = current_launchforce/80f;
            planet.transform.localScale += new Vector3(current_launchforce/400f, current_launchforce/400f, current_launchforce/400f);
            // planet.GetComponent<MeshRenderer>().material = mat ;
            //int temp;
            //System.Random RNG = new System.Random();
            int temp = UnityEngine.Random.Range(0, 4);
            
            if (temp == 0){
                planet.GetComponent<MeshRenderer>().material = mat1;
            }
            if (temp == 1){
                planet.GetComponent<MeshRenderer>().material = mat2;
            }
            if (temp == 2){
                planet.GetComponent<MeshRenderer>().material = mat3;
            }
            if (temp == 3){
                planet.GetComponent<MeshRenderer>().material = mat4;
            }
            
        }
    }
}