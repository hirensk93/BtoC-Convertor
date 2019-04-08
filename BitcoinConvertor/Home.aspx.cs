using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public partial class Home : System.Web.UI.Page
{
    String str;
    WebClient wc;
    SqlConnection sc;
    SqlCommand scmd;
    SqlDataAdapter sda;
    String cstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cstr = ConfigurationManager.ConnectionStrings["cstr"].ConnectionString;
            sc = new SqlConnection(cstr);
            scmd = new SqlCommand("select * from Currency", sc);
            sda = new SqlDataAdapter(scmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddl_Currency.DataSource = dt;
            ddl_Currency.DataBind();
            ddl_Currency.DataTextField = "Country_Name";
            ddl_Currency.DataValueField = "Country_Code";
            ddl_Currency.DataBind();
        }
    }

    protected void btn_Convert_Click(object sender, EventArgs e)
    {
        try
        {
            String url;
            url = "https://blockchain.info/tobtc?" + "currency=" + Server.UrlEncode(this.ddl_Currency.SelectedItem.Value.ToString()) + "&value=" + Server.UrlEncode(this.txt_val.Text.ToString());
            wc = new WebClient();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //str = wc.DownloadString("https://blockchain.info/ticker");
            str = wc.DownloadString(url);
            lbl_res.Text = str.ToString();
        }
        catch(Exception ex)
        {
            lbl_res.Text = "Your Country isn't available" + ex.Message.ToString();
        }

       
       
    }
}