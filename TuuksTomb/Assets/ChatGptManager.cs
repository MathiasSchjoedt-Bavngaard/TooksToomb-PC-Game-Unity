using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using UnityEngine.Events;
public class ChatGptManager : MonoBehaviour
{
    private ChatGptManager instance;
    private bool hasrun;
    public GameTimer GameTimer;
    public TextMeshProUGUI sphinxChat;
    public OnResponseEvent OnResponse;
    
    [Serializable]
    public class OnResponseEvent : UnityEvent<string> { }
    
    private CreateChatCompletionRequest _request;
    private const string Key = "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";
    private readonly OpenAIApi _openAI = new (Key);
    private readonly List<ChatMessage> _messages = new();
    
    private float _lastRequestTime;
    private const float RequestCooldown = 2.0f;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        DontDestroyOnLoad(instance);
        if (hasrun) return;
        _messages.Clear();
        AddInitialPrompt();
    }
    
    private async void AddInitialPrompt()
    {
        hasrun = true;
        const string initialPrompt = @" You will act as a sphinx. Abide the following information.
        Role: Sphinx, keeper of secrets and judge of souls

        Introduction: ""I am the Sphinx, the keeper of secrets and the judge of souls. To escape this realm, heed the ancient decree. Only a redeemed pharaoh can find freedom here. My riddle will reveal if you've found redemption.""
        
        Riddle: ""Born in the desert's fiery embrace, I flow gently through the land. Revered as a giver of life, my depths hold secrets sharp as a knife. I carry mysteries and histories, hidden in ancient sands. What am I?""

        Options:

        The Sphinx, sentinel of this ancient place.
        The Ankh, symbol of life and eternity.
        The River Nile, lifeline of Egypt's history.
        The Sahara Desert, land of pharaohs' dreams.
        Correct Answer: Option 3, The River Nile or The Nile or The Nile River.

        Instructions:

        If a wrong answer is chosen, respond with an insult about their knowledge of ancient Egypt, then allow another attempt with the wrong option removed.
        Limit responses to 65 words.
        Do not provide riddle hints or help unless the riddle is answered.
        When the player says the nile or the river nile or the nile river, respond with ""Well done, the answer is indeed the river nile.""
        ";
        
        ChatMessage initialMessage = new()
        {
            Content = initialPrompt,
            Role = "user"
        };
        
        _messages.Add(initialMessage);
        
        _request = new CreateChatCompletionRequest
        {
            Messages = _messages,
            Model = "gpt-4"
        };
        
        try { 
            var response = await _openAI.CreateChatCompletion(_request);
            
            Debug.Log(response.Choices[0].Message.Content);
            
            if (string.IsNullOrEmpty(response.ToString()))
            {
                Debug.LogWarning("Empty or null response from the API.");
                return;
            }
            
            if (response.Choices is not { Count: > 0 }) return;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    
    private IEnumerator RevealSphinxText(string fullText)
    {
        const int charactersPerFrame = 1;

        for (var i = 0; i <= fullText.Length; i += charactersPerFrame)
        {
            var endIndex = Mathf.Min(i + charactersPerFrame, fullText.Length);
            var partialText = fullText[..endIndex];
            sphinxChat.text = partialText;
            yield return null; // Wait for one frame
        }

        OnResponse.Invoke(fullText);
        if (!fullText.Contains("Well done,")) yield break;
        //End game
        GameTimer.StopTimer();
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }

    
    public async void AskChatGpt(string newText)
    {
        sphinxChat.text = string.Empty;
        
        if (Time.time - _lastRequestTime < RequestCooldown)
        {
            Debug.LogWarning("Request cooldown, please wait.");
            return;
        }

        _lastRequestTime = Time.time;
        
        ChatMessage newMessage = new()
        {
            Content = newText,
            Role = "user"
        };
        _messages.Add(newMessage);
        
        _request = new CreateChatCompletionRequest
        {
            Messages = _messages,
            Model = "gpt-4"
        };
        
        try { 
            var response = await _openAI.CreateChatCompletion(_request);
            
            Debug.Log(response);
            
            // Check if the response content is empty or null
            if (string.IsNullOrEmpty(response.ToString()))
            {
                Debug.LogWarning("Empty or null response from the API.");
                return;
            }
            
            if (response.Choices is not { Count: > 0 }) return;
            var chatResponse = response.Choices[0].Message;
            _messages.Add(chatResponse);
            
            Debug.Log(chatResponse.Content);
            // Trigger the character-by-character reveal coroutine
            StartCoroutine(RevealSphinxText(response.Choices[0].Message.Content));
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
