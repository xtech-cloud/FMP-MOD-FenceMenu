
@echo off

REM !!! Generated by the fmp-cli 1.88.0.  DO NOT EDIT!

md FenceMenu\Assets\3rd\fmp-xtc-fencemenu

cd ..\vs2022
dotnet build -c Release

copy fmp-xtc-fencemenu-lib-mvcs\bin\Release\netstandard2.1\*.dll ..\unity2021\FenceMenu\Assets\3rd\fmp-xtc-fencemenu\
