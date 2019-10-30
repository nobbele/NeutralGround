using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterController : MonoBehaviour {

    public PowerColor Color;

	// Use this for initialization
	void Start () {
        Direction = Vector2.right;
        line = Instantiate(Resources.Load<GameObject>("Line")).GetComponent<LineRenderer>();
        line.startWidth = LineThickness;
        line.endWidth = LineThickness;
    }

    public float LineThickness = 0.01f;

    LineRenderer line;

    Vector2 Origin {
        get {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }
    Vector2 End {
        get {
            Vector2 end = Origin;
            end.x += Distance;
            return end;
        }
    }
    Vector2 Direction = Vector2.right;
    float Distance;

    LaserRecieverController current;
	
	// Update is called once per frame
	void Update () {
        UpdateReciever();
        line.SetPosition(0, Origin);
        line.SetPosition(1, End);
	}
    private void OnDrawGizmos() {
        if(!Application.isPlaying)
            UpdateReciever();
        Gizmos.DrawLine(Origin, End);
    }
    void UpdateReciever() {
        RaycastHit2D hit = Physics2D.Raycast(Origin, Direction);
        if (hit.transform != null) {
            LaserRecieverController comp = hit.transform.GetComponent<LaserRecieverController>();
            if (!Application.isPlaying)
                comp = null;
            if (current != null) {
                if (comp != current) {
                    current.Powered = false;
                    current = null;
                }
            }
            if (comp != null) {
                //Found a reciever
                if (comp.Color == Color) {
                    //Found a reciever of the same color as the sender
                    comp.Powered = true;
                    current = comp;
                }
            }
            Distance = hit.distance;
        } else {
            Distance = 1000f;
        }
    }
}
