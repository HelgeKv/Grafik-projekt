using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial_code : MonoBehaviour
{   
    readonly float G = 10f;
    GameObject[] celestials;
    float rotationSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {

        celestials = GameObject.FindGameObjectsWithTag("Celestials");
        InitialVelocity();
        rotationSpeed = 12f/gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCelectials();
    }

    private void FixedUpdate() {
        Gravity();
    }

    void Gravity(){

                foreach(GameObject other_celestial in celestials){

                        if(other_celestial != null && gameObject != other_celestial) {
                            float m1 = gameObject.GetComponent<Rigidbody>().mass;
                            float m2 = other_celestial.GetComponent<Rigidbody>().mass;
                            float r = Vector3.Distance(gameObject.transform.position, other_celestial.transform.position);

                            gameObject.GetComponent<Rigidbody>().AddForce(G * ((m1 * m2) / (r * r)) * (other_celestial.transform.position  - gameObject.transform.position).normalized);
                        }
                }   
    
    }
    void InitialVelocity()
    {
        foreach(GameObject celestial in celestials){
            foreach (GameObject other_celestial in celestials){
                if(celestial.name != "Sun"){
                    //celestial.GetComponent<Rigidbody>().velocity = new Vector3(-130, 0, 0);
                }
            }
        }
    }

    void RotateCelectials()
    {
       
        gameObject.transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);

            
        
    }
}