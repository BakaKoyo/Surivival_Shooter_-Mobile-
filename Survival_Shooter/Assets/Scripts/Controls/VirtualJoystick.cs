using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    Image bgImg;

    [SerializeField]
    Image joyStickImg;

    public Vector2 InputVector { get; private set; }
    float joystickAnchoredValue = 3;
    float joystickEdgeValue;


    void Start()
    {
        /*
        Set this once here to reuse in our OnDrag Method so we don't have to keep doing a division.
        This assumes our joystick bg is a square (sizedelta.x is the same as sizedelta.y)
        */
        joystickEdgeValue = bgImg.rectTransform.sizeDelta.x / joystickAnchoredValue;
    }



    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        /* We are going to check the event data passed through rectTransfromUtility to set our pos Vector */
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,
                                                                    eventData.position,
                                                                    eventData.pressEventCamera,
                                                                    out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            /* This will now get us a vlaue of -1 to 1 */
            InputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);

            /* Make sure it's normalized if needed */
            InputVector = InputVector.magnitude > 1.0f ? InputVector.normalized : InputVector;

            /* Move Joystick img */
            joyStickImg.rectTransform.anchoredPosition = new Vector2(InputVector.x * (joystickEdgeValue),
                                                                     InputVector.y * (joystickEdgeValue));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector2.zero;
        joyStickImg.rectTransform.anchoredPosition = Vector2.zero;
    }

}
