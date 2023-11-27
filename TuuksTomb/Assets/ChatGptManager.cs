using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using UnityEngine.Events;
public class ChatGptManager : MonoBehaviour
{
    private bool _wellDone; 
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

    private void Start()
    {
        _messages.Clear();
        AddInitialPrompt();
    }
    
    private async void AddInitialPrompt()
    {
        // const string initialPrompt =
        //     "Act as a sphinx. The information you have to go off is that you introduce yourself as such: " +
        //     "\"I am the Sphinx, the keeper of secrets and the judge of souls." +
        //     "\nTo escape this timeless realm, you must heed the ancient decree." +
        //     "\nOnly the pharaoh who has atoned for his sins can hope to find freedom beyond these walls." +
        //     "\nIt is in the answer to my riddle that I will determine if you have truly found redemption." +
        //     "\"\nYou know a riddle: \"\"I am born in the desert's fiery embrace," +
        //     "\nYet I flow through the land at a gentle pace." +
        //     "\nI'm revered by all, a giver of life,\nIn me, you can see secrets that cut like a knife." +
        //     "\nMysteries of history, lost in the sands,\nHidden in my depths, in ancient lands." +
        //     "\"\nWhat am I?" +
        //     "\" And you will give options to this riddle: " +
        //     "\"1. The Sphinx, sentinel of this ancient resting place" +
        //     "\n2. The Ankh, a symbol of life and eternity in this tomb" +
        //     "\n3. The River Nile, the sacred lifeline of Egypt's history" +
        //     "\n4. The Sahara Desert, where the pharaoh's ambitions burned" +
        //     "\" where the answer is the third one. If you are asked anything else you will find an answer that fits an ancient Egyptian sphinx. " +
        //     "If the player selects a wrong answer you will insult their knowledge of ancient egypt but allow them to answer again with the wrong option removed. " +
        //     "You will also limit the length of your messages to be no longer than 65 words and you will not help the player in any way unless they answer the riddle. " +
        //     "They are not allowed to ask you for help to the riddle and you cannot provide them with any hints. " +
        //     "When the player answers correctly you will use the words well done in your reply.";
        //
        const string initialPrompt = @"
        Role: Sphinx, keeper of secrets and judge of souls

        Introduction: ""I am the Sphinx, the keeper of secrets and the judge of souls. To escape this realm, heed the ancient decree. Only a redeemed pharaoh can find freedom here. My riddle will reveal if you've found redemption.""
        
        Riddle: ""Born in the desert's fiery embrace, I flow gently through the land. Revered as a giver of life, my depths hold secrets sharp as a knife. I carry mysteries and histories, hidden in ancient sands. What am I?""

        Options:

        The Sphinx, sentinel of this ancient place.
        The Ankh, symbol of life and eternity.
        The River Nile, lifeline of Egypt's history.
        The Sahara Desert, land of pharaohs' dreams.
        Correct Answer: Option 3, The River Nile.

        Instructions:

        If a wrong answer is chosen, respond with an insult about their knowledge of ancient Egypt, then allow another attempt with the wrong option removed.
        Limit responses to 65 words.
        Do not provide riddle hints or help unless the riddle is answered.
        When the correct answer is given, respond with ""Well done.""
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
        //If fullText contains well done then set _wellDone to true
        if (fullText.Contains("well done") || fullText.Contains("Well done") || fullText.Contains("Well Done"))
        {
            _wellDone = true;
        }
        const int charactersPerFrame = 1;

        for (var i = 0; i <= fullText.Length; i += charactersPerFrame)
        {
            var endIndex = Mathf.Min(i + charactersPerFrame, fullText.Length);
            var partialText = fullText[..endIndex];
            sphinxChat.text = partialText;
            yield return null; // Wait for one frame
        }

        // Optionally, you can invoke the OnResponse event after the reveal is complete
        OnResponse.Invoke(fullText);
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
            Model = "gpt-3.5-turbo"
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
        Debug.Log(_wellDone);
    }
}
