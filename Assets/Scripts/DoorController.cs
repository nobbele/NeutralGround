using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public PowerColor Color;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private BoxCollider2D collider;
    private Renderer renderer;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    public bool StateOnPowered = true;

    public bool isOn;

    private void Start() {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        isOn = PowerHolder.IsOn(Color) ? StateOnPowered : !StateOnPowered;
        collider.enabled = !isOn;
        UnityEngine.Color color = renderer.material.color;
        color.a = collider.enabled ? 1f : .5f;
        renderer.material.color = color;
	}
}
