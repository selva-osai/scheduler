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
    public class SocialMediaDA
    {
        #region facebook
        public List<FaceBookSettingBO> GetFaceBookSettingDA(int webinarID)
        {
            List<FaceBookSettingBO> objFaceBookSettingBO = new List<FaceBookSettingBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(SocialMediaQuery.sqlFBSettingSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objFaceBookSettingBO.Add(new FaceBookSettingBO
                                { 
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    checkInterval = reader["checkInterval"] == DBNull.Value ? -1 : Convert.ToInt32(reader["checkInterval"]),
                                    defaultStatusMessage = reader["defaultStatusMessage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["defaultStatusMessage"]),
                                    isComments = reader["isComments"] == DBNull.Value ? false : Convert.ToBoolean(reader["isComments"]),
                                    isFriend = reader["isFriend"] == DBNull.Value ? false : Convert.ToBoolean(reader["isFriend"]),
                                    isLikeUnlike = reader["isLikeUnlike"] == DBNull.Value ? false : Convert.ToBoolean(reader["isLikeUnlike"]),
                                    isSearch = reader["isSearch"] == DBNull.Value ? false : Convert.ToBoolean(reader["isSearch"]),
                                    isStatusUpdate = reader["isStatusUpdate"] == DBNull.Value ? false : Convert.ToBoolean(reader["isStatusUpdate"]),
                                    messageReturn = reader["messageReturn"] == DBNull.Value ? -1 : Convert.ToInt32(reader["messageReturn"])
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
            return objFaceBookSettingBO;
        }

        public void SaveFaceBookSettingDA(FaceBookSettingBO objFaceBookSettingBO, bool isCancel)
        {
            //string sql1;
            //List<FaceBookSettingBO> objFBSettingBO = GetFaceBookSettingDA(objFaceBookSettingBO.WebinarID);
            //if (objFBSettingBO.Count > 0)
            //    sql1 = SocialMediaQuery.sqlFBSettingUpdate;
            //else
            //    sql1 = SocialMediaQuery.sqlFBSettingInsert;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateFaceBookSettings", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsStatusUpdate", objFaceBookSettingBO.isStatusUpdate));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDefaultStatusMessage", objFaceBookSettingBO.defaultStatusMessage));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsLikeUnlike", objFaceBookSettingBO.isLikeUnlike));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsComments", objFaceBookSettingBO.isComments));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsFriend", objFaceBookSettingBO.isFriend));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsSearch", objFaceBookSettingBO.isSearch));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCheckInterval", objFaceBookSettingBO.checkInterval));
                    sqlCmd.Parameters.Add(new MySqlParameter("pMessageReturn", objFaceBookSettingBO.messageReturn));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objFaceBookSettingBO.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsCancel", isCancel));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region LinkedIn
        public List<LinkedInSettingBO> GetLinkedInSettingDA(int webinarID)
        {
            List<LinkedInSettingBO> objLinkedInSettingBO = new List<LinkedInSettingBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(SocialMediaQuery.sqlLISettingSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objLinkedInSettingBO.Add(new LinkedInSettingBO
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    isLikeUnlike = reader["isLikeUnlike"] == DBNull.Value ? false : Convert.ToBoolean(reader["isLikeUnlike"]),
                                    isComments = reader["isComments"] == DBNull.Value ? false : Convert.ToBoolean(reader["isComments"]),
                                    isSearch = reader["isSearch"] == DBNull.Value ? false : Convert.ToBoolean(reader["isSearch"]),
                                    isFilterNetwork = reader["isFilterNetwork"] == DBNull.Value ? false : Convert.ToBoolean(reader["isFilterNetwork"]),
                                    checkInterval = reader["checkInterval"] == DBNull.Value ? -1 : Convert.ToInt32(reader["checkInterval"]),
                                    messageReturn = reader["messageReturn"] == DBNull.Value ? -1 : Convert.ToInt32(reader["messageReturn"]),
                                    defaultInviteSubject = reader["defaultInviteSubject"] == DBNull.Value ? string.Empty : Convert.ToString(reader["defaultInviteSubject"]),
                                    defaultInviteMessage = reader["defaultInviteMessage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["defaultInviteMessage"]),
                                    isNetworkUpdate = reader["isNetworkUpdate"] == DBNull.Value ? false : Convert.ToBoolean(reader["isNetworkUpdate"]),
                                    isInvitation = reader["isInvitation"] == DBNull.Value ? false : Convert.ToBoolean(reader["isInvitation"])
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
            return objLinkedInSettingBO;
        }

        public void SaveLinkedInSettingDA(LinkedInSettingBO objLinkedInSettingBO, bool isCancel)
        {
            //string sql1;
            //List<LinkedInSettingBO> objLISettingBO = GetLinkedInSettingDA(objLinkedInSettingBO.WebinarID);
            //if (objLISettingBO.Count > 0)
            //    sql1 = SocialMediaQuery.sqlLISettingUpdate;
            //else
            //    sql1 = SocialMediaQuery.sqlLISettingInsert;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateLinkedInSettings", sqlCon);
                    sqlCon.Open(); 
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsLikeUnlike", objLinkedInSettingBO.isLikeUnlike));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsComments", objLinkedInSettingBO.isComments));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsSearch", objLinkedInSettingBO.isSearch));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsFilterNetwork", objLinkedInSettingBO.isFilterNetwork));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCheckInterval", objLinkedInSettingBO.checkInterval));
                    sqlCmd.Parameters.Add(new MySqlParameter("pMessageReturn", objLinkedInSettingBO.messageReturn));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDefaultInviteSubject", objLinkedInSettingBO.defaultInviteSubject));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDefaultInviteMessage", objLinkedInSettingBO.defaultInviteMessage));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsNetworkUpdate", objLinkedInSettingBO.isNetworkUpdate));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsInvitation", objLinkedInSettingBO.isInvitation));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objLinkedInSettingBO.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsCancel", isCancel));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Twitter
        public List<TwitterSettingBO> GetTwitterSettingDA(int webinarID)
        {
            List<TwitterSettingBO> objTwitterSettingBO = new List<TwitterSettingBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(SocialMediaQuery.sqlTWSettingSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objTwitterSettingBO.Add(new TwitterSettingBO
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["WebinarID"]),
                                    dispFromAcct = reader["dispFromAcct"] == DBNull.Value ? string.Empty : Convert.ToString(reader["dispFromAcct"]),
                                    filterKeywords = reader["filterKeywords"] == DBNull.Value ? string.Empty : Convert.ToString(reader["filterKeywords"]),
                                    headerTitle = reader["headerTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["headerTitle"]),
                                    isUserTweet = reader["isUserTweet"] == DBNull.Value ? false : Convert.ToBoolean(reader["isUserTweet"]),
                                    tweetHashtags = reader["tweetHashtags"] == DBNull.Value ? string.Empty : Convert.ToString(reader["tweetHashtags"]),
                                    twitterAcct = reader["twitterAcct"] == DBNull.Value ? string.Empty : Convert.ToString(reader["twitterAcct"]),
                                    userHashtags = reader["userHashtags"] == DBNull.Value ? string.Empty : Convert.ToString(reader["userHashtags"]),
                                    userTextURL = reader["userTextURL"] == DBNull.Value ? string.Empty : Convert.ToString(reader["userTextURL"])
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
            return objTwitterSettingBO;
        }

        public void SaveTwitterSettingDA(TwitterSettingBO objTwitterSettingBO, bool isCancel)
        {
         
            //string sql1;
            //List<TwitterSettingBO> objTWSettingBO = GetTwitterSettingDA(objTwitterSettingBO.WebinarID);
            //if (objTWSettingBO.Count > 0)
            //    sql1 = SocialMediaQuery.sqlTWSettingUpdate;
            //else
            //    sql1 = SocialMediaQuery.sqlTWSettingInsert;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateTwitterSettings", sqlCon);
                    sqlCon.Open();  
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pTwitterAcct", objTwitterSettingBO.twitterAcct));
                    sqlCmd.Parameters.Add(new MySqlParameter("pHeaderTitle", objTwitterSettingBO.headerTitle));
                    sqlCmd.Parameters.Add(new MySqlParameter("pTweetHashtags", objTwitterSettingBO.tweetHashtags));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDispFromAcct", objTwitterSettingBO.dispFromAcct));
                    sqlCmd.Parameters.Add(new MySqlParameter("pFilterKeywords", objTwitterSettingBO.filterKeywords));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsUserTweet", objTwitterSettingBO.isUserTweet));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserHashtags", objTwitterSettingBO.userHashtags));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserTextURL", objTwitterSettingBO.userTextURL));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objTwitterSettingBO.WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsCancel", isCancel));
                    sqlCmd.ExecuteNonQuery();
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
