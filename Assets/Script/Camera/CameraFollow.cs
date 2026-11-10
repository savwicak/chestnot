using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothness = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, targetPos, smoothness * Time.deltaTime);
    }
}
