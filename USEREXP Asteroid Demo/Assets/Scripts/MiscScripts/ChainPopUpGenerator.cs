using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;

public class ChainPopUpGenerator : MonoBehaviour
{
    public static ChainPopUpGenerator Instance;
    public GameObject prefab;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreatePopUp(Vector3 position, int text, int tmpscore)
    {
        
        var popUp = Instantiate(prefab, position, Quaternion.identity);
        var tmpChain = popUp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        var tmpScore = popUp.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        if (text < 10)
        {
            tmpChain.gameObject.GetComponent<Animator>().Play("FadeOut");
            tmpChain.text = text.ToString() + "x";
        }            
        else
        {
            tmpChain.gameObject.GetComponent<Animator>().Play("Rainbow");
            tmpChain.text = "MAX";
        }
           
        tmpScore.text = tmpscore.ToString();
        Destroy(popUp, 1f);
    }
}
