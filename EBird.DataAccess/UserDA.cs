using System;
using System.Collections.Generic;
using System.Data;
using EBird.BusinessEntity;
using EBird.Common;
using MySql.Data.MySqlClient;
using System.Data.Odbc;

namespace EBird.DataAccess
{
    public class UserDA
    {
        #region MySQLConnection based

        public List<UserBE> GetAuthenticatedUserDA(string userName, string userPassword)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {

                    //using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserAuth, sqlCon))
                    using (MySqlCommand sqlCmd = new MySqlCommand("spAuthenticatedUser", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserID", userName));
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserPassword", userPassword));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader["returnFlag"].ToString() == "")
                                {
                                    objUserBE.Add(new UserBE
                                    {
                                        UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                        FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                        EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                        UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                        ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                        AuthenticationState = reader["returnFlag"] == DBNull.Value ? string.Empty : Convert.ToString(reader["returnFlag"])
                                    });
                                }
                                else
                                {
                                    objUserBE.Add(new UserBE
                                    {
                                        AuthenticationState = reader["returnFlag"] == DBNull.Value ? string.Empty : Convert.ToString(reader["returnFlag"])
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
            return objUserBE;
        }

        #endregion

        #region ODBC Connection based
        /*
        public List<UserBE> GetAuthenticatedUserDA(string userName, string userPassword)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserAuth, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("userName", userName));
                        sqlCmd.Parameters.Add(new MySqlParameter("userPassword", userPassword));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"])
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
            return objUserBE;
        }
        */
        #endregion

        public List<UserBE> GetUserListDA(int clientID)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientUsers, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            string fullName = string.Empty;
                            while (reader.Read())
                            {
                                fullName = Convert.ToString(reader["FirstName"]) + " " + Convert.ToString(reader["LastName"]);
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = fullName,
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public List<UserBE> GetUserListDA(int clientID, string userRole)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetUserList", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserRole", userRole));
                        sqlCmd.Parameters.Add(new MySqlParameter("pExcludeUserID", "0"));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = reader["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FullName"]),
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public List<UserBE> GetUserListDA(int clientID, string userRole, int excludeUserID)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("spGetUserList", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                        sqlCmd.Parameters.Add(new MySqlParameter("pUserRole", userRole));
                        sqlCmd.Parameters.Add(new MySqlParameter("pExcludeUserID", excludeUserID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = reader["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FullName"]),
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public List<UserBE> GetUserListDA(int clientID, string userName, string userStatus)
        {
            string sql1 = DBQuery.sqlClientUsers;
            if (userStatus != "")
            {
                if (userStatus.ToUpper() == "ACTIVE")
                    sql1 = sql1 + " and userStatus='Active'";
                else
                    sql1 = sql1 + " and userStatus = 'Inactive'";
            }
            else
                sql1 = sql1 + " and userStatus in ('Active','Inactive')";

            if (userName != "")
                sql1 = sql1 + " and (FirstName like '%" + userName + "%' or LastName like '%" + userName + "%')";

            List<UserBE> objUserBE = new List<UserBE>();
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
                            string fullName = string.Empty;
                            while (reader.Read())
                            {
                                fullName = Convert.ToString(reader["FirstName"]) + " " + Convert.ToString(reader["LastName"]);
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = fullName,
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public List<UserBE> GetUserListDA(int clientID, string userName, string userEmail, string userRole)
        {
            string sql1 = DBQuery.sqlClientUsers;
            if (userName != "")
                sql1 = sql1 + " and (FirstName like '%" + userName + "%' or LastName like '%" + userName + "%')";
            if (userEmail != "")
                sql1 = sql1 + " and (emailID like '%" + userEmail + "%') ";
            if (userRole != "All")
            {
                sql1 = sql1 + " and (role = '" + userRole + "') ";
            }
            List<UserBE> objUserBE = new List<UserBE>();
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
                            string fullName = string.Empty;
                            while (reader.Read())
                            {
                                fullName = Convert.ToString(reader["FirstName"]) + " " + Convert.ToString(reader["LastName"]);
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = fullName,
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public List<UserBE> GetAllUserListDA(int clientID, string userName)
        {
            string sql1 = DBQuery.sqlClientUsers;

            if (userName != "")
                sql1 = sql1 + " and (FirstName like '%" + userName + "%' or LastName like '%" + userName + "%')";

            List<UserBE> objUserBE = new List<UserBE>();
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
                            string fullName = string.Empty;
                            while (reader.Read())
                            {
                                fullName = Convert.ToString(reader["FirstName"]) + " " + Convert.ToString(reader["LastName"]);
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = fullName,
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public bool isDeletedUserExistDA(int clientID)
        {
            bool rtnVal = false;
            string sql1 = "Select count(*) from tbluseracct where clientID=" + clientID.ToString() + " and userStatus='Deleted'";

            List<UserBE> objUserBE = new List<UserBE>();
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
                            rtnVal = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0])) > 0 ? true : false;
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

        public bool isUserExistDA(string usrEmail)
        {
            bool rtnVal = false;
            string sql1 = "Select count(*) from tbluseracct where emailID=@usrEmail";

            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@usrEmail", usrEmail));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnVal = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0])) > 0 ? true : false;
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

        public bool isUserExistDA(string usrEmail, int excludeUserID)
        {
            bool rtnVal = false;
            string sql1 = "Select count(*) from tbluseracct where emailID = @usrEmail and userID <> @excludeUserID";

            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@usrEmail", usrEmail));
                        sqlCmd.Parameters.Add(new MySqlParameter("@excludeUserID", excludeUserID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnVal = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0])) > 0 ? true : false;
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

        public List<UserBE> GetDeletedUserListDA(int clientID)
        {
            string sql1 = DBQuery.sqlClientUsers + " and userStatus in ('Deleted','Deleted-L')";

            List<UserBE> objUserBE = new List<UserBE>();
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
                            string fullName = string.Empty;
                            while (reader.Read())
                            {
                                fullName = Convert.ToString(reader["FirstName"]) + " " + Convert.ToString(reader["LastName"]);
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    FullName = fullName,
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"])
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
            return objUserBE;
        }

        public List<UserBE> GetUserDetailDA(int userID)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientUserDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Telephone = reader["Telephone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Telephone"]),
                                    Department = reader["Department"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Department"]),
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    isPrimary = reader["isPrimary"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPrimary"]),
                                    JobTitle = reader["JobTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["JobTitle"]),
                                    isEmailWeeklyUpdate = reader["isEmailWeekly"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEmailWeekly"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? 5 : Convert.ToInt32(reader["TimeZoneID"]),
                                    Password = reader["userPassword"] == DBNull.Value ? string.Empty : Convert.ToString(reader["userPassword"]),
                                    PasswordChangedOn = reader["PassChangedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PassChangedOn"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"])
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
            return objUserBE;
        }

        public List<UserBE> GetUserDetailDA(string emailID)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientUserDetailEmail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@emailID", emailID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Telephone = reader["Telephone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Telephone"]),
                                    Department = reader["Department"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Department"]),
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    isPrimary = reader["isPrimary"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPrimary"]),
                                    JobTitle = reader["JobTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["JobTitle"]),
                                    isEmailWeeklyUpdate = reader["isEmailWeekly"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEmailWeekly"]),
                                    TimeZoneID = reader["TimeZoneID"] == DBNull.Value ? 5 : Convert.ToInt32(reader["TimeZoneID"]),
                                    Password = reader["userPassword"] == DBNull.Value ? string.Empty : Convert.ToString(reader["userPassword"]),
                                    PasswordChangedOn = reader["PassChangedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PassChangedOn"]),
                                    WebinarCount = reader["webinarCount"] == DBNull.Value ? -1 : Convert.ToInt32(reader["webinarCount"]),
                                    isAutoDLSave = reader["isAutoDLSave"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAutoDLSave"])
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
            return objUserBE;
        }

        public int getAdminCount(int clientID)
        {
            int cnt = 0;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserAcctAdminCount, sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("@client", clientID));
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        cnt = Convert.ToInt32(reader.GetValue(0));
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
            return cnt;
        }

        public int AddUserAccountDA(UserBE objUserBE)
        {
            int userID = 0;
            // SQL: Client record
            string sql1 = DBQuery.sqlUserInsert;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spAddNewUser", sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("pEmailID", objUserBE.EmailID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserPassword", objUserBE.Password));
                    sqlCmd.Parameters.Add(new MySqlParameter("pFirstName", objUserBE.FirstName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pLastName", objUserBE.LastName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pAddress1", objUserBE.Address));
                    sqlCmd.Parameters.Add(new MySqlParameter("pTelephone", objUserBE.Telephone));
                    sqlCmd.Parameters.Add(new MySqlParameter("pRole", objUserBE.Role));
                    sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objUserBE.ClientID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDepartment", objUserBE.Department));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserStatus", "Active"));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsPrimary", (objUserBE.isPrimary ? 1 : 0)));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCreatedBy", (objUserBE.CreatedBy)));

                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        userID = Convert.ToInt32(reader.GetValue(0));
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
            return userID;
        }

        public bool UpdateUserRecord(UserBE objUserBE, int priAdminID)
        {
            bool rtnVal = false;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateUserInfo", sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("pEmailID", objUserBE.EmailID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pFirstName", objUserBE.FirstName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pLastName", objUserBE.LastName));
                    sqlCmd.Parameters.Add(new MySqlParameter("pAddress1", objUserBE.Address));
                    sqlCmd.Parameters.Add(new MySqlParameter("pTelephone", objUserBE.Telephone));
                    sqlCmd.Parameters.Add(new MySqlParameter("pRole", objUserBE.Role));
                    sqlCmd.Parameters.Add(new MySqlParameter("pClientID", objUserBE.ClientID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pDepartment", objUserBE.Department));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserStatus", objUserBE.UserStatus));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsPrimary", (objUserBE.isPrimary ? 1 : 0)));
                    sqlCmd.Parameters.Add(new MySqlParameter("pPrimaryAdminID", priAdminID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", objUserBE.UserID));

                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (Convert.ToInt32(reader.GetValue(0)) == 0)
                            rtnVal = true;
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

        public void UpdateUserProfileRecord(UserBE objUserBE)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserProfileUpdate, sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("@FirstName", objUserBE.FirstName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@LastName", objUserBE.LastName));
                    sqlCmd.Parameters.Add(new MySqlParameter("@Telephone", objUserBE.Telephone));
                    sqlCmd.Parameters.Add(new MySqlParameter("@Department", objUserBE.Department));
                    sqlCmd.Parameters.Add(new MySqlParameter("@jobTitle", objUserBE.JobTitle));
                    sqlCmd.Parameters.Add(new MySqlParameter("@userID", objUserBE.UserID));

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

        public void UpdateUserAcccountSetting(UserBE objUserBE)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlUserAcctSettingUpdate, sqlCon);
                    sqlCmd.Parameters.Add(new MySqlParameter("@isEmailWeekly", objUserBE.isEmailWeeklyUpdate));
                    sqlCmd.Parameters.Add(new MySqlParameter("@TimeZoneID", objUserBE.TimeZoneID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@isAutoDLSave", objUserBE.isAutoDLSave));
                    sqlCmd.Parameters.Add(new MySqlParameter("@userID", objUserBE.UserID));
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

        public string ChangeUserPassword(string UserID, string CurrentPassword, string NewPassword)
        {
            string rtnCode = string.Empty;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spChangeUserPassword", sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", UserID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pCurrentPassword", CurrentPassword));
                    sqlCmd.Parameters.Add(new MySqlParameter("pNewPassword", NewPassword));
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnCode = Convert.ToString(reader.GetValue(0));
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
            return rtnCode;
        }

        // MiG 11/29/2012
        public void UpdateUserLockedFieldDA(string emailaddress, string UserStatus)
        {
            string sql1 = DBQuery.sqlClientUserUnlockStatus;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@emailID", emailaddress));
                    sqlCmd.Parameters.Add(new MySqlParameter("@userStatus", UserStatus));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteUserAccountDA(int userID, int altAdminID = -1)
        {
            string sql1 = DBQuery.sqlClientUserDelete;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spDeleteUser", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserID", userID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pAlternateAdminID", altAdminID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UnlockUserAccountDA(int userID)
        {
            string sql1 = DBQuery.sqlClientUserUnlock;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new MySqlParameter("@userID", userID));
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UserBE> GetPrimaryAdminDA(int clientID)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            try
            {

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetPrimaryAdmin, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();
                        //SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objUserBE.Add(new UserBE
                                {
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Role = reader["role"] == DBNull.Value ? string.Empty : Convert.ToString(reader["role"]),
                                    EmailID = reader["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailID"]),
                                    UserStatus = reader["UserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserStatus"]),
                                    ClientID = reader["clientID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["clientID"]),
                                    Telephone = reader["Telephone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Telephone"]),
                                    Department = reader["Department"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Department"])
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
            return objUserBE;
        }

        public int[] GetClientUserCountDA(int clientID)
        {
            int[] rtn = new int[2];
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlClientUserCount, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@clientID", clientID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtn[0] = Convert.ToInt32(reader[0]);
                            rtn[1] = Convert.ToInt32(reader[1]);
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

        public bool updateUserSeats(int clientID, int NoOfSeats, bool isIncrease, int updatedBy)
        {
            bool rtnCode = true;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("spUpdateUserSeats", sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("pClientID", clientID));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUserCounts", NoOfSeats));
                    sqlCmd.Parameters.Add(new MySqlParameter("pIsIncrease", (isIncrease ? 1:0)));
                    sqlCmd.Parameters.Add(new MySqlParameter("pUpdatedBy", updatedBy));
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnCode = Convert.ToBoolean(reader.GetValue(0));
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
            return rtnCode;
        }

        public void restoreUserAcct(string userAcctList, bool isToActive)
        {
            string sql1;
            if (isToActive)
                sql1 = "Update tbluseracct set userStatus = 'Active' where userID in (" + userAcctList + ")";
            else
                sql1 = "Update tbluseracct set userStatus = 'Inactive' where userID in (" + userAcctList + ")";
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);
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

        //General method

        public float getClientToServerTimeDiff(int clientID)
        {
            string sql1 = "Select @srvVal-timeDifference from tblclient a, tblmastimezone b where clientID = @client and a.timezoneID=b.timezoneID";
            float rtn = 0.0F;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("@client", clientID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@srvVal", Constant.SRV_TIME_DIFF));

                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtn = Convert.ToInt32(reader.GetValue(0));
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

        public float getTimezoneToServerTimeDiff(int timeZoneID)
        {
            string sql1 = "Select @srvVal-timeDifference from tblmastimezone where timezoneID=@timeZoneID";
            float rtn = 0.0F;
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand(sql1, sqlCon);

                    sqlCmd.Parameters.Add(new MySqlParameter("@timeZoneID", timeZoneID));
                    sqlCmd.Parameters.Add(new MySqlParameter("@srvVal", Constant.SRV_TIME_DIFF));

                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtn = Convert.ToInt32(reader.GetValue(0));
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

        public void UpdateUserPasswordEncrypted(string userID, string encPassword)
        {
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    MySqlCommand sqlCmd = new MySqlCommand("Update tbluseracct set userPassword='" + encPassword + "' where userID="+userID, sqlCon);

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
    }
}
