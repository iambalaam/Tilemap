using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PixelPerfectOutline : MonoBehaviour
{
    private const int PPU = 32;
    private Vector3[]_directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right } ;
    public bool isBackground = false;

    private void Start()
    {
        // Stop ∞ recursion
        if (isBackground) return;

        foreach (var dir in _directions)
        {
            var copy = Instantiate(gameObject, transform.parent);
            var ppo = copy.GetComponent<PixelPerfectOutline>();
            var map = copy.GetComponent<Tilemap>();
            ppo.isBackground = true;

            copy.name = "Outline";
            copy.transform.SetAsFirstSibling();
            copy.transform.position += dir / PPU + Vector3.back;
            map.color = Color.black;

        }
    }
}
