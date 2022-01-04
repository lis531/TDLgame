using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public float health = 100.0f;

    public void DealDamage(float damage)
    { 
        health -= damage;
        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        
    }
}