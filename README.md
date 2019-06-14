# Wiki

# Combining the commits
To squash the last 3 commits into one:

Method 1

git reset --soft HEAD~3

git commit -m "New message for the combined commit"

Pushing the squashed commit
If the commits have been pushed to the remote:
git push origin +name-of-branch
The plus sign forces the remote branch to accept your rewritten history, otherwise you will end up with divergent branches

If the commits have NOT yet been pushed to the remote:
git push origin name-of-branch


Method 2

git rebase -i HEAD~3

pick last comit
use squash/fixup for all other commits

Force push to origin

# SYSTIMESTAMP oracle

select systimestamp at time zone 'UTC',sys_extract_utc(systimestamp at time zone 'UTC') ,sys_extract_utc(SYSTIMESTAMP), cast (sys_extract_utc(SYSTIMESTAMP) as date)from dual;

sys_extract_utc(SYSTIMESTAMP) returns TIMESTAMP(6)

# Convert to UTC
 select sysdate,      
      (sysdate+ NUMTODSINTERVAL(-1*EXTRACT(timezone_hour FROM SYSTIMESTAMP),'hour') +  NUMTODSINTERVAL((-1*SIGN(EXTRACT(timezone_hour FROM SYSTIMESTAMP))*EXTRACT(timezone_minute FROM SYSTIMESTAMP)),'minute')) as utc ,
        SYS_EXTRACT_UTC(SYSTIMESTAMP) from (select cast(sysdate as timestamp) t, SYSTIMESTAMP from dual);
        
FUNCTION GetUtcDateTime
  RETURN DATE
  IS
  l_timezoneHour number;
  l_timezoneMinute number;
  utc date;
  BEGIN
  l_timezoneHour:=EXTRACT(timezone_hour FROM SYSTIMESTAMP);
  l_timezoneMinute:=EXTRACT(timezone_minute FROM SYSTIMESTAMP);
  select (sysdate+ NUMTODSINTERVAL(-1*l_timezoneHour,'hour') +  NUMTODSINTERVAL((-1*SIGN(l_timezoneHour)*l_timezoneMinute),'minute')) into utc from dual;
  RETURN utc;
  END GetUtcDateTime;
  
  # Microsoft style guide
  
  Link :  https://docs.microsoft.com/en-us/style-guide/welcome/
  
  https://docs.microsoft.com/en-us/style-guide/a-z-word-list-term-collections/i/invalid-not-valid
  
Chrome srcset for image 

     https://simpl.info/srcsetwvalues/
