using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Planet_destruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     private void OnCollisionEnter(Collision collision){
        gameObject.GetComponent<Rigidbody>().mass += collision.gameObject.GetComponent<Rigidbody>().mass;
        Debug.Log(Convert.ToSingle(Math.Pow(collision.gameObject.transform.localScale.y, -3)));

        float comb_vol;
        float new_r;

        comb_vol = Convert.ToSingle(4/3f * Math.PI * Math.Pow(collision.gameObject.transform.localScale.x, 3f) +
         4/3f * Math.PI * Math.Pow(gameObject.transform.localScale.x, 3f));
        
        new_r = Convert.ToSingle(Math.Pow(comb_vol / (Math.PI * 4/3f), 1/3f));
        
        gameObject.transform.localScale = new Vector3(new_r, new_r, new_r);

          
        Destroy(collision.gameObject);
    }
}
