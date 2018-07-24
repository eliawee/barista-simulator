using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour {

	public Obi.ObiEmitter emitter;

    private bool pouring = false;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.D)){
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(90,-transform.forward),Time.deltaTime*50);
		}else{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity,Time.deltaTime*100);
		}

		if (Input.GetKey(KeyCode.R)){
			emitter.KillAll();
		}

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (pouring)
            {
                emitter.speed = 0;
            }
            else
            {
                emitter.speed = 4;
            }
            pouring = !pouring;
        }
	}
}
