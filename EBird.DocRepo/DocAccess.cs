using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;

namespace EBird.DocRepo
{
    public class DocAccess
    {
        EBirdUtility objUtil = new EBirdUtility();

        public bool InitClientFolders(int clientID)
        {
            bool rtn = false;
            try
            {
                if (!Directory.Exists(Constant.DocRepoClient + clientID.ToString()))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Constant.DocRepoClient + clientID.ToString());
                    di.CreateSubdirectory("Profile");
                    di.CreateSubdirectory("Logo");
                    di.CreateSubdirectory("WebinarDocs");
                    di.CreateSubdirectory("SS");
                    rtn = true;
                }
                else
                {
                    if (!Directory.Exists(Constant.DocRepoClient + clientID.ToString() + "/Profile"))
                        Directory.CreateDirectory(Constant.DocRepoClient + clientID.ToString() + "/Profile");
                    if (!Directory.Exists(Constant.DocRepoClient + clientID.ToString() + "/Logo"))
                        Directory.CreateDirectory(Constant.DocRepoClient + clientID.ToString() + "/Logo");
                    if (!Directory.Exists(Constant.DocRepoClient + clientID.ToString() + "/WebinarDocs"))
                        Directory.CreateDirectory(Constant.DocRepoClient + clientID.ToString() + "/WebinarDocs");
                    if (!Directory.Exists(Constant.DocRepoClient + clientID.ToString() + "/SS"))
                        Directory.CreateDirectory(Constant.DocRepoClient + clientID.ToString() + "/SS");
                    rtn = true;
                }
            }
            catch (Exception ex)
            {
                rtn = false;
            }
            return rtn;
        }

        public int saveFiles(HttpRequest fs, string folderType, int clientID, int actionBy)
        {
            int DocID = 0;
            string fname = "";
            try
            {
                DocumentBE objDcoumentBE = new DocumentBE();
                DocumentDA objDocumentDA = new DocumentDA();

                for (int i = 0; i < fs.Files.Count; i++)
                {
                    if (fs.Files[i].FileName != "")
                    {
                        fname = System.IO.Path.GetFileName(fs.Files[i].FileName);
                        switch (folderType.ToUpper())
                        {
                            case "PROFILE":
                                fs.Files[i].SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Profile\\" + fname);
                                break;
                            case "LOGO":
                                fs.Files[i].SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Logo\\" + fname);
                                break;
                            case "BANNER":
                                fs.Files[i].SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Logo\\" + fname);
                                break;
                            case "PRESENTATION":
                                fs.Files[i].SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\WebinarDocs\\" + fname);
                                break;
                        }
                        objDcoumentBE.DocumentID = 0;
                        objDcoumentBE.ClientID = clientID;
                        objDcoumentBE.Category = folderType;
                        objDcoumentBE.OrginalFileName = fname;
                        objDcoumentBE.SavedFileName = fname;
                        objDcoumentBE.InsertedBy = actionBy;

                        DocID = objDocumentDA.SaveDocumentDA(objDcoumentBE);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return DocID;
        }

        public int saveFiles(UploadedFile fs, string folderType, int clientID, int actionBy)
        {
            int DocID = 0;
            string fname = "";
            try
            {
                DocumentBE objDcoumentBE = new DocumentBE();
                DocumentDA objDocumentDA = new DocumentDA();
                if (fs.FileName != "")
                {
                    fname = System.IO.Path.GetFileName(fs.FileName);
                    switch (folderType.ToUpper())
                    {
                        case "PROFILE":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Profile\\" + fname);
                            break;
                        case "LOGO":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Logo\\" + fname);
                            break;
                        case "BANNER":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Logo\\" + fname);
                            break;
                        case "PRESENTATION":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\WebinarDocs\\" + fname);
                            break;
                    }
                    objDcoumentBE.DocumentID = 0;
                    objDcoumentBE.ClientID = clientID;
                    objDcoumentBE.Category = folderType;
                    objDcoumentBE.OrginalFileName = fname; //fs.GetName();
                    objDcoumentBE.SavedFileName = fname; //fs.GetName();
                    objDcoumentBE.InsertedBy = actionBy;

                    DocID = objDocumentDA.SaveDocumentDA(objDcoumentBE);
                }
            }
            catch (Exception ex)
            {

            }
            return DocID;
        }

        public int saveFiles(UploadedFile fs, string folderType, int clientID, int actionBy, int userID, int webinarID)
        {
            int DocID = 0;
            string fname = "";
            try
            {
                DocumentBE objDcoumentBE = new DocumentBE();
                DocumentDA objDocumentDA = new DocumentDA();
                if (fs.FileName != "")
                {
                    fname = System.IO.Path.GetFileName(fs.FileName);
                    switch (folderType.ToUpper())
                    {
                        case "PROFILE":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Profile\\" + fname);
                            break;
                        case "LOGO":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Logo\\" + fname);
                            break;
                        case "BANNER":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Logo\\" + fname);
                            break;
                        case "PRESENTATION":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\WebinarDocs\\" + fname);
                            break;
                        case "SS":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\SS\\" + fname);
                            break;
                        case "TEMP":
                            fs.SaveAs(Constant.DocRepoClient + clientID.ToString() + "\\Temp\\" + fname);
                            break;
                    }
                    objDcoumentBE.DocumentID = 0;
                    objDcoumentBE.ClientID = clientID;
                    objDcoumentBE.Category = folderType;
                    objDcoumentBE.OrginalFileName = fname; //fs.GetName();
                    objDcoumentBE.SavedFileName = fname; //fs.GetName();
                    objDcoumentBE.InsertedBy = actionBy;
                    objDcoumentBE.PresenterID = userID;
                    objDcoumentBE.WebinarID = webinarID;
                    DocID = objDocumentDA.SaveDocumentDA(objDcoumentBE, userID, webinarID);
                }
            }
            catch (Exception ex)
            {
                objUtil.RecordLogToFS("DocAccess-saveFiles:" + ex.Message);
            }
            return DocID;
        }

        public void removePresenterprofileImage(int docID)
        {
            DocumentDA objDocDA = new DocumentDA();
            List<DocumentBE> objDocBE = new List<DocumentBE>();
            objDocBE = objDocDA.GetDocumentDA(docID);
            if (objDocBE.Count > 0)
            {
                if (File.Exists(Constant.DocRepRoot + "Profile\\" + objDocBE[0].SavedFileName))
                    File.Delete(Constant.DocRepRoot + "Profile\\" + objDocBE[0].SavedFileName);
            }
        }

        public void removeDocumentFromRepositary(int docID, int clientID, string documentType)
        {
            DocumentDA objDocDA = new DocumentDA();
            List<DocumentBE> objDocBE = new List<DocumentBE>();
            if (documentType.ToUpper() == "BANNER")
                documentType = "Logo";
            objDocBE = objDocDA.GetDocumentDA(docID);
            if (objDocBE.Count > 0)
            {
                if (File.Exists(Constant.DocRepoClient + clientID.ToString() + "\\" + documentType + "\\" + objDocBE[0].SavedFileName))
                    File.Delete(Constant.DocRepoClient + clientID.ToString() + "\\" + documentType + "\\" + objDocBE[0].SavedFileName);
            }
        }

        public string getUserProfileImagePath(int UserID)
        {
            DocumentDA objDocumentDA = new DocumentDA();
            return objDocumentDA.GetUserProfileImage(UserID);
        }

        public void resizeImage(int docID, int ClientID)
        {
            bool isResizeRequired = false;
            DocumentDA objDocDA = new DocumentDA();
            string rtnFilePath = objDocDA.GetDocumentPath(docID);

            if (rtnFilePath != "")
            {
                if (File.Exists(Constant.DocRepoClient + rtnFilePath))
                {
                    rtnFilePath = Constant.DocRepoClient + rtnFilePath;
                    isResizeRequired = true;
                }
                else
                    rtnFilePath = Constant.DocRepRoot + "NoDocs.png";
            }
            else
                rtnFilePath = Constant.DocRepRoot + "NoDocs.png";

            if (isResizeRequired)
            {
                List<DocumentBE> objDoc = objDocDA.GetDocumentDA(docID);
                System.Drawing.Image resized = null;
                if (objDoc.Count > 0)
                {
                    if (!objDoc[0].isResized)
                    {

                        ImageUtility objImgUtil = new ImageUtility();
                        System.Drawing.Image original = System.Drawing.Image.FromFile(rtnFilePath);
                        string fldName = "";
                        switch (objDoc[0].Category.ToUpper())
                        {
                            case "LOGO":
                                fldName = "Logo";
                                resized = objImgUtil.ResizeImage(original, Common.Constant.LogoSize);
                                break;
                            case "BANNER":
                                fldName = "Logo";
                                resized = objImgUtil.ResizeImage(original, Common.Constant.BannerSize);
                                break;
                            case "PROFILE":
                                fldName = "Profile";
                                resized = objImgUtil.ResizeImage(original, Common.Constant.UserProfileSize);
                                break;
                            case "SS-LOGO":
                                fldName = "SS";
                                resized = objImgUtil.ResizeImage(original, Common.Constant.LogoSize);
                                break;
                            case "SS-BANNER":
                                fldName = "SS";
                                resized = objImgUtil.ResizeImage(original, Common.Constant.BannerSize);
                                break;
                        }

                        string SavedFileName = "";

                        switch ((Path.GetExtension(objDoc[0].SavedFileName).Substring(1)).ToUpper())
                        {
                            case "PNG":
                                SavedFileName = docID.ToString() + "_rs.png";
                                resized.Save(Constant.DocRepoClient + ClientID.ToString() + "\\" + fldName + "\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case "JPG":
                                SavedFileName = docID.ToString() + "_rs.jpg";
                                resized.Save(Constant.DocRepoClient + ClientID.ToString() + "\\" + fldName + "\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case "GIF":
                                SavedFileName = docID.ToString() + "_rs.gif";
                                resized.Save(Constant.DocRepoClient + ClientID.ToString() + "\\" + fldName + "\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        objDocDA.SaveDocumentDA(docID, "isResized", "1");
                        objDocDA.SaveDocumentDA(docID, "SavedFileName", "'" + SavedFileName + "'");
                    }
                }
            }

        }

        public byte[] FileToByteArray(int docID)
        {
            DocumentDA objDocumentDA = new DocumentDA();
            string _FileName = objDocumentDA.GetDocumentPath(docID);

            byte[] _Buffer = null;
            try
            {
                _FileName = Constant.DocRepoClient + _FileName;
                if (File.Exists(_FileName))
                {
                    // Open file for reading
                    System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                    // attach filestream to binary reader
                    System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                    // get total byte length of the file
                    long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                    // read entire file into buffer
                    _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                    // close file reader
                    _FileStream.Close();
                    _FileStream.Dispose();
                    _BinaryReader.Close();
                }
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return _Buffer;
        }

        public void DeleteBriefcaseContentDocs(int WebinarID, int clientID)
        {
            //public List<WebinarResource> getWebinarResoures(int webinarID, string resourceType)
            WebinarDA objWebinarDA = new WebinarDA();
            List<WebinarResource> objRes = objWebinarDA.getWebinarResoures(WebinarID, "'Content Doc'");
            for (int idx = 0; idx < objRes.Count; idx++)
            {
                removeDocumentFromRepositary(objRes[idx].DocID, clientID, "WebinarDocs");
                objWebinarDA.DeleteRegFormResources(WebinarID, objRes[idx].DocID);
            }
        }
    }
}
