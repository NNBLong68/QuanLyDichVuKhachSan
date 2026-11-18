@echo off
setlocal EnableExtensions
chcp 65001 >nul
title Quản Lý Dịch Vụ Khách Sạn

set "APP_DIR=%~dp0"
set "APP_EXE=%APP_DIR%QuanLyDichVuKhachSan.exe"

if not exist "%APP_EXE%" goto :missing

start "" /d "%APP_DIR%" "%APP_EXE%"
exit /b 0

:missing
echo.
echo [LỖI] Không tìm thấy file chạy ứng dụng:
echo   %APP_EXE%
echo.
echo Thư mục này chưa có bản phát hành đầy đủ.
echo Hãy publish bản Release mới và chép đè vào thư mục "QuanLyDichVuKhachSan_App".
echo.
pause
exit /b 1
