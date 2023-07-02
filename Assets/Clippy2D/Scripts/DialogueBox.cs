using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    [TextArea(6, 6)]
    public List<string> tipLines;

    public TMP_Text textBubble; //This is a really garbage solution to render the text in front of the bubble, when the text has to be the parent...

    public GameObject previousButton;

    public GameObject nextButton;
    
    private int _tipIndex;

    private TMP_Text _text;

    
    // Start is called before the first frame update
    void Start() {
        _text = GetComponent<TMP_Text>();
        _text.SetText(tipLines[_tipIndex]);
        textBubble.SetText(tipLines[_tipIndex]);
        
        previousButton.GetComponent<Button>().onClick.AddListener(ShowPreviousTip);
        nextButton.GetComponent<Button>().onClick.AddListener(ShowNextTip);
    }

    // Update is called once per frame
    void Update()
    {
        previousButton.SetActive(_tipIndex != 0);
        nextButton.SetActive(_tipIndex < tipLines.Count - 1);

        _text.SetText(tipLines[_tipIndex]);
        textBubble.SetText(tipLines[_tipIndex]);
    }
    
    private void ShowPreviousTip()
    {
        if (_tipIndex > 0) {
            --_tipIndex;
        }
    }

    private void ShowNextTip()
    {
        if (_tipIndex < tipLines.Count - 1) {
            ++_tipIndex;
        }
    }
}
