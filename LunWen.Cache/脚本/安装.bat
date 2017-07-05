@echo off

cd /d %~dp0
cd..

call redis-server --service-install redis.windows-service.conf --loglevel verbose

echo;

echo ��װ���

echo; 
echo; 


pause	