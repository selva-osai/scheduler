using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EBird.BusinessEntity;
using MySql.Data.MySqlClient;
using System.Data;
using EBird.Common;

namespace EBird.DataAccess
{
    public class SnapSiteDA
    {
        #region SnapSite

        public List<WebinarBE> GetMyPublicWebinarListDA(int userID)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetMyPublicWebinar, sqlCon))
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
                                    isSSPublished = reader["isSSPublished"] == DBNull.Value ? false : Convert.ToBoolean(reader["isSSPublished"]),
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

        public List<SnapSiteBO> GetMySnapSiteDetailsDA(int userID)
        {
            string sql1 = "select * from tblmysnapsite where userID=@userID";
            List<SnapSiteBO> objSnapSiteBO = new List<SnapSiteBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objSnapSiteBO.Add(new SnapSiteBO
                                {
                                    HeaderType = reader["headerType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["headerType"]),
                                    SkinID = reader["SkinID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["SkinID"]),
                                    Title = reader["ssTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ssTitle"]),
                                    Description = reader["ssDescription"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ssDescription"]),
                                    IsEnabled = reader["isEnabled"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEnabled"]),
                                    IsFacebook = reader["isFacebook"] == DBNull.Value ? false : Convert.ToBoolean(reader["isFacebook"]),
                                    IsLinkedIn = reader["isLinkedIn"] == DBNull.Value ? false : Convert.ToBoolean(reader["isLinkedIn"]),
                                    IsTwitter = reader["isTwitter"] == DBNull.Value ? false : Convert.ToBoolean(reader["isTwitter"]),
                                    UserID = userID 
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
            return objSnapSiteBO;
        }

        public void SaveSnapSiteSetting(SnapSiteBO objSS)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateSnapSiteSettings", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;                   
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", objSS.UserID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pTitle", objSS.Title));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDescription", objSS.Description));
                    sqlCmd.Parameters.Add(new MySqlParameter("pHeaderType", objSS.HeaderType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pSkinID", objSS.SkinID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsFaceBook", objSS.IsFacebook));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsTwitter", objSS.IsTwitter));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsLinkedIn", objSS.IsLinkedIn));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsEnabled", objSS.IsEnabled));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void PublishWebinarToSnapSite(string pubList, string unpubList)
        {
            string sql1 = "Update tblwebinars set isSSPublished = 1 where webinarID in (" + pubList + ")";
            string sql2 = "Update tblwebinars set isSSPublished = 0 where webinarID in (" + unpubList + ")";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {

                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd = new MySqlCommand(sql2, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarBE> GetSSPubWebinarListDA(int userID, bool isUpcoming)
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetSSWebinars", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserID", userID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pIsUpcoming", isUpcoming));
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

        #endregion

    }
}
