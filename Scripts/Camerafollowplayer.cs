using UnityEngine;

public class Camerafollowplayer : MonoBehaviour
{
    public Transform player;
    public Rigidbody Playerrigidbody;
    public Vector3 offset;
    public float speed;

    private void Start()
    {
        Playerrigidbody = player.GetComponent<Rigidbody>();

    }



    private void LateUpdate()
    {
        Vector3 playerforword = (Playerrigidbody.linearVelocity + player.transform.forward).normalized;
        transform.position = Vector3.Lerp(transform.position, player.position + player.transform.TransformVector(offset) + playerforword * (-5f), speed * Time.deltaTime);
        transform.LookAt(player);
    }
   
}
