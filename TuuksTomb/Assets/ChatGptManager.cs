using System;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using UnityEngine.Events;
public class ChatGptManager : MonoBehaviour
{
    //public panel field in inspector
    public TextMeshProUGUI sphinxChat;
    
    public OnResponseEvent OnResponse;
    
    [Serializable]
    public class OnResponseEvent : UnityEvent<string> { }
    
    private readonly OpenAIApi _openAI = new ("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");
    private readonly List<ChatMessage> _messages = new();
    
    private float _lastRequestTime;
    private const float RequestCooldown = 2.0f;

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

        CreateChatCompletionRequest request = new()
        {
            Messages = _messages,
            Model = "gpt-3.5-turbo"
        };
        
        try { 
            var response = await _openAI.CreateChatCompletion(request);
            if (response.Choices is not { Count: > 0 }) return;
            var chatResponse = response.Choices[0].Message;
            _messages.Add(chatResponse);
            
            Debug.Log(chatResponse.Content);
            OnResponse.Invoke(chatResponse.Content);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
            
    }
}
