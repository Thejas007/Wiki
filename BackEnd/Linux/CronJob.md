## Format

                    minute         0-59
                    hour           0-23
                    day of month   0-31
                    month          0-12 (or names, see below)
                    day of week    0-7 (0 or 7 is Sun, or use names)

## Daylight Savings and Cron
  If Cron has a job scheduled to run at 2 am and one at 3 am how would those jobs be affected by daylight savings time?

  When the time shifts back an hour does the time go from 2:59:59 am to 2:00:00 am directly? Meaning that the 2 am job would run twice and the 3 am job would run once? Or is does the time first change to 3:00:00 am and then 2:00:00 am causing both jobs to run twice?

  When the time shifts forward an hour does the time go from 1:59:59 am to 3:00:00 am causing the 2 am job to not run and the 3 am job to run once? Or does the time shift from 2:00:00 to 3:00:00 am causing both jobs to run once?

  In short what I am wondering is when gaining an hour does the 3 am hour happen once or twice and and losing an hour does the 2 am hour happen at all. I have not been able to find anything about this when looking on Google.
  
  Answer
  The answer would be dependent on the variant/extension of cron you are using. Some variants do not handle the Daylight Saving Time, leading to missing jobs and twice run of the job.
  
  >cron checks each minute to see if its spool directory's modtime (or the modtime on /etc/crontab) has changed
  
  > Local time changes of less than three hours, such as  those  caused  by
   the  start or end of Daylight Saving Time, are handled specially.  This
   only applies to jobs that run at a specific time and jobs that are  run
   with  a    granularity  greater  than  one hour.  Jobs that run more fre-
   quently are scheduled normally.

   If time has moved forward, those jobs that would have run in the inter-
   val that has been skipped will be run immediately.  Conversely, if time
   has moved backward, care is taken to avoid running jobs twice.

   Time changes of more than 3 hours are considered to be  corrections  to
   the clock or timezone, and the new time is used immediately.
   
   So, whenever the time shifts may be 2:59:59 or at 3:00:00, cron's taking care of the job runs by handling the situation and running only the missed ones and avoids running the  already ran jobs.
   
   ### Examples
    Every Minute
    * * * * *
    Every Five Minutes
    */5 * * * *
    Every 10 Minutes
    */10 * * * *
    Every 15 Minutes
    */15 * * * *
    Every 30 Minutes
    */30 * * * *
    Every Hour
    0 * * * *
    Every Two Hours
    0 */2 * * *
    Every Six Hours
    0 */6 * * *
    Every 12 Hours
    0 */12 * * *
    During the Work Day
    */5 9-17 * * *
    Every day at Midnight
    0 0 * * *
    Every Two Weeks
    0 0 * * Sun [ $(expr $(date +%W) % 2) -eq 1 ] && /path/to/command
    At the Start of Every Month
    0 0 1 * *
    On January 1st at Midnight
    0 0 1 1 *

## CRON_TZ
  >The CRON_TZ specifies the time zone specific for the cron table. User type into the chosen table times in the time of the specified time zone. The time into log is taken from    local time zone, where is the daemon running.
  >CRON_TZ=EST so that job will run on EST time
  
       # use /bin/sh to run commands, no matter what /etc/passwd says
       SHELL=/bin/sh
       # mail any output to `paul', no matter whose crontab this is
       MAILTO=paul
       #
       CRON_TZ=Japan
       # run five minutes after midnight, every day
       5 0 * * *       $HOME/bin/daily.job >> $HOME/tmp/out 2>&1
       # run at 2:15pm on the first of every month -- output mailed to paul
       15 14 1 * *     $HOME/bin/monthly
       # run at 10 pm on weekdays, annoy Joe
       0 22 * * 1-5    mail -s "It's 10pm" joe%Joe,%%Where are your kids?%
       23 0-23/2 * * * echo "run 23 minutes after midn, 2am, 4am ..., everyday"
       5 4 * * sun     echo "run at 5 after 4 every sunday"
