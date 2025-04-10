﻿using Microsoft.Win32;
using TIME.Library.Interfaces;

namespace TIME.Library.Services;

public class FileDialog : IFileDialog
{
    public string OpenFile(string filter, string title)
    {
        var dialog = new OpenFileDialog
        {
            Filter = filter,
            Title = title
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }

    public string SaveFile(string filter, string title)
    {
        var dialog = new SaveFileDialog
        {
            Filter = filter,
            Title = title
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}
