using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableUI : EventTrigger
{
    private bool startDragging;
    // Start is called before the first frame update

    private Button _button;
    public Vector3 oldPosition;
    void Start()
    {
        _button = GetComponent<Button>();
        oldPosition = _button.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
            
        }
        if (Input.GetKeyDown("space"))
            {
                ResetPosition();
            }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        startDragging = true;
    }

    public void ResetPosition(){
        transform.position = oldPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        startDragging = false;
        ResetPosition();
    }
}
