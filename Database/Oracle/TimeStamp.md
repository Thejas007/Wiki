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
