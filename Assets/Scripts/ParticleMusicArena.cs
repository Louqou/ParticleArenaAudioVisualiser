using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Visualiser))]
public class ParticleMusicArena : MonoBehaviour
{
    public float pushSpeed = 10f;
    public int size = 40;
    public int edgeSize = 4;

    private int numParticles;
    private Visualiser visualiser;
    private ParticleSystem.Particle[] particles;
    private new ParticleSystem particleSystem;

    private void Start()
    {
        Cursor.visible = false;
        visualiser = GetComponent<Visualiser>();
        particleSystem = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule particleSystemMain = particleSystem.main;
        numParticles = size * size;
        particleSystemMain.maxParticles = numParticles;

        particles = new ParticleSystem.Particle[numParticles];
        particleSystem.Emit(numParticles);
        particleSystem.GetParticles(particles);

        // Lets assume particle x y dimension is 1x1
        for (int i = 0; i < numParticles; i++) {
            if (ParticleOnEdge(i)) {
                particles[i].position = new Vector3(i % size, i / size);
            }
            else {
                particles[i].position = new Vector3(i % size, i / size, -2);
            }
        }

        particleSystem.SetParticles(particles, numParticles);
    }

    private void LateUpdate()
    {
        float[] spectrum = visualiser.spectrum;

        for (int i = 0; i < numParticles; i++) {
            if (ParticleOnEdge(i)) {
                Vector3 particlePos = particles[i].position;

                float newParticleZ =
                      spectrum[i % size]
                    + spectrum[i / size]
                    + spectrum[(numParticles - i) % size]
                    + spectrum[(numParticles - i) / size];

                newParticleZ = Mathf.Clamp(newParticleZ * 15f, 0, 10);

                if (newParticleZ < particlePos.z) {
                    newParticleZ = particlePos.z - pushSpeed * Time.deltaTime;
                }

                particles[i].position = new Vector3(particlePos.x, particlePos.y, newParticleZ);
            }
        }

        particleSystem.SetParticles(particles, particles.Length);
    }

    private bool ParticleOnEdge(int index)
    {
        return index < edgeSize * size
            || index > numParticles - edgeSize * size
            || index % size < edgeSize
            || index % size >= size - edgeSize;
    }
}
