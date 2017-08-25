using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextController : MonoBehaviour {
    public GameObject HintText;
    public bool Clickable { get; set; }

    string DefaultText = "Hint";
    string ExpandText = "Pulcherrimum Nomen    (Latin)\nPulcherrimum means \"beautiful\"";
    Animator anim;

    public void ExpandTextChange()
    {
        var textComponent = HintText.GetComponent<Text>();
        textComponent.text = ExpandText;
        textComponent.fontStyle = FontStyle.Italic;
        textComponent.alignment = TextAnchor.MiddleLeft;
    }

    public void ShrinkTextChange()
    {
        var textComponent = HintText.GetComponent<Text>();
        textComponent.text = DefaultText;
        textComponent.fontStyle = FontStyle.Normal;
        textComponent.alignment = TextAnchor.MiddleCenter;
    }

    public void BeClickable()
    {
        Clickable = true;
    }

    bool ClickedInBox()
    {
        var inBox = false;
        if (Input.GetMouseButtonDown(0))
        {
            var MousePosition = Input.mousePosition;
            var rectTransform = GetComponent<RectTransform>();
            var anchor1 = new Vector2(rectTransform.position.x, rectTransform.position.y);
            var anchor2 = new Vector2(rectTransform.position.x + rectTransform.rect.width, rectTransform.position.y - rectTransform.rect.height);
            inBox = (MousePosition.x > anchor1.x && MousePosition.x < anchor2.x && MousePosition.y < anchor1.y && MousePosition.y > anchor2.y);
        }
        return inBox;
    }

	// Use this for initialization
	void Start () {
        ShrinkTextChange();
        Clickable = false;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && Clickable)
        {
            var expand = anim.GetBool("Expand");
            if (ClickedInBox() != expand)
            {
                anim.SetBool("Expand", !expand);
            }
        }
    }
}
