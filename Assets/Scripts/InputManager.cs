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
        // Debug.Log();
        var ray = m_Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var hit = Physics2D.Raycast(ray.origin, ray.direction);
    
        if (hit.collider != null)
        {
            //test
            var tPos = m_Tilemap.WorldToCell(hit.point);
            var t = m_Tilemap.GetTile(tPos);
            m_Tilemap.SetTileFlags(tPos, TileFlags.None);
            m_Circle.transform.position = hit.point;
            m_Tilemap.SetColor(tPos, Color.yellow);
            m_Tilemap.SetTileFlags(tPos, TileFlags.LockAll);

        }
    }
}
