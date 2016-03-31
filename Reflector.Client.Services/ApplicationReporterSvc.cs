using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Reflector.Client.Services
{
    public class ApplicationReporterSvc
    {
    }

    #region Classes to retrieve application data and report data
    public class ApplicationScan
    {
        DirectoryInfo directory;

        public ApplicationScan(string directoryPath, string fileName)
        {
            string versionNumber = string.Empty;
            directory = new DirectoryInfo(directoryPath);
            var file = directory.GetFiles(fileName, SearchOption.TopDirectoryOnly);

            if (file != null)
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(directoryPath + fileName);
                versionNumber = versionInfo.ProductVersion;
            }
        }
    }

    public class ReportingScan
    {
        DirectoryInfo directory;

        public ReportingScan(string directoryPath, string reportName)
        {
            string fileDate = string.Empty;
            string fileSize = string.Empty;

            directory = new DirectoryInfo(directoryPath);
            var file = directory.GetFiles(reportName, SearchOption.TopDirectoryOnly).FirstOrDefault();

            if (file != null)
            {
                fileDate = file.LastWriteTime.ToShortDateString();
                fileSize = file.Length.ToString();
            }
        }
    }
    #endregion
}
