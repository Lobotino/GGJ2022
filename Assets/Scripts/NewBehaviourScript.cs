using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        gameObject.transform.Translate(speed * Time.deltaTime * Vector3.left);    
    }
}
