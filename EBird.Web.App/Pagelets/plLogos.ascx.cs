using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using EBird.BusinessEntity;
using EBird.DataAccess;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pagelets
{
    public partial class plLogos : System.Web.UI.UserControl
    {
        private int intWebinarID = 0;
        private int strlogo1 = 0;
        private int strlogo2 = 0;
        private int strBanner = 0;
        private string strHeaderType = "";

        public string HeaderType
        {
            get
            {
                return strHeaderType;
            }
            set
            {
                strHeaderType = value;
            }
        }

        public int LogoID1
        {
            get
            {
                return strlogo1;
            }
            set
            {
                strlogo1 = value;
            }
        }


        public int WebinarID
        {
            get
            {
                return intWebinarID;
            }
            set
            {
                intWebinarID = value;
            }
        }


        public int LogoID2
        {
            get
            {
                return strlogo2;
            }
            set
            {
                strlogo2 = value;
            }
        }

        public int BannerID
        {
            get
            {
                return strBanner;
            }
            set
            {
                strBanner = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetLogoBanner();
            }
        }

        private void GetLogoBanner()
        {
            WebinarDA objWebinarDA = new WebinarDA();
            List<WebinarResource> objWRes = objWebinarDA.getRegFormResoures(intWebinarID);
            int Rec = objWRes.Count;
            if (Rec > 0)
            {
                HtmlTableRow trImageRow = new HtmlTableRow();
                if (objWRes[0].ResourceType == "BANNER")
                {
                    //phBanner.Visible = true;
                    //imgBanner.Src = "~/handler/showImage.ashx?ID=" + objWRes[idx].DocID.ToString();

                    HtmlTableCell tdImageCell = new HtmlTableCell();
                    HyperLink imgLogo = new HyperLink();
                    imgLogo.ID = "id0";
                    imgLogo.ImageUrl = "~/handler/showImage.ashx?ID=" + objWRes[0].DocID.ToString();
                    imgLogo.ToolTip = objWRes[0].LogoUrlName.ToString();
                    imgLogo.NavigateUrl = objWRes[0].LogoUrl.ToString();
                    imgLogo.Target = "_blank";
                    tdImageCell.Controls.Add(imgLogo);
                    trImageRow.Cells.Add(tdImageCell);
                    tLogo.Rows.Add(trImageRow);  
                }
                else
                {
                    //phLogo.Visible = true;
                    //Rec = (int) Math.Round(100 / objWRes.Count);
                    Rec = (int)(100 / objWRes.Count);
                    for (int idx = 0; idx < objWRes.Count; idx++)
                    {
                        HtmlTableCell tdImageCell = new HtmlTableCell();
                        HyperLink imgLogo = new HyperLink();
                        imgLogo.ID = "id" + idx.ToString();
                        imgLogo.ImageUrl = "~/handler/showImage.ashx?ID=" + objWRes[idx].DocID.ToString();
                        imgLogo.ToolTip = objWRes[idx].LogoUrlName.ToString();
                        imgLogo.NavigateUrl = objWRes[idx].LogoUrl.ToString();
                        imgLogo.Target = "_blank";
                        tdImageCell.Controls.Add(imgLogo);
                        trImageRow.Cells.Add(tdImageCell);
                        trImageRow.Cells[idx].VAlign = "middle"; 
                        trImageRow.Cells[idx].Width = Rec.ToString() + "%";
                        if (idx == objWRes.Count-1)
                            trImageRow.Cells[idx].Align = "right";
                        else
                        {
                            if (idx % 2 != 0 && idx != 0)
                              trImageRow.Cells[idx].Align = "center";
                            else
                              trImageRow.Cells[idx].Align = "left";

                        }
                    }
                    tLogo.Rows.Add(trImageRow);  
                }
            }
        }
    }
}