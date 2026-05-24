namespace AiCodingAgent.Services;

using System;
using Microsoft.SemanticKernel.ChatCompletion;
using Spectre.Console;

public class CodeParserService
{
    public readonly IChatCompletionService _CodeService;

    public CodeParserService(Services appServices)
    {
        this._CodeService = appServices.CodeService
            ?? throw new ArgumentNullException(nameof(appServices), "appServices.CodeService is null.");

        //Console.WriteLine($"CodeParserService initialized: {appServices}");
    }
}