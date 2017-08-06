using DBTools;
using MailerAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TrainingStudyFeedback.Repositoies
{
    class UnityRepository
    {
        private DB _dc { get; set; }

        public UnityRepository()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ITEIP.RinnaiPortal"].ToString();
            _dc = new DB(connectionString);
            if (!_dc.TestConnection()) { throw new Exception("連線失敗!"); }
        }


        public void SandMailHandlerFromStudyOpinion()
        {
            var info = new MailInfo()
            {
                AddresseeTemp = "{0}@rinnai.com.tw",
                DomainPattern = @".*@rinnai.com.tw*",
                Subject = "[資訊部測試，請勿理會]通知: 請填寫學員意見調查表以及受訓心得報告!",
                //Subject = "公告，若近期您收到類似的郵件，為系統測試通知逾期簽核郵件，請不予理會!通知: 逾期兩日以上簽核資訊!",
                CC = new List<string>() { "juncheng.liu", "susan.kao" },
                Body = new StringBuilder()
            };

            StringBuilder mailBody = new StringBuilder();

            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">Dear {0} 學員：</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">提醒通知您~請於{1}前 至<a href=""http://portal.rinnai.com.tw/"">Portal簽核系統</a>&nbsp;完成 『<span style=""color: #0000ff;"">學員意見調查</span>』及『<span style=""color: #0000ff;"">受訓心得報告</span>』</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">訓練目標：{2}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">上課學員：{0}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">開課日期：{3}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">上課時間：自{4}點{5}分 至{6}點{7}分 止共{8}小時</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">課程名稱：{9}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">&nbsp;</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">管理部：人力資源課 高淑娟</p>");

            try
            {
                var repo = new UnityRepository();
                var studyOpinionData = repo.QueryFilterStudyOpinion();
                if (studyOpinionData == null) { return; }
                //'定義Key值
                var filterData = studyOpinionData.Rows.Cast<DataRow>().GroupBy(row => row["Email"], row => row);

                if (filterData.Count() == 0) { return; }
                //遍歷filterData裡的資料
                filterData.All(data =>
                {
                    //遍歷filterData裡的row並將他插入到剛被複製的datatable
                    data.All(r =>
                    {
                        //收件人
                        //info.To = data.Key.ToString();
                        info.To = "juncheng.liu@rinnai.com.tw";
                        //信件內容 由BodyToTable將新table轉成html tag
                        string emailBody = mailBody.ToString();
                        var empName = r["EmployeeName"];
                        var classFeebackLimitDate = Convert.ToDateTime(r["開始日"]).AddDays(7).ToTaiwanDate();
                        var trPurpose = r["訓練目標"];
                        var classStartDate = Convert.ToDateTime(r["開始日"]).ToFullTaiwanDate();
                        var classStartTimeHours = r["上課時間"].ToString().Substring(0, 2);
                        var classStartTimeMin = r["上課時間"].ToString().Substring(2, 2);
                        var classEndTimeHours = r["下課時間"].ToString().Substring(0, 2);
                        var classEndTimeMin = r["下課時間"].ToString().Substring(2, 2);
                        var mathTime = Convert.ToDouble(classEndTimeHours) - Convert.ToDouble(classStartTimeHours);
                        var className = r["CLNAME"];
                        info.Body = new StringBuilder(string.Format(mailBody.ToString(),
                            empName,
                            classFeebackLimitDate,
                            trPurpose,
                            classStartDate,
                            classStartTimeHours,
                            classStartTimeMin,
                            classEndTimeHours,
                            classEndTimeMin,
                          mathTime,
                          className
                          ));
                        //寄信
                        Mailer mailer = new Mailer(info);
                        mailer.SendMail();
                        return true;
                    });
                    return true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Exception Occured! {0}", ex.Message));
                Console.ReadKey();
            }
        }

        public void SandMailHandlerFromFeedBack()
        {
            var info = new MailInfo()
            {
                AddresseeTemp = "{0}@rinnai.com.tw",
                DomainPattern = @".*@rinnai.com.tw*",
                Subject = "[資訊部測試，請勿理會]通知: 請填寫學員意見調查表以及受訓心得報告!",
                //Subject = "公告，若近期您收到類似的郵件，為系統測試通知逾期簽核郵件，請不予理會!通知: 逾期兩日以上簽核資訊!",
                CC = new List<string>() { "juncheng.liu","susan.kao" },
                Body = new StringBuilder()
            };

            StringBuilder mailBody = new StringBuilder();

            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">Dear {0} 學員：</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">提醒通知您~請於{1}前 至<a href=""http://portal.rinnai.com.tw/"">Portal簽核系統</a>&nbsp;完成 『<span style=""color: #0000ff;"">學員意見調查</span>』及『<span style=""color: #0000ff;"">受訓心得報告</span>』</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">訓練目標：{2}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">上課學員：{0}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">開課日期：{3}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">上課時間：自{4}點{5}分 至{6}點{7}分 止共{8}小時</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">課程名稱：{9}</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">&nbsp;</p>");
            mailBody.AppendLine(@"<p style=""padding-left: 30px;"">管理部：人力資源課 高淑娟</p>");

            try
            {
                var repo = new UnityRepository();
                var feedbackOpinionData = repo.QueryFilterStudyFeedback();
                if (feedbackOpinionData == null) { return; }
                //'定義Key值
                var filterData = feedbackOpinionData.Rows.Cast<DataRow>().GroupBy(row => row["Email"], row => row);

                if (filterData.Count() == 0) { return; }
                //遍歷filterData裡的資料
                filterData.All(data =>
                {
                    //遍歷filterData裡的row並將他插入到剛被複製的datatable
                    data.All(r =>
                    {
                        //收件人
                        //info.To = data.Key.ToString();
                        info.To = "juncheng.liu@rinnai.com.tw";
                        //信件內容 由BodyToTable將新table轉成html tag
                        string emailBody = mailBody.ToString();
                        var empName = r["EmployeeName"];
                        var classFeebackLimitDate = Convert.ToDateTime(r["開始日"]).AddDays(7).ToTaiwanDate();
                        var trPurpose = r["訓練目標"];
                        var classStartDate = Convert.ToDateTime(r["開始日"]).ToFullTaiwanDate();
                        var classStartTimeHours = r["上課時間"].ToString().Substring(0, 2);
                        var classStartTimeMin = r["上課時間"].ToString().Substring(2, 2);
                        var classEndTimeHours = r["下課時間"].ToString().Substring(0, 2);
                        var classEndTimeMin = r["下課時間"].ToString().Substring(2, 2);
                        var mathTime = Convert.ToDouble(classEndTimeHours) - Convert.ToDouble(classStartTimeHours);
                        var className = r["CLNAME"];
                        info.Body = new StringBuilder(string.Format(mailBody.ToString(),
                            empName,
                            classFeebackLimitDate,
                            trPurpose,
                            classStartDate,
                            classStartTimeHours,
                            classStartTimeMin,
                            classEndTimeHours,
                            classEndTimeMin,
                          mathTime,
                          className
                          ));
                        //寄信
                        Mailer mailer = new Mailer(info);
                        mailer.SendMail();
                        return true;
                    });
                    return true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Exception Occured! {0}", ex.Message));
                Console.ReadKey();
            }
        }





        /// <summary>
        /// 執行查詢已上課7日心學員意見為填寫者
        /// </summary>
        /// <returns></returns>
        private DataTable QueryFilterStudyOpinion()
        {
            /*
            篩出上完課超過七日未填寫學員意見調查表以及受訓心得報告者--
            等於0就符合學員意見未填寫--
            */
            var strSQL =
                            @"
                            select r.CLID,r.CLNAME ,r.PURPOSE as '訓練目標',r.START_DATE '開始日',r.START_TIME '上課時間', 
                            r.END_TIME as '下課時間',  n.TABLE_ID as '表格類別', e.EmployeeID, e.EmployeeName,e.ADAccount as 'Email', count(*) as '未達答題課程數'
                            from RTCLASS r 
                            left join NUMERIC_ANSWER n on n.CLID = r.clid
                            inner join STUDENTS s on s.CLID = r.CLID 
                            inner join RinnaiPortal.dbo.Employee e on e.EmployeeID = s.SID
                            where  n.qno is null
                            and (n.TABLE_ID is null or n.TABLE_ID = '01')
                            AND DATEDIFF (day,r.START_DATE,GETDATE()) = 7
                            AND s.DUTY = 'True'
                            group by r.CLID, r.CLNAME ,r.PURPOSE, r.START_DATE,r.START_TIME,r.END_TIME, n.TABLE_ID, e.EmployeeID, e.EmployeeName,e.ADAccount
                            ";
            return _dc.QueryForDataTable(strSQL, null);
        }
        /// <summary>
        /// 執行查詢已受訓7日內未填寫心得報告者
        /// </summary>
        /// <returns></returns>
        private DataTable QueryFilterStudyFeedback()
        {
            string strSQL = @"
                            select r.CLID,r.CLNAME ,r.PURPOSE as '訓練目標',r.START_DATE '開始日',r.START_TIME '上課時間', 
                            r.END_TIME as '下課時間',  c.TABLE_ID as '表格類別', e.EmployeeID, e.EmployeeName,e.ADAccount as 'Email', count(*) as '未達答題課程數'
                            from RTCLASS r 
                            left join CHARACTER_ANSWER c on c.CLID = r.clid
                            inner join STUDENTS s on s.CLID = r.CLID 
                            inner join RinnaiPortal_Formal.dbo.Employee e on e.EmployeeID = s.SID
                            where  c.qno is null
                            and (c.TABLE_ID is null or c.TABLE_ID = '02')
                            AND DATEDIFF (day,r.START_DATE,GETDATE()) = 7
                            AND s.DUTY = 'True'
                            group by r.CLID, r.CLNAME ,r.PURPOSE, r.START_DATE,r.START_TIME,r.END_TIME, c.TABLE_ID, e.EmployeeID, e.EmployeeName,e.ADAccount
                            ";
            return _dc.QueryForDataTable(strSQL, null);
        }
    }
    /// <summary>
    /// 日期當地化處理函士
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// To the full taiwan date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ToFullTaiwanDate(this DateTime datetime)
        {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

            return string.Format("{0}年{1}月{2}日",
                taiwanCalendar.GetYear(datetime),
                datetime.Month,
                datetime.Day);
        }

        /// <summary>
        /// To the simple taiwan date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ToTaiwanDate(this DateTime datetime)
        {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

            return string.Format("{0}.{1}.{2}",
                taiwanCalendar.GetYear(datetime),
                datetime.Month,
                datetime.Day);
        }
    }
}
