using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRecieverController : MonoBehaviour {

    public PowerColor Color;

    private void Update() {
        if(Powered) {
            PowerHolder.SetState(Color, true);
            if (!PowerHolder.IsOn(Color))
                throw new System.Exception();
        }
    }

    [SerializeField]
    private bool _powered;
    public bool Powered {
        get {
            return _powered;
        }
        set {
            _powered = value;
            if (value == false) {
                PowerHolder.SetState(Color, false);
            }
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
