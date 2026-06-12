using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        var MoveVec = playerInput.actions["Move"].ReadValue<Vector2>();
        var MoveVec3D = new Vector3(MoveVec.x*speed,0,MoveVec.y*speed);
        transform.position = transform.position + MoveVec3D * Time.deltaTime;
        transform.position = transform.position + MoveVec3D * Time.deltaTime;
        Debug.Log(MoveVec);
    }
}
