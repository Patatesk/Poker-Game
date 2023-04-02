using Cinemachine;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace PK.PokerGame
{
    public class ChangeCameraOnStart : MonoBehaviour
    {
        [SerializeField] private float time;
        private MMF_Player player;
        private CinemachineVirtualCamera cam;

        private void Awake()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            player = GetComponent<MMF_Player>();
        }
        IEnumerator MoveCam()
        {
            player.PlayFeedbacks();
            yield return new WaitForSeconds(time);
            cam.Priority= 0;
            yield return new WaitForSeconds(0.2f);
            GameStartSignal.Trigger();
        }

        public void StartMoveCam()
        {
            StartCoroutine(MoveCam());
        }

       
    }
}
