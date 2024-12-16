using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VerticalGrid : LayoutGroup
{
    [SerializeField] LayoutElement _parentLE;
    [SerializeField] RectTransform _parentRT;

    [SerializeField] int _cols, _cellHeight, _offsetActiveFalse;
    int _rows;
    Vector2 _cellSize;
    [SerializeField] Vector2 _spacing;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if ((transform.childCount - _offsetActiveFalse) % _cols != 0) _rows = Mathf.CeilToInt(((transform.childCount - _offsetActiveFalse) / _cols) + 1);
        else _rows = Mathf.CeilToInt((transform.childCount - _offsetActiveFalse) / _cols);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / _cols) - (_spacing.x / _cols * 2) - (padding.left / (float)_cols) - (padding.right / (float)_cols);
        float cellHeight = (parentHeight / _rows) - (_spacing.y / _rows * 2) - (padding.top / (float)_rows) - (padding.bottom / (float)_rows);

        _cellSize.x = cellWidth;
        _cellSize.y = cellHeight;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / _cols;
            columnCount = i % _cols;
            var item = rectChildren[i];

            var xPos = (_cellSize.x * columnCount) + (_spacing.x * columnCount) + padding.left;
            var yPos = (_cellSize.y * rowCount) + (_spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, _cellSize.x);
            SetChildAlongAxis(item, 1, yPos, _cellSize.y);
        }

        if (_parentLE != null)
        {
            _parentLE.preferredHeight = _cellHeight * _rows;
        }

        if (_parentRT != null)
        {
            _parentRT.sizeDelta = new Vector2(_parentRT.sizeDelta.x, _cellHeight * _rows);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
