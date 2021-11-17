# Commands to run with docker

open powershell
wsl -d docker-desktop
sysctl -w vm.max_map_count=262144
-  docker run -p 5601:5601 -p 9200:9200 -p 5044:5044 -it -e ES_HEAP_SIZE="2g" -e LS_HEAP_SIZE="1g" --name elk sebp/elk

managing wsl 
- notepad "$env:USERPROFILE/.wslconfig"
