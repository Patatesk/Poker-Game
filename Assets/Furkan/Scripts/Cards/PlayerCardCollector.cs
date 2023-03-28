using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class PlayerCardCollector : MonoBehaviour
    {
        [SerializeField] private HandManager handManager;
        [SerializeField] private MMF_Player collectFeedBack;
        private MMF_Scale scale;
        private MMF_Position position;
        private MMF_Events events;


        private void Awake()
        {
            scale = collectFeedBack.GetFeedbackOfType<MMF_Scale>();
            position = collectFeedBack.GetFeedbackOfType<MMF_Position>();
            events = collectFeedBack.GetFeedbackOfType<MMF_Events>();
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagContainer.CardTag))
            {
                Card card = other.GetComponent<Card>();
                card.gameObject.tag = TagContainer.DefaultTag;
                handManager.AddCardToHand(card);
                scale.AnimateScaleTarget = other.transform;
                position.AnimatePositionTarget = other.gameObject;
                events.PlayEvents.AddListener(card.CollectedEvent) ;
                collectFeedBack.PlayFeedbacks();
            }
        }
    }
}
