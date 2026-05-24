### Introduction

An intelligent command-line coding assistant built with C#, Semantic Kernel, and local LLM inference. Get real-time code debugging, Docker assistance, and AI-powered development help - all running locally on your machine.

Download Ready to use? Download the pre-built executable from the Releases page. No installation required - just download, configure, and run!

### Download

**Ready to use?** Download the pre-built executable from the Releases page.

No installation required - just download, configure, and run!

**Features**

*   Code Debugging & Fixing: Paste broken code, get working solutions
    
*   Docker Assistant: Generate Dockerfiles, docker-compose configs, and commands
    
*   Interactive Chat Interface: Beautiful terminal UI with Spectre.Console
    
*   Streaming Responses: Real-time AI output as it thinks
    
*   Plugin Architecture: Extensible Semantic Kernel plugins
    
*   100% Local: No cloud APIs, complete privacy with llama.cpp
    
*   Fast: Optimized for local LLM inference
    

### Prerequisites

### Required Software

*   Visual Studio 2022 or later (or Visual Studio Code with C# extension)
    
*   **Download**: https://visualstudio.microsoft.com/
    
*   .NET 10.0 SDK
    
*   **Download:** https://dotnet.microsoft.com/download/dotnet/10.0 Verify installation: dotnet --version
    
*   llama.cpp Server
    
*   **Download from**: https://github.com/ggerganov/llama.cpp/releases
    
*   Get the release with server support (e.g., **llama-b4045-bin-win-cuda-cu12.2.0-x64.zip** for Windows with NVIDIA GPU)
    
*   Extract to a folder (e.g., C:\\llama.cpp)
    
*   A GGUF Model File
    
*   Download from Hugging Face (search for GGUF models)
    
*   Recommended models: Llama 3.2 (3B or 8B parameters) , CodeLlama
    
*   Place in C:\\llama.cpp\\models\\ directory
    

### Optional but Recommended

*   Windows Terminal for better console experience
    
*   Git for version control
    

### Quick Start (Using Pre-built EXE)

If you just want to run the application without building from source:

*   Download the Release
    
*   Go to the Releases section of this repository
    
*   Download the latest **AiCodingAgent.zip** file
    
*   Extract the ZIP file to a folder (e.g., C:\\AiCodingAgent)
    

### Set Up llama.cpp Server You still need the llama.cpp server and a model

*   Download llama.cpp: [https://github.com/ggerganov/llama.cpp/releases](https://github.com/ggerganov/llama.cpp/releases)
    
*   Download a GGUF model from Hugging Face
    
*   Create start-llama-server.bat in your llama.cpp folder:
    

```plaintext
@echo off
set MODEL_PATH= Your_model_path.gguf
C:\llama.cpp\llama-server.exe ^
-m %MODEL_PATH% ^
--ctx-size 2048 ^
--port 8080 ^
--threads 12 ^
--chat-template chatml

pause
```

### Configure the Application

Edit Configuration/appsettings.json in the extracted folder:

```plaintext
{
  "modelId": "gemma 4",
  "apiKey": "dummy-key",
  "endpoint": "http://localhost:8080/",
  "HttpClientConfig": {
    "TimeoutSeconds": 10
  }
}
```

### Run the Application

*   Start the llama.cpp server: start-llama-server.bat
    
*   Run AiCodingAgent.exe from the extracted folder
    
*   Start coding!
    

### Installation & Setup (Building from Source)

**Step 1:**

Clone the Repository

```plaintext
git clone https://github.com/yourusername/ai-coding-agent.git
cd ai-coding-agent
```

**Step 2:**

Restore NuGet Packages

```plaintext
dotnet restore
```

This will install:

*   Microsoft.SemanticKernel (v1.75.0)
    
*   Spectre.Console (v0.55.2)
    
*   Microsoft.Extensions.Configuration packages
    
*   Other dependencies from AiCodingAgent.csproj
    

**Step 3:**

Configure Application Settings

Edit Configuration/appsettings.json:

```plaintext
{
  "LLM": {
    "Endpoint": "http://localhost:8080",
    "ModelId": "local-model",
    "ApiKey": "not-needed"
  }
}
```

### Configuration Options:

*   **Endpoint:** URL where your llama.cpp server is running
    
*   **ModelId:** Identifier for your model (can be any string for local models)
    
*   **ApiKey:** Not required for local llama.cpp server
    

**Step 4:**

Set Up llama.cpp Server

Create a batch file start-llama-server.bat in your llama.cpp directory:

```plaintext
@echo off
set MODEL_PATH= Your_model_path.gguf
C:\llama.cpp\llama-server.exe ^
-m %MODEL_PATH% ^
--ctx-size 2048 ^
--port 8080 ^
--threads 12 ^
--chat-template chatml

pause
```

**Parameters Explained:**

*   **\--model:** Path to your GGUF model file
    
*   \-**\-port:** Port number (must match appsettings.json)
    
*   **\--ctx-size:** Context window size (tokens)
    
*   **\--n-gpu-layers:** Number of layers to offload to GPU (0 for CPU-only)
    
*   **\--threads:** CPU threads to use
    
*   **\--chat-template** : For formating
    

**Step 5:**

**Build the Project**

In Visual Studio:

*   Open AiCodingAgent.csproj or the solution file
    
*   Build → Build Solution (or press Ctrl+Shift+B)
    

Or via command line:

```plaintext
 dotnet build
```

**Usage**

Starting the Agent

**Method 1:** Using Pre-built EXE (Easiest)

```plaintext
# Start llama.cpp server first
start-llama-server.bat  # Windows
 
or 

# Double-click AiCodingAgent.exe or run from command line
AiCodingAgent.exe
```

### Method 2: Visual Studio

*   Press F5 to run with debugging
    
*   Or Ctrl+F5 to run without debugging
    

### Method 3:

Command Line (from source)

```plaintext
# Start llama.cpp server first
start-llama-server.bat  # Windows

# In a new terminal, run the agent
dotnet run
```

# or

### Using the Agent

### Once started, you'll see the main menu:

```plaintext
=== AI Coding Agent ===
Commands:
  1 or Debug Code      - Debug and fix your code
  2 or Docker Commands - Get Docker help
  exit                 - Quit the application

You: 
```

  
**Option 1:** Debug Code

```plaintext
You: 1
Paste your code and press Ctrl+Z then Enter (Windows)
```

*   Type or paste your code
    
*   Press Ctrl+Z then Enter (Windows)
    
*   The AI will analyze and provide fixes/improvements in real-time
    

### Example:

```plaintext
def calculate(x, y):
    result = x + y
    return result

print(calculate(5))  # Missing argument!
```

The agent will identify the error and provide the corrected code.

**Option 2:**

Docker Commands

```plaintext
You: 2

Paste your Docker requirement and press Ctrl+Z then Enter
```

### Example requests:

*   "Create a Dockerfile for a Node.js application"
    
*   "Generate docker-compose for nginx and postgres"
    
*   "Fix this Docker error: \[paste error\]"
    

### Exiting the Application

Type **exit** and press Enter at any prompt.

### Project Structure

```plaintext
AiCodingAgent/
│
├── Agent/
│   ├── AgentPrompts.cs       # Display prompts and commands
│   ├── CodingAgent.cs        # Core agent logic with streaming
│   └── UserInput.cs          # Main loop and command handling
│
├── Configuration/
│   ├── AppSettings.cs        # Settings model classes
│   └── appsettings.json      # Configuration file
│
├── Kernel/
│   ├── KernelExtensions.cs   # Extension methods for kernel
│   └── KernelFactory.cs      # Kernel initialization
│
├── Plugins/
│   ├── CodingPlugin/         # Code assistance functions
│   │   ├── Code/
│   │   │   ├── config.json
│   │   │   └── skprompt.txt
│   │   ├── CodePython/
│   │   ├── DOSScript/
│   │   └── Entity/
│   │
│   └── DockerPlugin/         # Docker assistance functions
│       ├── docker/
│       │   ├── config.json
│       │   ├── skprompt.txt
│       │   └── docker_suggest.yaml
│       └── DockerPlugin.cs
│
├── Services/
│   ├── CodeParserService.cs  # Code parsing utilities
│   └── Services.cs           # Service initialization
│
├── Program.cs                # Application entry point
└── AiCodingAgent.csproj      # Project file with dependencies
```

### Customization

Adding New Plugins

*   Create a new folder in Plugins/
    
*   Add a YourPlugin.cs file
    
*   Create semantic functions with:  
    \- `config.json`: Function metadata  
    \- `skprompt.txt`: AI prompt template
    

### Example config.json: json

```plaintext
{
  "schema": 1,
  "description": "Your function description",
  "execution_settings": {
    "default": {
      "max_tokens": 1000,
      "temperature": 0.7
    }
  }
}
```

### Example skprompt.txt:

```plaintext
You are an expert [domain] assistant.

Task: [What the function does]

Rules:
-[Rule 1]
-[Rule 2]

User Input: 
{{$input}}
```

### Modifying Prompts

Edit the skprompt.txt files in plugin folders. Changes take effect on next run - no recompilation needed!

### Changing LLM Settings

Modify Configuration/appsettings.json to point to different servers or adjust parameters.

### Troubleshooting

**Common Issues**

"Connection refused" or "Cannot connect to LLM"

*   Ensure llama.cpp server is running (llama-server.exe)
    
*   Check port matches in both server and appsettings.json
    
*   Verify endpoint URL is correct
    

### "Model not found" error from llama.cpp

*   Check model path in batch file is correct
    
*   Ensure GGUF file exists and isn't corrupted
    
*   Try absolute path instead of relative
    

### Slow responses

*   Reduce --ctx-size in server startup
    
*   Decrease model size (use quantized versions like Q4\_K\_M)
    
*   If using CPU, reduce --threads
    
*   For GPU, increase --n-gpu-layers
    

### "Invalid command" in agent

*   Type exact commands: 1, 2, exit, or full names
    
*   Commands are case-insensitive
    

### Build errors about .NET 10.0

*   Install .NET 10.0 SDK from Microsoft
    
*   Or modify **TargetFramework** in **.csproj** to **net8.0** or **net9.0**
    

### Dependencies

**NuGet Packages:**

*   **Microsoft.SemanticKernel** (1.75.0) - AI orchestration framework
    
*   **Spectre.Console** (0.55.2) - Beautiful terminal UI
    
*   **Microsoft.Extensions.Configuration** (7.0.0) - Configuration management
    
*   **Microsoft.Extensions.Configuration.Json** (7.0.0) - JSON config support
    
*   **Microsoft.Extensions.VectorData.Abstractions** (10.5.2) - Vector data support
    

### External Tools:

*   **llama.cpp** - Local LLM inference server
    
*   **GGUF Model** - Quantized language model
    

### Use Cases

*   **Code Review**: Get instant feedback on code quality
    
*   **Bug Fixing**: Paste error messages and get solutions
    
*   **Docker Setup**: Generate container configurations quickly
    
*   **Learning**: Understand code patterns and best practices
    
*   **Prototyping**: Generate boilerplate code fast
    

### Contributing

**Contributions are welcome! Feel free to:**

*   Add new plugins
    
*   Improve prompts
    
*   Enhance UI/UX
    
*   Fix bugs
    
*   Add features
    

### License

**This project is licensed under the MIT License.**

**Acknowledgments**

*   **Microsoft Semantic Kernel** team for the amazing framework
    
*   **llama.cpp** developers for local inference capabilities
    
*   **Spectre.Console** for beautiful terminal experiences
    

### Support

For issues or questions:

*   Open a GitHub issue
    
*   Check existing documentation
    
*   Review llama.cpp
    
*   documentation for server issues