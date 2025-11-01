using UnityEngine;

public class SimpleExplosion : MonoBehaviour
{
    void Start()
    {
        // Get particle system component
        ParticleSystem particles = GetComponent<ParticleSystem>();
        
        if (particles != null)
        {
            // Configure explosion particles
            var main = particles.main;
            main.startLifetime = 1f;
            main.startSpeed = 5f;
            main.startSize = 0.1f;
            main.startColor = Color.yellow;
            main.maxParticles = 50;
            
            var emission = particles.emission;
            emission.SetBursts(new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0.0f, 50)
            });
            
            var shape = particles.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 0.1f;
            
            var velocityOverLifetime = particles.velocityOverLifetime;
            velocityOverLifetime.enabled = true;
            velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;
            velocityOverLifetime.radial = new ParticleSystem.MinMaxCurve(2f);
            
            // Play the explosion
            particles.Play();
        }
        
        // Auto-destroy this GameObject after 2 seconds
        Destroy(gameObject, 2f);
    }
}
