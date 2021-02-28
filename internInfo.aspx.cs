using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace patient.InternProgram
{
    public partial class internInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {// ddl population
            if (!IsPostBack)  // show intern the effectivness of this code and how it trick them if they do not use it correctly
            {
                populateCountry();
            }

        }
        protected void populateCountry()
        {
            string mySql = "select countryId , country from country ";

            CRUD myCrud = new CRUD();
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlCountry.DataBind();
            ddlCountry.DataSource = dr;
            ddlCountry.DataTextField = "country";
            ddlCountry.DataValueField = "countryId";
            ddlCountry.DataBind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFNameAr.Text = "";
            txtFNameEn.Text = "";
            txtUserName.Text = "";
            txtSalary.Text = "";
            //ddlCountry.SelectedIndex = 0;
            rbActive.ClearSelection();
            cblHobbies.ClearSelection();
            populateCountry();
            lblOutput.Visible = false;
            gvInfo.Visible = false;

         
        }

        protected void btnSD_Click(object sender, EventArgs e)// done but with multiple rows for each user for hobbies
        { // show all intern

            string mySql = @"SELECT intern.uName, intern.fNameEn, intern.fNameAr, intern.salary, intern.active, country.country, hobbies.hobbyName FROM country INNER JOIN intern ON country.countryId = intern.countryId INNER JOIN internHobbies ON intern.internId = internHobbies.internId inner JOIN hobbies on internHobbies.hobbyId = hobbies.hobbyId ";
            lblOutput.Visible = false;
                CRUD myCrud = new CRUD();
                SqlDataReader dr = myCrud.getDrPassSql(mySql);
                gvInfo.Visible = true;

                gvInfo.DataSource = dr;
                gvInfo.DataBind();
         
           
        }

        protected void btnSI_Click(object sender, EventArgs e) 
        {   // show secific intern by entering username

            if (txtUserName.Text == "")
            {
                lblOutput.Visible = true;
                lblOutput.Text = " Please enter intern user name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                return;
            }
          
            displayIntern();

          
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {  // internId	uName	fNameEn	fNameAr	salary	active	countryId
            string mySql = "insert into intern(uName, fNameEN , fNameAr,salary, active , countryId) " +
                "values(@uName , @fNameEn, @fNameAr ,@salary, @active , @countryId)";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            if (txtUserName.Text != "" && txtFNameEn.Text != "" && txtFNameAr.Text != "" && txtSalary.Text != "" && ddlCountry.SelectedItem.Value != "" && rbActive.SelectedItem.Selected == true)
            {
                myPara.Add("@uName", txtUserName.Text);
                myPara.Add("@fNameEn", txtFNameEn.Text);
                myPara.Add("@fNameAr", txtFNameAr.Text);
                myPara.Add("@salary", decimal.Parse(txtSalary.Text));
                int countryId = int.Parse(ddlCountry.SelectedValue);
                myPara.Add("@countryId", countryId); // from my mind
                if (rbActive.SelectedItem.Selected == true)
                {
                    bool choose = rbActive.SelectedItem.Value == "1";
                    myPara.Add("@active", choose);
                }
                CRUD myCrud = new CRUD();
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);// insert into intern basic columns
                int n = internId(myPara); // get recent internId added

                //   iterate cbl collection and capture the selected values
                List<int> mySelectedHobbies = new List<int>();
                foreach (ListItem item in cblHobbies.Items)
                {
                    if (item.Selected)
                    {
                        mySelectedHobbies.Add(int.Parse(item.Value));
                    }
                }
                int rtnInsHobby = chooseHobby(n, 1, mySelectedHobbies);// insert into internHobbies table for recent intern added
                if (rtn >= 1 && rtnInsHobby >= 1)
                {
                    btnSI_Click(sender, e);
                    lblOutput.Visible = true;
                    lblOutput.Text = "You have been added succesfuly :)";
                }
                else

                {
                    lblOutput.Visible = true;
                    lblOutput.Text = "You have not been added :(";
                }
            }
            else
            {
                lblOutput.Visible = true;
                //lblOutput.BackColor = System.Drawing.Color.BlanchedAlmond;
                lblOutput.ForeColor = System.Drawing.Color.Red;
                lblOutput.Text = "Please fill all fields :)";
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)// check update hobby
        {// internId	uName	fNameEn	fNameAr	salary	active	countryId
            string mySql = "update intern set fNameEN=@fNameEn , fNameAr=@fNameAr, salary=@salary ,active=@active, countryId=@countryId  where uName=@uName";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@uName", txtUserName.Text);
            if (txtFNameEn.Text != "" && txtFNameAr.Text != "" && txtSalary.Text != "" && ddlCountry.SelectedItem.Value != "" && rbActive.SelectedItem.Selected == true)
            {
                myPara.Add("@fNameEn", txtFNameEn.Text);
                myPara.Add("@fNameAr", txtFNameAr.Text);
                myPara.Add("@salary", decimal.Parse(txtSalary.Text));
                if (rbActive.SelectedItem.Selected == true)
                {
                    bool choose = rbActive.SelectedItem.Value == "1";
                    myPara.Add("@active", choose);
                }
                myPara.Add("@countryId", int.Parse(ddlCountry.SelectedValue));

                CRUD myCrud = new CRUD();
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                int n = internId(myPara); // get recent internId added

                //   iterate cbl collection and capture the selected values
                List<int> mySelectedHobbies = new List<int>();
                foreach (ListItem item in cblHobbies.Items)
                {
                    if (item.Selected)
                    {
                        mySelectedHobbies.Add(int.Parse(item.Value));
                    }
                }
                // update internHobbies table for specific intern
                int rtnInsHobby = chooseHobby(n , 2 , mySelectedHobbies);
                if (rtn >= 1)
                {
                    lblOutput.Visible = true;
                    lblOutput.Text = "Your information have been updated succesfuly :)";
                }
                else
                {
                    lblOutput.Visible = true;
                    lblOutput.Text = "Your information have not been updated :(";
                }
                // make call to show intern information
                btnSI_Click(sender, e);
            }
            else
            {
                lblOutput.Visible = true;
                lblOutput.ForeColor = System.Drawing.Color.Red;
                lblOutput.Text = "Please fill all fields :)";
            }
         }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string mySql = "delete intern where uName=@uName";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@uName", txtUserName.Text);

            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Visible = true;
                lblOutput.Text = "Your information have been deleted succesfuly :)"; }
            else
            {
                lblOutput.Visible = true;
                lblOutput.Text = "Your information have not been deleted :("; }
        }

        protected int chooseHobby(int internId , int n , List<int> mySelectedHobbies)// check update hobby
        {
               int rtnInsertHobby = -1;
               string addHobby = "insert into internHobbies(internId, hobbyId)" +
                    "values(@internId , @hobbyId)";
               string  updateHobby = "update internHobbies set hobbyId =@hobbyId where internId=@internId";
             
            if (n == 1)// add hobby
            {
                foreach (int item in mySelectedHobbies) // Loop through List with foreach
                {// mySelectedHobbies contains cbl values starting from 1
                    CRUD myCrud = new CRUD();
                    Dictionary<string, object> myPara2 = new Dictionary<string, object>();
                    myPara2.Add("@internId", internId);
                    myPara2.Add("@hobbyId", item);
                    rtnInsertHobby = myCrud.InsertUpdateDelete(addHobby, myPara2);
                }

            }
            else// update hobby
            {
                foreach (int item in mySelectedHobbies) // Loop through List with foreach
                {
                    CRUD myCrud = new CRUD();
                    Dictionary<string, object> myPara2 = new Dictionary<string, object>();
                    myPara2.Add("@internId", internId);
                    myPara2.Add("@hobbyId", item);
                    rtnInsertHobby = myCrud.InsertUpdateDelete(updateHobby, myPara2);
                }
              
             
            }
            return rtnInsertHobby;
            
        }

        protected int internId(Dictionary<string, object>  myPara)
        {
            int internId = 0;
            CRUD myCrud = new CRUD();
            string getId = "select internId from intern where uName=@uName";
            SqlDataReader idDr = myCrud.getDrPassSql(getId, myPara);// get id of recent intern added
            if (idDr.Read())
            {
                 internId = idDr.GetInt32(0);
            } 
            return internId;
        }
      
        protected void displayIntern()
        {
            lblOutput.Visible = false;
            // show secific intern by entering username
            string mySql = "SELECT intern.uName, intern.fNameEn, intern.fNameAr, intern.salary, intern.active, country.country, hobbies.hobbyName FROM country INNER JOIN intern ON country.countryId = intern.countryId INNER JOIN internHobbies ON intern.internId = internHobbies.internId inner JOIN hobbies on internHobbies.hobbyId = hobbies.hobbyId where intern.uName=@uName";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@uName", txtUserName.Text);
            CRUD myCrud = new CRUD();
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            gvInfo.Visible = true;
            gvInfo.DataSource = dr;
            gvInfo.DataBind();
            dr.Read();
            if (dr.HasRows)
            {
                SqlConnection con = new SqlConnection(CRUD.conStr);
                con.Open();
                SqlCommand cmd2 = new SqlCommand(mySql, con);
                foreach (KeyValuePair<string, object> p in myPara)
                {
                    // can put validation here to see if the value is empty or not 
                    cmd2.Parameters.AddWithValue(p.Key, p.Value);
                }
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        txtUserName.Text = dr2[0].ToString();
                        txtFNameEn.Text = dr2[1].ToString();
                        txtFNameAr.Text = dr2[2].ToString();
                        txtSalary.Text = dr2[3].ToString();
                        string active = dr2[4].ToString();
                        if (active == "True")
                            rbActive.Items.FindByValue("1").Selected = true;
                        else
                            rbActive.Items.FindByValue("0").Selected = true;
                        ddlCountry.SelectedItem.Text = dr2[5].ToString();
                        string hobby = dr2[6].ToString();
                        for (int i = 0; i < cblHobbies.Items.Count; i++)
                        {
                            if (hobby.Contains(cblHobbies.Items[i].Text))
                                cblHobbies.Items.FindByText(cblHobbies.Items[i].Text).Selected = true;
                        }
                    }
                }


            }
            else
            {
                lblOutput.Visible = true;
                lblOutput.Text = " Intern Does Not Exists !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }


        }

        
    }
}