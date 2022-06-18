using System;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Animator _animator;
    public bool IsDead { get; private set; }
    public float CurrentHealth { get; private set; }

    [SerializeField] private Behaviour[] _components;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        CurrentHealth = startingHealth;
    }

    public void TakeDamege(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            FindObjectOfType<AudioManager>().Play("takeDamage");
            _animator.SetTrigger("takeDamage");
        }
        else
        {
            if (!IsDead)
            {
                _animator.SetTrigger("die");

                foreach (Behaviour component in _components)
                {
                    component.enabled = false;
                }

                if (gameObject.CompareTag("Monster"))
                {
                    FindObjectOfType<AudioManager>().Play("takeDamage");
                    FindObjectOfType<AudioManager>().Play("monsterDie");
                }

                if (gameObject.CompareTag("Human"))
                {
                    FindObjectOfType<AudioManager>().Play("takeDamage");
                    FindObjectOfType<AudioManager>().Play("maleHurt");
                }
                
                if (gameObject.CompareTag("Boss1"))
                {
                    FindObjectOfType<AudioManager>().Play("bossDie");
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().LoadMap2();
                }
                
                if (gameObject.CompareTag("Boss2"))
                {
                    FindObjectOfType<AudioManager>().Play("bossDie");
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().LoadEndingScreen();
                }
                
                if (gameObject.CompareTag("Player"))
                {
                    FindObjectOfType<AudioManager>().Play("takeDamage");
                    FindObjectOfType<AudioManager>().Play("maleHurt");
                    GameObject.FindWithTag("GameController").GetComponent<GameController>().GameOver();
                }
                
                IsDead = true;
            }
        }
    }

    public void AddHealth(float health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, startingHealth);
    }
}
