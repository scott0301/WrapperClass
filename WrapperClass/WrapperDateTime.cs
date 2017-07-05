using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WrapperUnion
{
    public class WrapperDateTime
    {
        public static DateTime GetTimeEliminatedDatime(DateTime dtTime)
        {
            DateTime dtNew = new DateTime(dtTime.Year, dtTime.Month, dtTime.Day, 0, 0, 0);
            return dtNew;
        }
        public static string GetTodayString()
        {
            return DateTime.Now.ToShortDateString();    
        }
        public static DateTime GetTodayDatetime()
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtToday = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            return dtToday;
        }
        public static string GetTimeCode()
        {
            string strTime = string.Format("{0:00}-{1:00}|{2:00}:{3:00}:{4:00}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return strTime;
        }
        public  static string GetTimeCode4Save()
        {
            string strTime = string.Format("{0:00}_{1:00}_{2:00}{3:00}{4:00}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return strTime;
        }
        public static string GetTimeCodeYMD_HMS()
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}_{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            strTime = strTime.Remove(0, 2);
            return strTime;
        }
        public static string GetTimeCode_YMD()
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            strTime = strTime.Remove(0, 2);
            return strTime;

        }
        public static string GetTimeCode_HMS()
        {
            DateTime dtCurTime = DateTime.Now;
            return string.Format("{0:00}:{1:00}:{2:00}", dtCurTime.Hour, dtCurTime.Minute, dtCurTime.Second);
        }
        public static string GetTimeCode_HMS(DateTime dt)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", dt.Hour, dt.Minute, dt.Second);
        }
        public static string GetTimeCode_HMS(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }
        public static void GetWeekList(DateTime startDate, DateTime EndDate)
        {
            TimeSpan ts = EndDate.Subtract(startDate);
            int nPeriod = Convert.ToInt32(ts.Days);

            List<string> listWeek = new List<string>();

            for (int i = 0; i < nPeriod; i++)
            {
                startDate = startDate.AddDays(1);
                listWeek.Add(startDate.DayOfWeek.ToString());
            }
        }
        public static string Conv_DateCode2Date(string datecode)
        {
            string year = string.Concat(datecode[0], datecode[1]);
            string month = string.Concat(datecode[2], datecode[3]);
            string day = string.Concat(datecode[4], datecode[5]);

            string date = string.Format("20{0}-{1}-{2}", year, month, day);
            return date;
        }
        /// <summary> 170522
        /// Program Elapsed Time Calculation --> 시:분:초
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtTarget"></param>
        /// <returns></returns>
        public static string TIME_Get_Substracted_Time(DateTime dtStart, DateTime dtTarget)
        {
            TimeSpan ts = dtTarget.Subtract(dtStart);

            int nDay = ts.Days;
            int nHour = ts.Hours;
            int nMin = ts.Minutes;
            int nSec = ts.Seconds;


            nHour = nHour + (nDay * 24);

            return string.Format("{0:00}:{1:00}:{2:00}", nHour, nMin, nSec);
        }

        // Get Different Days
        public static int GetPeriod(DateTime DtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(DtStart);
            int nPeriod = Convert.ToInt32(ts.Days);

            return nPeriod;
        }
        // Get Different Hours
        public static int GetDiffHour(DateTime DtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(DtStart);
            int nDiff = Convert.ToInt32(ts.Hours);

            return nDiff;
        }
        // Get Different Mins
        public static int GetDiffMin(DateTime DtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(DtStart);
            int nDiff = Convert.ToInt32(ts.Minutes);

            return nDiff;
        }

        public static int GetDiffAbsoluteMin(DateTime DtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(DtStart);

            int nDiffDay = Convert.ToInt32(ts.Days);
            int nDiffHour = Convert.ToInt32(ts.Hours);
            int nDiffMin = Convert.ToInt32(ts.Minutes);

            int nAbsoluteMinDiff = (nDiffDay * 24 * 60) + (nDiffHour * 60) + nDiffMin;
            return nAbsoluteMinDiff;
        }
        public static int GetDiffDay(DateTime DtStart, DateTime DtEnd)
        {
            TimeSpan ts = DtEnd - DtStart;
            return ts.Days;
        }
        public static DateTime FirstDay(DateTime dt) // 선택달의 첫번째 날자 2015-06-01
        {
            DateTime firstDay = dt.AddDays(1 - dt.Day);

            return firstDay;
        }
        public static DateTime LastDay(DateTime dt) // 선택달의 마지막 날짜 2015-06-30
        {
            DateTime lastDay = dt.AddMonths(1).AddDays(0 - dt.Day);
            return lastDay;
        }
        public static DateTime GetFirstSunday(DateTime dt) // 첫번째 일요일은 언제인가
        {
            DateTime firstDay = FirstDay(dt);
            DateTime firstSunday = firstDay.AddDays(0 - (int)(firstDay.DayOfWeek));
            return firstSunday;
        }
        public static  DateTime GetLastSunday(DateTime dt) // 마지막 일요일은 언제인가
        {
            DateTime lastDay = LastDay(dt);

            DateTime lastSunday = lastDay.AddDays(0 - (int)(lastDay.DayOfWeek));
            return lastSunday;
        }
        public  static int GetWeekCount(DateTime dt) // 이번달의 주 개수는?
        {
            DateTime firstSunday = GetFirstSunday(dt);
            DateTime lastSunday = GetLastSunday(dt);

            int WeekCount = ((lastSunday.DayOfYear - firstSunday.DayOfYear) / 7) + 1;
            return WeekCount;
        }

        public static string Conv_DateTimeTo_YMD_HMS(DateTime dt)
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}_{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            strTime = strTime.Remove(0, 2);
            return strTime;
        }
        public static string Conv_YMD(DateTime dt)
        {
            string s = string.Format("{0:00}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);
            return s;
        }
        public static DateTime Conv_YMD_HMSToDateTime(string strTimeCode )
        {
            DateTime dt = DateTime.Now;

            try
            {
                string[] parse = strTimeCode.Split('_');

                string year = "20" + parse[0].Substring(0, 2);
                string month = parse[0].Substring(2, 2);
                string day = parse[0].Substring(4, 2);
                string hour = parse[1].Substring(0, 2);
                string min = parse[1].Substring(2, 2);
                string sec = parse[1].Substring(4, 2);
                
                dt = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec));
            }
            catch(Exception ex)
            {
                string s = ex.ToString();
            }
            
            return dt;
        }
        public static string GetBuildInfo()
        {
            string strinfo = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName;

            var parse = strinfo.Split(',');
            string filename = parse[0] + ".exe";

            string strCurPath = Application.StartupPath;
            FileInfo fi = new FileInfo(strCurPath);
            DateTime dt = fi.LastWriteTime;

            string[] t = { dt.Year.ToString(),                 string.Format("{0:00}", dt.Month),  string.Format("{0:00}", dt.Day  ), 
                                      string.Format("{0:00}", dt.Hour  ), string.Format("{0:00}", dt.Minute), string.Format("{0:00}", dt.Second) };

            string strDateTime = t[0] + t[1] + t[2] + "_" + t[3] + t[4] + t[5];

            return "BUILD INFORMATION : " + strDateTime;
        }
    }
    public class Tact
    {
        public System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private List<string> TactList = new List<string>();
        public string m_unit = string.Empty;

        public string Check(string workerName, Action action)
        {
            this.sw.Reset();
            this.sw.Start();

                action();

            this.sw.Stop();

            double fTime =  sw.ElapsedMilliseconds;
            if (fTime > 1000)
            {
                fTime /= 1000.0;
                m_unit = " sec";
            }
            else m_unit = " msec";

            return string.Format("{0} : {1} {2}", workerName, fTime.ToString("F4"), m_unit);
        }
    }
}
