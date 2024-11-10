using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilManager : MonoBehaviour
{
    public static UtilManager Instance;

    public List<SpriteUtilMapping> utilMappings = new();

    [Serializable]
    public struct SpriteUtilMapping
    {
        public Sprite sprite;
        public Sprite requriedToHold;
        public Sprite whateverIsGivenBack;
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
        Debug.Log($"{sprite} {cellPos}");

        foreach (var mapping in utilMappings){
            if (mapping.sprite == sprite 
            && mapping.requriedToHold == player.holdingSpriteRenderer.sprite){
                player.holdingSpriteRenderer.sprite = mapping.whateverIsGivenBack;
                return;
            }

        }
    }
}
