﻿using System;
using System.Threading;
using System.Windows;

namespace ApprovalUtilities.Utilities
{
    public class ClipboardUtilities
    {
        public static void CopyToClipboard(string text)
        {
            Exception caught = null;
            var t = new Thread(() =>
            {
                try
                {
                    if (OsUtils.IsWindowsOs())
                    {
                        // #warning Look into using Gtk.Clipboard?
                        Clipboard.SetData(DataFormats.Text, text);
                    }
                }
                catch (Exception e)
                {
                    caught = e;
                }
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            if (caught != null)
            {
                throw new Exception("Creating window failed.", caught);
            }
        }
    }
}