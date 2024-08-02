using JetBrains.Annotations;

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public bool AlreadyChecked = false;
    float PreGameTime = 3.5f;
    public bool GameStarted = false;
    [SerializeField] TextMeshProUGUI PreGameCountDown;
    [SerializeField] TextMeshProUGUI PreGameCountDownDecor;

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(MedPosition, new Vector2(MedPosition.x + 0.3f, MedPosition.y + 0.3f), Color.green);
    }

    private void Start()
    {
        SpeechBubble.enabled = false;
        Impatience = BaseImpatience;
        ImpatienceText.text = $"DAY LEFT: {Impatience:#0.0}s";
    }

    private void Update()
    {
        if (PreGameTime > 0.5)
        {
            PreGameTime -= Time.deltaTime;
            PreGameCountDown.text = $"{PreGameTime:#0}";
            FindObjectOfType<Pause>().GamePaused = true;
            if (SUtilities.IsInRange(PreGameTime, 1.5f, 1.6f))
            {
                try
                {
                    FindObjectOfType<SoundEffectPlayer>().PlayTick();
                }
                catch { Debug.LogError("Play from start screen for sound effects"); }
            }
            if (SUtilities.IsInRange(PreGameTime, 2.5f, 2.6f))
            {
                try
                {
                    FindObjectOfType<SoundEffectPlayer>().PlayTick();
                }
                catch { Debug.LogError("Play from start screen for sound effects"); }
            }
            return;
        }
        else if (!GameStarted)
        {
            FindObjectOfType<Pause>().GamePaused = false;
            GameStarted = true;
            Destroy(PreGameCountDown);
            Destroy(PreGameCountDownDecor);
        }
        if (!FindObjectOfType<Pause>().GamePaused) Impatience -= Time.deltaTime;
        ImpatienceText.text = $"DAY LEFT: {Impatience:#0.0}s";
        if (Impatience <= 0)
        {
            SceneManager.LoadScene("End");
            try
            {
                FindObjectOfType<SoundEffectPlayer>().StopGameplayMusic();
                FindObjectOfType<SoundEffectPlayer>().StartMenuMusic();
                FindObjectOfType<SoundEffectPlayer>().PlayBells();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            FindObjectOfType<FinalScore>().GameLose();
            FindObjectOfType<Scorekeeper>().LoseGame();
        
        }
        CheckForTimerTick();
        

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
        //Failsafe
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
            if (!AlreadyChecked)
            {
                AlreadyChecked = true;
                try
                {
                    FindObjectOfType<SoundEffectPlayer>().PlayWrongMed();
                }
                catch { Debug.LogError("Play from the start for sound effects!"); }
            }
            FindObjectOfType<PlayerCloseTo>().AppearingItem.text = "Not on the order!";
        }
    }

    void GaveMedicine(int medicine)
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayRightMed();
        }
        catch { Debug.LogError("Play from start scene for soundeffects!"); }
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

    void CheckForTimerTick()
    {
        if (SUtilities.IsInRange(Impatience, 1f, 1.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 2f, 2.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 3f, 3.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 4f, 4.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 5f, 5.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 6f, 6.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 7f, 7.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 8f, 8.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 9f, 9.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
        }
        else if (SUtilities.IsInRange(Impatience, 10f, 10.1f))
        {
            try
            {
                FindObjectOfType<SoundEffectPlayer>().PlayTick();
            }
            catch { Debug.LogError("Play from start screen for sound effects"); }
            ImpatienceText.color = new Color(0.5f, 0, 0);
            ImpatienceText.fontSize = 30;
        }
        else if (Impatience < 10)
        {
            ImpatienceText.color = Color.red;
        }
        else if (Impatience < 20)
        {
            ImpatienceText.color = Color.yellow;
        }
        else
        {
            ImpatienceText.fontSize = 22;
            ImpatienceText.color = Color.white;
        }
    }

    public void KILL()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from start screen for sound effects"); }
        Impatience = 0;
    }
}
