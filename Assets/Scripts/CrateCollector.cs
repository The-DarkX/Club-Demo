using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollector : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
        print(other.gameObject.name);

        if (other.gameObject.layer.Equals("Crate")) 
        {
            Destroy(other.gameObject);
            print("Crate Collected");
        }
	}

}
