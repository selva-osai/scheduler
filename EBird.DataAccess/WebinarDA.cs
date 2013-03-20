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
    public class WebinarDA
    {

        public List<WebinarBE> GetMyCompanyWebinarListDA(int clientID, string fromDate, string toDate, string webinarTitle)
        {
            EBirdUtility objUtil = new EBirdUtility();

            string sql1 = DBQuery.sqlGetMyCompanyWebinar;

            if (webinarTitle != "")
                sql1 += " and webinarTitle like '%" + webinarTitle + "%'";
            if (fromDate != "" && toDate != "")
                sql1 += " and startDate BETWEEN '" + fromDate + "' AND '" + toDate + "'";
            else if (fromDate == "" && toDate != "")
                sql1 += " and createdOn <= '" + toDate + "'";
            else if (toDate == "" && fromDate != "")
                sql1 += " and startDate >= '" + fromDate + "'";

            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    ClientID = clientID,
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? 0 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? 0 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        // 2/19/2013 - renamed the method to get my webinar for adv search 
        // sqlGetMyCompanyWebinar -> sqlGetMyWebinar, clientID -> userID
        public List<WebinarBE> GetMyWebinarAdvSearchListDA(int userID, string fromDate, string toDate, string webinarTitle, string advFilter)
        {
            EBirdUtility objUtil = new EBirdUtility();
            bool isAdvFilter = false;

            string advTitle = string.Empty;
            string advFieldSearch = string.Empty;
            string advStDate = string.Empty;
            string advEnDate = string.Empty;
            string sql2 = string.Empty;

            if (advFilter.Trim() != "")
            {
                ArrayList arr1 = objUtil.StringToArrayList(advFilter, new char[] { ';' });
                if (arr1.Count == 4)
                {
                    isAdvFilter = true;

                    advTitle = arr1[0].ToString();
                    advFieldSearch = arr1[1].ToString();
                    advStDate = arr1[2].ToString();
                    advEnDate = arr1[3].ToString();
                    if (advTitle != "")
                    {
                        switch (advFieldSearch.ToUpper())
                        {
                            case "TITLE":
                                sql2 += "webinarTitle like '%" + advTitle + "%'";
                                break;
                            case "DESCRIPTION":
                                sql2 += "description like '%" + advTitle + "%'";
                                break;
                            case "ALL":
                                sql2 += "webinarTitle like '%" + advTitle + "%' or description like '%" + advTitle + "%'";
                                break;
                        }
                    }
                    if (advStDate != "" && fromDate != "")
                    {
                        advStDate = objUtil.FormDBDate(Convert.ToDateTime(advStDate));
                        TimeSpan span = (Convert.ToDateTime(fromDate)).Subtract(Convert.ToDateTime(advStDate));
                        if (span.Days < 0)
                        {
                            fromDate = advStDate;
                        }
                    }
                    if (advEnDate != "" && toDate != "")
                    {
                        advEnDate = objUtil.FormDBDate(Convert.ToDateTime(advEnDate));
                        TimeSpan span1 = (Convert.ToDateTime(advEnDate)).Subtract(Convert.ToDateTime(toDate));
                        if (span1.Days < 0)
                        {
                            toDate = advEnDate;
                        }
                    }
                }
            }

            string sql1 = DBQuery.sqlGetMyWebinar;

            if (webinarTitle != "")
                sql1 += " and webinarTitle like '%" + webinarTitle + "%'";
            if (fromDate != "" && toDate != "")
                sql1 += " and startDate BETWEEN '" + fromDate + "' AND '" + toDate + "'";
            else if (fromDate == "" && toDate != "")
                sql1 += " and createdOn <= '" + toDate + "'";
            else if (toDate == "" && fromDate != "")
                sql1 += " and startDate >= '" + fromDate + "'";

            if (sql2 != string.Empty)
            {
                sql1 += " and (" + sql2 + ")";
            }

            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@createdBy", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    //ClientID = clientID,
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? 0 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? 0 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetMyWebinarListDA(int userID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetMyWebinar, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@createdBy", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {

                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? -1 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? -1 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? -1 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetMyWebinarListDA(int userID, string fromDate, string toDate, string webinarTitle, string orderby = "")
        {

            EBirdUtility objUtil = new EBirdUtility();

            string sql1 = DBQuery.sqlGetMyWebinar;

            if (webinarTitle != "")
                sql1 += " and webinarTitle like '%" + webinarTitle + "%'";
            if (fromDate != "" && toDate != "")
                sql1 += " and startDate BETWEEN '" + fromDate + "' AND '" + toDate + "'";
            else if (fromDate == "" && toDate != "")
                sql1 += " and createdOn <= '" + toDate + "'";
            else if (toDate == "" && fromDate != "")
                sql1 += " and startDate >= '" + fromDate + "'";

            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@createdBy", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? 0 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? 0 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["WebinarStatus"] == DBNull.Value ? string.Empty : reader["WebinarStatus"].ToString()
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetMyWebinarListDA(int userID, string fromDate, string toDate, string searchString, string searchField, int Registered, int LiveViewed, int OnDemandViewed)
        {

            EBirdUtility objUtil = new EBirdUtility();

            string sql1 = DBQuery.sqlGetMyWebinar;

            if (searchString != "")
            {
                if (searchField == "Title")
                    sql1 += " and webinarTitle like '%" + searchString + "%'";
                else if (searchField == "Description")
                    sql1 += " and Description like '%" + searchString + "%'";
                else
                    sql1 += " and (webinarTitle like '%" + searchString + "%' or Description like '%" + searchString + "%')";
            }

            if (fromDate != "" && toDate != "")
                sql1 += " and startDate BETWEEN '" + fromDate + "' AND '" + toDate + "'";
            else if (fromDate == "" && toDate != "")
                sql1 += " and createdOn <= '" + toDate + "'";
            else if (toDate == "" && fromDate != "")
                sql1 += " and startDate >= '" + fromDate + "'";

            sql1 += " and registered >= " + Registered.ToString() + " and live >= " + LiveViewed.ToString() + " and onDemand >= " + OnDemandViewed;

            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@createdBy", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? -1 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? -1 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? -1 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetMyCompanyWebinarListDA(int clientID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetMyCompanyWebinar, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    ClientID = clientID,
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? -1 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? -1 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? -1 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetMyCompanyWebinarListDA(int clientID, string fromDate, string toDate, string searchString, string searchField, int Registered, int LiveViewed, int OnDemandViewed)
        {
            EBirdUtility objUtil = new EBirdUtility();

            string sql1 = DBQuery.sqlGetMyCompanyWebinar;

            if (searchString != "")
            {
                if (searchField == "Title")
                    sql1 += " and webinarTitle like '%" + searchString + "%'";
                else if (searchField == "Description")
                    sql1 += " and Description like '%" + searchString + "%'";
                else
                    sql1 += " and (webinarTitle like '%" + searchString + "%' or Description like '%" + searchString + "%')";
            }

            if (fromDate != "" && toDate != "")
                sql1 += " and startDate BETWEEN '" + fromDate + "' AND '" + toDate + "'";
            else if (fromDate == "" && toDate != "")
                sql1 += " and createdOn <= '" + toDate + "'";
            else if (toDate == "" && fromDate != "")
                sql1 += " and startDate >= '" + fromDate + "'";

            sql1 += " and registered >= " + Registered.ToString() + " and live >= " + LiveViewed.ToString() + " and onDemand >= " + OnDemandViewed;


            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    ClientID = clientID,
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? -1 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? -1 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? -1 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetWebinarDetailDA(int webinarID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetWebinarDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {

                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    RecurrenceCriteria = reader["recurrCriteria"] == DBNull.Value ? string.Empty : Convert.ToString(reader["recurrCriteria"]),
                                    Registered = reader["registered"] == DBNull.Value ? -1 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? -1 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? -1 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"]),
                                    DeliveryChannel = reader["DeliveryChannel"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DeliveryChannel"]),
                                    isPublic = reader["isPublic"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPublic"]),
                                    isPasswordRequired = reader["isPassRequired"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPassRequired"]),
                                    WebinarPassword = reader["webinarPassword"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarPassword"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public string GetMinMaxWebinarDates(int userID)
        {
            string rtnVal = string.Empty;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetWebinarMaxMinDates", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserID", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rtnVal = reader[0].ToString() + "#" + reader[1].ToString() + "#" + reader[2].ToString();
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnVal;
        }

        public List<WebinarBE> GetMyCompanyRecycleWebinarListDA(int clientID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetMyCompanyRecycleWebinar, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    ClientID = clientID,
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? 0 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? 0 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public List<WebinarBE> GetMyRecycleWebinarListDA(int userID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetMyRecycleWebinar, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@createdBy", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    EndTime = reader["endTime"] == DBNull.Value ? string.Empty : reader["endTime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    Registered = reader["registered"] == DBNull.Value ? 0 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? 0 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        public bool IsWebinarRequiredPassword(int clientID)
        {
            bool rtn = false;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.GetConfigWebinarPassword, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@configID", Convert.ToInt32(ClientConfigMaster.ClinetConfig_Schedula_Webinar_Password_Required)));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                            rtn = true;
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtn;
        }

        #region Presenter

        public List<PresenterBE> GetPresenterDetail(int userID)
        {
            List<PresenterBE> objPresenterBE = new List<PresenterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserPresenter, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPresenterBE.Add(new PresenterBE
                                {
                                    PresenterID = reader["PresenterID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PresenterID"]),
                                    PresenterName = reader["PresenterName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PresenterName"]),
                                    Title = reader["Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Title"]),
                                    Organization = reader["Organization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Organization"]),
                                    Bio = reader["presenterBio"] == DBNull.Value ? string.Empty : Convert.ToString(reader["presenterBio"]),
                                    UserID = reader["UserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserID"]),
                                    ImageDocID = reader["imgDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["imgDocID"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objPresenterBE;
        }

        public List<PresenterBE> GetPresenterDetail(int ID, string IDType)
        {
            string str1 = DBQuery.sqlUserPresenter;

            if (IDType != "UID")
                str1 = DBQuery.sqlUserPresenterID;

            List<PresenterBE> objPresenterBE = new List<PresenterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(str1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@userID", ID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPresenterBE.Add(new PresenterBE
                                {
                                    PresenterID = reader["PresenterID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PresenterID"]),
                                    PresenterName = reader["PresenterName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PresenterName"]),
                                    Title = reader["Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Title"]),
                                    Organization = reader["Organization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Organization"]),
                                    Bio = reader["presenterBio"] == DBNull.Value ? string.Empty : Convert.ToString(reader["presenterBio"]),
                                    UserID = reader["UserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserID"]),
                                    ImageDocID = reader["imgDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["imgDocID"])
                                    // addedWebinarID
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objPresenterBE;
        }

        public void UpdatePresenterDetail(PresenterBE objPreBE)
        {
            try
            {
                string sql1 = string.Empty;

                List<PresenterBE> objP1 = GetPresenterDetail(objPreBE.UserID);
                if (objP1.Count > 0)
                    sql1 = DBQuery.sqlUserPresenterUpdate;
                else
                    sql1 = DBQuery.sqlUserPresenterInsert;

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@presenterName", objPreBE.PresenterName));
                            sqlCmd.Parameters.Add(new MySqlParameter("@title", objPreBE.Title));
                            sqlCmd.Parameters.Add(new MySqlParameter("@organization", objPreBE.Organization));
                            sqlCmd.Parameters.Add(new MySqlParameter("@presenterBio", objPreBE.Bio));
                            sqlCmd.Parameters.Add(new MySqlParameter("@userID", objPreBE.UserID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdatePresenterDetail(PresenterBE objPreBE, int webinarID)
        {
            int rtnVal = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand("spUpdatePresenterInfo", sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.Add(new MySqlParameter("pPresenterID", objPreBE.PresenterID));
                            sqlCmd.Parameters.Add(new MySqlParameter("pPresenterName", objPreBE.PresenterName));
                            sqlCmd.Parameters.Add(new MySqlParameter("pTitle", objPreBE.Title));
                            sqlCmd.Parameters.Add(new MySqlParameter("pOrganization", objPreBE.Organization));
                            sqlCmd.Parameters.Add(new MySqlParameter("pPresenterBio", objPreBE.Bio));
                            sqlCmd.Parameters.Add(new MySqlParameter("pUserID", objPreBE.UserID));
                            sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            //sqlCmd.ExecuteNonQuery();
                            MySqlDataReader reader = sqlCmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                reader.Read();
                                rtnVal = Convert.ToInt32(reader[0]);
                            }
                            reader.Close();
                            reader = null;
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rtnVal;
        }

        public void UpdateAdditionalPresenter(int presenterID, int WebinarID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand("spUpdateAdditionalPresenter", sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.Add(new MySqlParameter("pPresenterID", presenterID));
                            sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", WebinarID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddPresenter(PresenterBE objPreBE)
        {
            int presenterID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlPresenterInsert, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@userID", objPreBE.UserID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@presenterName", objPreBE.PresenterName));
                            sqlCmd.Parameters.Add(new MySqlParameter("@title", objPreBE.Title));
                            sqlCmd.Parameters.Add(new MySqlParameter("@organization", objPreBE.Organization));
                            sqlCmd.Parameters.Add(new MySqlParameter("@presenterBio", objPreBE.Bio));
                            sqlCmd.Parameters.Add(new MySqlParameter("@imgDocID", objPreBE.ImageDocID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@isExternal", objPreBE.isExternal));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();

                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return presenterID;
        }

        public void UpdatePresenterProfileDoc(int docID, int userID)
        {
            try
            {
                string sql1 = string.Empty;
                WebinarDA objDocDA = new WebinarDA();
                List<PresenterBE> objP1 = objDocDA.GetPresenterDetail(userID);

                if (objP1.Count > 0)
                    sql1 = DBQuery.sqlUserPresenterProfileDocUpdate;
                else
                    sql1 = DBQuery.sqlUserPresenterProfileDocInsert;

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@imgDocID", docID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public int SaveWebinarSchedule(WebinarBE objWebinarBE, WebinarRecurrencyBE objWebRecurrenceBE, string UpdateToEmails)
        {
            int webinarID = objWebinarBE.WebinarID;
            string sql1 = "";
            bool isNew = true;
            if (webinarID == 0)
                sql1 = "spAddNewWebinar";
            else
            {
                sql1 = "spUpdateWebinar";
                isNew = false;
            }
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarTitle", objWebinarBE.Title));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDescription", objWebinarBE.Description));
                    sqlCmd.Parameters.Add(new MySqlParameter("pStartDate", objWebinarBE.StartDate.ToString("yyyy-MM-dd")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pStartTime", Convert.ToDateTime(objWebinarBE.StartTime).ToString("HH:mm:ss")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pEndTime", Convert.ToDateTime(objWebinarBE.EndTime).ToString("HH:mm:ss")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pRecurrence", objWebinarBE.isRecurrence));
                    sqlCmd.Parameters.Add(new MySqlParameter("pTimeZoneID", objWebinarBE.TimeZoneID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDeliveryChannel", objWebinarBE.DeliveryChannel));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsPublic", objWebinarBE.isPublic));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsPassRequired", objWebinarBE.isPasswordRequired));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarPassword", objWebinarBE.WebinarPassword));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUpdateToEmails", UpdateToEmails));
                    if (webinarID == 0)
                    {
                        sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", objWebinarBE.Createdby));
                    }
                    else
                    {
                        sqlCmd.Parameters.Add(new MySqlParameter("pModifiedBy", objWebinarBE.Modifiedby));
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarBE.WebinarID));
                    }
                    sqlCmd.Parameters.Add(new MySqlParameter("pRecurrType", objWebRecurrenceBE.recurrType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pEndType", objWebRecurrenceBE.endType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pEndValue", objWebRecurrenceBE.endValue));
                    sqlCmd.Parameters.Add(new MySqlParameter("pRecurrCriteria", objWebRecurrenceBE.recurrCriteria));

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        webinarID = reader["WebinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarID"]);
                    }
                    reader.Close();
                    reader = null;
                    if (isNew)
                    {
                        SaveWebinarURLs(webinarID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return webinarID;
        }

        public int SaveWeibarScheduleAs(WebinarBE objWebinarBE)
        {
            int webinarID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spSaveWebinarAs", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarTitle", objWebinarBE.Title));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDescription", objWebinarBE.Description));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarBE.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pStartDate", objWebinarBE.StartDate.ToString("yyyy-MM-dd")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pStartTime", Convert.ToDateTime(objWebinarBE.StartTime).ToString("HH:mm:ss")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pEndTime", Convert.ToDateTime(objWebinarBE.EndTime).ToString("HH:mm:ss")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", objWebinarBE.Createdby));
                    sqlCmd.Parameters.Add(new MySqlParameter("pTimeZoneID", objWebinarBE.TimeZoneID));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        webinarID = reader["WebinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarID"]);
                    }
                    reader.Close();
                    reader = null;
                    if (webinarID != 0)
                        SaveWebinarURLs(webinarID);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return webinarID;

        }

        public int SaveWebinarRecurrenceInstance(int SourceWebinarID, int seriesNo, DateTime NewStartDate, string StartTime, string EndTime, int CreatedBy)
        {
            int webinarID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spCloneWebinar", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add(new MySqlParameter("pSourceWebinarID", SourceWebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pInstanceID", seriesNo));
                    sqlCmd.Parameters.Add(new MySqlParameter("pNewStartDate", NewStartDate.ToString("yyyy-MM-dd")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pStartTime", Convert.ToDateTime(StartTime).ToString("hh:mm:ss")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pEndTime", Convert.ToDateTime(EndTime).ToString("hh:mm:ss")));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", CreatedBy));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        webinarID = reader["WebinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarID"]);
                        SaveWebinarURLs(webinarID);
                    }
                    reader.Close();
                    reader = null;
                    return webinarID;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
       
        //private void SaveDefaultThemes(int clientID, int webinarID)
        //{
        //    try
        //    {
        //        using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
        //        {
        //            MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlInsertWebinarURL, sqlCon);
        //            sqlCon.Open();
        //            sqlCmd.CommandType = CommandType.Text;
        //            sqlCmd.Parameters.Add(new MySqlParameter("ClientID1", clientID));
        //            sqlCmd.Parameters.Add(new MySqlParameter("ClientID2", clientID));
        //            sqlCmd.Parameters.Add(new MySqlParameter("ClientID3", clientID));
        //            sqlCmd.Parameters.Add(new MySqlParameter("WebinarID", webinarID));
        //            sqlCmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        private void SaveWebinarURLs(int webinarID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlInsertWebinarURL, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;

                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@RegURLKey", Guid.NewGuid().ToString()));
                    sqlCmd.Parameters.Add(new MySqlParameter("@PreviewURLKey", Guid.NewGuid().ToString()));
                    sqlCmd.Parameters.Add(new MySqlParameter("@AudiURLKey", Guid.NewGuid().ToString()));
                    sqlCmd.Parameters.Add(new MySqlParameter("@CoCenterURLKey", Guid.NewGuid().ToString()));
                    sqlCmd.Parameters.Add(new MySqlParameter("@AnalyticURLKey", Guid.NewGuid().ToString()));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateWebinarRecurrence(WebinarRecurrencyBE objWebRecurrenceBE)
        {
            try
            {
                string sql1 = string.Empty;

                List<WebinarRecurrencyBE> objP1 = GetWebinarRecurrencyDetail(objWebRecurrenceBE.WebinarID);
                if (objP1.Count > 0)
                    sql1 = DBQuery.sqlWebinarRecurrUpdate;
                else
                    sql1 = DBQuery.sqlWebinarRecurrInsert;

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@recurrType", objWebRecurrenceBE.recurrType));
                            sqlCmd.Parameters.Add(new MySqlParameter("@endType", objWebRecurrenceBE.endType));
                            sqlCmd.Parameters.Add(new MySqlParameter("@endValue", objWebRecurrenceBE.endValue));
                            sqlCmd.Parameters.Add(new MySqlParameter("@recurrCriteria", objWebRecurrenceBE.recurrCriteria));
                            sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebRecurrenceBE.WebinarID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WebinarRecurrencyBE> GetWebinarRecurrencyDetail(int WebinarID)
        {
            List<WebinarRecurrencyBE> objWebinarRecurrencyBE = new List<WebinarRecurrencyBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRecurrDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarRecurrencyBE.Add(new WebinarRecurrencyBE
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    recurrType = reader["recurrType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["recurrType"]),
                                    endType = reader["endType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["endType"]),
                                    endValue = reader["endValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["endValue"]),
                                    recurrCriteria = reader["recurrCriteria"] == DBNull.Value ? string.Empty : Convert.ToString(reader["recurrCriteria"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarRecurrencyBE;
        }

        public List<ThemeMasterBE> GetWebinarThemeDetails(int WebinarID)
        {
            List<ThemeMasterBE> objThemeMasterBE = new List<ThemeMasterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarThemeDetails, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                        //sqlCmd.Parameters.Add(new MySqlParameter("wbinarID2", WebinarID));
                        //sqlCmd.Parameters.Add(new MySqlParameter("wbinarID3", WebinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objThemeMasterBE.Add(new ThemeMasterBE
                                {
                                    ThemeID = reader["EBThemeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EBThemeID"]),
                                    ThemeName = reader["EBThemeName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EBThemeName"]),
                                    ThemeShortName = reader["EBThemeSort"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EBThemeSort"]),
                                    ThemeCategory = reader["ThemeCategory"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ThemeCategory"]),
                                    ThumbNail = reader["ThumbNail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ThumbNail"]),
                                    ThemeStatus = reader["EBThemeStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EBThemeStatus"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objThemeMasterBE;
        }

        public List<WebinarURLs> GetWebinarURLsDA(int webinarID)
        {
            string sql1 = DBQuery.sqlGetWebinarURL;

            List<WebinarURLs> objWebinarURL = new List<WebinarURLs>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarURL.Add(new WebinarURLs
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    RegistrationURL = reader["RegURLKey"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RegURLKey"]),
                                    PreviewInterfaceURL = reader["PreviewURLKey"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PreviewURLKey"]),
                                    AudienceInterfaceURL = reader["AudiURLkey"] == DBNull.Value ? string.Empty : reader["AudiURLKey"].ToString(),
                                    CommandCenterURL = reader["CoCenterURLKey"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CoCenterURLKey"]),
                                    AnalyticsURL = reader["AnalyticURLKey"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AnalyticURLKey"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarURL;
        }

        public int getWebinarIDFromURLKey(string URLType, string URLKey)
        {
            int rtn = 0;
            string sql1 = "";
            switch (URLType.ToUpper())
            {
                case "REG":
                    sql1 = DBQuery.sqlGetWebinarRegURL;
                    break;
                case "PRE":
                    sql1 = DBQuery.sqlGetWebinarPreviewURL;
                    break;
                case "AUD":
                    sql1 = DBQuery.sqlGetWebinarAudiURL;
                    break;
                case "COC":
                    sql1 = DBQuery.sqlGetWebinarCoCURL;
                    break;
            }
            if (sql1 != "")
            {
                try
                {
                    using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                        {
                            sqlCon.Open();
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@key", URLKey));
                            MySqlDataReader reader = sqlCmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                reader.Read();
                                rtn = reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]);
                            }
                            reader.Close();
                            reader = null;
                        }
                        sqlCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return rtn;
        }

        public List<WebinarHostBE> GetWebinarHostDA(int webinarID)
        {
            List<WebinarHostBE> objWebinarHostBE = new List<WebinarHostBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetWebinarHosts, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarHostBE.Add(new WebinarHostBE
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    WebinarHost = reader["domainURL"] == DBNull.Value ? string.Empty : Convert.ToString(reader["domainURL"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarHostBE;
        }

        public int GetebinarNonStdHostCount(int webinarID)
        {
            int rtn = 0;
            string sql1 = "select count(*) from tblwebinarhost where domainURL not in ('hotmail.com','yahoo.com','gmail.com','aol.com','sbcglobal.com') and webinarID=" + webinarID.ToString();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtn = reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]);
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtn;
        }

        public List<WebinarTheme> getWebinarTheme(int webinarID)
        {
            List<WebinarTheme> objWebinarTheme = new List<WebinarTheme>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetWebinarTheme", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarTheme.Add(new WebinarTheme
                                {
                                    WebinarID = webinarID,
                                    HeaderType = reader["headerType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["headerType"]),
                                    PriThemeColor = reader["thPriColor"] == DBNull.Value ? string.Empty : Convert.ToString(reader["thPriColor"]),
                                    SecThemeColor = reader["thSecColor"] == DBNull.Value ? string.Empty : Convert.ToString(reader["thSecColor"]),
                                    ThemeFontName = reader["thFontName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["thFontName"]),
                                    ThemeLayoutID = reader["thLayoutID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["thLayoutID"]),
                                    ThemeName = reader["themeName"] == DBNull.Value ? "" : Convert.ToString(reader["themeName"]),
                                    Shade1 = reader["Shade1"] == DBNull.Value ? "#f5f5f5" : Convert.ToString(reader["Shade1"]),
                                    Shade2 = reader["Shade2"] == DBNull.Value ? "#ccc" : Convert.ToString(reader["Shade2"]),
                                    Shade3 = reader["Shade3"] == DBNull.Value ? "" : Convert.ToString(reader["Shade3"]),
                                    Shade4 = reader["Shade4"] == DBNull.Value ? "" : Convert.ToString(reader["Shade4"]),
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarTheme;
        }

        public void SaveWebinarDefaultTheme(WebinarTheme objWebinarTheme)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarThemeInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarTheme.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@headerType", objWebinarTheme.HeaderType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@thPriColor", objWebinarTheme.PriThemeColor));
                    sqlCmd.Parameters.Add(new MySqlParameter("@thSecColor", objWebinarTheme.SecThemeColor));
                    sqlCmd.Parameters.Add(new MySqlParameter("@thFontName", objWebinarTheme.ThemeFontName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@thLayoutID", objWebinarTheme.ThemeLayoutID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveWebinarTheme(string headerType, int headerimageID, int webinarID)
        {
            string sql1 = DBQuery.sqlWebinarThemeLogo1Update;
            switch (headerType)
            {
                case "Logo1":
                    sql1 = DBQuery.sqlWebinarThemeLogo1Update;
                    break;
                case "Logo2":
                    sql1 = DBQuery.sqlWebinarThemeLogo2Update;
                    break;
                case "BANNER":
                    sql1 = DBQuery.sqlWebinarThemeBannerUpdate;
                    break;
            }
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@logoID1", headerimageID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveWebinarTheme(WebinarTheme objWebinarTheme)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarThemeUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    //sqlCmd.Parameters.Add(new MySqlParameter("thPriColor", objWebinarTheme.PriThemeColor));
                    //sqlCmd.Parameters.Add(new MySqlParameter("thSecColor", objWebinarTheme.SecThemeColor));
                    //sqlCmd.Parameters.Add(new MySqlParameter("thFontName", objWebinarTheme.ThemeFontName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@headerType", objWebinarTheme.HeaderType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@thLayoutID", objWebinarTheme.ThemeLayoutID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarTheme.WebinarID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsWebinarOverlapping(string WebinarDate, string StartTime, string EndTime, int OwnerID, int webinarID)
        {
            bool rtn = false;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spIsWebinarOverlapping", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarDate", WebinarDate));
                        sqlCmd.Parameters.Add(new MySqlParameter("pStartTime", Convert.ToDateTime(StartTime).ToString("HH:mm:ss")));
                        sqlCmd.Parameters.Add(new MySqlParameter("pEndTime", Convert.ToDateTime(EndTime).ToString("HH:mm:ss")));
                        sqlCmd.Parameters.Add(new MySqlParameter("pOwnerID", OwnerID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtn = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0])) > 0 ? true : false;
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtn;
        }

        #region Webinar Audience

        public void SaveWebinarAudienceDefault(int webinarID, string audiBgColor)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarAudiDefaultInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@audiBgColor", audiBgColor));
                    sqlCmd.Parameters.Add(new MySqlParameter("@audiBackGround", "None"));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveWebinarAudience(int webinarID, string fieldName, string fieldValue)
        {
            string sql1 = (DBQuery.sqlWebinarAudiFieldUpdate.Replace("##FieldName", fieldName)).Replace("##FieldValue", fieldValue);
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveWebinarAudience(WebinarAudience objWebinarAudience)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarAudiComponentUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@audiBackGround", objWebinarAudience.AudienceViewBackground));
                    sqlCmd.Parameters.Add(new MySqlParameter("@Download", objWebinarAudience.Download));
                    sqlCmd.Parameters.Add(new MySqlParameter("@chat", objWebinarAudience.Chat));
                    sqlCmd.Parameters.Add(new MySqlParameter("@submitQuestion", objWebinarAudience.SubmitQuestion));
                    sqlCmd.Parameters.Add(new MySqlParameter("@Wiki", objWebinarAudience.Wiki));
                    sqlCmd.Parameters.Add(new MySqlParameter("@Email", objWebinarAudience.Email));
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarAudience.WebinarID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public WebinarAudience getWebinarAudience(int webinarID)
        {
            WebinarAudience objWebinarAudience = new WebinarAudience();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarAudiSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarAudience.WebinarID = webinarID;
                                objWebinarAudience.AudienceViewBgColor = reader["audiBgColor"] == DBNull.Value ? string.Empty : Convert.ToString(reader["audiBgColor"]);
                                objWebinarAudience.AudienceViewBgImageID = reader["audiBgImageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["audiBgImageID"]);
                                objWebinarAudience.AudienceViewBackground = reader["audiBackGround"] == DBNull.Value ? string.Empty : Convert.ToString(reader["audiBackGround"]);
                                objWebinarAudience.Email = reader["Email"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Email"]);
                                objWebinarAudience.Chat = reader["Chat"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Chat"]);
                                objWebinarAudience.Content = reader["Content"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Content"]);
                                objWebinarAudience.Download = reader["Download"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Download"]);
                                objWebinarAudience.FaceBook = reader["FaceBook"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FaceBook"]);
                                objWebinarAudience.LinkedIn = reader["LinkedIn"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LinkedIn"]);
                                objWebinarAudience.Search = reader["Search"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Search"]);
                                objWebinarAudience.SpeakerBio = reader["SpeakerBio"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpeakerBio"]);
                                objWebinarAudience.SubmitQuestion = reader["SubmitQuestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SubmitQuestion"]);
                                objWebinarAudience.Twitter = reader["Twitter"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Twitter"]);
                                objWebinarAudience.Video = reader["Video"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Video"]);
                                objWebinarAudience.Wiki = reader["Wiki"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Wiki"]);
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarAudience;
        }

        public List<WebinarPresentations> getWebinarAudiPresentation(int webinarID)
        {
            List<WebinarPresentations> objWebinarPresentations = new List<WebinarPresentations>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarAudiPresentation, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarPresentations.Add(new WebinarPresentations
                                {
                                    WebinarID = webinarID,
                                    WebpresentationID = reader["presentationID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["presentationID"]),
                                    DocumentID = reader["docID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["docID"]),
                                    FileName = reader["SavedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SavedFileName"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarPresentations;
        }

        public void UpdateBriefcaseDocStatus(int DocID, int WebinarID, bool isInBriefcase)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateBriefcaseDocStatus", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDocID", DocID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsBriefcase", isInBriefcase));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetBriefcaseDocCount(int WebinarID, int ConfigNo)
        {
            int rtnVal = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spCheckContentInBriefcase", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pConfigNo", ConfigNo));

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnVal;
        }

        public int DeleteContentFromBriefcase(int WebinarID)
        {
            int rtnVal = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spRemoveContentInBriefcase", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", WebinarID));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnVal;
        }

        //public List<DocumentBE> GetBriefcaseContentDocs
        #endregion

        #region Webinar Registration

        public void SaveWebinarRegistrationDefault(WebinarRegistration objWebReg)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegDefaultInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebReg.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isRegEnabled", objWebReg.isRegistrationEnabled));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isVideoFile", objWebReg.isVideoFile));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isAddPresenter", objWebReg.isAdditionalPresenter));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isAddWebinar", objWebReg.isAdditionalWebinar));
                    sqlCmd.Parameters.Add(new MySqlParameter("@ConnectedAPIEmails", objWebReg.APIEmails));
                    sqlCmd.Parameters.Add(new MySqlParameter("@includeLogoBanner", objWebReg.IncludeLogoBanner));
                    sqlCmd.Parameters.Add(new MySqlParameter("@includeSummary", objWebReg.IncludeSummary));
                    sqlCmd.Parameters.Add(new MySqlParameter("@includeSpeakerBio", objWebReg.IncludeSpeakerBio));

                    sqlCmd.ExecuteNonQuery();

                    ////Insert Registration form defaults
                    sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegDefaultFormFieldsInsert, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebReg.WebinarID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateRegFormFields(List<WebinarRegFormFields> objWebinarRegFormFields)
        {
            if (objWebinarRegFormFields.Count > 0)
            {
                try
                {
                    using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                    {
                        MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegFormFieldsDelete, sqlCon);
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarRegFormFields[0].webinarID));
                        sqlCmd.ExecuteNonQuery();

                        sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegFormFieldsInsert, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        for (int idx = 0; idx < objWebinarRegFormFields.Count; idx++)
                        {
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarRegFormFields[idx].webinarID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@regfieldID", objWebinarRegFormFields[idx].FieldID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@fieldLabel", objWebinarRegFormFields[idx].FieldLabel));
                            sqlCmd.Parameters.Add(new MySqlParameter("@isRequired", objWebinarRegFormFields[idx].isRequired));
                            sqlCmd.ExecuteNonQuery();
                        }
                        sqlCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void SaveWebinarRegistration(WebinarRegistration objWebReg)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@isVideoFile", objWebReg.isVideoFile));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isAddWebinar", objWebReg.isAdditionalWebinar));
                    sqlCmd.Parameters.Add(new MySqlParameter("@ConnectedAPIEmails", objWebReg.APIEmails));
                    sqlCmd.Parameters.Add(new MySqlParameter("@inclLogoBanner", objWebReg.IncludeLogoBanner));
                    sqlCmd.Parameters.Add(new MySqlParameter("@inclSummary", objWebReg.IncludeSummary));
                    sqlCmd.Parameters.Add(new MySqlParameter("@inclSpeakerBio", objWebReg.IncludeSpeakerBio));
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebReg.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@campTrackerEmail", objWebReg.CampaignTrackerEmails));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateWebinarRegStatus(int webinarID, int updatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateWebinarRegistrationStatus", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pActionBy", updatedBy));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateWebinarRegStatus(bool regStatus, int webinarID, int updatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegStatusUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@isRegEnabled", regStatus));
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    sqlCmd.ExecuteNonQuery();

                    WebinarAuditLog objWebinarAuditLog = new WebinarAuditLog();
                    objWebinarAuditLog.ActionByID = updatedBy;
                    objWebinarAuditLog.WebinarAction = "Status Change";
                    objWebinarAuditLog.ActionDetails = "Status updated to " + regStatus.ToString();
                    objWebinarAuditLog.WebinarID = webinarID;
                    sqlCon.Close();
                    RecordWebinarAction(objWebinarAuditLog);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteWebinar(int webinarID, int updatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spDeleteWebinar", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pActionedBy", updatedBy));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateWebinarStatus(int webinarID, string ToStatus, int updatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateWebinarStatus", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarStatus", ToStatus));
                    sqlCmd.Parameters.Add(new MySqlParameter("pActionedBy", updatedBy));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarRegistration> getWebinarRegistration(int webinarID)
        {
            List<WebinarRegistration> objWebReg = new List<WebinarRegistration>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarRegistration
                                {
                                    WebinarID = webinarID,
                                    isAdditionalPresenter = reader["isAddPresenter"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddPresenter"]),
                                    isAdditionalWebinar = reader["isAddWebinar"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddWebinar"]),
                                    isRegistrationEnabled = reader["isRegEnabled"] == DBNull.Value ? false : Convert.ToBoolean(reader["isRegEnabled"]),
                                    isVideoFile = reader["isVideoFile"] == DBNull.Value ? false : Convert.ToBoolean(reader["isVideoFile"]),
                                    APIEmails = reader["ConnectedAPIEmails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ConnectedAPIEmails"]),
                                    isConnectRegEmailed = reader["isConnectedRegEmailed"] == DBNull.Value ? false : Convert.ToBoolean(reader["isConnectedRegEmailed"]),
                                    CampaignTrackerEmails = reader["campTrackerEmails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["campTrackerEmails"]),
                                    isCampTrackerEmailed = reader["isCampTrackerEmailed"] == DBNull.Value ? false : Convert.ToBoolean(reader["isCampTrackerEmailed"]),
                                    //FormFields = reader["regFormFields"] == DBNull.Value ? string.Empty : Convert.ToString(reader["regFormFields"]),
                                    //FormFieldRequired = reader["regFormFieldReq"] == DBNull.Value ? string.Empty : Convert.ToString(reader["regFormFieldReq"]),
                                    IncludeLogoBanner = reader["inclLogoBanner"] == DBNull.Value ? false : Convert.ToBoolean(reader["inclLogoBanner"]),
                                    IncludeSummary = reader["inclSummary"] == DBNull.Value ? false : Convert.ToBoolean(reader["inclSummary"]),
                                    IncludeSpeakerBio = reader["inclSpeakerBio"] == DBNull.Value ? false : Convert.ToBoolean(reader["inclSpeakerBio"]),
                                    videoFileDocID = reader["videoFileID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["videoFileID"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public List<WebinarRegFormFields> getWebinarRegFormFields(int webinarID)
        {
            List<WebinarRegFormFields> objWebReg = new List<WebinarRegFormFields>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegFormFieldsSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarRegFormFields
                                {
                                    webinarID = webinarID,
                                    FieldID = reader["regfieldID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["regfieldID"]),
                                    FieldLabel = reader["fieldLabel"] == DBNull.Value ? string.Empty : Convert.ToString(reader["fieldLabel"]),
                                    isRequired = reader["isRequired"] == DBNull.Value ? false : Convert.ToBoolean(reader["isRequired"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public void InsertRegFormResources(WebinarResource objWebinarResource)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarResourceInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarResource.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@docID", objWebinarResource.DocID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@resourceType", objWebinarResource.ResourceType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@resourceOrder", objWebinarResource.ResourceOrder));
                    sqlCmd.Parameters.Add(new MySqlParameter("@ResourceTitle", objWebinarResource.ResourceTitle));
                    sqlCmd.Parameters.Add(new MySqlParameter("@resourceValue", objWebinarResource.ResourceValue));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertWebinarResources(WebinarResource objWebinarResource)
        {
            int rtnVal = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spWebinarResourceInsert", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarResource.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDocId", objWebinarResource.DocID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pResourceType", objWebinarResource.ResourceType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pResourceTitle", objWebinarResource.ResourceTitle));
                    sqlCmd.Parameters.Add(new MySqlParameter("pResourceValue", objWebinarResource.ResourceValue));
                    //sqlCmd.Parameters.Add(new MySqlParameter("@WebinarID1", objWebinarResource.WebinarID));
                    //sqlCmd.Parameters.Add(new MySqlParameter("@ResourceType1", objWebinarResource.ResourceType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsAddToBriefcase", objWebinarResource.IsBriefcase));
                    sqlCmd.Parameters.Add(new MySqlParameter("pLogoUrlName", objWebinarResource.LogoUrlName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pLogoUrl", objWebinarResource.LogoUrl));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnVal;
        }

        public int UpdateWebinarResources(WebinarResource objWebinarResource)
        {
            int rtnVal = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spWebinarResourceUpdate", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarResource.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDocId", objWebinarResource.DocID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pLogoUrlName", objWebinarResource.LogoUrlName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pLogoUrl", objWebinarResource.LogoUrl));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnVal;
        }

        public void DeleteRegFormResources(int webinarID, int docID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spDeleteRegFormResources", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pDocID", docID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                    sqlCmd.ExecuteNonQuery();

                    //sqlCmd.Parameters.Clear();
                    //sqlCmd = new MySqlCommand(DBQuery.sqlDocumentDelete, sqlCon);
                    //sqlCmd.CommandType = CommandType.Text;
                    //sqlCmd.Parameters.Add(new MySqlParameter("@DocID", docID));
                    //sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteRegFormResources(int regresourceID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarResourceIDDelete, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@regresourceID", regresourceID));
                    sqlCmd.ExecuteNonQuery();

                    //sqlCmd.Parameters.Clear();
                    //sqlCmd = new MySqlCommand(DBQuery.sqlDocumentDelete, sqlCon);
                    //sqlCmd.CommandType = CommandType.Text;
                    //sqlCmd.Parameters.Add(new MySqlParameter("DocID", docID));
                    //sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarRegFormQA> getWebinarRegFormQA(int webinarID)
        {
            List<WebinarRegFormQA> objWebinarRegFormQA = new List<WebinarRegFormQA>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantFormQASelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarRegFormQA.Add(new WebinarRegFormQA
                                {
                                    webinarID = webinarID,
                                    qaID = reader["qaID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["qaID"]),
                                    QuestionOrder = reader["QuestionOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QuestionOrder"]),
                                    RegFormQuestion = reader["RegFormQuestion"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RegFormQuestion"]),
                                    QResponseType = reader["QResponseType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["QResponseType"]),
                                    QResponseOptions = reader["QResponseOptions"] == DBNull.Value ? string.Empty : Convert.ToString(reader["QResponseOptions"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarRegFormQA;
        }

        public void UpdateRegFormQAOrder(int qaID, int qaOrder)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarQAReOrderUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@OrderNo", qaOrder));
                    sqlCmd.Parameters.Add(new MySqlParameter("@qaID", qaID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarRegFormQA> getWebinarRegFormQADetail(int qaID)
        {
            List<WebinarRegFormQA> objWebinarRegFormQA = new List<WebinarRegFormQA>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantFormQA, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@qaID", qaID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarRegFormQA.Add(new WebinarRegFormQA
                                {
                                    webinarID = reader["webinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["webinarID"]),
                                    qaID = reader["qaID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["qaID"]),
                                    QuestionOrder = reader["QuestionOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QuestionOrder"]),
                                    RegFormQuestion = reader["RegFormQuestion"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RegFormQuestion"]),
                                    QResponseType = reader["QResponseType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["QResponseType"]),
                                    QResponseOptions = reader["QResponseOptions"] == DBNull.Value ? string.Empty : Convert.ToString(reader["QResponseOptions"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarRegFormQA;
        }

        public string InsertWebinarFormQA(WebinarRegFormQA objWebinarRegFormQA)
        {
            string rtn = string.Empty;
            try
            {
                //string sql1 = "";
                //if (objWebinarRegFormQA.qaID == 0)
                //    sql1 = DBQuery.sqlWebinarRegistrantFormQAInsert;
                //else
                //    sql1 = DBQuery.sqlWebinarRegistrantFormQAUpdate;

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spAddUpdateAdditionalRegQ", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pQaID", objWebinarRegFormQA.qaID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarRegFormQA.webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pRegFormQuestion", objWebinarRegFormQA.RegFormQuestion));
                    sqlCmd.Parameters.Add(new MySqlParameter("pQResponseType", objWebinarRegFormQA.QResponseType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pQResponseOptions", objWebinarRegFormQA.QResponseOptions));
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtn = (reader[0] == DBNull.Value ? string.Empty : Convert.ToString(reader[0]));
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtn;
        }

        public void DeleteWebinarFormqaID(int webinarID, int deleteqaID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantFormQAOrderUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@qaID", deleteqaID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Parameters.Clear();

                    sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantFormqaDelete, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@qaID", deleteqaID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarResource> getRegFormResoures(int webinarID)
        {
            List<WebinarResource> objWebReg = new List<WebinarResource>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarResourceLogoSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarResource
                                {
                                    WebinarID = webinarID,
                                    ResourceID = reader["regresourceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["regresourceID"]),
                                    DocID = reader["docID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["docID"]),
                                    ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                    ResourceOrder = reader["resourceOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["resourceOrder"]),
                                    ResourceTitle = reader["resourceTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceTitle"]),
                                    ResourceValue = reader["ResourceValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResourceValue"]),
                                    LogoUrlName = reader["logoUrlName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrlName"]),
                                    LogoUrl = reader["logoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrl"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public void UpdateRegFormLogoOrder(int webinarID, ArrayList arrLogos)
        {
            try
            {
                string sql1 = DBQuery.sqlWebinarResourceLogoOrderUpdate;

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;

                    EBirdUtility objUtil = new EBirdUtility();

                    for (int idx = 0; idx < arrLogos.Count; idx++)
                    {
                        if (objUtil.IsNumeric(arrLogos[idx]))
                        {
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add(new MySqlParameter("@resourceOrder", idx + 1));
                            sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@docID", arrLogos[idx].ToString()));
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarResource> getWebinarResoures(int webinarID, string resourceType)
        {
            List<WebinarResource> objWebReg = new List<WebinarResource>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarResourceTypeSelect.Replace("###", resourceType), sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@resourceType", resourceType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarResource
                                {
                                    WebinarID = webinarID,
                                    ResourceID = reader["regresourceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["regresourceID"]),
                                    DocID = reader["docID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["docID"]),
                                    ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                    ResourceOrder = reader["resourceOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["resourceOrder"]),
                                    ResourceTitle = reader["resourceTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceTitle"]),
                                    ResourceValue = reader["ResourceValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResourceValue"]),
                                    IsBriefcase = reader["isAddToBriefcase"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddToBriefcase"]),
                               LogoUrlName = reader["logoUrlName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrlName"]),
                                    LogoUrl = reader["logoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrl"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public List<WebinarResource> getWebinarResoures(int webinarID)
        {

            List<WebinarResource> objWebReg = new List<WebinarResource>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetBriefcaseContents", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                        //sqlCmd.Parameters.Add(new MySqlParameter("@resourceType", resourceType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarResource
                                {
                                    WebinarID = webinarID,
                                    ResourceID = reader["regresourceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["regresourceID"]),
                                    DocID = reader["docID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["docID"]),
                                    ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                    ResourceOrder = reader["resourceOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["resourceOrder"]),
                                    ResourceTitle = reader["resourceTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceTitle"]),
                                    ResourceValue = reader["ResourceValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResourceValue"]),
                                    IsBriefcase = reader["isAddToBriefcase"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddToBriefcase"]),
                                    LogoUrlName = reader["logoUrlName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrlName"]),
                                    LogoUrl = reader["logoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrl"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public List<WebinarResource> getWebinarResouresByID(int resourceID)
        {
            List<WebinarResource> objWebReg = new List<WebinarResource>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarResourceIDSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@regresourceID", resourceID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarResource
                                {
                                    WebinarID = reader["webinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["webinarID"]),
                                    ResourceID = reader["regresourceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["regresourceID"]),
                                    DocID = reader["docID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["docID"]),
                                    ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                    ResourceOrder = reader["resourceOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["resourceOrder"]),
                                    ResourceTitle = reader["resourceTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceTitle"]),
                                    ResourceValue = reader["ResourceValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResourceValue"]),
                                    IsBriefcase = reader["isAddToBriefcase"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddToBriefcase"]),
                                    LogoUrlName = reader["logoUrlName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrlName"]),
                                    LogoUrl = reader["logoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrl"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public List<WebinarResource> getWebinarDocID(int DocID)
        {
            List<WebinarResource> objWebReg = new List<WebinarResource>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarDocIDSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@DocID", DocID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebReg.Add(new WebinarResource
                                {
                                    WebinarID = reader["webinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["webinarID"]),
                                    ResourceID = reader["regresourceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["regresourceID"]),
                                    DocID = reader["docID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["docID"]),
                                    ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                    ResourceOrder = reader["resourceOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["resourceOrder"]),
                                    ResourceTitle = reader["resourceTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceTitle"]),
                                    ResourceValue = reader["ResourceValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResourceValue"]),
                                    IsBriefcase = reader["isAddToBriefcase"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddToBriefcase"]),
                                    LogoUrlName = reader["logoUrlName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrlName"]),
                                    LogoUrl = reader["logoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(reader["logoUrl"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebReg;
        }

        public void UpdateWebinarResourceOrder(int resID, int resOrder)
        {
            //public readonly static string  = "Update tblwebinarregresources set resourceOrder=@ where regresourceID="
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarResourceOrderUpdate, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@resourceOrder", resOrder));
                            sqlCmd.Parameters.Add(new MySqlParameter("@resID", resID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateWebinarRegistrationEmailUpdate(int webinarID, string emailType)
        {
            string sql1 = string.Empty;
            if (emailType == "ConnectReg")
                sql1 = "Update tblwebinarregistration set isConnectedRegEmailed=1 where webinarID=@webinar";
            else
                sql1 = "Update tblwebinarregistration set isCampTrackerEmailed=1 where webinarID=@webinar";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@webinar", webinarID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Following method may need to be deprecated ???
        //public List<RegistrationFormFieldBE> getWebinarRegFormFields(int webinarID)
        //{
        //    List<RegistrationFormFieldBE> objWebReg = new List<RegistrationFormFieldBE>();
        //    try
        //    {
        //        using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
        //        {
        //            using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegFormFieldSelect, sqlCon))
        //            {
        //                sqlCon.Open();
        //                sqlCmd.CommandType = CommandType.Text;
        //                sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
        //                MySqlDataReader reader = sqlCmd.ExecuteReader();
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        objWebReg.Add(new RegistrationFormFieldBE
        //                        {
        //                            //WebinarID = reader["@webinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
        //                            FieldID = reader["regFormFieldID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["regFormFieldID"]),
        //                            FieldDisplay = reader["FormFieldDisplayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FormFieldDisplayName"]),
        //                            FieldName = reader["FormFieldName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FormFieldName"]),
        //                            ContainerID = reader["dispContainerID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["dispContainerID"]),
        //                            isRequired = reader["isreq"] == DBNull.Value ? false : Convert.ToBoolean(reader["isreq"])
        //                        });
        //                    }
        //                }
        //                reader.Close();
        //                reader = null;
        //            }
        //            sqlCon.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return objWebReg;
        //}

        public List<PresenterBE> GetWebinarPresenterDetail(int webinarID, bool ExcludeEnabled)
        {
            List<PresenterBE> objPresenterBE = new List<PresenterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetPresenterByWebinarID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pExcludeEnabled", (ExcludeEnabled ? 1 : 0)));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPresenterBE.Add(new PresenterBE
                                {
                                    PresenterID = reader["PresenterID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PresenterID"]),
                                    PresenterName = reader["PresenterName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PresenterName"]),
                                    Title = reader["Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Title"]),
                                    Organization = reader["Organization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Organization"]),
                                    Bio = reader["presenterBio"] == DBNull.Value ? string.Empty : Convert.ToString(reader["presenterBio"]),
                                    UserID = reader["UserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserID"]),
                                    IsEnabled = reader["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsEnabled"]),
                                    ImageDocID = reader["imgDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["imgDocID"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    //using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarPresenterSelect2, sqlCon))
                    //{
                    //    //sqlCmd = new MySqlCommand(DBQuery.sqlWebinarPresenterSelect2, sqlCon);
                    //    sqlCmd.CommandType = CommandType.Text;
                    //    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    //    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    //    if (reader.HasRows)
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            objPresenterBE.Add(new PresenterBE
                    //            {
                    //                PresenterID = reader["PresenterID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PresenterID"]),
                    //                PresenterName = reader["PresenterName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PresenterName"]),
                    //                Title = reader["Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Title"]),
                    //                Organization = reader["Organization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Organization"]),
                    //                Bio = reader["presenterBio"] == DBNull.Value ? string.Empty : Convert.ToString(reader["presenterBio"]),
                    //                UserID = reader["UserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserID"]),
                    //                ImageDocID = reader["imgDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["imgDocID"])
                    //            });
                    //        }
                    //    }
                    //    reader.Close();
                    //    reader = null;
                    //}

                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objPresenterBE;
        }

        public List<PresenterBE> GetWebinarPresenterDetail(int webinarID, int presenterID)
        {
            List<PresenterBE> objPresenterBE = new List<PresenterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlPresenterDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@presenterID", presenterID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPresenterBE.Add(new PresenterBE
                                {
                                    PresenterID = reader["PresenterID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PresenterID"]),
                                    PresenterName = reader["PresenterName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PresenterName"]),
                                    Title = reader["Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Title"]),
                                    Organization = reader["Organization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Organization"]),
                                    Bio = reader["presenterBio"] == DBNull.Value ? string.Empty : Convert.ToString(reader["presenterBio"]),
                                    UserID = reader["UserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserID"]),
                                    IsEnabled = reader["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsEnabled"]),
                                    //IsEnabled = reader["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(((byte[])(reader["IsEnabled"]))[0]),
                                    ImageDocID = reader["imgDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["imgDocID"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objPresenterBE;
        }

        public List<PresenterBE> GetOtherWebinarPresenters(int UserID, int WebinarID)
        {
            List<PresenterBE> objPresenterBE = new List<PresenterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetOtherPresenters", sqlCon))
                    {  
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserID", UserID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", WebinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPresenterBE.Add(new PresenterBE
                                {
                                    PresenterID = reader["PresenterID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["PresenterID"]),
                                    PresenterName = reader["PresenterName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PresenterName"]),
                                    Title = reader["Title"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Title"]),
                                    Organization = reader["Organization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Organization"]),
                                    Bio = reader["presenterBio"] == DBNull.Value ? string.Empty : Convert.ToString(reader["presenterBio"]),
                                    UserID = reader["UserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserID"]),
                                    IsEnabled = reader["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsEnabled"]),
                                    //IsEnabled = reader["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(((byte[])(reader["IsEnabled"]))[0]),
                                    ImageDocID = reader["imgDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["imgDocID"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objPresenterBE;
        }

        public void updateWebinarEnabledState(bool isEnabled, int presenterID, int webinarID)
        {
            string sql1 = "Update tbladditionalpresenter set isEnabled = " + (isEnabled ? "1" : "0") + " where presenterID = " + presenterID.ToString() + " and webinarID = " + webinarID.ToString();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        //using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlPresenterUpdateEnabled, sqlCon))
                        using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            //sqlCmd.Parameters.Add(new MySqlParameter("@isEnabled", isEnabled ? "1" : "0"));
                            //sqlCmd.Parameters.Add(new MySqlParameter("@presenterID", presenterID));
                            //sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRegPageVideo(int docID, int webinarID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarEventUpdateVideoFile, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@videoFileID", docID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region webinar events

        public int SaveWebinarRegistrant(Registrants objRegData)
        {
            int webEventRegID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objRegData.webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld1", objRegData.Fld1));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld2", objRegData.Fld2));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld3", objRegData.Fld3));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld4", objRegData.Fld4));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld5", objRegData.Fld5));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld6", objRegData.Fld6));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld7", objRegData.Fld7));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld8", objRegData.Fld8));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld9", objRegData.Fld9));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld10", objRegData.Fld10));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld11", objRegData.Fld11));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld12", objRegData.Fld12));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld13", objRegData.Fld13));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld14", objRegData.Fld14));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld15", objRegData.Fld15));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld16", objRegData.Fld16));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld17", objRegData.Fld17));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld18", objRegData.Fld18));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld19", objRegData.Fld19));
                    sqlCmd.Parameters.Add(new MySqlParameter("@fld20", objRegData.Fld20));
                    sqlCmd.Parameters.Add(new MySqlParameter("regFromIP", objRegData.RegFromIP));
                    sqlCmd.ExecuteNonQuery();

                    sqlCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", sqlCon);
                    sqlCmd.CommandType = CommandType.Text;

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        webEventRegID = Convert.ToInt32(reader.GetValue(0));
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                    UpdateRegistrantCount(objRegData.webinarID);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return webEventRegID;

        }

        public void SaveAdditionalQA(int regID, int qaID, string QAResponse)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantQAInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@regID", regID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@qaID", qaID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@QAResponse", QAResponse));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Registrants> GetWebinarRegistrants(int WebinarID)
        {
            List<Registrants> objRegistrants = new List<Registrants>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantSelectMin, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objRegistrants.Add(new Registrants
                                {
                                    RegistrationID = reader["regID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["regID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = reader["Registrant"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Registrant"]),
                                    EmailAddress = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString(),
                                    RegisteredOn = reader["RegisteredOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RegisteredOn"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objRegistrants;

        }

        public List<Registrants> GetWebinarRegistrantDetail(int WebinarID, string registrantEmail)
        {
            List<Registrants> objRegistrants = new List<Registrants>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarRegistrantDetailSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@registrantEmail", registrantEmail));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objRegistrants.Add(new Registrants
                                {
                                    RegistrationID = reader["regID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["regID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = reader["Registrant"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Registrant"]),
                                    EmailAddress = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString(),
                                    RegisteredOn = reader["RegisteredOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RegisteredOn"]),
                                    RegFromIP = reader["RegFromIP"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RegFromIP"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objRegistrants;

        }

        //public int SaveWebinarEvent(WebinarRegistrationData objRegData)
        //{
        //    int webEventRegID = 0;
        //    try
        //    {
        //        using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
        //        {
        //            MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarEventRegInsert, sqlCon);
        //            sqlCon.Open();
        //            sqlCmd.CommandType = CommandType.Text;
        //            sqlCmd.Parameters.Add(new MySqlParameter("webinarID", objRegData.webinarID));
        //            sqlCmd.Parameters.Add(new MySqlParameter("firstName", objRegData.FirstName));
        //            sqlCmd.Parameters.Add(new MySqlParameter("lastName", objRegData.LastName));
        //            sqlCmd.Parameters.Add(new MySqlParameter("email", objRegData.Email));
        //            sqlCmd.Parameters.Add(new MySqlParameter("address", objRegData.Address));
        //            sqlCmd.Parameters.Add(new MySqlParameter("city", objRegData.City));
        //            sqlCmd.Parameters.Add(new MySqlParameter("state", objRegData.State));
        //            sqlCmd.Parameters.Add(new MySqlParameter("zip", objRegData.Phone));
        //            sqlCmd.Parameters.Add(new MySqlParameter("country", objRegData.Country));
        //            sqlCmd.Parameters.Add(new MySqlParameter("phone", objRegData.Phone));
        //            sqlCmd.Parameters.Add(new MySqlParameter("industry", objRegData.Industry));
        //            sqlCmd.Parameters.Add(new MySqlParameter("organization", objRegData.Organization));
        //            sqlCmd.Parameters.Add(new MySqlParameter("jobTitle", objRegData.JobTitle));
        //            sqlCmd.Parameters.Add(new MySqlParameter("purchaseTime", objRegData.PurchaseTime));
        //            sqlCmd.Parameters.Add(new MySqlParameter("purchaseRole", objRegData.PurchaseRole));
        //            sqlCmd.Parameters.Add(new MySqlParameter("NoOfEmp", objRegData.NoOfEmp));
        //            sqlCmd.Parameters.Add(new MySqlParameter("regFromIP", objRegData.RegFromIP));
        //            sqlCmd.ExecuteNonQuery();

        //            sqlCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", sqlCon);
        //            sqlCmd.CommandType = CommandType.Text;

        //            MySqlDataReader reader = sqlCmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                reader.Read();
        //                webEventRegID = Convert.ToInt32(reader.GetValue(0));
        //            }
        //            reader.Close();
        //            reader = null;

        //            ////Setting the default themes for the client
        //            //sqlCmd = new MySqlCommand(DBQuery.sqlWebinarUpdateDefaultTheme, sqlCon);
        //            //sqlCmd.CommandType = CommandType.Text;
        //            //sqlCmd.Parameters.Add(new MySqlParameter("ClientID", clientID));
        //            //reader = sqlCmd.ExecuteReader();
        //            //reader.Close();
        //            //reader = null;
        //            sqlCon.Close();
        //            UpdateRegistrantCount(objRegData.webinarID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return webEventRegID;
        //}

        public void UpdateRegistrantCount(int webinarID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarEventRegCountUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                    //sqlCmd.Parameters.Add(new MySqlParameter("WebinarID1", webinarID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarBE> GetRelatedWebinarsDA(int webinarID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarEventRelatedWebinar, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarBE.Add(new WebinarBE
                                {

                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    Title = reader["webinartitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinartitle"]),
                                    Description = reader["description"] == DBNull.Value ? string.Empty : Convert.ToString(reader["description"]),
                                    StartDate = reader["startdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["startdate"]),
                                    StartTime = reader["starttime"] == DBNull.Value ? string.Empty : reader["starttime"].ToString(),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isRecurrence = reader["recurrence"] == DBNull.Value ? false : Convert.ToBoolean(reader["recurrence"]),
                                    RecurrenceCriteria = reader["recurrCriteria"] == DBNull.Value ? string.Empty : Convert.ToString(reader["recurrCriteria"]),
                                    Registered = reader["registered"] == DBNull.Value ? -1 : Convert.ToInt32(reader["registered"]),
                                    Live = reader["live"] == DBNull.Value ? -1 : Convert.ToInt32(reader["live"]),
                                    OnDemand = reader["OnDemand"] == DBNull.Value ? -1 : Convert.ToInt32(reader["OnDemand"]),
                                    Createdby = reader["Createdby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Createdby"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedOn"]),
                                    ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ModifiedOn"]),
                                    Modifiedby = reader["Modifiedby"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Modifiedby"]),
                                    WebinarStatus = reader["webinarStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["webinarStatus"]),
                                    DeliveryChannel = reader["DeliveryChannel"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DeliveryChannel"]),
                                    isPublic = reader["isPublic"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPublic"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarBE;
        }

        #endregion

        #region refer collegue

        public List<WebinarReferCollegue> GetWebinarReferedCollegueDetail(int WebinarID, string CollegueEmail, string RefererEmail)
        {
            List<WebinarReferCollegue> objCollegue = new List<WebinarReferCollegue>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetReferCollegue, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@collegueEmailID", CollegueEmail));
                        sqlCmd.Parameters.Add(new MySqlParameter("@referEmailID", RefererEmail));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCollegue.Add(new WebinarReferCollegue
                                {
                                    CollegueFirstName = reader["colleaguefirstname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["colleaguefirstname"]),
                                    CollegueLastName = reader["colleaguelastname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["colleaguelastname"]),
                                    CollegueEmail = reader["colleagueemailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["colleagueemailID"]),
                                    RefererFirstName = reader["refererfirstname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["refererfirstname"]),
                                    RefererLastName = reader["refererlastname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["refererlastname"]),
                                    RefererEmail = reader["refereremailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["refereremailID"]),
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarID"]),
                                    EmailedOn = reader["emailedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailedOn"]),
                                    ReferInitiatedIP = reader["referInitiatedIP"] == DBNull.Value ? string.Empty : Convert.ToString(reader["referInitiatedIP"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objCollegue;

        }

        public int SaveWebinarReferCollegue(WebinarReferCollegue objRefData)
        {
            int webEventRefID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlInsertReferColleque, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objRefData.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@colleaguefirstname", objRefData.CollegueFirstName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@colleaguelastname", objRefData.CollegueLastName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@colleagueemailID", objRefData.CollegueEmail));
                    sqlCmd.Parameters.Add(new MySqlParameter("@refererfirstname", objRefData.RefererFirstName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@refererlastname", objRefData.RefererLastName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@refereremailID", objRefData.RefererEmail));
                    sqlCmd.Parameters.Add(new MySqlParameter("@referInitiatedIP", objRefData.ReferInitiatedIP));
                    sqlCmd.ExecuteNonQuery();

                    sqlCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", sqlCon);
                    sqlCmd.CommandType = CommandType.Text;

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        webEventRefID = Convert.ToInt32(reader.GetValue(0));
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return webEventRefID;
        }

        #endregion

        public void SaveWebinarNotificationDefault(WebinarNotification objWebNotify)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarNotifyDefaultInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebNotify.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isConfirmEmailAllReg", objWebNotify.isConfirmEmailAllReg));
                    sqlCmd.Parameters.Add(new MySqlParameter("@RegConfirmEmailContentID", objWebNotify.RegConfirmEmailContentID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@ReminderEmailContentID", objWebNotify.ReminderEmailContentID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@FollowupAEmailContentID", objWebNotify.FollowupAEmailContentID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@FollowupNAEmailContentID", objWebNotify.FollowupAEmailContentID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@invitationContentID", objWebNotify.RegConfirmEmailContentID));

                    //sqlCmd.Parameters.Add(new MySqlParameter("RegConfirmEmailContentID", objWebNotify.RegConfirmEmailContentID));
                    //sqlCmd.Parameters.Add(new MySqlParameter("ReminderEmailHour", objWebNotify.ReminderEmailHour));
                    //sqlCmd.Parameters.Add(new MySqlParameter("ReminderEmailDay", objWebNotify.ReminderEmailDay));
                    //sqlCmd.Parameters.Add(new MySqlParameter("ReminderEmailWeek", objWebNotify.ReminderEmailWeek));
                    //sqlCmd.Parameters.Add(new MySqlParameter("ReminderEmailContentID", objWebNotify.ReminderEmailContentID));
                    //sqlCmd.Parameters.Add(new MySqlParameter("RegListEmailOn", objWebNotify.RegListEmailOn));
                    //sqlCmd.Parameters.Add(new MySqlParameter("isEmailNewReg", objWebNotify.isEmailNewReg));
                    //sqlCmd.Parameters.Add(new MySqlParameter("UpdateSendToEmail", objWebNotify.UpdateSendToEmail));
                    //sqlCmd.Parameters.Add(new MySqlParameter("FollowupAttendee", objWebNotify.FollowupAttendee));
                    //sqlCmd.Parameters.Add(new MySqlParameter("FollowupAEmailContentID", objWebNotify.FollowupAEmailContentID));
                    //sqlCmd.Parameters.Add(new MySqlParameter("FollowupNonAttendee", objWebNotify.FollowupNonAttendee));
                    //sqlCmd.Parameters.Add(new MySqlParameter("FollowupNAEmailContentID", objWebNotify.FollowupNAEmailContentID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarNotification> getWebinarNotification(int webinarID)
        {
            List<WebinarNotification> objWebNotify = new List<WebinarNotification>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarNotifySelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebNotify.Add(new WebinarNotification
                                {
                                    WebinarID = webinarID,
                                    isConfirmEmailAllReg = reader["isConfirmEmailAllReg"] == DBNull.Value ? false : Convert.ToBoolean(reader["isConfirmEmailAllReg"]),
                                    RegConfirmEmailContentID = reader["RegConfirmEmailContentID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["RegConfirmEmailContentID"]),
                                    ReminderEmailContentID = reader["ReminderEmailContentID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReminderEmailContentID"]),
                                    FollowupAEmailContentID = reader["FollowupAEmailContentID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["FollowupAEmailContentID"]),
                                    FollowupNAEmailContentID = reader["FollowupNAEmailContentID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["FollowupNAEmailContentID"]),
                                    InvitationContentID = reader["InvitationContentID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["InvitationContentID"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebNotify;
        }

        public bool IsWebinarHostExistDA(int webinarID, string webinarHost)
        {
            bool rtn = false;

            string sql1 = DBQuery.sqlGetWebinarHostsExist;

            List<WebinarHostBE> objWebinarHostBE = new List<WebinarHostBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@domainURL", webinarHost));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            rtn = true;
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtn;
        }

        public void SaveWebinarDomains(int webinarID, string webinarHost)
        {
            //if (!IsWebinarHostExistDA(webinarID, webinarHost))
            //{
            //    try
            //    {
            //        using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
            //        {
            //            MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarHostsInsert, sqlCon);
            //            sqlCon.Open();
            //            sqlCmd.CommandType = CommandType.Text;
            //            sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
            //            sqlCmd.Parameters.Add(new MySqlParameter("@domainURL", webinarHost));
            //            sqlCmd.ExecuteNonQuery();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}

            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("Delete from tblwebinarhost where domainURL not in (" + webinarHost + ") and webinarID = " + webinarID, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.ExecuteNonQuery();
                    if (webinarHost != "")
                    {
                        EBirdUtility objUtil = new EBirdUtility();
                        ArrayList arr1 = objUtil.StringToArrayList(webinarHost, new char[] { ',' });
                        sqlCmd = new MySqlCommand("spUpdateWebinarDomains", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        for (int idx = 0; idx < arr1.Count; idx++)
                        {
                            if (arr1[idx].ToString().Trim() != "")
                            {
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                                sqlCmd.Parameters.Add(new MySqlParameter("pDomainURL", arr1[idx].ToString().Replace("'", "")));
                                sqlCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RecordWebinarAction(WebinarAuditLog objWebinarAuditLog)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    //MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarAuditInsert, sqlCon);
                    MySqlCommand sqlCmd = new MySqlCommand("spAddWebinarLogs", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarAuditLog.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarAction", objWebinarAuditLog.WebinarAction));
                    sqlCmd.Parameters.Add(new MySqlParameter("pActionDetail", objWebinarAuditLog.ActionDetails));
                    sqlCmd.Parameters.Add(new MySqlParameter("pActionby", objWebinarAuditLog.ActionByID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Webinar Audience search setting

        public List<WebinarSearchSettings> getWebinarSearchSettings(int webinarID)
        {
            List<WebinarSearchSettings> objWebSearch = new List<WebinarSearchSettings>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarSearchSettingSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebSearch.Add(new WebinarSearchSettings
                                {
                                    WebinarID = webinarID,
                                    isBing = reader["isBing"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBing"]),
                                    isGoogle = reader["isGoogle"] == DBNull.Value ? false : Convert.ToBoolean(reader["isGoogle"]),
                                    isYahoo = reader["isYahoo"] == DBNull.Value ? false : Convert.ToBoolean(reader["isYahoo"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebSearch;
        }

        public void saveWebinarSearchSettings(WebinarSearchSettings objWebinarSearchSettings)
        {
            //string sql1;
            //List<WebinarSearchSettings> objWebSearchSettings = getWebinarSearchSettings(objWebinarSearchSettings.WebinarID);
            //if (objWebSearchSettings.Count > 0)
            //    sql1 = DBQuery.sqlWebinarSearchSettingUpdate;
            //else
            //    sql1 = DBQuery.sqlWebinarSearchSettingInsert;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateSearchSettings", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsYahoo", objWebinarSearchSettings.isYahoo));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsBing", objWebinarSearchSettings.isBing));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsGoogle", objWebinarSearchSettings.isGoogle));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objWebinarSearchSettings.WebinarID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region webinar page contents

        public List<WebinarContentBE> GetDefaultWebinarContent(string ContentType, int languageID)
        {
            List<WebinarContentBE> objWebinarContentBE = new List<WebinarContentBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlDefaultContentSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@contentType", ContentType));
                        sqlCmd.Parameters.Add(new MySqlParameter("@languageID", languageID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarContentBE.Add(new WebinarContentBE
                                {
                                    ContentDescription = reader["contentDetail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["contentDetail"])
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarContentBE;

        }

        public bool SaveDefaultWebinarContent(int languageID, int WebinarID, string contentType)
        {
            int rtnRec = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarDefaultContentInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@contentType", contentType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@languageID", languageID));
                    rtnRec = sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            if (rtnRec == 0)
                return false;
            else
                return true;
        }

        public List<WebinarContentBE> GetWebinarContent(int webinarID, string ContentType)
        {
            List<WebinarContentBE> objWebinarContentBE = new List<WebinarContentBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarContentSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@contentType", ContentType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarContentBE.Add(new WebinarContentBE
                                {
                                    ContentType = reader["ContentType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContentType"]),
                                    ContentDescription = reader["contentDetail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["contentDetail"])
                                });
                            }
                            reader.Close();
                            reader = null;
                        }
                        else
                        {
                            if (!SaveDefaultWebinarContent(1, webinarID, ContentType))
                            {
                                objWebinarContentBE.Add(new WebinarContentBE
                                {
                                    WebinarID = webinarID,
                                    ContentDescription = "No default content",
                                    ContentType = ContentType
                                });
                            }
                            else
                            {
                                return GetWebinarContent(webinarID, ContentType);
                            }
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarContentBE;

        }

        public void SaveWebinarContent(int WebinarID, string contentType, string contentDesc)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlWebinarContentSave, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@contentDetail", contentDesc));
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@contentType", contentType));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

    }
}
