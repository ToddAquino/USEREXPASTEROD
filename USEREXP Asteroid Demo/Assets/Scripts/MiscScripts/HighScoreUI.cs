using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static HighScoreManager;

public class HighScoreUI : MonoBehaviour
{
    List<HighScoreElement> highScoreList = new List<HighScoreElement>();
    [SerializeField] GameObject panel;
    [SerializeField] GameObject HighScorePrefab;
    [SerializeField] Transform wrapper;

    List<GameObject> uiElements = new List<GameObject>();

    private void LoadHighScores()
    {
        highScoreList = FileHandler.ReadListFromJSON<HighScoreElement>("highscores");
    }
    private void OnEnable()
    {
        HighScoreManager.onHighScoreListChanged += UpdateUI;
    }
    private void OnDisable()
    {
        HighScoreManager.onHighScoreListChanged -= UpdateUI;
    }
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    private void UpdateUI(List<HighScoreElement> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            HighScoreElement el = list[i];

            if (i >= uiElements.Count)
            {
                var inst = Instantiate(HighScorePrefab, Vector3.zero, Quaternion.identity);
                inst.transform.SetParent(wrapper, false);

                uiElements.Add(inst);
            }

            var texts = uiElements[i].GetComponentsInChildren<Text>();
            texts[0].text = el.score.ToString();
            Debug.Log(el.score);

        }
    }
}
