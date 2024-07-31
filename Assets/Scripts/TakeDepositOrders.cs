using JetBrains.Annotations;

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeDepositOrders : MonoBehaviour
{
    public List<int> order = new List<int>();
    private List<GameObject> InstantiatedObjects = new List<GameObject>();
    [SerializeField] GameObject PrefabBlueMed;
    [SerializeField] GameObject PrefabGreenMed;
    [SerializeField] GameObject PrefabRedMed;
    [SerializeField] Vector2 MedPosition;
    [SerializeField] SpriteRenderer SpeechBubble;
    public GameObject ObjectHolding;
    public bool OrderTaken = false;
    [SerializeField] int BaseImpatience;
    float Impatience;
    [SerializeField] TextMeshProUGUI ImpatienceText;

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(MedPosition, new Vector2(MedPosition.x + 0.3f, MedPosition.y + 0.3f), Color.green);
    }

    private void Start()
    {
        SpeechBubble.enabled = false;
        Impatience = BaseImpatience;
    }

    private void Update()
    {
        Impatience -= Time.deltaTime;
        ImpatienceText.text = $"DAY LEFT: {Impatience:#0.0}s";
        if (Impatience <= 0)
        {
            SceneManager.LoadScene("End");
            FindObjectOfType<FinalScore>().GameLose();
            FindObjectOfType<Scorekeeper>().LoseGame();
        }
    }

    public void TakeOrder()
    {
        OrderTaken = true;
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            order.Add(Random.Range(0, 3));
        }
        PrintMedicine();
    }

    private void PrintMedicine()
    {
        SpeechBubble.enabled = true;
        float xOffset = 0;
        for (int i = 0; i < order.Count; i++)
        {
            if (order[i] == 0)
            {
                InstantiatedObjects.Add(Instantiate(PrefabRedMed, new Vector2(MedPosition.x + xOffset, MedPosition.y) , Quaternion.identity));
            }
            else if (order[i] == 1)
            {
                InstantiatedObjects.Add(Instantiate(PrefabGreenMed, new Vector2(MedPosition.x + xOffset, MedPosition.y), Quaternion.identity));
            }
            else if (order[i] == 2)
            {
                InstantiatedObjects.Add(Instantiate(PrefabBlueMed, new Vector2(MedPosition.x + xOffset, MedPosition.y), Quaternion.identity));
            }
            xOffset += 0.3f;
        }
    }

    public void CheckGivenMedicine(int medicine)
    {
        bool isInOrder = false;
        if (order.Count <= 0)
        {
            CompletedOrder();
            return;
        }
        foreach (int item in order)
        {
            if (item == medicine) isInOrder = true;
        }
        if (isInOrder)
        {
            FindObjectOfType<PlayerCloseTo>().AppearingItem.text = "Press space to deposit";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GaveMedicine(medicine);
            }
        }
        else
        {
            FindObjectOfType<PlayerCloseTo>().AppearingItem.text = "Not on the order!";
        }
    }

    void GaveMedicine(int medicine)
    {
        for (int i = 0; i < order.Count; i++)
        {
            if (order[i] == medicine)
            {
                if (order.Count == 1)
                {
                    CompletedOrder();
                }
                else
                {
                    order.RemoveAt(i);
                }
                Debug.LogWarning("removed an object!");
                break;
            }
        }
        try
        {
            ObjectHolding.GetComponent<PickupableObjects>().DropByPlayer();
        }
        catch 
        {
            Debug.LogError($"Unable to clear object from player: Object does not have a Pickupable component.");
        }
        ClearInstantiatedObjects();
        if (order.Count != 0)PrintMedicine();
    }

    void CompletedOrder()
    {
        order.Clear();
        FindObjectOfType<Scorekeeper>().AddScore();
        ClearInstantiatedObjects();
        OrderTaken = false;
        SpeechBubble.enabled = false;
    }

    void ClearInstantiatedObjects()
    {
        foreach (GameObject obj in InstantiatedObjects)
        {
            Destroy(obj);
        }
        InstantiatedObjects.Clear();
    }
}
