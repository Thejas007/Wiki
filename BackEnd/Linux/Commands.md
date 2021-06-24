# Script permissions
```
perl -p -i -e "s/\r//g" $HOME/scripts/*.*
perl -p -i -e "s/\r//g" $HOME/scripts/.*.sh
perl -p -i -e "s/\r//g" $HOME/scripts/exceptions/*.*

chmod u+x $HOME/scripts/*.ksh
chmod u+x $HOME/scripts/*.sh
chmod u+x $HOME/scripts/.*.sh

vim $HOME/scripts/.set_ora11g_env.sh
cat -v $HOME/scripts/.set_ora11g_env.sh

ls -Al $HOME/scripts
```

# Crontab
```
crontab $HOME/scripts/CRONTAB.txt
vim $HOME/scripts/CRONTAB.txt
sudo cat /var/log/cron
service crond status
service crond restart
```



# Mail
```
mail -s "Test mail" test@gmail.com
mail -s "Test mail" -r "no_replay@gmail.com" test@gmail.com
mail -s "Test mail" test@gmail.com

mail -s "Test mail" test@gmail.com

sudo cat /var/log/maillog

timedatectl 
 - Cron job time zone is set to EST.
CRON_TZ=EST

02 2 * * * mail -s "Test mail" test@gmail.com
cd /usr/share/zoneinfo
```
# SSH
- Windows Openssh-x64
``` ssh -i server1.pem user@ip_address ```

# aws-cli
```
aws configure set profile.awlocal.aws_access_key_id abcd
aws configure set profile.awlocal.aws_secret_access_key efg

aws configure set profile.awlocal.aws_session_token hijk

aws s3 ls s3://bucket-name/test/ --profile awlocal
```


