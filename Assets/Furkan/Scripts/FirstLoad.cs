using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public class FirstLoad : MonoBehaviour
    {
        [SerializeField] private GameObject text1;
        [SerializeField] private GameObject text2;
        [SerializeField] private GameObject ingame;

        private int firstLoad;

        private void Awake()
        {
            firstLoad = PlayerPrefs.GetInt("First",0);
        }
        public void StartOnBoard()
        {
            if(firstLoad == 0)
            StartCoroutine(OnBoard());
            else ingame.SetActive(true);
        }

        IEnumerator OnBoard()
        {
            ingame.SetActive(false);
            text1.SetActive(true);
            yield return new WaitForSeconds(2);
            text1.SetActive(false);
            text2 .SetActive(true);
            yield return new WaitForSeconds(2);
            text2.SetActive(false);
            ingame.SetActive(true);
            PlayerPrefs.SetInt("First", 1);
        }
    }
}
