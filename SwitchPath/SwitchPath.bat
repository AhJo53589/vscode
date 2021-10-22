@echo off
setlocal enabledelayedexpansion

@REM ----------------------------------------------------------------------------------------------------
@REM 调试 给剪切板复制文字
@REM set/p = "D:\depot_matchsrpg\matchsrpg\trunk\xls_config\shared\campaign\cfg_campaign.xlsx" < nul | clip
@REM set/p = "//depot_matchsrpg/matchsrpg/trunk/xls_config/shared/campaign/cfg_campaign.xlsx" < nul | clip
@REM set/p = "D:\depot_matchsrpg\matchsrpg\b\1.2.0\xls_config\shared\campaign\cfg_campaign.xlsx" < nul | clip
@REM set/p = "//depot_matchsrpg/matchsrpg/b/1.2.0/xls_config/shared/campaign/cfg_campaign.xlsx" < nul | clip




@REM ----------------------------------------------------------------------------------------------------
@REM 默认输入参数为 \trunk\
if "%1"=="" (
    set param1=\trunk\
) else (
    set param1=%1
)
echo [param] !param1!


@REM ----------------------------------------------------------------------------------------------------
@REM 将剪切板的文字导入 clip.txt
mshta "javascript:var s=clipboardData.getData('text');if(s)new ActiveXObject('Scripting.FileSystemObject').GetStandardStream(1).Write(s);close();"|more >clip.txt


@REM ----------------------------------------------------------------------------------------------------
@REM 输入参数为目标分支路径 如：\trunk\ 或 \b\140\ 或 /trunk/ 或 /b/140/
set target=!param1!

@REM 用来安全检查的字符串，符合路径特点
set trunk=\trunk\
set branch=\b\*\

@REM 清空上次结果
if exist result.txt del /q result.txt

@REM ----------------------------------------------------------------------------------------------------
@REM 按行处理字符串
for /f "delims=" %%i in (clip.txt) do (
    echo --------------------------------------------------

    @REM 要处理的字符串
    set text=%%i
    echo [text] !text!

    @REM 根据路径类型，调整 \ 或 /
    echo !text! | findstr "\\" > nul && (
        set type=1
        set target=!target:/=\!
        set trunk=!trunk:/=\!
        set trunk2=!trunk:\=\\!
        set branch=!branch:/=\!
        set branch2=!branch:\=\\!
    ) || (
        set type=2
        set target=!target:\=/!
        set trunk=!trunk:\=/!
        set trunk2=!trunk:/=\/!
        set branch=!branch:\=/!
        set branch2=!branch:/=\/!
    )

    @REM 计算 source
    set source=
    echo !text! | findstr /i !trunk2! > nul && (
        set source=!trunk!
    ) 
    echo !text! | findstr /i !branch2! > nul && (
        set ver=
        set verStr=%%i
        if !type!==1 (
            call:func_GetVersion1
            set source=\b\!ver!\
        ) else (
            call:func_GetVersion2
            set source=/b/!ver!/
        )
    )
    
    if defined source (
        echo [source] !source!
        echo [target] !target!
        
        @REM 替换 source 和 target
        set text=%%i
        for /f "delims=" %%x in ("!source!=!target!") do set "text=!text:%%x!"

        @REM 输出到 result.txt
        echo !text! >> result.txt
        echo [result] !text!
    ) else (
        echo ERROR: String format does not match, skip this line
    )
)

@REM ----------------------------------------------------------------------------------------------------
@REM 输出到剪切板
if exist result.txt (clip < result.txt)

@REM 结束
goto:lastLine


@REM ----------------------------------------------------------------------------------------------------
@REM 本地路径获取版本号
:func_GetVersion1
    @REM echo Run func_GetVersion1
    set findFlag=0
    :next11
    for /f "delims=\" %%a in ('echo !verStr!') do (
        if !findFlag!==1 (
            set ver=%%a
            goto:next12
        )
        if %%a==b (set findFlag=1)

        set verStr=!verStr:%%a=!
        set verStr=!verStr:~1!
        @REM echo [func_GetVersion1][verStr] !verStr!
        goto:next11
    )
    :next12
goto:eof

@REM p4路径获取版本号
:func_GetVersion2
    @REM echo Run func_GetVersion2
    set findFlag=0
    :next21
    for /f "delims=/" %%a in ('echo !verStr!') do (
        if !findFlag!==1 (
            set ver=%%a
            goto:next22
        )
        if %%a==b (set findFlag=1)

        set verStr=!verStr:%%a=!
        set verStr=!verStr:~1!
        @REM echo [func_GetVersion2][verStr] !verStr!
        goto:next21
    )
    :next22
goto:eof

:lastLine