using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] Transform cam, player;
    [SerializeField] Joystick joystickMove, joystickRotate;
    [SerializeField] float speed = 5f, rotateSpeed = 0.2f;
    private float x, z, rotateH;
    private Vector3 move;

    private void FixedUpdate()
    {
        Moving();
        Rotate();
    }
    void Moving()
    {   /*Karakterin saða-sola, yukarý-aþaðý hareketi saðlanýr.*/
        x = joystickMove.Horizontal + Input.GetAxis("Horizontal");
        z = joystickMove.Vertical + Input.GetAxis("Vertical");
        move = player.right * x + player.forward * z;
        player.Translate(speed * Time.deltaTime * move, Space.World);
    }
    void Rotate()
    {   /*Karakterin saða-sola döndürülmesi saðlanýr.*/
        rotateH = joystickRotate.Horizontal * rotateSpeed;
        player.Rotate(0, rotateH, 0);
    }
}
