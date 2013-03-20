using System;
using System.Collections.Generic;
using System.Data;
using EBird.BusinessEntity;
using EBird.Common;
using MySql.Data.MySqlClient;
using System.Data.Odbc;

namespace EBird.DataAccess
{
    public class DocumentDA
    {

        public List<DocumentBE> GetDocumentDA(int documentID)
        {
            List<DocumentBE> objDocumentBE = new List<DocumentBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetDocumentDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objDocumentBE.Add(new DocumentBE
                                {
                                    DocumentID = reader["DocID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["DocID"]),
                                    Category = reader["Category"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Category"]),
                                    OrginalFileName = reader["OrgFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrgFileName"]),
                                    SavedFileName = reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]),
                                    InsertDate = reader["InsertDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InsertDate"]),
                                    InsertedBy = reader["InsertedBy"] == DBNull.Value ? -1 : Convert.ToInt32(reader["InsertedBy"]),
                                    isResized = reader["isResized"] == DBNull.Value ? false : Convert.ToBoolean(reader["isResized"]),
                                    ClientID = reader["ClientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ClientID"])
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
            return objDocumentBE;
        }

        public string GetDocumentNameDA(int documentID, string DocType)
        {
            string rtnVal = "";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetDocumentDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            if (DocType.ToUpper() == "ORIGINAL")
                                rtnVal = reader["OrgFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrgFileName"]);
                            else
                                rtnVal = reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]);
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

        public string GetUserProfileImage(int userID)
        {
            string rtnVal = "";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserProfileImgPath, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnVal = reader["ClientID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientID"]);
                            rtnVal += "\\" + (reader["Category"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Category"]));
                            rtnVal += "\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
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

        public string GetPresenterProfileImage(int presenterID)
        {
            string rtnVal = "";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlPresenterProfileImgPath, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@presenterID", presenterID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnVal = reader["ClientID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientID"]);
                            rtnVal += "\\" + (reader["Category"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Category"]));
                            rtnVal += "\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
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

        public string GetDocumentPath(int documentID, bool isBase = false)
        {
            string rtnVal = "";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetDocumentDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        string folderName = string.Empty;
                        if (reader.HasRows)
                        {
                            reader.Read();

                            rtnVal = reader["ClientID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientID"]);
                            if (isBase)
                                rtnVal = Constant.DocRepoClient + rtnVal;
                            folderName = (reader["Category"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Category"]));
                            switch (folderName)
                            {
                                case "LOGO":
                                    rtnVal += "\\logo\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "BANNER":
                                    rtnVal += "\\logo\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "PROFILE":
                                    rtnVal += "\\Profile\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "WEBINARDOCS":
                                    rtnVal += "\\webinardocs\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "PRESENTATION":
                                    rtnVal += "\\webinardocs\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "SS":
                                    rtnVal += "\\SS\\" + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
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

        public string GetDocumentPath(int documentID, bool isBase, bool isHTMLPath)
        {
            string rtnVal = "";
            string pathDelimiter = "";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetDocumentDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        string folderName = string.Empty;
                        if (reader.HasRows)
                        {
                            reader.Read();

                            rtnVal = reader["ClientID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientID"]);
                            if (isBase)
                            {
                                if (isHTMLPath)
                                {
                                    rtnVal = Constant.ClientURL + rtnVal;
                                    pathDelimiter = "/";
                                }
                                else
                                {
                                    rtnVal = Constant.DocRepoClient + rtnVal;
                                    pathDelimiter = "\\";
                                }
                            }
                            folderName = (reader["Category"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Category"]));
                            switch (folderName)
                            {
                                case "LOGO":
                                    rtnVal += pathDelimiter + "logo" + pathDelimiter + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "BANNER":
                                    rtnVal += pathDelimiter + "logo" + pathDelimiter + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "PROFILE":
                                    rtnVal += pathDelimiter + "Profile" + pathDelimiter + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "WEBINARDOCS":
                                    rtnVal += pathDelimiter + "webinardocs" + pathDelimiter + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
                                case "PRESENTATION":
                                    rtnVal += pathDelimiter + "webinardocs" + pathDelimiter + (reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]));
                                    break;
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

        public List<DocumentBE> GetPresenterProfImg(int documentID, int userID)
        {
            {
                List<DocumentBE> objDocumentBE = new List<DocumentBE>();
                try
                {
                    using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlProImgRefSelectPresenter, sqlCon))
                        {
                            sqlCon.Open();
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                            sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                            MySqlDataReader reader = sqlCmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    objDocumentBE.Add(new DocumentBE
                                    {
                                        DocumentID = reader["DocID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["DocID"]),
                                        Category = reader["Category"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Category"]),
                                        OrginalFileName = reader["OrgFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrgFileName"]),
                                        SavedFileName = reader["savedFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["savedFileName"]),
                                        InsertDate = reader["InsertDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InsertDate"]),
                                        InsertedBy = reader["InsertedBy"] == DBNull.Value ? -1 : Convert.ToInt32(reader["InsertedBy"])
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
                return objDocumentBE;
            }
        }

        public int SaveDocumentDA(DocumentBE objDocumentBE)
        {
            int docID = objDocumentBE.DocumentID;
            string sql1 = "";

            if (docID == 0)
                sql1 = DBQuery.sqlDocumentInsert;
            else
                sql1 = DBQuery.sqlDocumentUpdate;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {


                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateDocumentRef", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objDocumentBE.ClientID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCategory", objDocumentBE.Category));
                    sqlCmd.Parameters.Add(new MySqlParameter("pOrgFileName", objDocumentBE.OrginalFileName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pSavedFileName", objDocumentBE.SavedFileName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pInsertedBy", objDocumentBE.InsertedBy));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDocID", objDocumentBE.DocumentID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", objDocumentBE.PresenterID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", objDocumentBE.WebinarID));

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        docID = Convert.ToInt32(reader.GetValue(0));
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                    //}
                    return docID;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int SaveDocumentDA(DocumentBE objDocumentBE, int userID, int webinarID)
        {
            int rtnDocID = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateDocumentRef", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objDocumentBE.ClientID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCategory", objDocumentBE.Category));
                    sqlCmd.Parameters.Add(new MySqlParameter("pOrgFileName", objDocumentBE.OrginalFileName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pSavedFileName", objDocumentBE.SavedFileName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pInsertedBy", objDocumentBE.InsertedBy));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDocID", objDocumentBE.DocumentID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", userID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebinarID", webinarID));

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnDocID = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnDocID;
        }

        public void SaveDocumentDA(int documentID, string fieldName, string fieldValue)
        {
            string sql1 = DBQuery.sqlDocumentFieldUpdate.Replace("##FieldName", fieldName).Replace("##FieldValue", fieldValue);
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePresenterImgDocID(int documentID, int presenterID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("Update tblpresenter set imgDocID=" + documentID.ToString() + ",isExternal=1 where presenterID=" + presenterID.ToString(), sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ResetPresenterImgDocID(int documentID, int userID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlResetPresenterImgRef, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();

                    sqlCmd = new MySqlCommand(DBQuery.sqlDocumentDelete, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@DocID", documentID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
