using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IClickable
{
    public int maxHealth;
    public int health;
    public bool isInvinsible = false;
    private HealthBar healthBar;
    private void Awake() {
        healthBar = GetComponent<HealthBar>();
    }
    private void Start() {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage){
        health -= damage;
        healthBar.SetHealth(health);
    }
    public void OnClick()
    {
        Interact();
    }
    void Interact()
    {
        Debug.Log($"You clicked on the wall with health: {health}.");
    }
    //To do: display health bar
}