using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PK.PokerGame
{
    public class FreeJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject joystick;
        public void OnPointerDown(PointerEventData eventData)
        {
            joystick.transform.position = eventData.pressPosition;

            joystick.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joystick.SetActive(false);
        }
    }
}
