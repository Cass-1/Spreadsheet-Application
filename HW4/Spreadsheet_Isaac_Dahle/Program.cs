// Copyright (c) Cass Dahle 11775278.
// Licensed under the GPL v3.0 License. See LICENSE in the project root for license information.

namespace HW4;

using System;
using Avalonia;
using Avalonia.ReactiveUI;

/// <inheritdoc cref="Program" />
#pragma warning disable SA1404
#pragma warning restore SA1404
internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
#pragma warning disable SA1600
    public static void Main(string[] args) => BuildAvaloniaApp()
#pragma warning restore SA1600
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    // ReSharper disable once IdentifierTypo
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
