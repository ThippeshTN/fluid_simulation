using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidSimulator : MonoBehaviour
{

    [SerializeField]
    GameObject particlePrefab;


    public float gravity = 0;

    GameObject[] particles;
    public System.UInt32 numOfParticles;

    public Vector2 boundingBoxResolution;

    public float influenceRadius;



    // Start is called before the first frame update
    void Start()
    {
        particles = new GameObject[numOfParticles];

        for (int i = 0; i < numOfParticles; i++)
        {
            Vector3 position = new Vector3( Random.Range(-boundingBoxResolution.x/2, boundingBoxResolution.x/2),
                                            Random.Range(-boundingBoxResolution.y / 2, boundingBoxResolution.y / 2),  
                                            0);

            particles[i] = Instantiate(particlePrefab, this.transform);
            particles[i].GetComponent<Particle>().accelaration.y = -gravity;
            particles[i].GetComponent<Particle>().boundingBoxResolution = boundingBoxResolution;
            particles[i].transform.localPosition = position;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        for (int i = 0; i < numOfParticles; i++)
        {
            for(int j = 0; j < numOfParticles; j++)
            {
                if(i == j)
                {
                    continue;
                }
                else
                {

                    float distance = Vector3.Distance(particles[i].transform.localPosition, particles[j].transform.localPosition);

                    if (distance >= 0 && distance < influenceRadius)
                    {

                        float normalizedForce = 1 - distance / influenceRadius;
                        Vector3 directionVector =Vector3.Normalize(particles[i].transform.localPosition - particles[j].transform.localPosition);

                        if (Vector3.Distance(particles[i].transform.localPosition, particles[j].transform.localPosition) < influenceRadius)
                        {
                            particles[j].GetComponent<Particle>().velocity -= new Vector2(directionVector.x * normalizedForce,
                                                                                          directionVector.y * normalizedForce);
                        }
                    }
                }
            }
        }


    }
}
