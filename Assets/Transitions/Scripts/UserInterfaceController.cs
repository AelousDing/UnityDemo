using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UserInterfaceController : MonoBehaviour
{
    private VisualElement menu;
    private VisualElement[] mainMenuOptions;
    private List<VisualElement> widgets;

    private const string POPUP_ANIMATION = "pop-animation-hide";
    private int mainPopupIndex = -1;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        menu = root.Q<VisualElement>("Menu");
        mainMenuOptions = menu.Q<VisualElement>("MainNav").Children().ToArray();
        widgets = root.Q<VisualElement>("Body").Children().ToList();

        menu.RegisterCallback<TransitionEndEvent>(Menu_TransitionEnd);
    }

    private void Menu_TransitionEnd(TransitionEndEvent evt)
    {
        if (!evt.stylePropertyNames.Contains("opacity"))
        {
            return;
        }
        if (mainPopupIndex < mainMenuOptions.Length - 1)
        {
            mainPopupIndex++;
            mainMenuOptions[mainPopupIndex].ToggleInClassList(POPUP_ANIMATION);
        }
        else
        {
            widgets.ForEach(x => x.style.translate = new StyleTranslate(new Translate(0, 0, 0)));
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        menu.ToggleInClassList(POPUP_ANIMATION);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
