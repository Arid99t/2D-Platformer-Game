using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCol;

    //collider variables
    private Vector2 boxCollnitSize;
    private Vector2 boxCollnitOffset;

    private void Start()
    {
        //initial collider properties
        boxCollnitSize = boxCol.size;
        boxCollnitOffset = boxCol.offset;
    }
    
    private void Update()
    {
        //horizontal
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));

        //vertical
        float verticalInput = Input.GetAxisRaw("Vertical");
        PlayJumpAnimation(verticalInput);

        if(Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }

        Vector3 scale = transform.localScale;
        if (speed < 0)
        {           
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    public void Crouch(bool crouch)
    {
        if( crouch == true)
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

    public void PlayJumpAnimation(float vertical)
    {
        if (vertical > 0)
        {
            animator.SetTrigger("Jump");
        }
    }
}
