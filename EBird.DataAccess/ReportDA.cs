using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using EBird.BusinessEntity;
using EBird.Common;
using MySql.Data.MySqlClient;

namespace EBird.DataAccess
{

    public class ReportDA
    {
        public List<DailyStatusReportBO> getWeeklyStatusReport(int userID)
        {
            List<DailyStatusReportBO> objRpt = new List<DailyStatusReportBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spReportWeeklyStatus", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserId", userID));

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objRpt.Add(new DailyStatusReportBO
                            {
                                userFirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                NoOfWebinar = reader["WebinarCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarCount"]),
                                LastWebinarDaysAway = reader["NoDays"] == DBNull.Value ? -1 : Convert.ToInt32(reader["NoDays"]),
                                NextWebinar = reader["NextWebinar"] == DBNull.Value ? string.Empty : Convert.ToString(reader["NextWebinar"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objRpt;
        }

        public List<WebinarInfoListBO> getWebinarWeeklyList(int userID, int NoOfWeeks)
        {
            List<WebinarInfoListBO> objRpt = new List<WebinarInfoListBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spReportWebinarListInWeek", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWeeks", NoOfWeeks));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", userID));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int iDays;
                        string strDays;
                        while (reader.Read())
                        {
                            iDays = reader["DaysToStart"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DaysToStart"]);
                            if (iDays == 0)
                                strDays = "-";
                            else if (iDays == 1)
                                strDays = "0 day";
                            else
                                strDays = iDays.ToString() + " days";

                            objRpt.Add(new WebinarInfoListBO
                            {
                                UpcomingWebinar = reader["webinarTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarTitle"]),
                                When = strDays,
                                Registrants = reader["registered"] == DBNull.Value ? 0 : Convert.ToInt32(reader["registered"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objRpt;
        }

        public List<GeneralWebinarTagsBO> getGeneralWebinarTagValues(int webinarID)
        {
            List<GeneralWebinarTagsBO> objtags = new List<GeneralWebinarTagsBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spGetAllWebinarTagValues", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinar", webinarID));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objtags.Add(new GeneralWebinarTagsBO
                            {
                                WebinarList = new WebinarBE
                                {
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"])
                                },
                                WebinarURLList = new WebinarURLs
                                {
                                    AnalyticsURL = reader["AnalyticURLKey"] == DBNull.Value ? string.Empty : Constant.WebinarAnalyticsBaseURL + Convert.ToString(reader["AnalyticURLKey"]),
                                    AudienceInterfaceURL = reader["AudiURLKey"] == DBNull.Value ? string.Empty : Constant.WebinarViewerBaseURL + Convert.ToString(reader["AudiURLKey"]),
                                    RegistrationURL = reader["RegURLKey"] == DBNull.Value ? string.Empty : Constant.WebinarbaseURL + Convert.ToString(reader["RegURLKey"]),
                                    CommandCenterURL = reader["CoCenterURLKey"] == DBNull.Value ? string.Empty : Constant.WebinarCoCBaseURL + Convert.ToString(reader["CoCenterURLKey"]),
                                    PreviewInterfaceURL = reader["PreviewURLKey"] == DBNull.Value ? string.Empty : Constant.WebinarPreviewBaseURL + Convert.ToString(reader["PreviewURLKey"])
                                },
                                Registrantlist = new Registrants
                               {
                                   FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : reader["FirstName"].ToString()
                               },
                                TimeZoneShortName = reader["TimeZoneShortName"] == DBNull.Value ? string.Empty : reader["TimeZoneShortName"].ToString(),
                                TimeZoneName = reader["TimeZoneName"] == DBNull.Value ? string.Empty : reader["TimeZoneName"].ToString(),
                                UserEmail = reader["UserEmail"] == DBNull.Value ? string.Empty : reader["UserEmail"].ToString()
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objtags;
        }

        public List<themeCSSBO> getThemeCSSValues(int themeID)
        {
            List<themeCSSBO> objtheme = new List<themeCSSBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("select * from tblthemeref where themerefID=" + themeID.ToString(), sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objtheme.Add(new themeCSSBO
                            {
                                ThemeName = reader["themeName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["themeName"]),
                                shade1 = reader["shade1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["shade1"]),
                                shade2 = reader["shade2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["shade2"]),
                                shade3 = reader["shade3"] == DBNull.Value ? string.Empty : Convert.ToString(reader["shade3"]),
                                shade4 = reader["shade4"] == DBNull.Value ? string.Empty : Convert.ToString(reader["shade4"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objtheme;
        }
    }
}
