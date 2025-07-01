using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SituationRoomWiring : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI continueText;
    public Button continueButton;

    private Generate4Rooms generate4Rooms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button1.onClick.AddListener(() => OnButtonClicked(1));
        button2.onClick.AddListener(() => OnButtonClicked(2));
        button3.onClick.AddListener(() => OnButtonClicked(3));
        generate4Rooms = GetComponentInParent<Generate4Rooms>();
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClicked);
            continueButton.gameObject.SetActive(false);
        }
    }

    void OnButtonClicked(int buttonNumber)
    {
        // Hide all buttons first
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        // Show only button 3
        button3.gameObject.SetActive(true);

        // Update message
        switch (buttonNumber)
        {
            case 1:
                messageText.text = "Button 1 was clicked!";
                break;
            case 2:
                messageText.text = "Button 2 was clicked!";
                break;
            case 3:
                messageText.text = "Button 3 was clicked!";
                break;
        }

        // Show continue UI
        if (continueText != null)
        {
            continueText.text = "Click to continue....";
            continueText.gameObject.SetActive(true);
        }
        if (continueButton != null)
            continueButton.gameObject.SetActive(true);
    }

    void OnContinueClicked()
    {
        if (generate4Rooms != null)
            generate4Rooms.OnEventCompleted();
        Destroy(gameObject);
    }
}
