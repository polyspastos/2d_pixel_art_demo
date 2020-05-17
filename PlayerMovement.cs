using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject panel;
    public Vector2 lastMove;

    public float attackingTime;
    public string startPoint;

    private Animator anim;
    private Rigidbody2D myBody;
    private bool playerMoving;
    private static bool playerExist;
    private bool attacking;
    private float attackTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();

        if (!playerExist)
        {
            playerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.position = PixelPerfectClamp(pos, 16.0f);



        playerMoving = false;

        if (!attacking)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                myBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myBody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
                lastMove = PixelPerfectClamp(lastMove, 16.0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
                lastMove = PixelPerfectClamp(lastMove, 16.0f);
            }
            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f) //freeze
            {
                myBody.velocity = new Vector2(0, myBody.velocity.y);
                //lastMove = PixelPerfectClamp(lastMove, 16.0f);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f) //freeze
            {
                myBody.velocity = new Vector2(myBody.velocity.x, 0f);
                //lastMove = PixelPerfectClamp(lastMove, 16.0f);
            }
        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("PlayerAttacking", false);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }

    private Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit)
    {
        //Debug.Log(lastMove);

        if ((lastMove.x == -1 && lastMove.y == 0) || (lastMove.x == 0 && lastMove.y == -1))
        {
            Vector2 vectorInPixels = new Vector2(
                Mathf.Ceil(moveVector.x * pixelsPerUnit), // bal le
                Mathf.Ceil(moveVector.y * pixelsPerUnit));
            return vectorInPixels / pixelsPerUnit;
        }
        else
        {
            Vector2 vectorInPixels = new Vector2(
                Mathf.FloorToInt(moveVector.x * pixelsPerUnit), // jobb fol
                Mathf.FloorToInt(moveVector.y * pixelsPerUnit));
            return vectorInPixels / pixelsPerUnit;
        }


        //Debug.Log("Clamp X: " + vectorInPixels.x + " into " + vectorInPixels.x / pixelsPerUnit);
        //Debug.Log("Clamp X: " + vectorInPixels.y + " into " + vectorInPixels.y / pixelsPerUnit);
        //return vectorInPixels / pixelsPerUnit;
    }

}
