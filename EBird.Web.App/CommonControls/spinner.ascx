<%@ Control Language="C#" ClassName="EBird.Web.App.CommonControls.Spinner" %>
<script language="javascript" type="text/javascript">
    function getNextNum(sAction) {
        var minVal = document.getElementById("hMin");
        var minVal = document.getElementById("hMax");
        if (sAction == 'D') {
            //var txt1 = document.getElementById("txtProblem");
        }
        else {
        }
    }
</script>
<script runat="server">
    private int m_minValue;
    private int m_maxValue = 100;
    private int m_currentNumber = 0;
    public int MinValue
    {
        get
        {
            return m_minValue;
        }
        set
        {
            if (value >= this.MaxValue)
            {
                throw new Exception("MinValue must be less than MaxValue.");
            }
            else
            {
                m_minValue = value;
            }
        }
    }

    public int MaxValue
    {
        get
        {
            return m_maxValue;
        }
        set
        {
            if (value <= this.MinValue)
            {
                throw new
                    Exception("MaxValue must be greater than MinValue.");
            }
            else
            {
                m_maxValue = value;
            }
        }
    }

    public int CurrentNumber
    {
        get
        {
            return m_currentNumber;
        }
    }

    protected void Page_Load(Object sender, EventArgs e)
    {
        //if (IsPostBack)
        //{
        //    m_currentNumber =
        //        Int16.Parse(ViewState["currentNumber"].ToString());
        //}
        //else
        //{
        //    m_currentNumber = this.MinValue;
        //}
        if (!this.IsPostBack)
        {
            hMin.Value = this.MinValue.ToString();
            hMax.Value = this.MaxValue.ToString();
            textNumber.Text = this.MinValue.ToString();
        }
        //DisplayNumber();
    }

    protected void DisplayNumber()
    {
        textNumber.Text = this.CurrentNumber.ToString();
        ViewState["currentNumber"] = this.CurrentNumber.ToString();
    }

    protected void buttonUp_Click(Object sender, EventArgs e)
    {
        if (m_currentNumber == this.MaxValue)
        {
            m_currentNumber = this.MinValue;
        }
        else
        {
            m_currentNumber += 1;
        }
        DisplayNumber();
    }

    protected void buttonDown_Click(Object sender, EventArgs e)
    {
        if (m_currentNumber == this.MinValue)
        {
            m_currentNumber = this.MaxValue;
        }
        else
        {
            m_currentNumber -= 1;
        }
        DisplayNumber();
    }
</script>
<asp:TextBox ID="textNumber" runat="server" ReadOnly="True" Width="32px" Enabled="False" />
<asp:Button OnClientClick="javascript:getNextNum('U');" Font-Bold="True" ID="buttonUp" runat="server" Text="^" />
<asp:Button OnClientClick="javascript:getNextNum('D');" Font-Bold="True" ID="buttonDown" runat="server" Text="v" />
<asp:HiddenField ID="hMin" runat="server" />
<asp:HiddenField ID="hMax" runat="server" />