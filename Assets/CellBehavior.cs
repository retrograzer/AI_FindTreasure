using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellBehavior : MonoBehaviour
{
    private bool _isWall = false;
    private bool _isTreasure = false;
    private Image _rend;

    private void Awake()
    {
        _rend = GetComponent<Image>();
    }

    public void SetCellColor (Color cellColor)
    {
        _rend.color = cellColor;
    }

    public bool IsWall { get => _isWall; set { _isWall = value; } }

    public bool IsTreasure { get => _isTreasure; set { _isTreasure = value; } }
}
