using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Wrapper
{
    public static class WrapperFile
    {
        public static string FileOpenDlg4Exel()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel files (*.xlsx)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return string.Empty;

            return dlg.FileName;
        }

        public static string FileOpenDlg4Log()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Log files (*.log)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return string.Empty;

            return dlg.FileName;
        }

        public static string FileSaveDlg4Log()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Log files (*.log)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return string.Empty;

            return dlg.FileName;
        }
        public static string FileSaveDlg4Log(string strPath )
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = strPath;
            dlg.Filter = "Log files (*.log)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return string.Empty;

            return dlg.FileName;
        }
        public static string FileSaveDlg4Exel()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel files (*.xlsx)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return string.Empty;

            return dlg.FileName + ".xlsx";
        }

        public static string[] MakeFileNameArray(string [] filePathList)
        {
            List<string> list = new List<string>();

            for( int i = 0; i < filePathList.Count(); i++)
            {
                list.Add(GetFileName(filePathList[i]));
            }
            return list.ToArray();
        }
        public static string [] GetFileList(string strPath, string strExt)
        {
            string[] arr = Directory.GetFiles(strPath, strExt);

            return arr;
        }
        public static string GetFileName( string strPath)
        {
            return Path.GetFileName(strPath);
        }
        public static bool FileExist(string strPath)
        {
            return File.Exists(strPath);
        }
        public static string GetFileExt(string strPath)
        {
            string strExt = Path.GetExtension(strPath).ToUpper();
            return strExt;
        }

        public static void DeleteFile(string strPath)
        {
            if (strPath == "") return;

            try
            {
                File.Delete(strPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void CreateFolder(string strPath)
        {
            if( Directory.Exists( strPath )  != true )
            {
                Directory.CreateDirectory(strPath);
            }
        }

        public static void RemoveFolder(string strPath)
        {
            Directory.Delete(strPath);
        }

        public static void EnsureFolderExsistance(string strPath)
        {
            CreateFolder(strPath);
        }

        public static void FileCopy(string strFileSource, string strDestination)
        {
            File.Copy(strFileSource, strDestination);
        }

        public static bool IsDirectory(string strPath)
        {
            FileAttributes attr = File.GetAttributes(strPath);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;

            return false;
        }
        public static string SelectFolder()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            return folderBrowser.SelectedPath;

        }
        
    }
}
