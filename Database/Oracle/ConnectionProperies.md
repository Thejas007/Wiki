- WIth only Decr Pool Size =3 
  11 connection created, After 3 minutes connections are not closed automaticaaly. 
  after 6 minutes closed 3 connections . Connections left was 8 .
  after 9 minutes closed 3 more connections . Connections left was 5 .


- With Connection Lifetime=10; Decr Pool Size =3 
  10 connection created, 
  After 3 minutes connections are not closed automaticaaly. 
  after 6 minutes closed 3 connections . Connections left was 7 .
  hit new request , 2 connection got closed .  Connections left was 5 .
  hit new request , 2 connection got closed .  Connections left was 3 .

  Basically it closed expired connections and used active connection for the request.
  
  Query for active conections : 
  ```
  select sid, program,logon_time, prev_exec_start, wait_time_micro/1000 from v$session where program like '%w3wp.exe%' order by logon_time desc;
  ```
