@echo off

set working_dir=%~dp0

cd /d D:\Tools\Wox
Wox.exe installplugin %working_dir%bin\Release\Pushbill.wox
