using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class PlayerFightStarter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer circle;
        private bool canFight;
        private GameObject otherObj;
        private GameObject thisObj;
        public bool fightStarted;
        private void Awake()
        {
            thisObj = transform.root.gameObject;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                circle.color = Color.red;
                otherObj = other.gameObject;
                canFight = true;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && canFight && !fightStarted)
            {
                StartFight();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagContainer.AITag))
            {
                circle.color = Color.white;
                ResetFight();
            }
        }

        public void ResetFight()
        {
            canFight = false;
            otherObj = null;
        }

        private void StartFight()
        {
            if (otherObj == null || thisObj == null) return;
            fightStarted = true;
            gameObject.GetComponent<Collider>().enabled = false;
            AI ai = otherObj.GetComponentInParent<AI>();
            ai.ForceToFight();
            otherObj.gameObject.layer = 9;
            thisObj.gameObject.layer = 9;
            StartFightSignal.Trigger(transform.root.GetComponent<Player>(), ai);
        }
    }
}
