// Strongly-typed config model
namespace AiCodingAgent.Configuration;


/// Root configuration model — maps 1:1 with appsettings.json


public class AppSettings
    
 
{
    public string ApiKey { get; set; } = string.Empty;

    ///Chat model to use, e.g. gpt-4o, gpt-4-turbo
    public string ModelId { get; set; } = string.Empty;
    public Uri ? Endpoint { get; set; }
    public string timeout_ { get; set; } = string.Empty;

    public string userinput { get; set; } = string.Empty;
    public string SystemPrompt { get; set; } = string.Empty;
   
    public Agent Agent { get; set; } = new();
}


public class Agent
{
    /// <summary>Switch between OpenAI and Azure OpenAI
    //public bool UseAzureOpenAI { get; set; } = false;

    /// <summary>Max tokens the model may return per response
    //public int MaxTokens { get; set; } = 400;

    ///// <summary>0 = deterministic, 1 = creative. Keep low for code tasks.</summary>
    //public double Temperature { get; set; } = 0.1;

    /// <summary>System prompt / persona injected at the start of every conversation
    public string SystemPrompt { get; set; } = string.Empty;
}
