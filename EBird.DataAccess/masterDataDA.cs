using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using EBird.BusinessEntity;
using EBird.Common;
using MySql.Data.MySqlClient;

namespace EBird.DataAccess
{
    public class MasterDA
    {

        public List<CountryMasterBE> GetCountryListDA()
        {
            List<CountryMasterBE> objCountryMasterBE = new List<CountryMasterBE>();
            try
            {

                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetCountryMaster, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCountryMasterBE.Add(new CountryMasterBE
                                {
                                    CountryID = reader["countryID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["countryID"]),
                                    CountryCode = reader["countryCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["countryCode"]),
                                    CountryName = reader["countryName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["countryName"]),
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
            return objCountryMasterBE;
        }

        public List<IndustryMasterBE> GetIndustryListDA()
        {
            List<IndustryMasterBE> objIndustryMasterBE = new List<IndustryMasterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetIndustryMaster, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objIndustryMasterBE.Add(new IndustryMasterBE
                                {
                                    IndustryID = reader["IndustryID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IndustryID"]),
                                    IndustryName = reader["IndustryName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["IndustryName"]),
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
            return objIndustryMasterBE;
        }

        public List<ThemeMasterBE> GetThemeListDA()
        {
            List<ThemeMasterBE> objThemeMasterBE = new List<ThemeMasterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetThemeMaster, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objThemeMasterBE.Add(new ThemeMasterBE
                                {
                                    ThemeID = reader["ThemeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ThemeID"]),
                                    ThemeName = reader["EBThemeName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EBThemeName"]),
                                    ThemeShortName = reader["EBThemeSort"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EBThemeSort"]),
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

        public List<ThemeMasterBE> GetThemeListDA(string themeCategory)
        {
            List<ThemeMasterBE> objThemeMasterBE = new List<ThemeMasterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetThemeMasterByCategory, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@ThemeCategory", themeCategory));
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

        public List<ThemeMasterBE> GetThemeDetailDA(int themeID)
        {
            List<ThemeMasterBE> objThemeMasterBE = new List<ThemeMasterBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetThemeMasterDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@EBThemeID", themeID));
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

        public List<PackageFeature> GetPackageFeatures(string packageName)
        {
            List<PackageFeature> objPackageFeature = new List<PackageFeature>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetFeatureMasterDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@package", packageName));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPackageFeature.Add(new PackageFeature
                                {
                                    FeatureID = reader["FeatureID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FeatureID"]),
                                    FeatureName = reader["FeatureName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FeatureName"]),
                                    Category = reader["FeatureCategory"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FeatureCategory"]),
                                    isConfig = reader["isConfig"] == DBNull.Value ? false : Convert.ToBoolean(reader["isConfig"]),
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
            return objPackageFeature;
        }

        public List<PackageFeature> GetPackageFeatures(int featureID)
        {
            List<PackageFeature> objPackageFeature = new List<PackageFeature>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetFeatureMasterDetailByID, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@featureID", featureID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPackageFeature.Add(new PackageFeature
                                {
                                    FeatureID = reader["FeatureID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FeatureID"]),
                                    FeatureName = reader["FeatureName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FeatureName"]),
                                    Category = reader["FeatureCategory"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FeatureCategory"]),
                                    isConfig = reader["isConfig"] == DBNull.Value ? false : Convert.ToBoolean(reader["isConfig"]),
                                    IsPremium = reader["IsPremium"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPremium"]),
                                    FeatureDesc = reader["FeatureDetails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FeatureDetails"])
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
            return objPackageFeature;
        }

        public List<TimeZoneBE> getTimeZoneName(int timeZoneID)
        {
            List<TimeZoneBE> objTimeZoneBE = new List<TimeZoneBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand(DBQuery.sqlGetTimeZoneDetail, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new MySqlParameter("@TimeZoneID", timeZoneID));
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objTimeZoneBE.Add(new TimeZoneBE
                                {
                                    ZoneID = reader["timeZoneID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["timeZoneID"]),
                                    TimeZoneDiff = reader["timeDifference"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["timeDifference"]),
                                    TimeZoneName = reader["TimeZoneName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TimeZoneName"]),
                                    ShortTimeZoneName = reader["ShortTimeZoneName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ShortTimeZoneName"])
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
            return objTimeZoneBE;
        }

        public List<MetaTagBE> getMetaTagList()
        {
            List<MetaTagBE> objMetaTagBE = new List<MetaTagBE>();
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(Constant.EBirdConnectionString))
                {
                    using (MySqlCommand sqlCmd = new MySqlCommand("select * from tblmetatags", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        MySqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objMetaTagBE.Add(new MetaTagBE
                                {
                                    TagID = reader["tagID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["tagID"]),
                                    TagName = reader["TagName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TagName"]),
                                    Detail = reader["TagDetail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TagDetail"]),
                                    Type = reader["TagType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TagType"])
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
            return objMetaTagBE;
        }
    }
}
