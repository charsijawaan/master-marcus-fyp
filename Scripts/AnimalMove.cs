using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public float Speed;
    public bool SwitchFace = false;

    public void Update()
    {
        if (Vector3.Magnitude(this.gameObject.transform.position) == Vector3.Magnitude(EndPosition))
        {
            this.gameObject.transform.rotation = new Quaternion(this.gameObject.transform.rotation.x, 180f, this.gameObject.transform.rotation.z, this.gameObject.transform.rotation.w);
            SwitchFace = true;
        }
        else if (Vector3.Magnitude(this.gameObject.transform.position) == Vector3.Magnitude(StartPosition))
        {
            this.gameObject.transform.rotation = new Quaternion(this.gameObject.transform.rotation.x, 0f, this.gameObject.transform.rotation.z, this.gameObject.transform.rotation.w);
            SwitchFace = false;
        }
        if (!SwitchFace)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, EndPosition, Time.deltaTime * Speed);
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, StartPosition, Time.deltaTime * Speed);
        }
    }

}
