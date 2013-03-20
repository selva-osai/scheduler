
namespace EBird.BusinessEntity
{
    public class DocumentBE
    {
        public int DocumentID { get; set; }
        public int ClientID { get; set; }
        public string Category { get; set; }
        public string OrginalFileName { get; set; }
        public string SavedFileName { get; set; }
        public string InsertDate { get; set; }
        public int InsertedBy { get; set; }
        public bool isResized { get; set; }
        public int PresenterID { get; set; }
        public int WebinarID { get; set; }
    }
}
