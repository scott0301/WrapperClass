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
        //**************************************************************************************
        #region TIMECODE_FOR_SAVE

        public static string GetTimeCode4Save_MM_DD_HH_MM_SS()
        {
            string strTime = string.Format("{0:00}_{1:00}_{2:00}{3:00}{4:00}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return strTime;
        }
        public static string GetTimeCode4Save_MM_DD_HHMMSS()
        {
            DateTime curr = DateTime.Now;
            string strTime = string.Format("{0:00}_{1:00}_{2:00}{3:00}{4:00}", curr.Month, curr.Day, curr.Hour, curr.Minute, curr.Second);
            return strTime;
        }
        public static string GetTimeCode4Save_HH_MM_SS_MMM()
        {
            DateTime curr = DateTime.Now;
            return string.Format("{0:00}_{1:00}_{2:00}_{3:000}", curr.Hour, curr.Minute, curr.Second, curr.Millisecond);

        }
        public static string GetTImeCode4Save_YYYY_MM_DD_HH_MM_SS_MMM()
        {
            DateTime curr = DateTime.Now;
            return string.Format("{0:0000}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}_{6:000}", curr.Year, curr.Month, curr.Day, curr.Hour, curr.Minute, curr.Second, curr.Millisecond);
        }
        public static DateTime Conv_TimeCode_2_DateTime_YYYY_MM_DD_HH_MM_SS_MMM(string strDate)
        {
            strDate = strDate.Replace("-", "");
            int year = Convert.ToInt32(strDate.Substring(0, 4));
            int month = Convert.ToInt32(strDate.Substring(4, 2));
            int day = Convert.ToInt32(strDate.Substring(6, 2));
            int hour = Convert.ToInt32(strDate.Substring(8, 2));
            int min = Convert.ToInt32(strDate.Substring(10, 2));
            int sec = Convert.ToInt16(strDate.Substring(12, 2));
            int msec = Convert.ToInt16(strDate.Substring(14, 3));

            return new DateTime(year, month, day, hour, min, sec, msec);
        }
        public static string GetTimeCode4Save_YYYY_MM_DD()
        {
            DateTime t = DateTime.Now;
            return string.Format("{0:0000}_{1:00}_{2:00}", t.Year, t.Month, t.Day);
        }
        public static DateTime Conv_TimeCode_2_DateTime_YYYY_MM_DD(string strDate)
        {
            strDate = strDate.Replace("-", "");
            int year = Convert.ToInt32(strDate.Substring(0, 4));
            int month = Convert.ToInt32(strDate.Substring(4, 2));
            int day = Convert.ToInt16(strDate.Substring(6, 2));

            return new DateTime(year, month, day);
        }
        
        #endregion
        //**************************************************************************************


        //**************************************************************************************
        #region TIME CODE
        public static string GetTactCode_HH_MM_SS(double fSec)
        {
            int nHour = Convert.ToInt32(fSec / 3600);
            int nMin  = Convert.ToInt32((fSec - nHour) / 60);
            int nSec  = Convert.ToInt32((fSec - nHour) % 60);

            string strTime = string.Format("{0:00}:{1:00}:{2:00}", nHour, nMin, nSec);
            return strTime;
        }
        public static string GetTactCode_DD_HH_MM_SS(double fTotalSec)
        {
            int nDay = 0;
            int nHour = 0;
            int nMin = 0;
            int nSec = 0;

            string strTime = string.Empty;

            const double SEC_DAY = 86400.0;
            const double SEC_HOUR = 3600.0;
            const double SEC_MIN = 60.0;
            
            // if exceed days unit
            if (fTotalSec >= SEC_DAY)
            {
                nDay = Convert.ToInt32( Math.Floor(fTotalSec / SEC_DAY));
                fTotalSec -= nDay * SEC_DAY;

                nHour = Convert.ToInt32(Math.Floor(fTotalSec / SEC_HOUR));
                fTotalSec -= nHour * SEC_HOUR;

                nMin = Convert.ToInt32( Math.Floor(fTotalSec  / SEC_MIN));
                fTotalSec -= nMin * SEC_MIN;

                nSec = Convert.ToInt32(fTotalSec % 60);
                strTime = string.Format("{0:00}|{1:00}:{2:00}:{3:00}", nDay, nHour, nMin, nSec);
            }
            else
            {
                nHour = Convert.ToInt32( Math.Floor(fTotalSec / SEC_HOUR));
                fTotalSec -= nHour * SEC_HOUR;

                nMin = Convert.ToInt32( Math.Floor(fTotalSec / SEC_MIN));
                fTotalSec -=  nMin * SEC_MIN;

                nSec = Convert.ToInt32(fTotalSec % 60);
                strTime = string.Format("{0:00}:{1:00}:{2:00}", nHour, nMin, nSec);

            }


            return strTime;
        }
        public static string GetTimeCode_MM_DD_HHMMSS()
        {
            string strTime = string.Format("{0:00}-{1:00}|{2:00}:{3:00}:{4:00}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return strTime;
        }
        public static string Conv_DateTime_2_TimeCode_YYMMDD_HHMMSS(DateTime dt)
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}_{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            strTime = strTime.Remove(0, 2);
            return strTime;
        }
        
        
        public static string TIME_GetTImeCode_YYYY_MM_DD()
        {
            DateTime curr = DateTime.Now;
            string strTime = string.Format("[{0:0000}-{1:00}-{2:00}]", curr.Year, curr.Month, curr.Day);
            return strTime;
        }
        public static string TIME_GetTimeCode_MMDD_HHMMSS_MMM()
        {
            DateTime curr = DateTime.Now;
            string strTime = string.Format("[{0:00}{1:00}_{2:00}:{3:00}:{4:00}_{5:00}]", curr.Month, curr.Day, curr.Hour, curr.Minute, curr.Second, curr.Millisecond);
            return strTime;
        }

        public static string GetTimeCode_YYYYMMDD_HHMMSS()
        {
            string strTime = string.Format("{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return strTime;
        }
        public static string GetTimeCode_YYMMDD_HHMNSS()
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}_{3:00}{4:00}{5:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            strTime = strTime.Remove(0, 2);
            return strTime;
        }
        public static string GetTimeCode_YYMMDD()
        {
            string strTime = string.Format("{0:00}{1:00}{2:00}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            strTime = strTime.Remove(0, 2);
            return strTime;

        }
        public static string GetTimeCode_HHMMSS()
        {
            DateTime dtCurTime = DateTime.Now;
            return string.Format("{0:00}:{1:00}:{2:00}", dtCurTime.Hour, dtCurTime.Minute, dtCurTime.Second);
        }
        public static string GetTimeCode_HHMMSS(DateTime dt)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", dt.Hour, dt.Minute, dt.Second);
        }
        public static string GetTimeCode_HHMMSS(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }

        #endregion
        //**************************************************************************************


      
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
        public static int GetDiffSec(DateTime DtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(DtStart);
            int nDiff = Convert.ToInt32(ts.Seconds);

            return nDiff;
        }
        public static int GetDiffTotalSec(DateTime DtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(DtStart);
            int nDiff = Convert.ToInt32(ts.TotalSeconds);

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

        public static string GetPeriodTime_HH_MM_SS(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan ts = dtEnd.Subtract(dtStart);

            int nDiffDay = Convert.ToInt32(ts.Days);
            int nDiffHour = Convert.ToInt32(ts.Hours);
            int nDiffMin = Convert.ToInt32(ts.Minutes);
            int nDiffSec = Convert.ToInt32(ts.Seconds);

            string time = string.Format("{0:00}:{1:00}:{2:00}", nDiffDay * 24 + nDiffHour, nDiffMin, nDiffSec);
            return time;
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
