using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum InputMode
{
    Normal,           // Regular gameplay (selecting objects, moving, etc.)
    BuildingMenu,     // Interacting with the building menu
}


public class InputManager : MonoBehaviour
{
    private RectTransform selectionBox;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isDragging = false;
    private InputMode inputMode = InputMode.Normal;
    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = this.gameObject.transform;
        selectionBox = canvas.Find("SelectionBox").GetComponent<RectTransform>();
        selectionBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inputMode = InputMode.BuildingMenu;
        }
        switch (inputMode)
        {
            case InputMode.Normal:
                NormalInput();
                break;
            case InputMode.BuildingMenu:
                /*to implement*/
                break;
            
        }
    }

    void NormalInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            endPos = Input.mousePosition;
            isDragging = true;
            
        }

        if (isDragging)
        {
            endPos = Input.mousePosition;
            UpdateSelectionBox();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            selectionBox.gameObject.SetActive(false);
            SelectObjectsWithinBox();
        }
    }

    void UpdateSelectionBox()
    {
        selectionBox.gameObject.SetActive(isDragging);
        
        float width = endPos.x - startPos.x;
        float height = endPos.y - startPos.y;
        
        selectionBox.sizeDelta = new Vector2(Math.Abs(width), Math.Abs(height));
        selectionBox.anchoredPosition = new Vector2(startPos.x, startPos.y) + new Vector2(width / 2, height / 2);
    }

    private void SelectObjectsWithinBox()
    {
        Rect selectionRect = new Rect(
            Math.Min(startPos.x, endPos.x),
            Math.Min(startPos.y, endPos.y),
            Math.Abs(endPos.x - startPos.x),
            Math.Abs(endPos.y - startPos.y)
        );
        if (selectionRect.width == 0 || selectionRect.height == 0)
        {
            SingleClick(startPos);
            return;
        }
            
        foreach (var selectable in FindObjectsOfType<Interactable>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(selectable.transform.position);
            if (selectionRect.Contains(screenPos, true))
            {
                Debug.Log(selectable.name);
            }
        }
    }

    private void SingleClick(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Interactable selectable = hit.collider.GetComponent<Interactable>();
            if (selectable)
            {
                Debug.Log(selectable);
            }
            else
            {
                
            }
        }
    }
}
