using System;
using System.Collections.Generic;
using System.Data;
using EBird.BusinessEntity;
using EBird.Common;
using MySql.Data.MySqlClient;
using System.Data.Odbc;

namespace EBird.DataAccess
{
    public class ClientDA
    {
        public List<ClientBE> GetClientDetailDA(int clientID)
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.vwClient + " where clientID=" + clientID.ToString(), sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objClientBE.Add(new ClientBE
                                {
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    ClientName = reader["ClientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    Address2 = reader["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address2"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    PostCode = reader["PostCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PostCode"]),
                                    CountryID = reader["CountryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["CountryID"]),
                                    AnnualRevID = reader["AnnualRevID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["AnnualRevID"]),
                                    ClientStatus = reader["ClientStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientStatus"]),
                                    CurrentPkgSubscribed = reader["CurrentPkgSubscribed"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CurrentPkgSubscribed"]),
                                    DateFormat = reader["DateFormat"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateFormat"]),
                                    Website = reader["Website"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Website"]),
                                    IndustryID = reader["IndustryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IndustryID"]),
                                    LanguageID = reader["LanguageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["LanguageID"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"]),
                                    NoOfUsers = reader["NoOfUsers"] == DBNull.Value ? -1 : Convert.ToInt32(reader["NoOfUsers"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    LastModified = reader["LastModified"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastModified"])
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
            return objClientBE;
        }

        public List<ClientBE> GetClientDetailDA()
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.vwClient + " where clientStatus='Active'", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objClientBE.Add(new ClientBE
                                {
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    ClientName = reader["ClientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    Address2 = reader["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address2"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    PostCode = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    CountryID = reader["CountryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["CountryID"]),
                                    AnnualRevID = reader["AnnualRevID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["AnnualRevID"]),
                                    ClientStatus = reader["ClientStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientStatus"]),
                                    CurrentPkgSubscribed = reader["CurrentPkgSubscribed"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CurrentPkgSubscribed"]),
                                    DateFormat = reader["DateFormat"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateFormat"]),
                                    Website = reader["Website"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Website"]),
                                    IndustryID = reader["IndustryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IndustryID"]),
                                    LanguageID = reader["LanguageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["LanguageID"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"]),
                                    NoOfUsers = reader["NoOfUsers"] == DBNull.Value ? -1 : Convert.ToInt32(reader["NoOfUsers"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"])
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
            return objClientBE;
        }

        public List<ClientBE> GetClientDetailDA(string clientStatus)
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.vwClient + " where clientStatus='" + clientStatus + "'", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objClientBE.Add(new ClientBE
                                {
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    ClientName = reader["ClientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    Address2 = reader["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address2"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    PostCode = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    CountryID = reader["CountryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["CountryID"]),
                                    AnnualRevID = reader["AnnualRevID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["AnnualRevID"]),
                                    ClientStatus = reader["ClientStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientStatus"]),
                                    CurrentPkgSubscribed = reader["CurrentPkgSubscribed"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CurrentPkgSubscribed"]),
                                    DateFormat = reader["DateFormat"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateFormat"]),
                                    Website = reader["Website"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Website"]),
                                    IndustryID = reader["IndustryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IndustryID"]),
                                    LanguageID = reader["LanguageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["LanguageID"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"]),
                                    NoOfUsers = reader["NoOfUsers"] == DBNull.Value ? -1 : Convert.ToInt32(reader["NoOfUsers"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"])
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
            return objClientBE;
        }

        public List<ClientBE> GetClientDetailDA(string clientName, string packageType, string regStartDate, string regEndDate)
        {

            string sql1 = DBQuery.vwClient + " where clientStatus in ('Active','Inactive') ";

            if (clientName != "")
                sql1 += " and clientName like '%" + clientName + "%'";
            if (packageType != "All")
                sql1 += " and currentPkgSubscribed='" + packageType + "'";
            if (regStartDate != "" && regEndDate != "")
                sql1 += " and createdOn BETWEEN '" + regStartDate + "' AND '" + regEndDate + "'";
            else if (regStartDate == "" && regEndDate != "")
                sql1 += " and createdOn <= '" + regEndDate + "'";
            else if (regEndDate == "" && regStartDate != "")
                sql1 += " and createdOn >= '" + regStartDate + "'";

            List<ClientBE> objClientBE = new List<ClientBE>();
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
                            int webCount = 0;
                            while (reader.Read())
                            {
                                webCount = getWebinarCount(Convert.ToInt32(reader["clientID"]));
                                objClientBE.Add(new ClientBE
                                {
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    ClientName = reader["ClientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    Address2 = reader["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address2"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    PostCode = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    CountryID = reader["CountryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["CountryID"]),
                                    AnnualRevID = reader["AnnualRevID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["AnnualRevID"]),
                                    ClientStatus = reader["ClientStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientStatus"]),
                                    CurrentPkgSubscribed = reader["CurrentPkgSubscribed"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CurrentPkgSubscribed"]),
                                    DateFormat = reader["DateFormat"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateFormat"]),
                                    Website = reader["Website"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Website"]),
                                    IndustryID = reader["IndustryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IndustryID"]),
                                    LanguageID = reader["LanguageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["LanguageID"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"]),
                                    NoOfUsers = reader["NoOfUsers"] == DBNull.Value ? -1 : Convert.ToInt32(reader["NoOfUsers"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    NoOfWebinars = webCount
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
            return objClientBE;
        }

        public List<ClientBE> GetClientDetailDA(string date1, string date2)
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.vwClientBetweenDates, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@date1", date1));
                        sqlCmd.Parameters.Add(new MySqlParameter("@date2", date2));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            int webCount = 0;
                            while (reader.Read())
                            {
                                webCount = getWebinarCount(Convert.ToInt32(reader["clientID"]));
                                objClientBE.Add(new ClientBE
                                {
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    ClientName = reader["ClientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    Address2 = reader["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address2"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    PostCode = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    CountryID = reader["CountryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["CountryID"]),
                                    AnnualRevID = reader["AnnualRevID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["AnnualRevID"]),
                                    ClientStatus = reader["ClientStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ClientStatus"]),
                                    CurrentPkgSubscribed = reader["CurrentPkgSubscribed"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CurrentPkgSubscribed"]),
                                    DateFormat = reader["DateFormat"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateFormat"]),
                                    Website = reader["Website"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Website"]),
                                    IndustryID = reader["IndustryID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IndustryID"]),
                                    LanguageID = reader["LanguageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["LanguageID"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TimeZoneID"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"]),
                                    NoOfUsers = reader["NoOfUsers"] == DBNull.Value ? -1 : Convert.ToInt32(reader["NoOfUsers"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    LastModified = reader["LastModified"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastModified"]),
                                    NoOfWebinars = webCount
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
            return objClientBE;
        }

        public int getWebinarCount(int clientID)
        {
            string sql1 = "select count(b.webinarID) as webinatCount from tbluseracct a, tblwebinars b where b.createdBy = a.userID and a.clientID=" + clientID.ToString();
            int rtnVal = 0;

            List<ClientBE> objClientBE = new List<ClientBE>();
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
                            rtnVal = Convert.ToInt32(reader[0]);
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

        public int SaveClientProfileDA(ClientBE objClientBE)
        {
            int rtnVal = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spSaveClientProfile", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add(new MySqlParameter("pClientName", objClientBE.ClientName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pAddress1", objClientBE.Address1));
                    sqlCmd.Parameters.Add(new MySqlParameter("pAddress2", objClientBE.Address2));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCity", objClientBE.City));
                    sqlCmd.Parameters.Add(new MySqlParameter("pState", objClientBE.State));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCountryID", objClientBE.CountryID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pPostCode", objClientBE.PostCode));
                    sqlCmd.Parameters.Add(new MySqlParameter("pPhone", objClientBE.Phone));
                    sqlCmd.Parameters.Add(new MySqlParameter("pWebsite", objClientBE.Website));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIndustryID", objClientBE.IndustryID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pAnnualRevID", objClientBE.AnnualRevID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pNoOfUsers", objClientBE.NoOfUsers));
                    sqlCmd.Parameters.Add(new MySqlParameter("pClientStatus", objClientBE.ClientStatus));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCurrentPkgSubscribed", objClientBE.CurrentPkgSubscribed));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", objClientBE.CreatedBy));
                    sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objClientBE.ClientID));

                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                    return rtnVal;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //public void LogAudit(int createdby, int actionID, int ClientID, String Actiondetail)
        //{
            
            
        //}

        public void UpdateClientStatusDA(int clientID, string clientStatus, int modifiedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spUpdateClientStatus", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientStatus", clientStatus));
                        sqlCmd.Parameters.Add(new MySqlParameter("pModifiedBy", modifiedBy));
                        sqlCmd.Parameters.Add(new MySqlParameter("pClient", clientID));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ContactBE> GetClientContactDA(int clientID)
        {
            List<ContactBE> objContactBE = new List<ContactBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetClientContact, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objContactBE.Add(new ContactBE
                                {
                                    ContactID = reader["ContactID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ContactID"]),
                                    Contactname = reader["ContactName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContactName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                    Department = reader["Department"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Department"]),
                                    ClientID = reader["ClientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ClientID"]),
                                    ContactStatus = reader["ContactStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContactStatus"]),
                                    JobTitle = reader["JobTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["JobTitle"])
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
            return objContactBE;
        }

        public int SaveClientContactDA(ContactBE objContactBE)
        {
            int contactID = objContactBE.ContactID;
            string sql1 = "";
            if (contactID == 0)
                sql1 = DBQuery.sqlContactInsert;
            else
                sql1 = DBQuery.sqlContactUpdate;

            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@ContactName", objContactBE.Contactname));
                        sqlCmd.Parameters.Add(new MySqlParameter("@Phone", objContactBE.Phone));
                        sqlCmd.Parameters.Add(new MySqlParameter("@Email", objContactBE.Email));
                        sqlCmd.Parameters.Add(new MySqlParameter("@Department", objContactBE.Department));
                        sqlCmd.Parameters.Add(new MySqlParameter("@Address1", objContactBE.Address1));
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", objContactBE.ClientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@ContactStatus", "Active"));
                        sqlCmd.Parameters.Add(new MySqlParameter("@jobTitle", objContactBE.JobTitle));
                        if (contactID != 0)
                            sqlCmd.Parameters.Add(new MySqlParameter("@ContactID", objContactBE.ContactID));

                        sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                    return contactID;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InitClientConfigDA(int clientID, string package, bool isClientPkgUpdate)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    if (isClientPkgUpdate)
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientPackageUpdate, sqlCon))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add(new MySqlParameter("@currentPkgSubscribed", package));
                            sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                            sqlCmd.Connection.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Connection.Close();
                        }
                    }
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientConfigInit, sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@package", package));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlPkgHistoryInsert, sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@pkgName", package));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        // -----------------------------------------------------------------//
        //   Delete all feature association to client-> clientID            //
        //   Update the package info in client table followed by            //
        //   Insert the package feature in clientconfig table               //
        // -----------------------------------------------------------------//
        public void UpdateClientPackageFeatureDA(int clientID, string package, int updatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spUpdateClientPkgFeature", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pPackageSub", package));
                        sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", updatedBy));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        // -----------------------------------------------------------------//
        //   Custom Package Update                                          //
        //   Delete all feature association to client-> clientID            //
        //   Update the package info in client table followed by            //
        //   Insert the package feature in clientconfig table               //
        // -----------------------------------------------------------------//
        public void UpdateClientPackageFeaturesDA(int clientID, string featureList, int updatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spUpdateClientCustomPkgFeature", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pFeaturlist", featureList));
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", updatedBy));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ConfigParameterBE> GetClientConfigIDsDA(int clientID)
        {
            List<ConfigParameterBE> objClientConfigBE = new List<ConfigParameterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetClientConfigdetail", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objClientConfigBE.Add(new ConfigParameterBE
                                {
                                    ConfigID = reader["featureID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["featureID"])
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
            return objClientConfigBE;
        }

        public List<ConfigParameterBE> GetConfigMasterDA()
        {
            List<ConfigParameterBE> objClientConfigBE = new List<ConfigParameterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlConfigMaster, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objClientConfigBE.Add(new ConfigParameterBE
                                {
                                    ConfigID = reader["ConfigID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ConfigID"]),
                                    ConfigName = reader["ConfigName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Configname"])
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
            return objClientConfigBE;
        }

        public List<ConfigParameterBE> GetConfigMasterDA(string packageName)
        {
            List<ConfigParameterBE> objClientConfigBE = new List<ConfigParameterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlConfigPkgMaster, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@package", packageName));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objClientConfigBE.Add(new ConfigParameterBE
                                {
                                    ConfigID = reader["ConfigID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ConfigID"]),
                                    ConfigName = reader["ConfigName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Configname"])
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
            return objClientConfigBE;
        }

        public string GetPremiumFeatureDetailDA(int clientID)
        {
            string rtnval = string.Empty;
            List<ConfigParameterBE> objClientConfigBE = new List<ConfigParameterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetPremiumFeatureDetails", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                        //sqlCmd.Parameters.Add(new MySqlParameter("pFeatureID", featureID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnval = reader[0].ToString();
                            //while (reader.Read())
                            //{
                            //    objClientConfigBE.Add(new ConfigParameterBE
                            //    {
                            //        ClientID = reader["ClientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ClientID"]),
                            //        ConfigID = reader["featureID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["featureID"]),
                            //        ConfigName = reader["featureName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["featureName"]),
                            //        FeatureDetails = reader["featureDetails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["featureDetails"])
                            //    });
                            //}
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
            return rtnval;
        }

        public List<AuditLogBE> GetAuditrecord()
        {
            List<AuditLogBE> objAuditLogBE = new List<AuditLogBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlAduitrecord, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        // sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objAuditLogBE.Add(new AuditLogBE
                                {
                                    // AuditlogID = reader["auditID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["auditID"]),
                                    AuditlogID = reader["auditID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["auditID"]),
                                    ActionBy = reader["actionbyName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["actionbyName"]),
                                    ActionDetail = reader["actiondetail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["actiondetail"]),
                                    ActionDate = reader["actiondatetime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["actiondatetime"])
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
            return objAuditLogBE;
        }

        public void SaveAuditRecord(AuditLogBE objAuditLogBE)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spInsertAuditLog", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pActionbyID", objAuditLogBE.ActionByID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pActionID", objAuditLogBE.ActionID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pActiondetail", objAuditLogBE.ActionDetail));
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objAuditLogBE.ClientID));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public List<AuditLogBE> GetClientSubscription(int clientID)
        {
            List<AuditLogBE> objAuditLogBE = new List<AuditLogBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientSubscription, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objAuditLogBE.Add(new AuditLogBE
                                {
                                    AuditlogID = reader["pkgHisID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pkgHisID"]),
                                    ActionDetail = reader["pkgName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["pkgName"]),
                                    ActionDate = reader["subscribedDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["subscribedDate"])
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
            return objAuditLogBE;
        }

        public void SaveDefaultClientConfigDA(ClientBE objClientBE, int CreatedBy)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spClientDefaultConfigUpdate", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pTimezoneID", objClientBE.TimeZoneID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pLanguageID", objClientBE.LanguageID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pDateFormat", objClientBE.DateFormat));
                        sqlCmd.Parameters.Add(new MySqlParameter("pIsAutoDLSave", objClientBE.isAutoDLSave));
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objClientBE.ClientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", CreatedBy));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //MARKED FOR DEPRECATE
        public void SaveClientConfigDA(int clientID, int configID)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientConfigInsert, sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@configID", configID));
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        sqlCmd.Connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
