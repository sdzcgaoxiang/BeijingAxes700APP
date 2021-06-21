using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertiacalFixed : MonoBehaviour
{
    public GameObject m_Target;
     private Vector3 m_FixedPosition;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        m_FixedPosition = new Vector3(m_Target.transform.position.x, gameObject.transform.position.y, m_Target.transform.position.z);
        if (m_FixedPosition != gameObject.transform.position)
        {
            gameObject.transform.position = m_FixedPosition;
        }
      
    }
}
