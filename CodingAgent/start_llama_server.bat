@echo off
set MODEL_PATH=C:\Users\Administrator\Documents\AI-Models\gemma-4-E4B-it-Q4_K_M\gemma-4-E4B-it-Q4_K_M.gguf
C:\llama.cpp\llama-server.exe ^
-m %MODEL_PATH% ^
--ctx-size 2048 ^
--port 8080 ^
--threads 12 ^
--chat-template chatml

pause

