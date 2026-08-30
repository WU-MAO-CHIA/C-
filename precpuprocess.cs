#!/usr/bin/env -S dotnet run --
#:package Spectre.Console@*

using Spectre.Console;

AnsiConsole.MarkupLine("[bold green]Hello[/] from a file-based app!");

// 條件式編譯
static void ConfigureLogging()
{
#if DEBUG
    Console.WriteLine("Debug logging enabled - verbose output active.");
#else
    Console.WriteLine("Release logging - error only.");
#endif
}

static void ShowPlatformInfo()
{
#if NET10_0_OR_GREATER
    Console.WriteLine("Running on .NET 10 or later.");
#elif NET8_0_OR_GREATER
    Console.WriteLine("Running on .NET 8 or 9.");
#else
    Console.WriteLine("Running on an older .NET version.");
#endif
}

//抑制特定錯誤警告
static void ProcessData()
    {
        try
        {
            // Some operation that might fail
            var data = File.ReadAllText("config.json");
            Console.WriteLine($"Config loaded: {data.Length} characters");
        }
#pragma warning disable CS0168 // Variable is declared but never used
        catch (FileNotFoundException ex)
#pragma warning restore CS0168
        {
            // Fall back to defaults — the exception details aren't needed here
            Console.WriteLine("Config file not found, using defaults.");
        }
    }