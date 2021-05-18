using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    MeshRenderer mr;
    float pickedUpTime, respawnTime, speedTime, effectTime, originalSpeed;

    PlayerHealth health;
    PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        respawnTime = 5f;
        effectTime = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y + 1f, transform.rotation.z);
        if (Time.time - pickedUpTime >= respawnTime)
        {
            mr.enabled = true;
        }
        if (movement && (Time.time - speedTime >= effectTime))
        {
            movement.speed = originalSpeed;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerPowerUpHitBox"))
        {
            mr.enabled = false;
            pickedUpTime = Time.time;

            Transform parent = collision.gameObject.transform.parent;

            if (name == "HealthPowerUp")
            {
                health = parent.gameObject.GetComponent<PlayerHealth>(); 
                health.currentHealth += 30;
                if(health.currentHealth >= health.startingHealth)
                {
                    health.currentHealth = health.startingHealth;
                }
                health.healthSlider.value = health.currentHealth;
            }
            else if (name == "MovementPowerUp")
            {
                movement = parent.gameObject.GetComponent<PlayerMovement>();
                originalSpeed = movement.speed;
                movement.speed += 6;
                speedTime = Time.time;
            }
        }
    }
}
