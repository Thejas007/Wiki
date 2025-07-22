sudo vi /etc/ssh/sshd_config

MaxSessions 10

ClientAliveInterval 60

ClientAliveCountMax 3

MaxStartUps 10:30:100

sudo systemctl restart sshd

sudo systemctl status sshd


sudo sshd -T | grep -iE "clientalive|tcpkeepalive"


journalctl --since "2025-07-21 03:50"

sudo netstat -tnp | grep ':22' 

ps -ef | grep sshd   

ps ax | grep sshd 

ss -tnp | grep sshd
 
 
find . -name "*ystem*" -delete

find . -name "*shd*" -delete
