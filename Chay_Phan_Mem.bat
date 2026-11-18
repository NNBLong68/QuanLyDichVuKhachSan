@echo off
setlocal
chcp 65001 >nul
title Quản Lý Dịch Vụ Khách Sạn

rem ============================================================
rem  CHAY PHAN MEM
rem  Chi mo ban da publish trong thu muc QuanLyDichVuKhachSan_App.
rem  Khong build. Nguoi cap nhat code tu publish ban moi vao do.
rem ============================================================

set "ROOT=%~dp0"
set "APP_DIR=%ROOT%QuanLyDichVuKhachSan_App"
set "APP_EXE=%APP_DIR%\QuanLyDichVuKhachSan.exe"

if exist "%APP_EXE%" (
    start "" /d "%APP_DIR%" "%APP_EXE%"
    exit /b 0
)

echo.
echo [LỖI] Chưa có bản chạy đã publish trong thư mục "QuanLyDichVuKhachSan_App".
echo Hãy publish bản Release mới và chép vào thư mục đó.
echo.
pause
exit /b 1
