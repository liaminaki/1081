using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabGroup : MonoBehaviour {

    [SerializeField] private List<Tab> _tabs;
    [SerializeField] private Sprite _default;
    [SerializeField] private TMP_FontAsset _defaultFontAsset;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Sprite _selected;
    [SerializeField] private TMP_FontAsset _selectedFontAsset;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Sprite _hover;
    [SerializeField] private List<GameObject> _correspondingPages;

    private Tab selectedTab; 

    private void Start() {
        // Check if there are tabs in the list
        if (_tabs != null && _tabs.Count > 0) {
            // Set the first tab as selected
            OnTabSelected(_tabs[_tabs.Count-1]);
        }
    }

    public void AddToList(Tab tab) {

        if (_tabs == null) {
            _tabs = new List<Tab>();
        }

        _tabs.Add(tab);
    }

    public void OnTabEnter(Tab tab) {
        UpdateTabs();
        if (selectedTab == null || tab != selectedTab) {
            tab.SetImage(_hover);
        }
    }

    public void OnTabExit(Tab tab) {
        UpdateTabs();
    }

    public void OnTabSelected(Tab tab) {
        selectedTab = tab;
        UpdateTabs(); 
        tab.SetImage(_selected);
        tab.SetColor(_selectedColor);
        tab.SetFontAsset(_selectedFontAsset);

        // Get index of the page of tab
        // Precondition: the tab button and the corresponding page must have same index

        int index = tab.transform.GetSiblingIndex();

        for (int i = 0; i < _correspondingPages.Count; i++) {
            if (i == index)
                _correspondingPages[i].SetActive(true);
            else
                _correspondingPages[i].SetActive(false);
        }
    }

    public void UpdateTabs() {
        foreach (Tab tab in _tabs) {

            if (selectedTab != null && tab == selectedTab) { continue; }
            tab.SetImage(_default);
            tab.SetColor(_defaultColor);
            tab.SetFontAsset(_defaultFontAsset);
        }
    }
    
}