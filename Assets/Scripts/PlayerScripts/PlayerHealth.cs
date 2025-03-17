using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public float maxHealth = 10f;
    [SerializeField] public Slider healthSlider;
    public float currentHealth;

    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Debug.Log(currentHealth);
        // if (currentHealth <= 0) SceneManager.LoadScene("GameOverScene");
        if (currentHealth <= 0)
            StartCoroutine(LoadGameOverSceneAfterDelay(3f));
        healthSlider.value = currentHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    IEnumerator LoadGameOverSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOverScene");
    }
}
