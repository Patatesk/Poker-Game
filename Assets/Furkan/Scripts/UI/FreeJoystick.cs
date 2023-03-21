using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PK.PokerGame
{
    public class FreeJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject joystick;
        [SerializeField] private GameObject backGround;
        public void OnPointerDown(PointerEventData eventData)
        {
            joystick.transform.position = eventData.pressPosition;
            backGround.transform.position = eventData.pressPosition;

            joystick.SetActive(true);
            backGround.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joystick.SetActive(false);
            backGround.SetActive(false);
        }
    }
}
