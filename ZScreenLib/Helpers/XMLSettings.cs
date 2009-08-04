﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
   
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using ZSS.ImageUploadersLib;
using ZSS.IndexersLib;
using ZSS.TextUploadersLib;
using ZSS;
using ZScreenLib.Helpers;

namespace ZScreenLib
{
    [XmlRoot("Settings")]
    public class XMLSettings
    {
        public XMLSettings()
        {
            #region "Default Values"

            //~~~~~~~~~~~~~~~~~~~~~
            //  Accounts / FTP
            //~~~~~~~~~~~~~~~~~~~~~

            BackupFTPSettings = true;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Options / Actions Toolbar
            //~~~~~~~~~~~~~~~~~~~~~

            ActionsToolbarMode = false;
            ActionToolbarLocation = Point.Empty;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Options / General
            //~~~~~~~~~~~~~~~~~~~~~

            ShowTrayUploadProgress = true;
            WriteDebugFile = true;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Options / Interaction
            //~~~~~~~~~~~~~~~~~~~~~

            AutoShortenURL = true;
            LimitLongURL = 100;
            MakeTinyURL = false;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Options / Paths
            //~~~~~~~~~~~~~~~~~~~~~
            BackupApplicationSettings = true;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Screenshots / Bevel
            //~~~~~~~~~~~~~~~~~~~~~

            BevelEffect = false;
            BevelEffectOffset = 15;
            BevelFilterType = FilterType.Brightness;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Screenshots / General
            //~~~~~~~~~~~~~~~~~~~~~

            AutoIncrement = 0;
            BackgroundRegionBrightnessValue = -10;
            BackgroundRegionTransparentValue = 100;
            CopyImageUntilURL = false;
            PromptForUpload = false;
            RegionBrightnessValue = 15;
            RegionTransparentValue = 75;

            //~~~~~~~~~~~~~~~~~~~~~
            //  Screenshots / Reflection
            //~~~~~~~~~~~~~~~~~~~~~

            DrawReflection = false;
            ReflectionOffset = 0;
            ReflectionPercentage = 20;
            ReflectionSkew = true;
            ReflectionSkewSize = 25;
            ReflectionTransparency = 255;

            #endregion
        }

        #region Settings

        //~~~~~~~~~~~~~~~~~~~~~
        //  Misc Settings
        //~~~~~~~~~~~~~~~~~~~~~

        public bool RunOnce = false;
        public Size WindowSize = Size.Empty;
        public Point WindowLocation = Point.Empty;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Main
        //~~~~~~~~~~~~~~~~~~~~~

        public ImageDestType ScreenshotDestMode = ImageDestType.IMAGESHACK;
        public ClipboardUriType ClipboardUriMode = ClipboardUriType.FULL;
        public TextDestType TextDestMode = TextDestType.FTP;
        public long ScreenshotDelayTime = 0;
        public Times ScreenshotDelayTimes = Times.Seconds;
        [Category("Screenshots / General"), DefaultValue(false), Description("Show Confirmation for Entire Screen or Active Window")]
        public bool PromptForUpload { get; set; }
        public bool ManualNaming = false;
        public bool ShowCursor = false;
        public bool ShowWatermark = false;
        public bool CropGridToggle = false;
        public Size CropGridSize = new Size(100, 100);
        public string HelpToLanguage = "en";

        //~~~~~~~~~~~~~~~~~~~~~
        //  Hotkeys
        //~~~~~~~~~~~~~~~~~~~~~

        public Keys HotkeyEntireScreen = Keys.PrintScreen;
        public Keys HotkeyActiveWindow = Keys.Alt | Keys.PrintScreen;
        public Keys HotkeyCropShot = Keys.Control | Keys.PrintScreen;
        public Keys HotkeySelectedWindow = Keys.Shift | Keys.PrintScreen;
        public Keys HotkeyClipboardUpload = Keys.Control | Keys.PageUp;
        public Keys HotkeyLastCropShot = Keys.None;
        public Keys HotkeyAutoCapture = Keys.None;
        public Keys HotkeyDropWindow = Keys.None;
        public Keys HotkeyActionsToolbar = Keys.None;
        public Keys HotkeyQuickOptions = Keys.None;
        public Keys HotkeyLanguageTranslator = Keys.None;
        public Keys HotkeyScreenColorPicker = Keys.None;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Capture
        //~~~~~~~~~~~~~~~~~~~~~

        // General

        [Category("Screenshots / General"), DefaultValue(false), Description("Copy image to clipboard until URL is retrieved.")]
        public bool CopyImageUntilURL { get; set; }
        [Category("Screenshots / General"), DefaultValue(75), Description("Region style setting. Must be between these values: 0, 255")]
        public int RegionTransparentValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(15), Description("Region style setting. Must be between these values: -100, 100")]
        public int RegionBrightnessValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(100), Description("Region style setting. Must be between these values: 0, 255")]
        public int BackgroundRegionTransparentValue { get; set; }
        [Category("Screenshots / General"), DefaultValue(-10), Description("Region style setting. Must be between these values: -100, 100")]
        public int BackgroundRegionBrightnessValue { get; set; }

        [Category("Screenshots / Reflection"), DefaultValue(false), Description("Draw reflection bottom of screenshots.")]
        public bool DrawReflection { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(20), Description("Reflection height size relative to screenshot height.")]
        public int ReflectionPercentage { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(255), Description("Reflection transparency start from this value to 0.")]
        public int ReflectionTransparency { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(0), Description("Reflection position will be start: Screenshot height + Offset")]
        public int ReflectionOffset { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(true), Description("Adding skew to reflection from bottom left to bottom right.")]
        public bool ReflectionSkew { get; set; }
        [Category("Screenshots / Reflection"), DefaultValue(25), Description("How much pixel skew left to right.")]
        public int ReflectionSkewSize { get; set; }

        [Category("Screenshots / Bevel"), DefaultValue(false), Description("Add bevel effect to screenshots.")]
        public bool BevelEffect { get; set; }
        [Category("Screenshots / Bevel"), DefaultValue(15), Description("Bevel effect size.")]
        public int BevelEffectOffset { get; set; }
        [Category("Screenshots / Bevel"), DefaultValue(FilterType.Brightness), Description("Bevel effect filter type.")]
        public FilterType BevelFilterType { get; set; }

        // Crop Shot

        public RegionStyles CropRegionStyles = RegionStyles.BACKGROUND_REGION_BRIGHTNESS;
        public bool CropRegionRectangleInfo = true;
        public bool CropRegionHotkeyInfo = true;

        public bool CropDynamicCrosshair = true;
        public int CropInterval = 25;
        public int CropStep = 1;
        public int CrosshairLineCount = 2;
        public int CrosshairLineSize = 25;
        public string CropCrosshairColor = SerializeColor(Color.Black);
        public bool CropShowBigCross = true;
        public bool CropShowMagnifyingGlass = true;

        public bool CropShowRuler = true;
        public bool CropDynamicBorderColor = true;
        public decimal CropRegionInterval = 75;
        public decimal CropRegionStep = 5;
        public decimal CropHueRange = 50;
        public string CropBorderColor = SerializeColor(Color.FromArgb(255, 0, 255));
        public decimal CropBorderSize = 1;
        public bool CropShowGrids = false;

        // Selected Window

        public RegionStyles SelectedWindowRegionStyles = RegionStyles.BACKGROUND_REGION_BRIGHTNESS;
        public bool SelectedWindowRectangleInfo = true;
        public bool SelectedWindowRuler = true;
        public string SelectedWindowBorderColor = SerializeColor(Color.FromArgb(255, 0, 255));
        public decimal SelectedWindowBorderSize = 2;
        public bool SelectedWindowDynamicBorderColor = true;
        public decimal SelectedWindowRegionInterval = 75;
        public decimal SelectedWindowRegionStep = 5;
        public decimal SelectedWindowHueRange = 50;
        public bool SelectedWindowCaptureObjects = true;

        // Interaction

        public decimal FlashTrayCount = 1;
        public bool CaptureEntireScreenOnError = false;
        public bool ShowBalloonTip = true;
        public bool BalloonTipOpenLink = false;
        public bool ShowUploadDuration = false;
        public bool CompleteSound = false;
        public bool CloseDropBox = false;
        public Point LastDropBoxPosition = Point.Empty;
        public bool CloseQuickActions = false;
        [Category("Options / Interaction"), DefaultValue(false), Description("Minimize ZScreen to Taskbar on Close")]
        public bool MinimizeOnClose { get; set; }
        [Category("Options / Interaction"), DefaultValue(false), Description("Optionally shorten the URL after completing a task")]
        public bool MakeTinyURL { get; set; }
        [Category("Options / Interaction"), DefaultValue(100),
        Description("URL Shortening will only be activated if the length of a URL exceeds this value. To always shorten a URL set this value to 0.")]
        public int LimitLongURL { get; set; }
        [Category("Options / Interaction"), DefaultValue(true),
        Description("If you use Clipboard Upload and the clipboard contains a URL then the URL will be shortened instead of performing a text upload.")]
        public bool AutoShortenURL { get; set; }

        // Naming Conventions

        public string ActiveWindowPattern = "%t-%y.%mo.%d-%h.%mi.%s";
        public string EntireScreenPattern = "SS-%y.%mo.%d-%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        [Category("Screenshots / General"), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        // Image Settings

        public int FileFormat = 0;
        public decimal ImageQuality = 90;
        public decimal SwitchAfter = 512;
        public int SwitchFormat = 1;

        public ImageSizeType ImageSizeType = ImageSizeType.DEFAULT;
        public int ImageSizeFixedWidth = 500;
        public int ImageSizeFixedHeight = 500;
        public float ImageSizeRatioPercentage = 50.0f;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Watermark
        //~~~~~~~~~~~~~~~~~~~~~

        public WatermarkType WatermarkMode = WatermarkType.NONE;
        public WatermarkPositionType WatermarkPositionMode = WatermarkPositionType.BOTTOM_RIGHT;
        public decimal WatermarkOffset = 5;
        public bool WatermarkAddReflection = false;
        public bool WatermarkAutoHide = true;

        public string WatermarkText = "%h:%mi";
        public XmlFont WatermarkFont = SerializeFont(new Font("Arial", 8));
        public string WatermarkFontColor = SerializeColor(Color.White);
        public decimal WatermarkFontTrans = 255;
        public decimal WatermarkCornerRadius = 4;
        public string WatermarkGradient1 = SerializeColor(Color.FromArgb(85, 85, 85));
        public string WatermarkGradient2 = SerializeColor(Color.Black);
        public string WatermarkBorderColor = SerializeColor(Color.Black);
        public decimal WatermarkBackTrans = 225;
        public System.Drawing.Drawing2D.LinearGradientMode WatermarkGradientType = System.Drawing.Drawing2D.LinearGradientMode.Vertical;

        public string WatermarkImageLocation = "";
        public bool WatermarkUseBorder = false;
        public decimal WatermarkImageScale = 100;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Text Uploaders & URL Shorteners
        //~~~~~~~~~~~~~~~~~~~~~

        public List<TextUploader> TextUploadersList = new List<TextUploader>();
        public int TextUploaderSelected = 0;

        public List<TextUploader> UrlShortenersList = new List<TextUploader>();
        public int UrlShortenerSelected = 0;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Editors
        //~~~~~~~~~~~~~~~~~~~~~

        public List<Software> ImageEditors = new List<Software>();
        public Software ImageEditor = null;
        public Software TextEditorActive;
        public List<Software> TextEditors = new List<Software>();
        public bool TextEditorEnabled = false;
        public bool ImageEditorAutoSave = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  FTP
        //~~~~~~~~~~~~~~~~~~~~~

        public List<FTPAccount> FTPAccountList = new List<FTPAccount>();
        public int FTPSelected = 0;
        public bool FTPCreateThumbnail = false;
        public bool AutoSwitchFTP = true;
        [Category("Accounts / FTP"), DefaultValue(true), Description("Periodically backup FTP settings.")]
        public bool BackupFTPSettings { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~
        //  DekiWiki
        //~~~~~~~~~~~~~~~~~~~~~

        public List<DekiWikiAccount> DekiWikiAccountList = new List<DekiWikiAccount>();
        public int DekiWikiSelected = 0;
        public bool DekiWikiForcePath = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  HTTP
        //~~~~~~~~~~~~~~~~~~~~~

        // Image Uploaders

        public UploadMode UploadMode = UploadMode.API;
        public decimal ErrorRetryCount = 3;
        public bool ImageUploadRetry = true;
        public bool AddFailedScreenshot = false;
        public bool AutoChangeUploadDestination = true;
        public decimal UploadDurationLimit = 10000;

        // ImageShack

        public string ImageShackRegistrationCode = "";
        public string ImageShackUserName = "";
        public bool ImageShackShowImagesInPublic = false;

        // TinyPic

        public string TinyPicShuk = "";
        public string TinyPicUserName = "";
        public string TinyPicPassword = "";
        public bool RememberTinyPicUserPass = false;
        public bool TinyPicSizeCheck = true;

        // TwitPic

        public string TwitPicUserName = "";
        public string TwitPicPassword = "";
        public TwitPicUploadType TwitPicUploadMode = TwitPicUploadType.UPLOAD_IMAGE_ONLY;
        public bool TwitPicShowFull = true;
        public TwitPicThumbnailType TwitPicThumbnailMode = TwitPicThumbnailType.Thumb;

        // Indexer

        public IndexerConfig IndexerConfig = new IndexerConfig();

        // Custom Image Uploaders

        public List<ImageHostingService> ImageUploadersList = null;
        public int ImageUploaderSelected = 0;

        // Language Translator

        public string FromLanguage = "auto";
        public string ToLanguage = "en";
        public string ToLanguage2 = "?";
        public bool ClipboardTranslate = false;
        public bool AutoTranslate = false;
        public int AutoTranslateLength = 20;

        //~~~~~~~~~~~~~~~~~~~~~
        //  History
        //~~~~~~~~~~~~~~~~~~~~~

        // History Settings

        public HistoryListFormat HistoryListFormat = HistoryListFormat.NAME;
        public int HistoryMaxNumber = 50;
        public bool HistorySave = true;
        public bool HistoryShowTooltips = true;
        public bool HistoryAddSpace = false;
        public bool HistoryReverseList = false;
        [Category("Options / History Settings"), DefaultValue(false), Description("Prefer browser view to navigate uploaded text.")]
        public bool PreferBrowserForText { get; set; }
        [Category("Options / History Settings"), DefaultValue(false), Description("Prefer browser view to navigate uploaded images.")]
        public bool PreferBrowserForImages { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~
        //  Options
        //~~~~~~~~~~~~~~~~~~~~~

        // Actions Toolbar 

        [Category("Options / Actions Toolbar"), DefaultValue(false), Description("Open Actions Toolbar on startup.")]
        public bool ActionsToolbarMode { get; set; }
        [Category("Options / Actions Toolbar"), Description("Action Toolbar Location.")]
        public Point ActionToolbarLocation { get; set; }

        // General - Program

        public bool OpenMainWindow = true;
        public bool ShowInTaskbar = true;
        public bool ShowHelpBalloonTips = true;
        public bool SaveFormSizePosition = true;
        public bool LockFormSize = false;
        public bool AutoSaveSettings = true;

        // General - Check Updates

        public bool CheckUpdates = true;
        public bool CheckUpdatesBeta = false;

        // Proxy Settings

        public List<ProxyInfo> ProxyList = new List<ProxyInfo>();
        public int ProxySelected = 0;
        public ProxyInfo ProxyActive = null;
        public bool ProxyEnabled = false;
        [Category("Options / General"), DefaultValue(true), Description("Showing upload progress percentage in tray icon")]
        public bool ShowTrayUploadProgress { get; set; }

        // Paths

        [Browsable(false)]
        public bool DeleteLocal { get; set; }
        public decimal ScreenshotCacheSize = 50;
        [Category("Options / Paths"), DefaultValue(true), Description("Periodically backup application settings.")]
        public bool BackupApplicationSettings { get; set; }
        [Category("Options / Paths"), Description("Custom Images directory where screenshots and pictures will be stored locally.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string CustomImagesDir { get; set; }
        [Category("Options / Paths"), DefaultValue(false), Description("Use Custom Images directory")]
        public bool UseCustomImagesDir { get; set; }

        [Category("Options / General"), DefaultValue(true), Description("Write debug information into a log file.")]
        public bool WriteDebugFile { get; set; }

        [Category("Options / Watch Folder"), DefaultValue(false), Description("Automatically upload files saved in to this folder.")]
        public bool FolderMonitoring { get; set; }
        [Category("Options / Watch Folder"), Description("Folder monitor path where files automatically get uploaded.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string FolderMonitorPath { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~
        //  Auto Capture
        //~~~~~~~~~~~~~~~~~~~~~

        public AutoScreenshotterJobs AutoCaptureScreenshotTypes = AutoScreenshotterJobs.TAKE_SCREENSHOT_SCREEN;
        public long AutoCaptureDelayTime = 10000;
        public Times AutoCaptureDelayTimes = Times.Seconds;
        public bool AutoCaptureAutoMinimize = false;
        public bool AutoCaptureWaitUploads = true;

        #endregion

        #region Serialization Helpers

        public enum ColorFormat
        {
            NamedColor,
            ARGBColor
        }

        public static string SerializeColor(Color color)
        {
            if (color.IsNamedColor)
            {
                return string.Format("{0}:{1}", ColorFormat.NamedColor, color.Name);
            }
            return string.Format("{0}:{1}:{2}:{3}:{4}", ColorFormat.ARGBColor, color.A, color.R, color.G, color.B);
        }

        public static Color DeserializeColor(string color)
        {
            if (!color.Contains(":")) //For old method
            {
                return Color.Black;
            }

            byte a, r, g, b;

            string[] pieces = color.Split(new[] { ':' });

            ColorFormat colorType = (ColorFormat)Enum.Parse(typeof(ColorFormat), pieces[0], true);

            switch (colorType)
            {
                case ColorFormat.NamedColor:
                    return Color.FromName(pieces[1]);

                case ColorFormat.ARGBColor:
                    a = byte.Parse(pieces[1]);
                    r = byte.Parse(pieces[2]);
                    g = byte.Parse(pieces[3]);
                    b = byte.Parse(pieces[4]);

                    return Color.FromArgb(a, r, g, b);
            }
            return Color.Empty;
        }

        public static XmlFont SerializeFont(Font font)
        {
            return new XmlFont(font);
        }

        public static Font DeserializeFont(XmlFont font)
        {
            return font.ToFont();
        }

        public struct XmlFont
        {
            public string FontFamily;
            public GraphicsUnit GraphicsUnit;
            public float Size;
            public FontStyle Style;

            public XmlFont(Font f)
            {
                FontFamily = f.FontFamily.Name;
                GraphicsUnit = f.Unit;
                Size = f.Size;
                Style = f.Style;
            }

            public Font ToFont()
            {
                return new Font(FontFamily, Size, Style, GraphicsUnit);
            }
        }

        #endregion

        #region I/O Methods

        public void Save()
        {
            new Thread(SaveThread).Start(Program.XMLSettingsFile);
        }

        public void SaveThread(object filePath)
        {
            lock (this)
            {
                Save((string)filePath);
            }
        }

        public void Save(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                XmlSerializer xs = new XmlSerializer(typeof(XMLSettings), TextUploader.Types.ToArray());
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        public static XMLSettings Read()
        {
            return Read(Program.XMLSettingsFile);
        }

        public static XMLSettings Read(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(XMLSettings), TextUploader.Types.ToArray());
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return xs.Deserialize(fs) as XMLSettings;
                    }
                }
                catch (Exception ex)
                {
                    // We dont need a MessageBox when we rename enumerations
                    // Renaming enums tend to break parts of serialization
                    FileSystem.AppendDebug(ex.ToString());
                    OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_SETTINGS };
                    dlg.Title = string.Format("{0} Load Settings from Backup...", ex.Message);
                    dlg.InitialDirectory = Program.appSettings.RootDir;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        return XMLSettings.Read(dlg.FileName);
                    }
                }
            }

            return new XMLSettings();
        }

        #endregion

        #region Other methods

        public object GetFieldValue(string name)
        {
            FieldInfo fieldInfo = this.GetType().GetField(name);
            if (fieldInfo != null) return fieldInfo.GetValue(this);
            return null;
        }

        public bool SetFieldValue(string name, object value)
        {
            FieldInfo fieldInfo = this.GetType().GetField(name);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(this, value);
                return true;
            }
            return false;
        }

        public bool SoftwareExist(string sName)
        {
            foreach (Software iS in this.ImageEditors)
            {
                if (iS.Name == sName) return true;
            }
            return false;
        }

        public bool SoftwareRemove(string sName)
        {
            if (SoftwareExist(sName))
            {
                foreach (Software iS in this.ImageEditors)
                {
                    if (iS.Name == sName)
                    {
                        this.ImageEditors.Remove(iS);
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}