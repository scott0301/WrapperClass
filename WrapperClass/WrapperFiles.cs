using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WrapperUnion
{
    public static partial class WrapperFile
    {
        public static string GetFileName(string strPath){return Path.GetFileName(strPath);}
        public static bool FileExist(string strPath){return File.Exists(strPath);}
        public static string GetFileExt(string strPath){string strExt = Path.GetExtension(strPath).ToUpper();return strExt;}
    }
    //*********************************************************************************************
    // IO functions
    //*********************************************************************************************
    public static partial class WrapperFile
    {
        // example
        //blic void WriteListOnFile()
        //
        //  FileStream fs = StreamFileMake(strFilePath);
        //
        //  for (int nIndex = 0; nIndex < 10; nIndex++)
        //  {
        //      string strData = m_ItemList[nIndex].OutData();
        //      WriteLine(strData, ref fs);
        //  }
        //
        //  fs.Close();
        //
        public static FileStream StreamFileMake(string strFileName)
        {
            //*****************************************************************************************
            // Name      - StreamFileMake()
            // Function  - 파일 입출력을 하기 위한 파일 포인터 생성
            // Parameter - (string strFileName) : 파일 이름을 포함한 Full Path
            // Date      - 201210??
            //*****************************************************************************************
            FileStream fs;
            try
            {
                fs = new FileStream(strFileName, FileMode.Open, FileAccess.ReadWrite);
            }
            catch
            {
                MessageBox.Show("File Not Found : " + strFileName + "\n File will be created automatically.");
                fs = new FileStream(strFileName, FileMode.Create, FileAccess.ReadWrite);
            }
            return fs;
        }

        public static void WriteLine(string strLine, ref FileStream fs)
        {
            //*****************************************************************************************
            // Name      - WriteLine()
            // Function  - 입력받은 문자열을 파일에 기록한다, 기록 형태는 FileStreamd에 종속적이다.
            // Parameter - ( string strLine ) : 파일에 기록하고자 하는 문장
            //             ( ref FileStream fs ) : 파일에 접근하고자 하는 파일 스트림
            // Date      - 201210??
            //*****************************************************************************************
            Encoding encoding = Encoding.Default;

            byte[] buffer = encoding.GetBytes(strLine.Normalize());

            fs.Write(buffer, 0, buffer.Length);

        }
        public static FileStream StreamFileContinue(string strFileName)
        {
            //*****************************************************************************************
            // Name      - StreamFileContinue()
            // Function  - 파일 입출력을 하기 위한 파일 포인터 생성 
            //             Concat 붙여넣기 전용 포인터 임.  
            //             붙여넣기 시 Access Mode 는 반드시 Write 모드여야 한다.
            // Parameter - (string strFileName) : 파일 이름을 포함한 Full Path
            // Date      - 201210??
            //*****************************************************************************************
            FileStream fs;
            try
            {
                // C8 !!! Fuck !!!! 
                // Appending only can be run under the option ( FileAccess.Write ) 
                // In case that Read or ReadWrite makes Ultra Fucking Situation!!!

                fs = new FileStream(strFileName, FileMode.Append, FileAccess.Write);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("File Not Found : " + strFileName + "\n File will be created automatically.\n" + ex.ToString());
                fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write);
            }
            return fs;
        }
        
        public static string[] ParseLine(string strLine)
        {
            //*****************************************************************************************
            // Name      - ParseLine()
            // Function  - 입력받은 한줄을 Parsing 해서 Token들을 strign []로 반환한다.
            //             // 주석문은 Skip
            //             @ 구분자
            //             ; 문장의 종료문자
            // Parameter - (string strPath) : 파일 이름을 포함한 Full Path
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strLine))
            {
                var line = strLine;

                // if there is any comment --> return null;
                int nComment = line.IndexOf("//");
                if (nComment >= 0) return null;

                string[] strParsed = line.Split('#');

                int nLast = strParsed.Length - 1;
                strParsed[nLast] = strParsed[nLast].Replace(";", "");

                return strParsed;
            }
            return null;
        }
        public static string[] ReadAllLines(string strPath)
        {
            //*****************************************************************************************
            // Name      - ReadAllLines()
            // Function  - Text File의 모든 라인을 읽어 String [] 로 반환
            // Parameter - (string strPath) : 파일 이름을 포함한 Full Path
            // Date      - 201210??
            //*****************************************************************************************
            if (File.Exists(strPath))
            {
                var allLine = System.IO.File.ReadAllLines(strPath, System.Text.Encoding.Default).Where(line => !line.Equals(string.Empty) && !line.StartsWith(";"));

                string[] strLine = allLine.ToArray<string>();
                return strLine;
            }
            return null;
        }

        public static void CreateFolder(string strPath)
        {
            if (Directory.Exists(strPath) != true)
            {
                Directory.CreateDirectory(strPath);
            }
        }
        public static void RemoveFolder(string strPath)
        {
            Directory.Delete(strPath);
        }
        public static void DeleteAllFilesAndFolders(string strPath)
        {
            //*****************************************************************************************
            // Name      - DeleteAllFilesAndFolders()
            // Function  - 입력받은 경로에 대하여 자기 자신 및 서브 디렉토리를 포함하여 모두 삭제
            // Parameter - (string strPath ) : 삭제하고자 하는 폴더경로 
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strPath))
            {
                DirectoryInfo di = new DirectoryInfo(strPath);
                di.Delete(true);
            }
        }
        public static void RenameFolder(string strSrc, string strNewName)
        {
            //*****************************************************************************************
            // Name      - RenameFolder()
            // Function  - 폴더 이름 변경
            // Parameter - (string strSrc  ) : 변경할 폴더의 Full Path
            //             (string strDest ) : 새로 적용할 이름
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strSrc) && !string.IsNullOrEmpty(strNewName))
            {
                DirectoryInfo di = new DirectoryInfo(strSrc);

                string strCurName = di.Name;
                string strNewDirName = strSrc.Replace(strCurName, strNewName);

                MoveFolder(strSrc, strNewDirName);
            }
        }
        public static void DeleteCurrentFolderFiles(string strPath)
        {
            //*****************************************************************************************
            // Name      - DeleteCurrentFolderFiles()
            // Function  - 입력받은 경로 내부의 파일만 삭제
            // Parameter - (string strPath ) : 삭제할 파일이 포함된 폴더 경로
            // Etc       - SubDirectory는 건드리지 않음
            // Date      - 201210??
            //*****************************************************************************************
            string[] strFileList = GetAllFileList(strPath, string.Empty, false);

            if (strFileList != null)
            {
                foreach (string file in strFileList)
                {
                    File.Delete(file);
                }
            }
        }
        public static void DeleteFile(string strPath)
        {
            if (strPath == "") return;

            try
            {
                File.Delete(strPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void MoveFolder(string strSrc, string strDest)
        {
            //*****************************************************************************************
            // Name      - MoveFolder()
            // Function  - 폴더 이동
            // Parameter - (string strSrc    ) : 이동할 폴더의 경로
            //             (string strDest   ) : 이동되는 위치의 경로
            //  Date     - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strSrc) && !string.IsNullOrEmpty(strDest))
            {
                try
                {
                    Directory.Move(strSrc, strDest);
                }
                catch (System.Exception ex)
                {
                    Console.Write("Input values are  equivalent!!! Fuck.\r\n" + ex.ToString());
                }
            }
        }

        public static void RenameFile(string strSrc, string strNewName)
        {
            //*****************************************************************************************
            // Name      - RenameFile()
            // Function  - 파일의 이름을 변경
            // Parameter - (string strSrc     ) : 변경할 파일의 Full Path
            //             (string strNewName ) : 새로 적용할 이름
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strSrc) && !string.IsNullOrEmpty(strNewName))
            {
                FileInfo fi = new FileInfo(strSrc);
                string strCurName = fi.Name;
                string strNewFileName = strSrc.Replace(strCurName, strNewName);

                MoveFile(strSrc, strNewFileName);
            }
        }
        public static void MoveFile(string strSrc, string strDest)
        {
            //*****************************************************************************************
            // Name      - MoveFile()
            // Function  - 파일 이동 
            // Parameter - (string strSrc    ) : 이동한 원본 파일 경로
            //             (strign strDest   ) : 이동되는 위치의 경로
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strSrc) && !string.IsNullOrEmpty(strDest))
            {
                try
                {
                    File.Move(strSrc, strDest);
                }
                catch (System.Exception ex)
                {
                    Console.Write("Input values are  equivalent!!! Fuck.\r\n" + ex.ToString());
                }
            }
        }
        public static void FileCopy(string strFileSource, string strDestination)
        {
            File.Copy(strFileSource, strDestination, true);
        }
    }

    //*********************************************************************************************
    //  Dialog Related
    //*********************************************************************************************
    public static partial class WrapperFile
    {
       
        public static string OpenSelectFile()
        {
            //*****************************************************************************************
            // Name     - OpenSelectFile()
            // Function - 파일 선택 다이얼로그를 통해 파일 선택후 선택한 파일명 반환
            // Etc      - 모든 파일에 대한 필터 적용
            // Date     - 201210??
            //*****************************************************************************************
            String strFilename = string.Empty;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "All files (*.*)|*.*";

                dlg.InitialDirectory = Application.StartupPath;
                dlg.Title = "Select any file";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    strFilename = dlg.FileName;
                    return strFilename;
                }
                if (strFilename == String.Empty)
                {
                    return string.Empty;
                }
                return string.Empty;
            }

        }
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
        public static string OpenFileSaveDialog()
        {
            //*****************************************************************************************
            // Name     - OpenFileSaveDialog()
            // Function - 파일 저장을 위한 폴더 선택과 파일입력을 받고 입력받은 파일명을 반환한다.
            // Date     - 201210??
            //*****************************************************************************************
            string strFileName = string.Empty;
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "All files (*.*)|*.*";

                dlg.InitialDirectory = Application.StartupPath;
                dlg.Title = "Select any file";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    strFileName = dlg.FileName;
                    return strFileName;
                }
                if (strFileName == String.Empty)
                {
                    return string.Empty;
                }
                return string.Empty;
            }
        }
        public static string FileSaveDlg4Log()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Log files (*.log)|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
                return string.Empty;

            return dlg.FileName;
        }
        public static string FileSaveDlg4Log(string strPath)
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
        public static string FileSelectDialog_Zip()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Zip Files|*.zip";
            openFileDialog1.Title = "Select a Zip File";

            string strFile = string.Empty;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                strFile = openFileDialog1.FileName;
            }
            return strFile;
        }
    }

    //*********************************************************************************************
    //  File List related functions
    //*********************************************************************************************
    public static partial class WrapperFile
    {
        public static string[] MakeFileNameArray(string[] arrFullPathList)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < arrFullPathList.Count(); i++)
            {
                list.Add(GetFileName(arrFullPathList[i]));
            }
            return list.ToArray();
        }
        public static string[] GetAllFileList(string strPath, string strExt, bool bSubFolder)
        {
            //*****************************************************************************************
            // Name      - GetAllFileList()
            // Function  - 폴더 내에 존재하는 파일의 목록 반환
            // Parameter -  (string strPath   ) : 파일 리스트 획득을 위한 경로
            //              (string strExt    ) : 검색할 확장자
            //              (bool   bSubFolder) : 하위 폴더 검색 여부
            // Etc       -  [Examples]
            //              [1]. GetAllFileList( "c:\\Test", "",      false ) : 해당 폴더에서만 모든 파일 획득
            //              [2]. GetAllFileList( "c:\\Test", "*.bmp", false ) : 해당 폴더에서 bmp 파일만 획득
            //              [3]. GetAllFileList( "c:\\Test", "*.bmp", true  ) : 해당 폴더 및 모든 하위 폴더에서 bmp 파일 획득
            //              [4]. GetAllFileList( "c:\\Test", "*.*", true    ) : 해당 폴더 및 모든 하위 폴더의 모든 파일 획득
            // Warnning  - Disk Drive에 걸면 주옥됨.
            // Date      - 201210??
            //*****************************************************************************************
            if (string.IsNullOrEmpty(strPath))
            {
                return null;
            }
            // Extension Input with Self Search  
            else if (!string.IsNullOrEmpty(strExt) && bSubFolder == false)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(strPath, strExt, SearchOption.TopDirectoryOnly);
                    return filePaths;
                }
                catch { /* what the fuck */ }
            }
            // Extension Input with SubDirector Search
            else if (!string.IsNullOrEmpty(strExt) && bSubFolder == true)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(strPath, strExt, SearchOption.AllDirectories);
                    return filePaths;
                }
                catch { /* what the fuck */ }
            }
            return null;
        }
        public static string[] GetFileList_Itself(string strDirPath, string strExt)
        {
            string[] arr = Directory.GetFiles(strDirPath, strExt);

            return arr;
        }
        public static string[] GetFileList_within_subdir(string strDirPath)
        {
            string[] entries = Directory.GetFileSystemEntries(strDirPath, "*", SearchOption.AllDirectories);
            return entries;
        }
        public static string[] GetAllFiles_with_Extention(string strDirPath, string strExtension)
        {
            List<string> listFiles = new List<string>();

            string[] arrFileList = GetFileList_within_subdir(strDirPath);

            for (int i = 0; i < arrFileList.Length; i++)
            {
                string strFilePath = arrFileList.ElementAt(i);

                if (isValidPath(strFilePath) == true)
                {
                    if (IsDirectory(strFilePath) == false)
                    {
                        string strFileName = GetFileName(strFilePath);
                        string strExt = GetFileExt(strFilePath);

                        if (strExt == strExtension)
                        {
                            listFiles.Add(strFilePath);
                        }
                    }
                }
            }
            return listFiles.ToArray();
        }
       
    }
    //*********************************************************************************************
    // Folder related Functions
    //*********************************************************************************************
    public static partial class WrapperFile
    {
        // 171121 
        public static bool isValidImageFile(string strPath)
        {
            string extension = GetFileExt(strPath).ToUpper();

            bool bValid = false;

            if (extension == ".BMP" || extension == "PNG" || extension == "JPG " || extension == "JPEG")
            {
                bValid = true;
            }
            return bValid;
        }
        public static void EnsureFolderExsistance(string strPath) { CreateFolder(strPath); }
        public static bool IsDirectory(string strPath)
        {
            FileAttributes attr = File.GetAttributes(strPath);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;

            return false;
        }
        public static string SelectFolderAndGetName()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            return folderBrowser.SelectedPath;

        }
       
    }

    //*********************************************************************************************
    // Path Functions
    //*********************************************************************************************
    public static partial class WrapperFile
    {
        public static string GetFolderPath(string strFullPath){return Path.GetDirectoryName(strFullPath);}
        public static string GetCurrentPath()
        {
            //*****************************************************************************************
            // Name     - GetCurrentPath()
            // Function - 현재 실행 경로 반환
            // Date     - 201210??
            //*****************************************************************************************
            return Application.StartupPath;
        }
        public static string getPath_Desktop(){return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);}
        public static string getPath_CurretWorking(){return Directory.GetCurrentDirectory();}
        public static bool isValidPath(string strFilePath)
        {
            bool bValid = true;
            if (strFilePath.Contains('~') == true || strFilePath.Contains('$') == true)
            {
                bValid = false;
            }
            return bValid;
        }
    }

    //*********************************************************************************************
    // Size & Time functions
    //*********************************************************************************************
    public static partial class WrapperFile
    {
        public static string[] GetFileLastTime(string strPath)
        {
            //*****************************************************************************************
            // Name      - GetFileLastTime()
            // Function  - 마지막으로 파일이 업데이트 된 시간을 반환
            // Parameter - 시간조회를 할 파일의 경로
            // Return    - string array type으로 반환, 배열 인덱스별 시간 데이터 저장
            //           - [0] == Year [1] == month [2] == Day [3] == Hour [4] == Min   [5] == Second
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strPath))
            {
                FileInfo fi = new FileInfo(strPath);
                DateTime dt = fi.LastWriteTime;

                string[] strTime = { dt.Year.ToString(),                 string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };
                return strTime;
            }
            return null;
        }

        public static string GetFolderSize(string strPath)
        {
            //*****************************************************************************************
            // Name      - GetFolderSize()
            // Function  - 입력받은 폴더 경로의 전체 크기를 반환
            // Parameter - (string strPath) : 폴더의 Full Path
            // Etc       - 수치 크기를 string type으로 변환, "XXX,XXX,XXX,XXX byte" 혹은 "XX.X GB" 형태로 가공하려면 MakeDateSizeTo() 함수 이용
            //             관련 내용은 아래 MakeDataSizeTo()함수 참조
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strPath))
            {
                string[] strFileList = GetAllFileList(strPath, "*.*", true);

                if (strFileList != null)
                {
                    double dblTotalSize = 0;
                    foreach (string file in strFileList)
                    {
                        double dblFilesize = Convert.ToDouble(GetFileSize(file));
                        dblTotalSize += (dblFilesize);
                    }

                    //decimal nResult = Math.Ceiling(Convert.ToDecimal(dblTotalSize));
                    return dblTotalSize.ToString();
                }
            }
            return string.Empty;
        }
        public static string GetFileSize(string strPath)
        {
            //*****************************************************************************************
            // Name      - GetFileSize()
            // Function  - 입력 받은 File Path에 해당하는 파일의 크기를 반환
            // Parameter - (string strPath) : 파일 이름을 포함한 Full Path
            // Date      - 201210??
            //*****************************************************************************************
            if (!string.IsNullOrEmpty(strPath))
            {
                FileInfo fi = new FileInfo(strPath);
                return fi.Length.ToString();
            }
            return string.Empty;
        }
        public static string[] GetDiskSize(string strPath)
        {
            //*****************************************************************************************
            // Name      - GetDiskSize()
            // Function  -  선택한, 혹은 임의의 드라이브에 대한 크기를 반환한다.
            // Parameter - (strign strDrive) 드라이브 경로명 [ example ] c:\\, d:\\
            // Etc       - 반환은 string array 이며,  index 구성은 다음고 같다. [0] == Total size [1] == free size 이다
            // Date      - 201210??
            //*****************************************************************************************

            if (string.IsNullOrEmpty(strPath)) return null;

            DriveInfo dvi = new DriveInfo(strPath);

            string[] strDiskInfo = { dvi.TotalSize.ToString(), dvi.TotalFreeSpace.ToString() };

            return strDiskInfo;
        }
        public static string MakeDataSizeTo(string strSize, string strUnit)
        {
            //*****************************************************************************************
            // Name      - MakeDataSizeTo()
            // Function  - 파일이나 폴더 크기를 구한 후 출력형태에 맞게 가공해서 string 으로 반환한다.
            // Parameter - (string strSize ) - 구해진 크기 입력 값
            //             (string strUnit)  - 가공하고자 하는 출력 단위, 대소문자 구분 없음 [ "byte" | "giga" ]
            // Etc       -  [Example]  입력값  strSize = 1234567890 인 경우,
            //              MakeDataSize( strSize, "byte");  --> 리턴 값 == 1,534,567,890
            //              MakeDataSize( strSize, "giga");  --> 기턴 값 == 1.5
            // Date      - 201210??
            //*****************************************************************************************
            if (string.IsNullOrEmpty(strSize))
            {
                return string.Empty;
            }

            decimal datasize = Convert.ToDecimal(strSize);

            string strResult = string.Empty;
            strUnit = strUnit.ToUpper();

            switch (strUnit)
            {
                case "BYTE":
                    strResult = string.Format("{0:N}", datasize);
                    strResult = strResult.Remove(strResult.Length - 3);
                    break;
                case "GIGA":
                    const decimal GIGA_BYTE = 1073741824.0m;
                    strResult = string.Format("{0,0:N}", datasize / GIGA_BYTE);
                    strResult = strResult.Remove(strResult.Length - 1);
                    break;
            }

            return strResult;
        }
    }
}
