using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public PowerColor Color;

    private bool White, Black;

    public bool isStanding = false;

    private Dictionary<int, bool> objects = new Dictionary<int, bool>();

    protected int ObjectCount { get { return objects.Count; } }

    private bool IsValid(GameObject gameObject) {
        return gameObject.name.Contains("Player") || gameObject.name.Contains("MovableBlock");
    }

    private void Update() {
        if (isStanding)
            On();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (IsValid(collision.gameObject)) {
            On();
            isStanding = true;
            objects.Add(collision.gameObject.GetInstanceID(), true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (IsValid(collision.gameObject)) {
            objects.Remove(collision.gameObject.GetInstanceID());
            isStanding = false;
            Off();
        }
    }

    protected virtual void On() {
        PowerHolder.SetState(Color, true);
    }
    protected virtual void Off() {
        Debug.Log(ObjectCount);
        if (ObjectCount == 0) {
            PowerHolder.SetState(Color, false);
        }
    }

    private void OnDrawGizmos() {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Gray")) {
            
            if (obj.name.Contains("Door") && obj.name.Contains(PowerColorHelper.GetCamelCaseName(Color))) {
                Gizmos.DrawLine(transform.position, obj.transform.position);
            }
        }
    }
}
