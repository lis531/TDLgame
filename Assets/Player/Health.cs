using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static float health;
    private Slider healthBar;
    public Image LowHealth;
    float alpha;
    void Start()
    {   
        health = 100;
        healthBar = gameObject.GetComponent<Slider>();
    }
    void Update()
    {
        healthBar.value = health;
        alpha = health / 100;
        if (health <= 40)
        {
            Debug.Log("Low Health");
            LowHealth.color = new Color(1, 1, 1, 0.4f - alpha);
        }
        else if (health > 50)
        {
            LowHealth.color = new Color(1, 1, 1, 0);
        }
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}