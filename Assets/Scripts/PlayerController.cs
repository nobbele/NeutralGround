using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayableColor
{
    Black, White
}

public class PlayerController : MonoBehaviour {

    public PlayableColor PlayerColor;

    KeyCode jump;

    Rigidbody2D rb;
    AudioSource source;

    public AudioClip jumpClip;

    const int Speed = 5;
    const int JumpForce = 8;
    const float gravityScale = 0.85f;

    bool canJump = false;

    public int DeathZone = -2;

    bool facingright = true;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        if (PlayerColor == PlayableColor.White) {
            jump = KeyCode.UpArrow;
        } else {
            jump = KeyCode.W;
        }
        rb.gravityScale = (1f + (-Physics2D.gravity.y / JumpForce)) * gravityScale;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(jump) && canJump) {
            canJump = false;
            rb.velocity = Vector2.up * JumpForce;
            source.PlayOneShot(jumpClip);
        }

        float movement;
        if (PlayerColor == PlayableColor.Black)
            movement = Input.GetAxis("HorizontalBlack");
        else
            movement = Input.GetAxis("HorizontalWhite");
        if (movement > 0 && !facingright) {
                Flip();
        } else if (movement < 0 && facingright) {
            Flip();
        }
        rb.velocity = new Vector2(movement * Speed, rb.velocity.y);

        if (transform.position.y < DeathZone) {
            GameOver();
        }
    }

    private bool Dangerous(string tag, string name) {
        return tag != "Gray" && tag != System.Enum.GetName(typeof(PlayableColor), PlayerColor) && !name.Contains("Player");
    }

    private void CheckDeath(Collision2D collision) {
        if (Dangerous(collision.transform.tag, collision.transform.name))
            GameOver();
    }
    Vector2 CheckSize;
    private void CheckJump() {
        canJump = false;
        PhysicsMaterial2D material = rb.sharedMaterial;
        material.friction = 0;
        CheckSize = new Vector2(0.98f, 0.5f);
        Vector2 groundCheck = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] collisions = Physics2D.OverlapBoxAll(new Vector2(groundCheck.x, groundCheck.y - 0.275f), CheckSize, 0);
        foreach (var collision in collisions) {
            if (collision.transform != null)
                if (collision.name != transform.name) {
                    if (Dangerous(collision.tag, collision.name))
                        GameOver();
                    canJump = true;
                    if(!collision.name.Contains("Player") && !collision.name.Contains("Movable"))
                        material.friction = 10;
                }
        }
        rb.sharedMaterial = material;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 0.275f), CheckSize);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Dangerous(collision.gameObject.tag, collision.gameObject.name)) {
            GameOver();
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        CheckJump();
    }

    private void GameOver() {
        ResetLevel();
    }

    public static void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Flip() {
        facingright = !facingright;
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}
