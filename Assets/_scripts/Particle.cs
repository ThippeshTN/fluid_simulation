using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector2 velocity = Vector2.zero;
    public Vector2 accelaration = Vector2.zero;

    public Vector2 boundingBoxResolution;

    [Range(0f, 1f)]
    public float collisionDamping;

    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = Vector3.one * radius;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += accelaration * Time.deltaTime;
        Vector2 distance = (velocity * Time.deltaTime) + ( accelaration * Time.deltaTime * Time.deltaTime) / 2.0F;
        this.transform.localPosition += new Vector3(distance.x, distance.y, 0);

        if(Mathf.Abs(this.transform.localPosition.x) >= boundingBoxResolution.x / 2.0F)
        {
            velocity.x = -velocity.x * collisionDamping;
        }

        if (Mathf.Abs(this.transform.localPosition.y) >= boundingBoxResolution.y / 2.0F)
        {
            velocity.y = -velocity.y * collisionDamping;
        }
    }
}
