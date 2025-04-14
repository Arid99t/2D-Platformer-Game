using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCol;
    public float speed;
    public float jump;
    private Rigidbody2D rb2d;

    //collider variables
    private Vector2 boxCollnitSize;
    private Vector2 boxCollnitOffset;
    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //initial collider properties
        boxCollnitSize = boxCol.size;
        boxCollnitOffset = boxCol.offset;
    }   
    private void Update()
    {
        //horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        
        
        PlayerMovementAnimationHorizontal(horizontal);
        PlayerMovementAnimationVertical(vertical);
        MoveCharacter(horizontal, vertical);

        //crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }
    private void MoveCharacter(float horizontal, float vertical)
    {
        //move horizontally
        Vector2 position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;

        //move vertically
        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
        }
    }
    private void PlayerMovementAnimationHorizontal(float horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
        
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        
    }
    public void PlayerMovementAnimationVertical(float vertical)
    {
        
        if (vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else 
        {
            animator.SetBool("Jump", false);
        }
    }
    public void Crouch(bool crouch)
    {
        if (crouch == true)
        {
            float offX = 0.03022841f;
            float offY = 1.061377f; //fix it

            float sizeX = 0.8104871f;
            float sizeY = 2.380636f;  // fix it

            boxCol.size = new Vector2(sizeX, sizeY);
            boxCol.offset = new Vector2(offX, offY);
        }
        else
        {
            //reset collider
            boxCol.size = boxCollnitSize;
            boxCol.offset = boxCollnitOffset;
        }

        //play animation
        animator.SetBool("Crouch", crouch);
    }
}
