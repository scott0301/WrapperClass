using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 이거 걸어야 된다. 참조추가해야된다.
using System.Management;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Diagnostics;

namespace WrapperUnion
{
    public static class WrapperSystemInfo
    {
        public static int GetSelfProcessCount()
        {
            int cnt = 0;

            Process[] procs = Process.GetProcesses();
            Process prcMy = Process.GetCurrentProcess();

            foreach (Process p in procs)
            {
                if (p.ProcessName.Equals(prcMy.ProcessName) == true)
                {
                    cnt++;
                }
            }
            return cnt;
        }

        public static int GetProcessSize()
        {

            System.Diagnostics.Process proc =  Process.GetCurrentProcess();

            int memsize = 0; // memsize in Megabyte
            PerformanceCounter PC = new PerformanceCounter();
            PC.CategoryName = "Process";
            PC.CounterName = "Working Set - Private";
            PC.InstanceName = proc.ProcessName;
            memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024);
            PC.Close();
            PC.Dispose();
            return memsize;
        }
        public static string GetProcessName()
        {
            return Process.GetCurrentProcess().ProcessName;
        }
        public  static void ProcessKill(string psName)
        {
            Process[] processlist = Process.GetProcesses();

            Process[] processname = Process.GetProcessesByName("DNF_Schedule_Manager");

            Process psTarget = null;
            foreach (Process ps in processname)
            {
                if (ps.ProcessName == psName)
                {
                    psTarget = ps;
                }
            }
            if (psTarget != null)
            {
                foreach (Process p in processlist)
                {
                    if (p.ProcessName == psTarget.ProcessName)
                        
                        p.Kill();
                }
            }
        }
        public static void ProcessKillMyself()
        {
            Process ps = Process.GetCurrentProcess();

            Process [] psList = Process.GetProcesses();

            foreach( Process single in psList)
            {
                if( single == ps )
                {
                    ps.Kill();
                }
                
            }
        }
  
        public static string GetBuildInfo()
        {
            string strinfo = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName;

            var parse = strinfo.Split(',');
            string filename = parse[0] + ".exe";

            string strCurPath = Application.StartupPath;
            FileInfo fi = new FileInfo(strCurPath + "\\"+ filename);
            DateTime dt = fi.LastWriteTime;

            string[] t = { dt.Year.ToString(),                 string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };

            string strDateTime = t[0] + t[1] + t[2] + "_" + t[3] + t[4] + t[5];

            return "BUILD INFORMATION : " + strDateTime;
        }
        public static string GetBuildInfo(string strFile )
        {

            string strCurPath = Application.StartupPath;
            FileInfo fi = new FileInfo( strFile);
            DateTime dt = fi.LastWriteTime;

            string[] t = { dt.Year.ToString(),                 string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };

            string strDateTime = t[0] + t[1] + t[2] + "_" + t[3] + t[4] + t[5];

            return "BUILD INFORMATION : " + strDateTime;
        }
        public static string GetCPU_Name()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if( PC.Name == "Name")
                    {
                        value =  PC.Value.ToString();
                        bFind = true;
                        break;
                    }

                }
            }

            return value;
        }
        public static string GetCPU_Manufacturer()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "Manufacturer")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }

                }
            }

            return value;
        }
        public static string GetCPU_ID()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {

                    if (PC.Name == "ProcessorId")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }

                }
            }

            return value;
        }

        public static string GetSystemName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_LogicalDisk");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "SystemName")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }

        public static string GetBios_Version()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_BIOS");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "Version")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }
        public static string GetBios_Manufacturer()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_BIOS");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "Manufacturer")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }
        
        public static string GetBios_ReleaseDate()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_BIOS");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "ReleaseDate")
                    {
                        value = PC.Value.ToString();
                        value = value.Substring(0, 8);

                        string year = value.Substring(0, 4);
                        string month = value.Substring(4, 2);
                        string day = value.Substring(6, 2);

                        value = string.Format("{0}-{1}-{2}", year, month, day);
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }

        public static string GetVGA_Name()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_VideoController");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "Name")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }
        public static string GetVGA_Mode()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_VideoController");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "VideoModeDescription")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }
        public static List<string[]> GetAllDiskInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");

            List<string[]> list = new List<string[]>();

            foreach (ManagementObject share in searcher.Get())
            {
                string[] value = new string[3];

                foreach (PropertyData PC in share.Properties)
                {

                    if (PC.Value != null)
                    {
                        if (PC.Name == "Model")
                        {
                            value[0] = PC.Value.ToString();
                        }
                        if (PC.Name == "SerialNumber")
                        {
                            value[1] = PC.Value.ToString();
                        }
                        if (PC.Name == "Size")
                        {
                            value[2] = PC.Value.ToString();
                            double fSize = Convert.ToDouble(value[2]);
                            fSize /= 1000 * 1000 * 1000;
                            value[2] = fSize.ToString("N0") + " GB";
                        }
                    }
                }
                list.Add(value);

            }
            return list;
        }
        public static string GetDisk_Model()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "Model")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }
        public static string GetDisk_ID()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "SerialNumber")
                    {
                        value = PC.Value.ToString();
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }
        public static string GetDisk_Size()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");

            string value = string.Empty;
            bool bFind = false;
            foreach (ManagementObject share in searcher.Get())
            {
                if (bFind == true) break;
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Name == "Size")
                    {
                        value = PC.Value.ToString();
                        double fSize = Convert.ToDouble(value);
                        fSize /= 1000 * 1000 * 1000;
                        value = fSize.ToString("N0") + " GB";
                        bFind = true;
                        break;
                    }
                }
            }
            return value;
        }

        public static List<string[]> GetAllMacAddress()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_NetworkAdapter");

            List<string[]> list = new List<string[]>();

            foreach (ManagementObject share in searcher.Get())
            {
                string[] value = new string[3] { string.Empty, string.Empty, string.Empty};

                foreach (PropertyData PC in share.Properties)
                {

                    if (PC.Name == "Name")
                    {
                        value[0] = PC.Value.ToString();
                    }
                    if (PC.Name == "MACAddress" && PC.Value != null)
                    {
                        value[1] = PC.Value.ToString();
                    }
                    if (PC.Name == "Manufacturer")
                    {
                        value[2] = PC.Value.ToString();
                    }
                }

                if (value[1] != string.Empty)
                {
                    list.Add(value);
                }

            }
            return list;
        }
         
    }
}
