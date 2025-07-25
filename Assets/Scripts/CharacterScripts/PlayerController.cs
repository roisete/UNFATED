using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    public Vector2 direction = Vector2.zero;
    private Vector2 posChar = Vector2.zero;
    private Rigidbody2D rb2d;
    public Animator animator;

    public static PlayerController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        posChar = rb2d.transform.position;
        if (direction.y < 0 && direction.x == 0)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", false);
        }
        else if (direction.y > 0 && direction.x == 0)
        {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", false);
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", true);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", false);
        }
        else if ((direction.x < 0 && direction.y < 0) || (direction.x < 0 && direction.y == 0))
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", true);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", false);
        }
        else if (direction.x > 0 && direction.y > 0) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", true);
            animator.SetBool("RightDown", false);
        }
        else if ((direction.x > 0 && direction.y < 0)  || (direction.x > 0 && direction.y == 0))
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", true);
        }
        else if (direction.x > 0 && direction.y == 0) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", true);
        }
        else if (direction.x < 0 && direction.y == 0) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", true);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", false);
        }
        else
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("LeftUp", false);
            animator.SetBool("LeftDown", false);
            animator.SetBool("RightUp", false);
            animator.SetBool("RightDown", false);
        }
    }
    private void FixedUpdate()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        rb2d.velocity = direction * speed;
    }
}
