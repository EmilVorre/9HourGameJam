using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilManager : MonoBehaviour
{
    public static UtilManager Instance;

    public List<SpriteUtilMapping> utilMappings = new();

    public Sprite table;

    [Serializable]
    public struct SpriteUtilMapping
    {
        public Sprite sprite;
        public Sprite requriedToHold;
        public Sprite whateverIsGivenBack;
        public AudioClip sound;
    }

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError("Duplicates");
        }
    }

    public void TryInteract(Vector3Int cellPos, Sprite sprite, PlayerMovement player)
    {
        if (sprite == table && player.holdingSpriteRenderer.sprite == utilMappings[^1].whateverIsGivenBack)
        {
            if (CustomerSpawner.customers.Count > 0)
            {
                Destroy(CustomerSpawner.customers[0]);
                CustomerSpawner.customers.RemoveAt(0);
            }

            if (CustomerSpawner.customers.Count > 0)
            {
                Destroy(CustomerSpawner.customers[0]);
                CustomerSpawner.customers.RemoveAt(0);
            }
            player.holdingSpriteRenderer.sprite = null;
        }

        Debug.Log($"{sprite} {cellPos}");

        foreach (var mapping in utilMappings){
            if (mapping.sprite == sprite 
            && mapping.requriedToHold == player.holdingSpriteRenderer.sprite) {
                player.audioSource.PlayOneShot(mapping.sound, 4);
                player.holdingSpriteRenderer.sprite = mapping.whateverIsGivenBack;
                return;
            }
        }
    }
}
