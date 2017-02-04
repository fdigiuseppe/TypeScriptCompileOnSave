using System;
using Microsoft.VisualStudio.Shell.Interop;
using TypeScriptCompileOnSave;

internal static class Logger
{
    private static IVsOutputWindowPane _pane;
    private static IVsOutputWindow _output = VsHelpers.GetService<SVsOutputWindow, IVsOutputWindow>();

    public static void Log(object message)
    {
        try
        {
            if (EnsurePane())
            {
                _pane.OutputString(DateTime.Now.ToString() + ": " + message + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Write(ex);
        }
    }

    private static bool EnsurePane()
    {
        if (_pane == null)
        {
            var guid = Guid.NewGuid();
            _output.CreatePane(ref guid, Vsix.Name, 1, 1);
            _output.GetPane(ref guid, out _pane);
        }

        return _pane != null;
    }
}