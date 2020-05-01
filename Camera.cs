using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Distance;
    public float Speed;

    public static object main { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Distance, Speed * Time.deltaTime);
        transform.LookAt(Target);
    }
}
