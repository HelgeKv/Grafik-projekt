using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial_code : MonoBehaviour
{   
    readonly float G = 100f;
    GameObject[] celestials;
    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestials");
        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Gravity();
    }

    void Gravity(){
        foreach(GameObject celestial in celestials){
            foreach(GameObject other_celestial in celestials){
                if(celestial != other_celestial){
                    float m1 = celestial.GetComponent<Rigidbody>().mass;
                    float m2 = other_celestial.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(celestial.transform.position, other_celestial.transform.position);

                    celestial.GetComponent<Rigidbody>().AddForce(G * ((m1 * m2) / (r * r)) * (other_celestial.transform.position  - celestial.transform.position).normalized);
                }
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
}
