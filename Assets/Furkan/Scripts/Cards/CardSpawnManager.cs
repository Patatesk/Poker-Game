using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PK.PokerGame
{
    public class CardSpawnManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cardPrefaps = new List<GameObject>();
        [SerializeField] private List<Transform> allCardSpawnPoints = new List<Transform>();
        [Header("Spawn Properties")]
        [SerializeField] private float spawnRate;
        [SerializeField] private float totalMaxActiveCardCount;
        [SerializeField] private Vector3 spawnRotationOffset = Vector3.zero;

        public Queue<GameObject> spawnQueue = new Queue<GameObject>();
        private Queue<Transform> SpawnTransformsQueue = new Queue<Transform>();
        private WaitForSeconds spawnTime;
        private int acitveCardCount;
        private GameObject parent;
        private void Awake()
        {
            parent = new GameObject("CardPoolParent");
            GetAllChilds();
            AddAllCardsToQueue();
            spawnTime = new WaitForSeconds(spawnRate);
        }

        private void OnEnable()
        {
            GetBackQueueSignal.GetBack += ReturnQueue;
        }
        private void OnDisable()
        {
            GetBackQueueSignal.GetBack -= ReturnQueue;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(CardSpawner());
        }
        private IEnumerator CardSpawner()
        {
            while (true)
            {
                SpawnACard();
                yield return spawnTime;
            }
        }

        private void SpawnACard()
        {
            if (spawnQueue.Count <= 0 || SpawnTransformsQueue.Count <= 0 || totalMaxActiveCardCount <= acitveCardCount) return;
            GameObject _card = spawnQueue.Dequeue();
            Card cardScript = _card.GetComponent<Card>();
            Transform cardTransform = SpawnTransformsQueue.Dequeue();
            if (cardTransform.GetComponent<Check>().hascard) return;
            if (_card == null || cardScript == null || cardTransform == null) return;
            _card.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            cardScript.spawnPoint = cardTransform;
            _card.transform.position = cardTransform.position;
            _card.transform.rotation = Quaternion.Euler(spawnRotationOffset);
            _card.SetActive(true);
            cardTransform.GetComponent<Check>().hascard = true;
            acitveCardCount++;
        }

        private void GetAllChilds()
        {
            foreach (Transform child in transform)
            {
                allCardSpawnPoints.Add(child);
            }
            allCardSpawnPoints.Shuffle();
            foreach (Transform item in allCardSpawnPoints)
            {
                SpawnTransformsQueue.Enqueue(item);
            }
        }
        private void AddAllCardsToQueue()
        {
            cardPrefaps.Shuffle();
            foreach (GameObject card in cardPrefaps)
            {
                GameObject cardToAdd = Instantiate(card);
                cardToAdd.SetActive(false);
                cardToAdd.transform.SetParent(parent.transform);
                spawnQueue.Enqueue(cardToAdd);
            }
        }

        private void ReturnQueue(Card card)
        {
            if (!SpawnTransformsQueue.Contains(card.spawnPoint) && card.spawnPoint.GetComponent<Check>().hascard)
            {
                StartCoroutine(WaitBeforeAdding(card));
            }
            acitveCardCount--;
            if (acitveCardCount < 0) acitveCardCount = 0;
            
        }

        IEnumerator WaitBeforeAdding(Card card)
        {
            yield return new WaitForSeconds(4);
            card.spawnPoint.GetComponent<Check>().hascard = false;
            SpawnTransformsQueue.Enqueue(card.spawnPoint);
        }

    }
    public class GetBackQueueSignal
    {
        public static event Action<Card> GetBack;
        public static void Trigger(Card card)
        {
            GetBack?.Invoke(card);
        }
    }
    public static class ShuffleList
    {
        public static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }


}
