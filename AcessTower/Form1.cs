using System;
using System.Collections.Generic;
using System.Windows.Forms;
//
using AcessTower.myAplication;

namespace AcessTower
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //MY METHODS_UI - USERS
        private void Method_UI_SetComboGroup1(bool act)
        {
            var groupApp = new myGoupAplication();
            myComboGroup1.ValueMember = "id";
            myComboGroup1.DisplayMember = "nameGroup";
            myComboGroup1.DataSource = groupApp.Method_APP_SelectAll();
            myComboGroup1.Enabled = act;
        }

        private void Method_UI_SetComboJobTitle1(bool act)
        {
            var userApp = new myUserAplication();
            myComboJobTitle1.ValueMember = "jobTitle";
            myComboJobTitle1.DisplayMember = "jobTitle";
            myComboJobTitle1.DataSource = userApp.Method_APP_SelectAll_JobTitle();
            myComboJobTitle1.Enabled = act;
        }

        private void Method_UI_EnableAplly(int count)
        {
            myButtonApply1.Enabled = false;
            var userApp = new myUserAplication();
            userApp.Method_APP_ResetList();
            Method_UI_SetComboGroup1(false);
        }

        private void Method_UI_SetCountSelecRowUser(int count)
        {
            if (count>0)
            {
                myButtonCheckAllResults1.Enabled = true;
            }
            else
            {

                myButtonCheckAllResults1.Enabled = false;
            }


            myLblCountQuery1.Text = count.ToString();
        }

        private void Method_UI_SetCountSelecRowDistributionGroup(int count)
        {
            if (count > 0)
            {
                myButtonCheckAllResults2.Enabled = true;
            }
            else
            {

                myButtonCheckAllResults2.Enabled = false;
            }


            myLblCountQuery2.Text = count.ToString();
        }

        private List<string> Method_UI_CreateListUser()
        {
            var listIdUser = new List<string>();
            if (myDataGridView1.SelectedRows.Count > 0)
            {
                int lengthRows = Convert.ToInt32(myDataGridView1.RowCount.ToString());
                for (int i = 0; i < lengthRows; i++)
                {
                    if (myDataGridView1.Rows[i].Selected)
                    {
                        listIdUser.Add(myDataGridView1.Rows[i].Cells[0].Value.ToString());
                    }
                }
            }
            return listIdUser;
        }

        private List<string> Method_UI_CreateListGroup()
        {
            var listIdZone = new List<string>();
            var groupApp = new myGoupAplication();
            listIdZone.Add(myComboGroup1.SelectedValue.ToString().ToUpper());
            return listIdZone;
        }

        private void Method_UI_ApplyDistributionGroupForUsers( )
        {
            var distributionGroupApp = new myDistributionGroupAplication();
            foreach (var user in Method_UI_CreateListUser())
            {
                foreach (var grupo in Method_UI_CreateListGroup())
                {
                    distributionGroupApp.Method_APP_InsertOneDistributionGroup(user.ToString().ToUpper(), grupo.ToString().ToUpper());
                }
            }
        }

        private void Method_UI_RefreshGridUser()
        {
            var userApp = new myUserAplication();
            myDataGridView1.Enabled = true;
            this.myDataGridView1.DataSource = userApp.Method_APP_SelectAll();
            this.myDataGridView1.ClearSelection();
            Method_UI_SetCountSelecRowUser(userApp.Method_APP_SelectAll().Count);
        }

        private void Method_UI_ClearGridUser()
        {
            var userApp = new myUserAplication();
            this.myDataGridView1.DataSource = userApp.Method_APP_ResetList();
            this.myDataGridView1.ClearSelection();
            //RESERT LABEL COUNT QUERY
            myLblCountQuery1.Text = "0";
            myButtonCheckAllResults1.Enabled = false;
            myDataGridView1.Enabled = false;
        }



        //MY METHODS_UI - DISTRIBUTION GROUPS
        private List<string> Method_UI_CreateListDistributionGroup()
        {
            var listIdDistribution = new List<string>();
            if (myDataGridView2.SelectedRows.Count > 0)
            {
                int lengthRows = Convert.ToInt32(myDataGridView2.RowCount.ToString()) ;
                for (int i = 0; i < lengthRows; i++)
                {
                    if (myDataGridView2.Rows[i].Selected)
                    {
                        listIdDistribution.Add(myDataGridView2.Rows[i].Cells[0].Value.ToString());
                    }
                }
            }
            return listIdDistribution;
        }

        private void Method_UI_RemoveDistributionGroupForUsers()
        {
            var distributionGroupApp = new myDistributionGroupAplication();
            foreach (var group in Method_UI_CreateListDistributionGroup())
            {
                distributionGroupApp.Method_APP_DeleteOneDistributionGroup(group.ToString().ToUpper());
            }
        }

        private void Method_UI_RefreshGridDistributionGroup()
        {
            var distribuitionGroupApp = new myDistributionGroupAplication();
            myDataGridView2.Enabled = true;
            this.myDataGridView2.DataSource = distribuitionGroupApp.Method_APP_SelectAll();
            this.myDataGridView2.ClearSelection();
            Method_UI_SetCountSelecRowDistributionGroup(distribuitionGroupApp.Method_APP_SelectAll().Count);
        }

        private void Method_UI_ClearGridDistributionGroup()
        {
            var userApp = new myDistributionGroupAplication();
            this.myDataGridView2.DataSource = userApp.Method_APP_ResetList();
            this.myDataGridView2.ClearSelection();
            myLblCountQuery2.Text = "0";
            myButtonCheckAllResults2.Enabled = false;
            myDataGridView2.Enabled = false;
        }

        private void Method_UI_SetComboJobTitle2(bool act)
        {
            var userApp = new myUserAplication();
            myComboJobTitle2.ValueMember = "jobTitle";
            myComboJobTitle2.DisplayMember = "jobTitle";
            myComboJobTitle2.DataSource = userApp.Method_APP_SelectAll_JobTitle();
            myComboJobTitle2.Enabled = act;
        }



        //MY METHODS_EVENTS_ACTIONS - USERS
        private void Form1_Load(object sender, EventArgs e)
        {
            Method_UI_SetComboGroup1(false);
        }

        private void myButtonSearch1_Click(object sender, EventArgs e)
        {
            var userApp = new myUserAplication();
           
            if (myCheckBoxJobTitle1.Checked == false && myCheckBoxFirstName1.Checked==false)
            {
                myDataGridView1.Enabled = true;
                this.myDataGridView1.DataSource = userApp.Method_APP_SelectAll();
                this.myDataGridView1.ClearSelection();
                Method_UI_SetCountSelecRowUser(userApp.Method_APP_SelectAll().Count);
            }
            else if (myCheckBoxJobTitle1.Checked == true && myCheckBoxFirstName1.Checked == false)
            {
                myDataGridView1.Enabled = true;
                this.myDataGridView1.DataSource = userApp.Method_APP_SelectByJobTitle(myComboJobTitle1.SelectedItem.ToString());
                this.myDataGridView1.ClearSelection();
                Method_UI_SetCountSelecRowUser(userApp.Method_APP_SelectByJobTitle(myComboJobTitle1.SelectedItem.ToString()).Count);
            }
            else if (myCheckBoxJobTitle1.Checked == false && myCheckBoxFirstName1.Checked == true)
            {
                myDataGridView1.Enabled = true;
                this.myDataGridView1.DataSource = userApp.Method_APP_SelectByName(myTxtBoxFirstName1.Text.ToString());
                this.myDataGridView1.ClearSelection();
                Method_UI_SetCountSelecRowUser(userApp.Method_APP_SelectByName(myTxtBoxFirstName1.Text.ToString()).Count);
            }
            else if (myCheckBoxJobTitle1.Checked == true && myCheckBoxFirstName1.Checked == true)
            {
                myDataGridView1.Enabled = true;
                this.myDataGridView1.DataSource = userApp.Method_APP_SelectByNameAndJob(myTxtBoxFirstName1.Text.ToString(), myComboJobTitle1.SelectedItem.ToString());
                this.myDataGridView1.ClearSelection();
                Method_UI_SetCountSelecRowUser(userApp.Method_APP_SelectByNameAndJob(myTxtBoxFirstName1.Text.ToString(), myComboJobTitle1.SelectedItem.ToString()).Count);
            }
        }

        private void myButtonCheckAllResults1_Click(object sender, EventArgs e)
        {
            this.myDataGridView1.SelectAll();
        }

        private void myButtonUncheckAllResults1_Click(object sender, EventArgs e)
        {
            this.myDataGridView1.ClearSelection();
        }

        private void myDataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            int countRow = myDataGridView1.SelectedRows.Count;
            myLblCountSelectRow1.Text = countRow.ToString();

            if (countRow>0)
            {
                myButtonApply1.Enabled = true;
                Method_UI_SetComboGroup1(true);
                myButtonUncheckAllResults1.Enabled = true;
            }
            else if (countRow == 0)
            {
                myButtonApply1.Enabled = false;
                Method_UI_SetComboGroup1(false);
                myButtonUncheckAllResults1.Enabled = false;
            }

        }
        
        private void myButtonApply1_Click(object sender, EventArgs e)
        {
            Method_UI_ApplyDistributionGroupForUsers();
            Method_UI_RefreshGridUser();
        }

        private void myButtonClear1_Click(object sender, EventArgs e)
        {
            Method_UI_ClearGridUser();
        }

        private void myCheckBoxJobTitle1_CheckedChanged(object sender, EventArgs e)
        {
            if (myCheckBoxJobTitle1.Checked == true)
            {
                //ADIONAR COMBO
                Method_UI_SetComboJobTitle1(true);
                myComboJobTitle1.Enabled = true;
                myComboJobTitle1.Focus();
            }
            else
            {
                myComboJobTitle1.DataSource = null;
                myComboJobTitle1.Enabled = false;
                if (myCheckBoxFirstName1.Checked==false)
                {
                    Method_UI_ClearGridUser();
                }
               
            }
        }

        private void myCheckBoxFirstName1_CheckedChanged(object sender, EventArgs e)
        {
            if (myCheckBoxFirstName1.Checked == true)
            {
                myTxtBoxFirstName1.Enabled = true;
                myTxtBoxFirstName1.Focus();
            }
            else
            {
                myTxtBoxFirstName1.Text = null;
                myTxtBoxFirstName1.Enabled = false;
                if (myCheckBoxJobTitle1.Checked == false)
                {
                    Method_UI_ClearGridUser();
                }
            }
        }




        //MY METHODS_EVENTS_ACTIONS - DISTRIBUTION GROUPS
        private void myButtonSearch2_Click(object sender, EventArgs e)
        {
            var distribuitionGroupApp = new myDistributionGroupAplication();
            if (myCheckBoxJobTitle2.Checked == false && myCheckBoxFirstName2.Checked == false)
            {
                myDataGridView2.Enabled = true;
                this.myDataGridView2.DataSource = distribuitionGroupApp.Method_APP_SelectAll();
                this.myDataGridView2.ClearSelection();
                Method_UI_SetCountSelecRowDistributionGroup(distribuitionGroupApp.Method_APP_SelectAll().Count);
            }
            else if (myCheckBoxJobTitle2.Checked == true && myCheckBoxFirstName2.Checked == false)
            {
                myDataGridView2.Enabled = true;
                this.myDataGridView2.DataSource = distribuitionGroupApp.Method_APP_SelectByJobTitle(myComboJobTitle2.SelectedItem.ToString());
                this.myDataGridView2.ClearSelection();
                Method_UI_SetCountSelecRowDistributionGroup(distribuitionGroupApp.Method_APP_SelectByJobTitle(myComboJobTitle2.SelectedItem.ToString()).Count);
            }
            else if (myCheckBoxJobTitle2.Checked == false && myCheckBoxFirstName2.Checked == true)
            {
                myDataGridView2.Enabled = true;
                this.myDataGridView2.DataSource = distribuitionGroupApp.Method_APP_SelectByName(myTxtBoxFirstName2.Text.ToString());
                this.myDataGridView2.ClearSelection();
                Method_UI_SetCountSelecRowDistributionGroup(distribuitionGroupApp.Method_APP_SelectByName(myTxtBoxFirstName2.Text.ToString()).Count);
            }
            else if (myCheckBoxJobTitle2.Checked == true && myCheckBoxFirstName2.Checked == true)
            {
                myDataGridView2.Enabled = true;
                this.myDataGridView2.DataSource = distribuitionGroupApp.Method_APP_SelectByNameAndJob(myTxtBoxFirstName2.Text.ToString(), myComboJobTitle2.SelectedItem.ToString());
                this.myDataGridView2.ClearSelection();
                Method_UI_SetCountSelecRowDistributionGroup(distribuitionGroupApp.Method_APP_SelectByNameAndJob(myTxtBoxFirstName2.Text.ToString(), myComboJobTitle2.SelectedItem.ToString()).Count);
            }

        }

        private void myCheckBoxFirstName2_CheckedChanged(object sender, EventArgs e)
        {
            if (myCheckBoxFirstName2.Checked == true)
            {
                myTxtBoxFirstName2.Enabled = true;
                myTxtBoxFirstName2.Focus();
            }
            else
            {
                myTxtBoxFirstName2.Text = null;
                myTxtBoxFirstName2.Enabled = false;
                if (myCheckBoxJobTitle2.Checked == false)
                {
                    Method_UI_ClearGridDistributionGroup();
                }
            }
        }

        private void myCheckBoxJobTitle2_CheckedChanged(object sender, EventArgs e)
        {
            if (myCheckBoxJobTitle2.Checked == true)
            {
                Method_UI_SetComboJobTitle2(true);
                myComboJobTitle2.Enabled = true;
                myComboJobTitle2.Focus();
            }
            else
            {
                myComboJobTitle2.DataSource = null;
                myComboJobTitle2.Enabled = false;
                if (myCheckBoxFirstName2.Checked == false)
                {
                    Method_UI_ClearGridDistributionGroup();
                }
            }
        }

        private void myDataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            int countRow = myDataGridView2.SelectedRows.Count;
            myLblCountSelectRow2.Text = countRow.ToString();

            if (countRow > 0)
            {
                myButtonRemove2.Enabled = true;
                myButtonUncheckAllResults2.Enabled = true;
            }
            else if (countRow == 0)
            {
                myButtonRemove2.Enabled = false;
                myButtonUncheckAllResults2.Enabled = false;
            }
        }

        private void myButtonClear2_Click(object sender, EventArgs e)
        {
            Method_UI_ClearGridDistributionGroup();
        }

        private void myButtonCheckAllResults2_Click(object sender, EventArgs e)
        {
            this.myDataGridView2.SelectAll();
        }

        private void myButtonUncheckAllResults2_Click(object sender, EventArgs e)
        {
            this.myDataGridView2.ClearSelection();
        }

        private void myButtonRemove2_Click(object sender, EventArgs e)
        {
            Method_UI_RemoveDistributionGroupForUsers();
            string texto = "LISTA DE IDS DISTRUTION GROUP \n";
            foreach (var distribution in Method_UI_CreateListDistributionGroup())
            {
                texto += "ID: " + distribution.ToString() + "\n";
            }
            //MessageBox.Show(texto);
            Method_UI_RefreshGridDistributionGroup();

        }
    }


}