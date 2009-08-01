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
using System.Linq;
using System.Text;
using ZSS.TextUploadersLib.Helpers;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using ZSS.TextUploadersLib.URLShorteners;

namespace ZSS.TextUploadersLib
{
    [Serializable]
    public abstract class TextUploader : ITextUploader
    {
        // ** THIS HAS TO BE UP-TO-DATE OTHERWISE XML SERIALIZING IS GOING TO FUCK UP ** 
        public static List<Type> Types = new List<Type> { typeof(FTPUploader), typeof(Paste2Uploader), typeof(PastebinCaUploader), typeof (PastebinUploader),
                                                          typeof(SlexyUploader), typeof(SniptUploader), typeof(TinyURLUploader), typeof(ThreelyUploader),
                                                          typeof(KlamUploader), typeof(IsgdUploader), typeof(BitlyUploader), typeof(TextUploader)};

        public TextUploader() { }

        public List<string> Errors { get; set; }

        [XmlIgnore]
        public IWebProxy ProxySettings { get; set; }

        /// <summary>
        /// String that is uploaded
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public abstract string UploadText(TextInfo text);

        /// <summary>
        /// Descriptive name for the Text Uploader
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "TextUploader";
        }

        /// <summary>
        /// String used to test the functionality
        /// </summary>
        public virtual string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public virtual object Settings { get; set; }

        public string UploadTextFromClipboard()
        {
            if (Clipboard.ContainsText())
            {
                return UploadText(TextInfo.FromClipboard());
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string filePath = Clipboard.GetFileDropList()[0];
                if (filePath.EndsWith(".txt"))
                {
                    return UploadTextFromFile(filePath);
                }
            }
            return "";
        }

        public string UploadTextFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {                
                return UploadText(TextInfo.FromFile(filePath));
            }
            return "";
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        /// <summary>
        /// Method to retrieve Link from Header
        /// </summary>
        /// <param name="url"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                string post = string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());
                byte[] data = Encoding.UTF8.GetBytes(post);

                request.Method = "POST";
                request.Proxy = this.ProxySettings;
                request.UserAgent = Application.ProductName + " " + Application.ProductVersion;
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    return response.ResponseUri.OriginalString;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        /// <summary>
        /// Method to return Source of the Response
        /// </summary>
        /// <param name="url"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected string GetResponse2(string url, Dictionary<string, string> arguments)
        {
            try
            {
                url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + x.Value).ToArray());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = this.ProxySettings;
                request.UserAgent = Application.ProductName + " " + Application.ProductVersion;

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return "";
        }

        protected string CombineURL(string url1, string url2)
        {
            if (string.IsNullOrEmpty(url1) || string.IsNullOrEmpty(url2))
            {
                return "";
            }
            if (url1.EndsWith("/"))
            {
                url1 = url1.Substring(0, url1.Length - 1);
            }
            if (url2.StartsWith("/"))
            {
                url2 = url2.Remove(0, 1);
            }
            return url1 + "/" + url2;
        }
    }

    public abstract class TextUploaderSettings
    {
        public abstract string Name { get; set; }
        public abstract string URL { get; set; }
    }
}