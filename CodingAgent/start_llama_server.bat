@echo off
set MODEL_PATH=YOUR_MODEL_NAME.gguf
C:\llama.cpp\llama-server.exe ^
-m %MODEL_PATH% ^
--ctx-size 2048 ^
--port 8080 ^
--threads 12 ^
--chat-template chatml
pause

