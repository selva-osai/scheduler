using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

namespace EBird.Common
{
    public class EBErrorMessages
    {
        public string getMessage(string errCode)
        {
            string rtnMessage = string.Empty;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("Select * from tblmaserrors where errorCode='" + errCode + "' and languageID=1", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rtnMessage = reader["errorMessage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["errorMessage"]);
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
            return rtnMessage;
        }

        public string getMessage(string errCode, string[] arrValues)
        {
            string rtnMessage = string.Empty;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("Select * from tblmaserrors where errorCode='" + errCode + "' and languageID=1", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rtnMessage = reader["errorMessage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["errorMessage"]);
                            }
                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                    if (rtnMessage != string.Empty)
                    {
                        for (int i = 0; i < arrValues.Length; i++)
                        {
                            rtnMessage = rtnMessage.Replace("#" + (i + 1).ToString() + "#", arrValues[i].ToString()); 
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return rtnMessage;
        }

    //    public string getMessage(string errCode, int languageID)
    //    {
    //        string rtnmessage = string.Empty;
    //        try
    //        {
    //            using (OdbcConnection sqlCon = new OdbcConnection(Constant.EBirdConnectionString))
    //            {
    //                using (OdbcCommand sqlCmd = new OdbcCommand(Select * from tblmasErrors where errorCode='" + errCode + "' and languageID=" + languageID.ToString() , sqlCon))
    //                {
    //                    sqlCon.Open();
    //                    sqlCmd.CommandType = CommandType.Text;
    //                    OdbcDataReader reader = sqlCmd.ExecuteReader();
    //                    if (reader.HasRows)
    //                    {
    //                        while (reader.Read())
    //                        {
    //                            rtnMessage = reader["errorMessage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["errorMessage"]),
    //                        }
    //                    }
    //                    reader.Close();
    //                    reader = null;
    //                }
    //                sqlCon.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw;
    //        }
    //        return rtnmessage;
    //    }
    
    }
}
