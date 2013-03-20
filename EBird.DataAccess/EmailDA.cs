using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.Framework;
using MySql.Data.MySqlClient;

namespace EBird.DataAccess
{
    public class EmailDA
    {
        public int SaveEmailRequest(EmailBE objEmailBE)
        {
            int requestID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    //MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlRequestInsert, sqlCon);
                    MySqlCommand sqlCmd = new MySqlCommand("spSaveEmailingRequest", sqlCon);
                    
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add(new MySqlParameter("pMailType", objEmailBE.RequestType));
                    sqlCmd.Parameters.Add(new MySqlParameter("pSubject", objEmailBE.Subject));
                    sqlCmd.Parameters.Add(new MySqlParameter("pEmailContent", objEmailBE.EmailContent));
                    sqlCmd.Parameters.Add(new MySqlParameter("pFromEmail", objEmailBE.FromEmail));
                    sqlCmd.Parameters.Add(new MySqlParameter("pFromName", objEmailBE.FromName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsToEmailRef", objEmailBE.isToEmailRef));
                    sqlCmd.Parameters.Add(new MySqlParameter("pToEmail", objEmailBE.ToEmail));
                    sqlCmd.Parameters.Add(new MySqlParameter("pToEmailName", objEmailBE.ToEmailName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pSubmittedBy", objEmailBE.SubmittedBy));
                    sqlCmd.Parameters.Add(new MySqlParameter("pRequestStatus", objEmailBE.RequestStatus));
                    //sqlCmd.ExecuteNonQuery();

                    //sqlCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", sqlCon);
                    //sqlCmd.CommandType = CommandType.Text;

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        requestID = Convert.ToInt32(reader.GetValue(0));
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
            return requestID;
        }

        public void SaveToEmail(EmailTo objEmailTo)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlEmailToInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailRequestID", objEmailTo.EmailRequestID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@ToType", objEmailTo.ToType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@ToEmails", objEmailTo.ToEmails));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EmailAddressBO> GetToEmailAddress(int webinarID, string ToType)
        {
            string sql1 = "";
            switch (ToType.ToUpper())
            {
                case "REGISTERED":
                    sql1 = DBEmailQuery.sqlRegistrantEmail;
                    break;
                case "ATTENDED":
                    sql1 = DBEmailQuery.sqlAttendedRegistrantEmail;
                    break;
                case "NOTATTENDED":
                    sql1 = DBEmailQuery.sqlNotAttendedRegistrantEmail;
                    break;
                case "LIVE":
                    sql1 = DBEmailQuery.sqlAttendedLiveRegistrantEmail;
                    break;
                case "ONDEMAND":
                    sql1 = DBEmailQuery.sqlAttendedOnDemandRegistrantEmail;
                    break;
            }
            List<EmailAddressBO> objEmailAddress = new List<EmailAddressBO>();
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
                                objEmailAddress.Add(new EmailAddressBO
                                {
                                    AddresseName = reader["Registrant"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Registrant"]),
                                    EmailAddress = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"])
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
            return objEmailAddress;
        }

        public List<RegistrantEmailSettingBO> GetRegistrantEmailSetting(int webinarID, string SettingType)
        {
            List<RegistrantEmailSettingBO> objRegistrantEmailSettingBO = new List<RegistrantEmailSettingBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlRegistrantEmailSetting, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@SettingType", SettingType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objRegistrantEmailSettingBO.Add(new RegistrantEmailSettingBO
                                {
                                    setID = reader["setID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["setID"]),
                                    intervalType = reader["intervalType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["intervalType"]),
                                    intervalValue = reader["intervalValue"] == DBNull.Value ? 0 : Convert.ToInt32(reader["intervalValue"]),
                                    EmailScheduleStatus = reader["EmailScheduleStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailScheduleStatus"])
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
            return objRegistrantEmailSettingBO;
        }

        public void SaveRegistrantEmailSetting(RegistrantEmailSettingBO objRegistrantEmailSettingBO)
        {
            string sql1 = "";
            if (objRegistrantEmailSettingBO.setID == 0)
                sql1 = DBEmailQuery.sqlRegistrantEmailSettingInsert;
            else
                sql1 = DBEmailQuery.sqlRegistrantEmailSettingUpdate;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objRegistrantEmailSettingBO.webinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@intervalType", objRegistrantEmailSettingBO.intervalType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@intervalValue", objRegistrantEmailSettingBO.intervalValue));
                    sqlCmd.Parameters.Add(new MySqlParameter("@SettingType", objRegistrantEmailSettingBO.SettingType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@EmailScheduleStatus", objRegistrantEmailSettingBO.EmailScheduleStatus));
                    if (objRegistrantEmailSettingBO.setID > 0)
                        sqlCmd.Parameters.Add(new MySqlParameter("@setID", objRegistrantEmailSettingBO.setID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RegistrantUpdateBO> GetRegistrantUpdate(int webinarID)
        {
            List<RegistrantUpdateBO> objRegistrantUpdateBO = new List<RegistrantUpdateBO>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlGetRegistrantUpdates, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@WebinarID", webinarID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objRegistrantUpdateBO.Add(new RegistrantUpdateBO
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarID"]),
                                    IsRegularUpdate = reader["IsRegularUpdate"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRegularUpdate"]),
                                    UpdateWeekday = reader["UpdateWeekday"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UpdateWeekday"]),
                                    UpdateTime = reader["UpdateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UpdateTime"]),
                                    IsUpdateWhenRegister = reader["IsUpdateWhenReg"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsUpdateWhenReg"]),
                                    updateToEmails = reader["UpdateToEmails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UpdateToEmails"])
                                });
                            }
                            reader.Close();
                            reader = null;
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objRegistrantUpdateBO;
        }
    
        public void SaveRegistrantUpdate(RegistrantUpdateBO objRegistrantUpdateBO)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spSaveRegistrantUpdate", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsRegularUpdate", objRegistrantUpdateBO.IsRegularUpdate));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUpdateWeekday", objRegistrantUpdateBO.UpdateWeekday));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUpdateTime", objRegistrantUpdateBO.UpdateTime));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsUpdateWhenReg", objRegistrantUpdateBO.IsUpdateWhenRegister));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUpdateToEmails", objRegistrantUpdateBO.updateToEmails));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objRegistrantUpdateBO.WebinarID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarEmailBE> GetWebinarEmail(int webinarID, string emailType)
        {
            List<WebinarEmailBE> objWebinarEmailBE = new List<WebinarEmailBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetWebinarEmail", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pEmailType", emailType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarEmailBE.Add(new WebinarEmailBE
                                {
                                    WebinarID = reader["WebinarID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WebinarID"]),
                                    EmailContent = reader["emailContent"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailContent"]),
                                    Subject = reader["emailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailSubject"]),
                                    RequestType = emailType,
                                    ThemeID = reader["thLayoutID"] == DBNull.Value ? 1 : Convert.ToInt32(reader["thLayoutID"]),
                                    IsSystemReq = reader["isSystemReq"] == DBNull.Value ? false : Convert.ToBoolean(reader["isSystemReq"]),
                                    IsOutlookLink = reader["isCalenderAddition"] == DBNull.Value ? false : Convert.ToBoolean(reader["isCalenderAddition"]),
                                    IsAdditionalWebinar = reader["isAddtnlWebinar"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAddtnlWebinar"])
                                });
                            }
                            reader.Close();
                            reader = null;
                        }
                        else
                        {
                            if (!SaveDefaultWebinarEmailContent(1, webinarID, emailType))
                            {
                                objWebinarEmailBE.Add(new WebinarEmailBE
                                {
                                    WebinarID = webinarID,
                                    EmailContent = "No default content",
                                    Subject = "",
                                    RequestType = emailType
                                });
                            }
                            else
                            {
                                return GetWebinarEmail(webinarID, emailType);
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
            return objWebinarEmailBE;
        }
    
        public void SaveWebinarEmail(WebinarEmailBE objWebinarEmailBE, bool isInsert = false)
        {
            string sql1 = "";
            if (!isInsert)
                sql1 = DBEmailQuery.sqlWebinarEmailUpdate;
            else
                sql1 = DBEmailQuery.sqlWebinarEmailInsert;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();  
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailSubject", objWebinarEmailBE.Subject));
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailContent", objWebinarEmailBE.EmailContent));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isSystemReq", objWebinarEmailBE.IsSystemReq));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isCalenderAddition", objWebinarEmailBE.IsOutlookLink)); 
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailType", objWebinarEmailBE.RequestType)); 
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", objWebinarEmailBE.WebinarID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<WebinarEmailBE> GetWebinarEmailDefault(int languageID, string emailType)
        {
            List<WebinarEmailBE> objWebinarEmailBE = new List<WebinarEmailBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlDefaultEmailContentSelect, sqlCon))
                    {  
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@languageID", languageID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@emailType", emailType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarEmailBE.Add(new WebinarEmailBE
                                {
                                    EmailContent = reader["emailContent"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailContent"]),
                                    Subject = reader["emailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailSubject"])
                                });
                            }
                            reader.Close();
                            reader = null;
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarEmailBE;
        }

        public List<WebinarEmailBE> GetGeneralEmailDefault(string emailType, int languageID)
        {
            List<WebinarEmailBE> objWebinarEmailBE = new List<WebinarEmailBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlDefaultEmailContentSelect, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@languageID", languageID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@emailType", emailType));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objWebinarEmailBE.Add(new WebinarEmailBE
                                {
                                    EmailContent = reader["emailContent"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailContent"]),
                                    Subject = reader["emailSubject"] == DBNull.Value ? string.Empty : Convert.ToString(reader["emailSubject"])
                                });
                            }
                            reader.Close();
                            reader = null;
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objWebinarEmailBE;
        }

        public void SaveWebinarEmailDefault(int languageID, string emailType, string emailContent, string emailSubject)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlDefaultEmailContentUpdate, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@emaillContent", emailContent));
                    sqlCmd.Parameters.Add(new MySqlParameter("@languageID", languageID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailType", emailType));
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailSubject", emailSubject));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveDefaultEmailAFriendContent(int languageID,int WebinarID)
        {
            List<WebinarEmailBE> objWebinarEmailBE = GetWebinarEmailDefault(languageID, "Email a Friend");
            if (objWebinarEmailBE.Count > 0)
            {
                WebinarEmailBE objWebEmail = new WebinarEmailBE();
                objWebEmail.Subject = objWebinarEmailBE[0].Subject;
                
                if (objWebinarEmailBE[0].EmailContent.IndexOf("##AUDI_URL##") > 0)
                {
                    WebinarDA objWebDA = new WebinarDA();
                    List<WebinarURLs> objURL = objWebDA.GetWebinarURLsDA(WebinarID);
                    objWebEmail.EmailContent = objWebinarEmailBE[0].EmailContent.Replace("##AUDI_URL##", Constant.WebinarViewerBaseURL + objURL[0].AudienceInterfaceURL);
                }
                else
                    objWebEmail.EmailContent = objWebinarEmailBE[0].EmailContent;

                objWebEmail.RequestType = "Email a Friend";
                objWebEmail.WebinarID = WebinarID;
                SaveWebinarEmail(objWebEmail);
            }
        }

        public bool SaveDefaultWebinarEmailContent(int languageID, int WebinarID, string emailType)
        {
            int rtnRec = 0;
            try
            {  
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBEmailQuery.sqlDefaultWebinarEmailContentInsert, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@webinarID", WebinarID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@languageID", languageID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailType", emailType));
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

        public List<EmailBE> GetOpenRequests()
        {

            string strToEmail = "";
            List<EmailBE> objEmailBE = new List<EmailBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("select * from tblemailingrequest where mailType='No-delay' and requestStatus='Open'", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            List<EmailTo> objEmailTo = new List<EmailTo>();
                            while (reader.Read())
                            {
                                strToEmail = "";
                                objEmailTo.Clear();

                                if (Convert.ToBoolean(reader["isToEmailRef"]))
                                    objEmailTo = GetToEmails(Convert.ToInt32(reader["emailRequestID"]));
                                else
                                {
                                    strToEmail = reader["ToEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ToEmail"]);
                                    if (strToEmail != string.Empty)
                                    {
                                        objEmailTo.Add(new EmailTo
                                        {
                                            ToEmails = strToEmail
                                        });
                                    }
                                }

                                if (objEmailTo.Count == 0)
                                {
                                    UpdateRequestStatus(Convert.ToInt32(reader["emailRequestID"]), "Cancelled", "Missing To email(s)");
                                }
                                else
                                {
                                    objEmailBE.Add(new EmailBE
                                    {
                                        RequestID = reader["emailRequestID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["emailRequestID"]),
                                        FromEmail = reader["fromEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["fromEmail"]),
                                        ToEmailList = objEmailTo,
                                        Subject = reader["subject"] == DBNull.Value ? string.Empty : Convert.ToString(reader["subject"]),
                                        EmailContent = reader["emailContent"] == DBNull.Value ? string.Empty : reader["emailContent"].ToString()
                                    });
                                }
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
            return objEmailBE;
        }

        public List<EmailBE> GetRequest(int requestID)
        {
            string strToEmail = "";
            List<EmailBE> objEmailBE = new List<EmailBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("select * from tblemailingrequest where emailRequestID=" + requestID.ToString(), sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            List<EmailTo> objEmailTo = new List<EmailTo>();
                            while (reader.Read())
                            {
                                strToEmail = "";
                                objEmailTo.Clear();

                                if (Convert.ToBoolean(reader["isToEmailRef"]))
                                    objEmailTo = GetToEmails(Convert.ToInt32(reader["emailRequestID"]));
                                else
                                {
                                    strToEmail = reader["ToEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ToEmail"]);
                                   
                                    if (strToEmail != string.Empty)
                                    {
                                        EBirdUtility objutil = new EBirdUtility();
                                        System.Collections.ArrayList arr = objutil.StringToArrayList(strToEmail, new char[] { ';' });
                                        for (int idx = 0; idx < arr.Count; idx++)
                                        {
                                            objEmailTo.Add(new EmailTo
                                            {
                                                ToEmails = arr[idx].ToString()
                                            });
                                        }
                                    }
                                }

                                if (objEmailTo.Count == 0)
                                {
                                    UpdateRequestStatus(Convert.ToInt32(reader["emailRequestID"]), "Cancelled", "Missing To email(s)");
                                }
                                else
                                {
                                    objEmailBE.Add(new EmailBE
                                    {
                                        RequestType = reader["mailType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["mailType"]), 
                                        RequestID = reader["emailRequestID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["emailRequestID"]),
                                        FromEmail = reader["fromEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["fromEmail"]),
                                        ToEmailList = objEmailTo,
                                        Subject = reader["subject"] == DBNull.Value ? string.Empty : Convert.ToString(reader["subject"]),
                                        EmailContent = reader["emailContent"] == DBNull.Value ? string.Empty : reader["emailContent"].ToString()
                                    });
                                }
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
            return objEmailBE;
        }

        public List<EmailTo> GetToEmails(int requestID)
        {
            string strToEmail = "";
            List<EmailTo> objEmailTo = new List<EmailTo>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("select * from tblemailingto where emailRequestID=" + requestID.ToString(), sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objEmailTo.Add(new EmailTo
                                {
                                    ToEmails = reader["ToEmails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ToEmails"]),
                                    ToType = reader["ToType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ToType"])
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
            return objEmailTo;
        }

        public void UpdateRequestStatus(int requestID, string requestStatus, string statusComments)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("Update tblemailingrequest set requestStatus=@requestStatus,IssueDetail=@IssueDetail,processedOn=@processedOn where emailRequestID=@emailRequestID", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;

                    sqlCmd.Parameters.Add(new MySqlParameter("@requestStatus", requestStatus));
                    sqlCmd.Parameters.Add(new MySqlParameter("@IssueDetail", statusComments));
                    sqlCmd.Parameters.Add(new MySqlParameter("@processedOn", DateTime.Now));
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailRequestID", requestID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void UpdateEmailLog(EmailingLog objEmailingLog)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("Insert into tblemailinglogs(mailAction,mailFrom,mailTo,mailSubject,actionInfo) values(@mailAction,@mailFrom,@mailTo,@mailSubject,@actionInfo)", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@mailAction", objEmailingLog.MailAction));
                    sqlCmd.Parameters.Add(new MySqlParameter("@mailFrom", objEmailingLog.MailFrom));
                    sqlCmd.Parameters.Add(new MySqlParameter("@mailTo", objEmailingLog.MailTo));
                    sqlCmd.Parameters.Add(new MySqlParameter("@mailSubject", objEmailingLog.MailSubject));
                    sqlCmd.Parameters.Add(new MySqlParameter("@actionInfo", objEmailingLog.ActionInfo));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
