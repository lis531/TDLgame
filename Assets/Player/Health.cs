using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static float health;
    private Slider healthBar;
    void Start()
    {   
        health = 100;
        healthBar = GetComponent<Slider>();
        healthBar.value = health;
        
    }
    void Update()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
