using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

[System.Serializable]
public class Item
{
    public string name;
    public int quantity;

    public Item(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }
}

public class ItemPickup : MonoBehaviour
{
    public string itemName; // O nome do item
    public int quantity = 1; // A quantidade que o jogador coletará deste item

    // Quando o jogador interage com este objeto
    public void Interact(PlayerController player)
    {
        // Cria um novo item
        Item newItem = new Item(itemName, quantity);

        // Adiciona o item ao inventário do jogador
        player.AddItemToInventory(newItem);

        // Destrua o objeto de item na cena
        Destroy(gameObject);
    }
}

public class PlayerController : MonoBehaviour
{
    public float HP = 100f;
    public float MP = 100f;
    public float STA = 100f;
    public float ATK = 50f;
    public float DEF = 20f;

    public float healthPotionValue = 50f; // Valor de recuperação do HealthPotion
    public float manaPotionValue = 50f; // Valor de recuperação do ManaPotion
    public float staminaPotionValue = 50f; // Valor de recuperação do StaminaPotion

    public Item[] inventorySlots = new Item[10]; // Supondo que temos 10 slots

    void Update()
    {
        // Detecta se as teclas foram pressionadas
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem("HealthPotion");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem("ManaPotion");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem("StaminaPotion");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (HP <= 0)
        {
            GameOver();
        }
    }

    public void AddItemToInventory(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null || inventorySlots[i].name == item.name)
            {
                if (inventorySlots[i] == null)
                    inventorySlots[i] = new Item(item.name, item.quantity);
                else
                    inventorySlots[i].quantity += item.quantity;
                break;
            }
        }
    }

    public void UseItem(string itemName)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] != null && inventorySlots[i].name == itemName)
            {
                // Aqui você pode adicionar a lógica para usar o item
                if (itemName == "HealthPotion")
                {
                    HP = Mathf.Min(HP + healthPotionValue, 100f);
                }
                else if (itemName == "ManaPotion")
                {
                    MP = Mathf.Min(MP + manaPotionValue, 100f);
                }
                else if (itemName == "StaminaPotion")
                {
                    STA = Mathf.Min(STA + staminaPotionValue, 100f);
                }

                inventorySlots[i].quantity--;

                if (inventorySlots[i].quantity <= 0)
                {
                    inventorySlots[i] = null; // Remove o item do inventário quando a quantidade chegar a 0
                }

                break;
            }
        }
    }

    public void Interact()
    {
        // Crie um raycast para detectar objetos na frente do jogador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
        {
            // Se o objeto tiver um script ItemPickup, interaja com ele
            ItemPickup itemPickup = hit.transform.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                itemPickup.Interact(this);
            }
        }
    }



    // Função para calcular dano
    public float CalculateDamage(float enemyATK, float playerDEF)
    {
        float damage = enemyATK - playerDEF;
        if (damage < 0) damage = 0;
        return damage;
    }
    public void ReloadGame()
    {
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        // Sai do jogo
        Application.Quit();
    }
    void GameOver()
    {
        // Ativa a tela de Game Over
        gameOverScreen.SetActive(true);
    }

}
