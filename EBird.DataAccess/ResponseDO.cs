using System.Collections.Generic;

namespace EBird.DataAccess
{
    public class ResponseDO
    {
        private List<string> errorCode = new List<string>();
        private List<string> errorMessage = new List<string>();
        
        public ResponseDO()
        {
        }

        public ResponseDO(long _entityID)
        {
            entityID = _entityID;
        }
        
        public ResponseDO(long _entityID, List<string> _errorCode, List<string> _errorMessage)
        {
            entityID = _entityID;
            errorCode = _errorCode;
            errorMessage = _errorMessage;
        }

        private long entityID;

        public long EntityID
        {
            get { return entityID; }
            set { entityID = value; }
        }

        public List<string> ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        public List<string> ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
    }
}
