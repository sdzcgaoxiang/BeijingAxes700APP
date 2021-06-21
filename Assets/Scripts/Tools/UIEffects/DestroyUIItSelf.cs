using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUIItSelf : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartDestroy(float DestroyTime)
    {
        StartCoroutine(DestroyMyself(DestroyTime));
    }
    IEnumerator DestroyMyself(float DesTime)
    { 
        yield return new WaitForSeconds(DesTime);
        GameObject.Destroy(this.gameObject);
    }

}
