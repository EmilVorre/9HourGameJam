using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilManager : MonoBehaviour
{
    public static UtilManager Instance;

    Dictionary<Vector3Int, bool> cellActivities = new();

    public struct SpriteUtilMapping
    {
        public Sprite sprite;
        public GameObject util;
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

    public void TryInteract(Vector3Int cellPos, Sprite sprite, bool isGhost)
    {
        if (cellActivities.TryGetValue(cellPos, out bool active) == false)
        {

        }
        if (active == false)
        {

        }
    }
}
