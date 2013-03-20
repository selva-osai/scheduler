using System;
using Telerik.Web.UI;

namespace EBird.DocRepo
{
    public class telerikUploadConfig : AsyncUploadConfiguration
    {
        private int userID;
        private string folderType;
        private int clientID;

        public int ActionID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public int ClientID
        {
            get
            {
                return clientID;
            }

            set
            {
                clientID = value;
            }
        }

        public string FolderType
        {
            get
            {
                return folderType;
            }

            set
            {
                folderType = value;
            }
        }
    }

    public class telerikAsyncUploadResult : AsyncUploadResult
    {
        private int docID;

        public int DocumentID
        {
            get { return docID; }
            set { docID = value; }
        }
    }
}