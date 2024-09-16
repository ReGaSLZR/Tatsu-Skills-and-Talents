using UnityEngine;

public class PlayableCharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody2D;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Space]

    [SerializeField]
    private string animParamMoving;

    [Space]

    [SerializeField]
    private KeyCode keyUp = KeyCode.UpArrow;

    [SerializeField]
    private KeyCode keyDown = KeyCode.DownArrow;

    [SerializeField]
    private KeyCode keyLeft = KeyCode.LeftArrow;

    [SerializeField]
    private KeyCode keyRight = KeyCode.RightArrow;

    private void MovePlayerCharacter(Vector2 direction)
    {
        //TODO ren normalize and make rigidbody movement smoother
        rigidBody2D.velocity = (direction *
            PlayableCharacterSingleton.Instance.StatsHolder.Stats.walkSpeed);
    }

    private void FixedUpdate()
    {
        var isMoveKeyPressed = false;

        if (Input.GetKey(keyUp))
        {
            MovePlayerCharacter(Vector2.up);
            isMoveKeyPressed = true;
        }
        else if (Input.GetKey(keyDown))
        {
            MovePlayerCharacter(Vector2.down);
            isMoveKeyPressed = true;
        }
        else if (Input.GetKey(keyLeft))
        {
            MovePlayerCharacter(Vector2.left);
            spriteRenderer.flipX = true;
            isMoveKeyPressed = true;
        }
        else if (Input.GetKey(keyRight))
        {
            MovePlayerCharacter(Vector2.right);
            spriteRenderer.flipX = false;
            isMoveKeyPressed = true;
        }

        animator.SetBool(animParamMoving, isMoveKeyPressed);

        if (!isMoveKeyPressed)
        {
            rigidBody2D.velocity = Vector2.zero;
        }
    }

}