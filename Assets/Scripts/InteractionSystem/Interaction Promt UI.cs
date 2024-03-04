using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromtUI : MonoBehaviour
{
    private Camera _maincam;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private TextMeshProUGUI _promtText;
    // Start is called before the first frame update
    void Start()
    {
        _maincam = Camera.main;
        _uiPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = _maincam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool IsDisplayed = false;
    public void SetUp(string promptText)
    {
        _promtText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        _uiPanel.SetActive(false);
        IsDisplayed = false;
    }
}
