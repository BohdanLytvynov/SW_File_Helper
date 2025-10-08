@echo off
dotnet restore SW_File_Helper_Server\SW_File_Helper_Server.sln
msbuild SW_File_Helper_Server\SW_File_Helper_Server.sln

dotnet restore SW_File_Helper.sln
msbuild SW_File_Helper.sln

pause