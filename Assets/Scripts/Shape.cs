using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public string shapeName;
    public Rigidbody2D body;
    public Vector2 velocity;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("도형의 이름은 = " + shapeName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
