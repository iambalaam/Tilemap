using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    InputAction m_MouseInputAction;

    [SerializeField]
    Tilemap m_Tilemap;

    [SerializeField]
    Transform m_Circle;
    
    Camera m_Camera;

    void Awake()
    {
        m_Camera = Camera.main;
    }

    void OnEnable()
    {
        m_MouseInputAction.Enable();
        m_MouseInputAction.performed += OnClickPerformed;
    }

    void OnDisable()
    {
        m_MouseInputAction.Disable();        
        m_MouseInputAction.performed -= OnClickPerformed;
    }

    void OnClickPerformed(InputAction.CallbackContext obj)
    {
        var point = (Vector2)m_Camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var collider = Physics2D.OverlapPoint(point);
        m_Circle.transform.position = point;
        
        if (collider != null)
        {
            var tPos = m_Tilemap.WorldToCell(point);
            m_Tilemap.SetTileFlags(tPos, TileFlags.None);
            m_Tilemap.SetColor(tPos, Color.yellow);
            m_Tilemap.SetTileFlags(tPos, TileFlags.LockAll);
        }
    }
}
