using TMPro;
using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    public int cubeID;
    private TextMeshPro[] cubeTexts;
    public static int nextCubeNumber = 1;
    
    // Explosion effect settings
    public GameObject explosionPrefab; // Assign a particle system prefab
    public float explosionDuration = 0.5f;
    public Color explosionColor; // Color for the explosion effect
    private bool isExploding = false;
    
    // Audio settings
    public AudioClip wrongClickSound; // Assign your audio clip in Inspector
    public AudioClip correctClickSound; // Assign your audio clip for correct clicks
    private AudioSource audioSource;

    public int direction = 1; // Initialize with a default direction
    private int[] directions = { 1, 2, 3, 4 , 5, 6 };
    // Direction mapping: 1=left, 2=right, 3=back, 4=forward, 5=down, 6=up
    public static float cubeSpeed = 10f; // Default speed, can be changed by LevelManager

    void Start()
    {
        Debug.Log("Cube " + cubeID + " started with initial direction: " + direction);
        InitializeCubeTexts();
        cubeMovement();
        
        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void InitializeCubeTexts()
    {
        cubeTexts = GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro text in cubeTexts)
        {
            text.text = cubeID.ToString();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION WITH: " + collision.gameObject.name + " (Tag: " + collision.gameObject.tag + ")");
        
        /*if (collision.gameObject.CompareTag("Cube"))
        {
            cubeMovement();
        }*/
        if (collision.gameObject.CompareTag("rightWall"))
        {
            direction = 1; // Move left
        }
        else if (collision.gameObject.CompareTag("leftWall"))
        {
            direction = 2; // Move right
        }
        else if (collision.gameObject.CompareTag("frontWall"))
        {
            direction = 3; // Move back
        }
        else if (collision.gameObject.CompareTag("backWall"))
        {
            direction = 4; // Move forward
        }
        else if (collision.gameObject.CompareTag("upWall"))
        {
            direction = 5; // Move down
        }
        else if (collision.gameObject.CompareTag("Plane"))
        {
            direction = 6; // Move up
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("rightWall") ||
            collision.gameObject.CompareTag("leftWall") ||
            collision.gameObject.CompareTag("frontWall") ||
            collision.gameObject.CompareTag("backWall") ||
            collision.gameObject.CompareTag("upWall") ||
            collision.gameObject.CompareTag("Plane") ||
            collision.gameObject.CompareTag("Cube"))
        {
            cubeMovement(); // הגרלת כיוון חדש
        }
    }

    public void cubeMovement()
    {
        int randomDirection = directions[Random.Range(0, directions.Length)];
        while (randomDirection == direction)
        {
            randomDirection = directions[Random.Range(0, directions.Length)];
        }
        direction = randomDirection;    }


    void OnMouseDown()
    {
        if (isExploding) 
        {
            return; // Prevent multiple clicks during explosion
        }
        
        if (cubeID == nextCubeNumber && cubeID <= 33)
        {
            Debug.Log("Correct cube! " + cubeID);
            
            // Notify GameController that correct cube was clicked
            GameController gameController = FindFirstObjectByType<GameController>();
            if (gameController != null)
            {
                gameController.OnCubeClicked();
            }
            gameController.score++;
            gameController.scoreText.text = "Score: " + gameController.score;
            nextCubeNumber++;
            
            // Start explosion animation instead of immediately destroying
            StartCoroutine(ExplodeAndDestroy());
        }
        else
        {
            Debug.Log("Wrong cube! Try again.");
            // Optional: Add wrong click effect here (shake, red flash, etc.)
            StartCoroutine(WrongClickEffect());
        }
    }

    void Update()
    {
        if (!isExploding)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {                
                switch (direction)
                {
                    case 1:
                        rb.linearVelocity = Vector3.left * cubeSpeed;
                        break;
                    case 2:
                        rb.linearVelocity = Vector3.right * cubeSpeed;
                        break;
                    case 3:
                        rb.linearVelocity = Vector3.back * cubeSpeed;
                        break;
                    case 4:
                        rb.linearVelocity = Vector3.forward * cubeSpeed;
                        break;
                    case 5:
                        rb.linearVelocity = Vector3.down * cubeSpeed;
                        break;
                    case 6:
                        rb.linearVelocity = Vector3.up * cubeSpeed;
                        break;
                }
                
                // Use linearVelocity for Unity 6.0 - this respects physics and collisions
                rb.linearVelocity = rb.linearVelocity.normalized * cubeSpeed;
            }
            else
            {
                Debug.LogError("Cube " + cubeID + " has no Rigidbody component!");
            }
        }
        else
        {
            Debug.Log("Cube " + cubeID + " is exploding, not moving");
        }
    }
    
    private IEnumerator ExplodeAndDestroy()
    {
        isExploding = true;
        
        // Play correct click sound
        if (audioSource != null && correctClickSound != null)
        {
            audioSource.PlayOneShot(correctClickSound);
        }
        
        // Create explosion particle effect if prefab is assigned
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            // Apply the explosion color to the particle system
            ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startColor = explosionColor;
                
                // Also try to set the material color if it exists
                var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = explosionColor;
                }
                
                Debug.Log("Applied explosion color: " + explosionColor + " to particle system");
            }
            else
            {
                Debug.LogWarning("No ParticleSystem component found on explosion prefab!");
            }
            
            Destroy(explosion, 2f); // Clean up explosion after 2 seconds
        }
        
        // Scale up animation (explosion effect)
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 2f;
        float elapsedTime = 0f;
        
        // Expand animation
        while (elapsedTime < explosionDuration * 0.3f)
        {
            float progress = elapsedTime / (explosionDuration * 0.3f);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Make cube invisible but keep explosion going
        Renderer cubeRenderer = GetComponent<Renderer>();
        if (cubeRenderer != null)
        {
            cubeRenderer.enabled = false;
        }
        
        // Hide text
        foreach (TextMeshPro text in cubeTexts)
        {
            text.enabled = false;
        }
        
        // Wait for explosion to finish
        yield return new WaitForSeconds(explosionDuration * 0.7f);
        
        // Destroy the cube
        Destroy(gameObject);
    }
    
    private IEnumerator WrongClickEffect()
    {
        // Play wrong click sound
        if (audioSource != null && wrongClickSound != null)
        {
            audioSource.PlayOneShot(wrongClickSound);
        }
        
        // Flash red effect for wrong clicks
        Renderer cubeRenderer = GetComponent<Renderer>();
        if (cubeRenderer != null)
        {
            Color originalColor = cubeRenderer.material.color;
            
            // Flash red
            cubeRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            cubeRenderer.material.color = originalColor;
            yield return new WaitForSeconds(0.1f);
            cubeRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            cubeRenderer.material.color = originalColor;
        }
    }

}
