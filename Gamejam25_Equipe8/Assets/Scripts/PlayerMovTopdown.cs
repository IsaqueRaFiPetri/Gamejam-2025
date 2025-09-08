using UnityEngine;

public class PlayerMovTopdown : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed;

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        transform.position = transform.position + movement * speed * Time.deltaTime;
    }
}
//https://www.youtube.com/watch?v=P8GF_clL1Y0