using UnityEngine;
using System.Collections.Generic;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CardManager : MonoBehaviour
{
    public List<CardData> cardData;
    public GameObject cardPrefab;
    public List<GameObject> cardsInHand;
    public int handCount = 4;
    public bool showNextField;
    [BV.DrawIf("showNextField", true)]
    public string regularName;
    [ReadOnly] public string stringName; 

    public CardData GetCard(CardID _cardID) => cardData.Find(x => x.cardID == _cardID);

    private IEnumerator BuildDeck()
    {
        ListX.DestroyList(cardsInHand);
        ListX.ShuffleList(cardData);

        for(int i = 0; i < handCount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(i * 3, 5, 0), transform.rotation);
            newCard.GetComponent<Card>().Initialize(ListX.GetRandomItemFromList(cardData));
            cardsInHand.Add(newCard);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(BuildDeck());
    }

    private void OnValidate()
    {
        stringName = regularName;
    }

    #region Editor
#if UNITY_EDITOR
    [SerializeField] TextAsset cardDataSheet;
    [SerializeField] private string sheetsPath = "/Assets/Universal/";
    public void UpdateCards()
    {
        string[,] grid = CSVReader.GetCSVGrid(sheetsPath + cardDataSheet.name + ".csv");
        CardData card = new CardData();
        List<string> keys = new List<string>();

        //First create a list for holding our key values
        for (int y = 0; y < grid.GetUpperBound(1); ++y)
        {
            keys.Add(grid[0, y]);
        }

        //Loop through the columns, adding the value to the appropriate key
        for (int x = 1; x < grid.GetUpperBound(0); x++)
        {
            Dictionary<string, string> columnData = new Dictionary<string, string>();
            for (int k = 0; k < keys.Count; k++)
            {
                columnData.Add(keys[k], grid[x, k]);
                //Debug.Log("Key: " + keys[k] + ", Value: " + grid[x, k]);
            }

            //Loop through the dictionary using the key values
            foreach (KeyValuePair<string, string> item in columnData)
            {
                // Gets a unit data based off the ID and updates the data
                //if (item.Key.Contains("cardID"))
                //    card.cardID = EnumX.ToEnum<CardID>(item.Value);
                if (item.Key.Contains("description"))
                    card.description = item.Value;
                if (item.Key.Contains("value"))
                {
                    int temp = int.TryParse(item.Value, out temp) ? temp : 100;
                    card.value = temp;
                }
            }
            UpdateCard(card);
        }
    }

    private void UpdateCard(CardData _cardData)
    {
        CardData card = cardData.Find(x=> x.cardID == _cardData.cardID);
        card.description = _cardData.description;
        card.value = _cardData.value;

        //flag the object as "dirty" in the editor so it will be saved
        EditorUtility.SetDirty(card);

        // Prompt the editor database to save dirty assets, committing your changes to disk.
        AssetDatabase.SaveAssets();
    }


    [CustomEditor(typeof(CardManager))]
    public class CardManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            CardManager cardManager = (CardManager)target;
            GUILayout.Space(5);
            base.OnInspectorGUI();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.green;
            if(GUILayout.Button("Load Data From Spreadsheet"))
            {
                if(EditorUtility.DisplayDialog("Load Data", "Are you sure?", "Yes", "No"))
                {
                    cardManager.UpdateCards();
                    EditorUtility.SetDirty(cardManager);
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }
    }
#endif
    #endregion

}
