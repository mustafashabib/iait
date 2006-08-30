using System;
using System.Collections.Generic;
using System.Text;

namespace AlbumArtTagger
{
    public class Log
    {

        private static int _iCurrLine = 1;

        public static string Filename
        {
            get { return Environment.CurrentDirectory + "\\log.txt"; }
        }
        private static StringBuilder _logInfo = new StringBuilder();
        public static string Info
        {
            get
            {
                return _logInfo.ToString();
            }
        }
        
        public static void LogLine(string lineInfo)
        {
           // System.IO.File.AppendText(_strFilename).WriteLine("{0}: ({1}) {2}", _iCurrLine, DateTime.Now.Date.ToLongDateString(), lineInfo);
            _logInfo.Append(String.Format("{0}:\t({1})\t{2}" + Environment.NewLine, _iCurrLine, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), lineInfo));
            _iCurrLine++;
        }
    }
}
