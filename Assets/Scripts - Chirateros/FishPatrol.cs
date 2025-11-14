using UnityEngine;

public class FishPingPongHorizontal : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 1.5f;
    private float startX;
    private int direction = 1;
    private bool isFacingRight = true;

    void Start()
    {
        startX = transform.localPosition.x;
        speed = Random.Range(0.15f, 2.2f);
        distance = Random.Range(1f, 3f);
    }

    void Update()
    {
        transform.localPosition += new Vector3(direction * speed * Time.deltaTime, 0, 0);

        if (direction == 1 && transform.localPosition.x >= startX + distance)
        {
            direction = -1;
            FlipFish();
        }
        else if (direction == -1 && transform.localPosition.x <= startX - distance)
        {
            direction = 1;
            FlipFish();
        }
    }

    void FlipFish()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
