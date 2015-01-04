using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace FolderDesigner
{
    public class FolderIconChanger
    {
        private readonly AttributeChanger _attributeChanger;

        public FolderIconChanger(AttributeChanger attribChanger)
        {
            _attributeChanger = attribChanger;
        }

        public void CreateDesktopIni(string directoryPath, string relativeIconPath)
        {
            var FolderSettings = new LPSHFOLDERCUSTOMSETTINGS();
            FolderSettings.dwMask = 0x10;
            FolderSettings.pszIconFile = relativeIconPath;
            FolderSettings.iIconIndex = 0;

            UInt32 FCS_READ = 0x00000001;
            UInt32 FCS_FORCEWRITE = 0x00000002;
            UInt32 FCS_WRITE = FCS_READ | FCS_FORCEWRITE;

            string pszPath = directoryPath;
            UInt32 HRESULT = SHGetSetFolderCustomSettings(ref FolderSettings, pszPath, FCS_FORCEWRITE);

            _attributeChanger.AddAttributes(Path.Combine(directoryPath, "desktop.ini"), FileAttributes.Hidden);
            _attributeChanger.AddAttributes(Path.Combine(directoryPath, "foldericon.ico"), FileAttributes.Hidden);

        }

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        static extern UInt32 SHGetSetFolderCustomSettings(ref LPSHFOLDERCUSTOMSETTINGS pfcs, string pszPath, UInt32 dwReadWrite);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct LPSHFOLDERCUSTOMSETTINGS
        {
            public UInt32 dwSize;
            public UInt32 dwMask;
            public IntPtr pvid;
            public string pszWebViewTemplate;
            public UInt32 cchWebViewTemplate;
            public string pszWebViewTemplateVersion;
            public string pszInfoTip;
            public UInt32 cchInfoTip;
            public IntPtr pclsid;
            public UInt32 dwFlags;
            public string pszIconFile;
            public UInt32 cchIconFile;
            public int iIconIndex;
            public string pszLogo;
            public UInt32 cchLogo;
        }
    }



}
