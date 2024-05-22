using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Planet_destruction : MonoBehaviour
{
    public GameObject explosion;

    public GameObject explosion2;

    public GameObject glow;

    public GameObject lightSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     private void OnCollisionEnter(Collision collision){
        gameObject.GetComponent<Rigidbody>().mass += collision.gameObject.GetComponent<Rigidbody>().mass;

        float comb_vol = Convert.ToSingle(4/3f * Math.PI * Math.Pow(collision.gameObject.transform.localScale.x, 3f) +
         4/3f * Math.PI * Math.Pow(gameObject.transform.localScale.x, 3f));
        
        float new_r = Convert.ToSingle(Math.Pow(comb_vol / (Math.PI * 4/3f), 1/3f));
        
        gameObject.transform.localScale = new Vector3(new_r, new_r, new_r);
        glow.transform.localScale = new Vector3(new_r/10f, new_r/10f, new_r/10f);
        lightSource.GetComponent<Light>().range = new_r*10f;
        
    
        float dif_x = gameObject.transform.position.x - collision.gameObject.transform.position.x;
        float dif_z = gameObject.transform.position.z -  collision.gameObject.transform.position.z;
        float angle = Convert.ToSingle(Math.Atan2(dif_x, dif_z) * 180 / Math.PI);
        
        float rot_x = Convert.ToSingle(Math.Sin(angle));
        float rot_z = Convert.ToSingle(Math.Cos(angle));

        
    
        GameObject effect = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.Euler(0, angle, 0));
        effect.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x/12f, collision.gameObject.transform.localScale.y/12f, collision.gameObject.transform.localScale.z/12f);
        GameObject effect2 = Instantiate(explosion2, collision.gameObject.transform.position, Quaternion.Euler(0, angle+180f, 0));
        effect2.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x/15f, collision.gameObject.transform.localScale.y/15f, collision.gameObject.transform.localScale.z/15f);


        gameObject.GetComponent<Rigidbody>().velocity += collision.gameObject.GetComponent<Rigidbody>().mass * collision.gameObject.GetComponent<Rigidbody>().velocity  / gameObject.GetComponent<Rigidbody>().mass;
          
        Destroy(collision.gameObject);
        
    }
}
