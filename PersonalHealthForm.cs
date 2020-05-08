// Project: PersonalHealthForm
// Name: Tony Jordan
// Date: 5/3/2020
// Description: Demonstrate skill in using
// Sql and C# to produce a database program
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnthonyJordan_FinalProject.ExecutedDatasetTableAdapters;
using System.Data.SqlClient;
using System.IO;

namespace AnthonyJordan_FinalProject
{
    public partial class PersonalHealthForm : Form
    {
        // we use this to hold the user's password data when they sign in, this is needed for
        // changing the user's password
        private string password;
        // We hold the user's ID in a class variable because it is used almost every line
        private string ID;
        // database connection string
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30";

        public PersonalHealthForm(string ID)
        {
            this.ID = ID;
            InitializeComponent();
            updateData();
            
        }
        public void updateData()
        {
            // Update Data does exactly what it's name suggests, any time a change is made, or the
            // cancel button is pressed, we use this method to return the form to it's default 
            // setting (meaning all of the database information is displayed)
            SqlConnection connection = new SqlConnection(connectionString);
            string patientC = "SELECT * FROM PATIENT_TBL" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand patientTBL = new SqlCommand(patientC, connection);
            connection.Open();
            SqlDataReader patient_Reader = patientTBL.ExecuteReader(CommandBehavior.CloseConnection);
            // There is a lot of repitition in this code that could've very likely
            // been reduced with the use of more methods or classes, however I really
            // wanted to solidify the fact that I somewhat know what I'm doing, so I wanted 
            // the bulk of my work on one single file
            while (patient_Reader.Read())
            {
                tbID.Text = (string)patient_Reader["PATIENT_ID"];
                tbLastName.Text = (string)patient_Reader["LAST_NAME"];
                tbFirstName.Text = (string)patient_Reader["FIRST_NAME"];
                // we have to test for a null value in the address bar because 
                // when we record the address, we use substrings, which can 
                // cause errors if the string is not formatted correctly.
                dtpDOB.Text = patient_Reader["DATE_Of_BIRTH"].ToString();
                if (patient_Reader["ADDRESS_STREET"] != DBNull.Value)
                    tbmyAddress.Text = (string)patient_Reader["ADDRESS_STREET"];

                if (patient_Reader["ADDRESS_CITY"] != DBNull.Value)
                    tbMyCity.Text = (string)patient_Reader["ADDRESS_CITY"];

                if (patient_Reader["ADDRESS_STATE"] != DBNull.Value)
                    tbmyAddress.Text += ", " + (string)patient_Reader["ADDRESS_STATE"];

                if (patient_Reader["ADDRESS_ZIP"] != DBNull.Value)
                    tbMyPostalCode.Text = (string)patient_Reader["ADDRESS_ZIP"];

                if (patient_Reader["PHONE_HOME"] != DBNull.Value)
                    tbMyHomeTelephone.Text = (string)patient_Reader["PHONE_HOME"];

                if (patient_Reader["PHONE_MOBILE"] != DBNull.Value)
                    tbMyMobileTelephone.Text = (string)patient_Reader["PHONE_MOBILE"];

                if (patient_Reader["PHONE_WORK"] != DBNull.Value)
                    tbMyWorkTelephone.Text = (string)patient_Reader["PHONE_WORK"];

                if (patient_Reader["PRIMARY_ID"] != DBNull.Value)
                    tbID.Text += (string)patient_Reader["PRIMARY_ID"];

                if (patient_Reader["FAX"] != DBNull.Value)
                    tbMyFaxNumber.Text = (string)patient_Reader["FAX"];

                if (patient_Reader["EMAIL"] != DBNull.Value)
                    tbMyEmail.Text = (string)patient_Reader["EMAIL"];

                if (patient_Reader["EMERGENCY_CONTACT"] != DBNull.Value)
                    tbKin.Text = (string)patient_Reader["EMERGENCY_CONTACT"];

                if (patient_Reader["EMERGENCY_RELATIONSHIP"] != DBNull.Value)
                    tbRelationship.Text = (string)patient_Reader["EMERGENCY_RELATIONSHIP"];

                if (patient_Reader["EMERGENCY_STREET"] != DBNull.Value)
                    tbKinAddress.Text = (string)patient_Reader["EMERGENCY_STREET"] + " , ";

                if (patient_Reader["EMERGENCY_STATE"] != DBNull.Value)
                    tbKinAddress.Text += (string)patient_Reader["EMERGENCY_STATE"];

                if (patient_Reader["EMERGENCY_CITY"] != DBNull.Value)
                    tbKinCity.Text = (string)patient_Reader["EMERGENCY_CITY"];

                if (patient_Reader["EMERGENCY_ZIP"] != DBNull.Value)
                    tbKinZip.Text = (string)patient_Reader["EMERGENCY_ZIP"];

                if (patient_Reader["EMERGENCY_HOME"] != DBNull.Value)
                    tbKinHomeTelephone.Text = (string)patient_Reader["EMERGENCY_HOME"];

                if (patient_Reader["EMERGENCY_WORK"] != DBNull.Value)
                    tbKinWorkTelephone.Text = (string)patient_Reader["EMERGENCY_WORK"];

                if (patient_Reader["EMERGENCY_FAX"] != DBNull.Value)
                    tbKinFaxNumber.Text = (string)patient_Reader["EMERGENCY_FAX"];

                if (patient_Reader["EMERGENCY_EMAIL"] != DBNull.Value)
                    tbKinEmail.Text = (string)patient_Reader["EMERGENCY_EMAIL"];

                if (patient_Reader["EMERGENCY_MOBILE"] != DBNull.Value)
                    tbKinMobileTelephone.Text = (string)patient_Reader["EMERGENCY_MOBILE"];

                if (patient_Reader["INSURER"] != DBNull.Value)
                    tbInsurer.Text = (string)patient_Reader["INSURER"];

                if (patient_Reader["PLAN"] != DBNull.Value)
                    tbInsurancePlan.Text = (string)patient_Reader["PLAN"];

                if (patient_Reader["TITLE"] != DBNull.Value)
                    cbTitle.Text = (string)patient_Reader["TITLE"];

                if (patient_Reader["INSURANCE_NUMBER"] != DBNull.Value)
                    tbInsuranceNumber.Text = (string)patient_Reader["INSURANCE_NUMBER"];

                if (patient_Reader["INITIALS"] != DBNull.Value)
                    tbInitials.Text = (string)patient_Reader["INITIALS"];
            }
            connection.Close();

            connection.Open();
            string passwordsC = "SELECT * FROM PATIENTS_PASSWORDS" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand passwordTbl = new SqlCommand(passwordsC, connection);
            SqlDataReader password_Reader = passwordTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (password_Reader.Read())
            {
                lblUsername.Text = (string)password_Reader["USERNAME"];
                password = (string)password_Reader["PASSWORD"];
            }
            connection.Close();

            connection.Open();
            // This line of code is so if you press the cancel button, items in the list do not duplicate
            lbAllergies.Items.Clear();
            string allergyC = "SELECT * FROM ALLERGY_TBL" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand allergyTbl = new SqlCommand(allergyC, connection);
            SqlDataReader allergy_Reader = allergyTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (allergy_Reader.Read())
            {
                if (!(allergy_Reader["Allergen"] == DBNull.Value))
                    lbAllergies.Items.Add((string)allergy_Reader["Allergen"] + " , " + allergy_Reader["ONSET_DATE"].ToString().Substring(0, allergy_Reader["ONSET_DATE"].ToString().IndexOf(" "))
                        + " , " + (string)allergy_Reader["NOTE"] + " , " + (string)allergy_Reader["ALLERGY_ID"]);
            }
            connection.Close();

            connection.Open();
            lbMedicalProcedures.Items.Clear();
            string procedureC = "SELECT * FROM MED_PROC_TBL" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand procedureTbl = new SqlCommand(procedureC, connection);
            SqlDataReader procedure_reader = procedureTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (procedure_reader.Read())
            {
                if (!(procedure_reader["MED_PROCEDURE"] == DBNull.Value))
                    lbMedicalProcedures.Items.Add((string)procedure_reader["MED_PROCEDURE"] + " , " +
                        procedure_reader["DATE"] + " , " + (string)procedure_reader["DOCTOR"] + 
                        " , " +(string)procedure_reader["PROCEDURE_ID"]);

            }
            connection.Close();

            connection.Open();
            string perC = "SELECT * FROM PER_DETAILS_TBL" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand perTbl = new SqlCommand(perC, connection);
            SqlDataReader per_reader = perTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (per_reader.Read())
            {
                if (!(per_reader["BLOOD_TYPE"] == DBNull.Value))
                    cbBloodType.Text = (string)per_reader["BLOOD_TYPE"];
                if (!(per_reader["ORGAN_DONOR"] == DBNull.Value))
                {
                    if ((bool)per_reader["ORGAN_DONOR"])
                        cbOrganDonor.Checked = true;
                }
                if (!(per_reader["HIV_STATUS"] == DBNull.Value))
                {
                    if ((bool)per_reader["HIV_STATUS"])
                        rbtnPositive.Checked = true;
                    else
                        rbtnNegative.Checked = true;
                }
                if (!(per_reader["HEIGHT_INCHES"] == DBNull.Value))
                    tbHeight.Text = per_reader["HEIGHT_INCHES"].ToString();
                if (!(per_reader["WEIGHT_LBS"] == DBNull.Value))
                    tbWeight.Text = per_reader["WEIGHT_LBS"].ToString();
            }
            connection.Close();

            connection.Open();
            lbImmunizations.Items.Clear();
            string immuneC = "SELECT * FROM IMMUNIZATION_TBL " +
                "WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand immuneTbl = new SqlCommand(immuneC, connection);
            SqlDataReader immune_reader = immuneTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (immune_reader.Read())
            {
                lbImmunizations.Items.Add((string)immune_reader["IMMUNIZATION"] + ","
                    + immune_reader["DATE"].ToString() + " , " + (string)immune_reader["IMMUNIZATION_ID"]);
            }
            connection.Close();

            connection.Open();
            lbMedicalConditions.Items.Clear();
            string condC = "SELECT * FROM CONDITION" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand condTbl = new SqlCommand(condC, connection);
            SqlDataReader cond_reader = condTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (cond_reader.Read())
            {
                lbMedicalConditions.Items.Add((string)cond_reader["CONDITION"] +
                    " , " + cond_reader["ONSET_DATE"].ToString() + " , " + (string)cond_reader["CONDITION_ID"]);
            }
            connection.Close();

            connection.Open();
            lbTestResults.Items.Clear();
            string testC = "SELECT * FROM TEST_TBL" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand testTbl = new SqlCommand(testC, connection);
            SqlDataReader test_reader = testTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (test_reader.Read())
            {
                lbTestResults.Items.Add((string)test_reader["TEST"] + " , " +
                    (string)test_reader["RESULT"] + " , " + test_reader["DATE"].ToString() 
                    + " , " + (string)test_reader["TEST_ID"]);

            }
            connection.Close();

            connection.Open();
            lbMedication.Items.Clear();
            string medicationC = "SELECT * FROM MEDICATION_TBL" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand medicationTbl = new SqlCommand(medicationC, connection);
            SqlDataReader med_reader = medicationTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (med_reader.Read())
            {
                lbMedication.Items.Add((string)med_reader["MEDICATION"] + " , " + med_reader["DATE"].ToString() +
                    " , " + (string)med_reader["MED_ID"]);
            }
            connection.Close();

            connection.Open();
            string primeC = "SELECT * FROM PRIMARY_CARE_TBL" +
                " WHERE (PRIMARY_ID = " + ID + ")";
            SqlCommand primeTbl = new SqlCommand(primeC, connection);
            SqlDataReader prime_reader = primeTbl.ExecuteReader(CommandBehavior.CloseConnection);
            while (prime_reader.Read())
            {
                tbPrimaryName.Text = (string)prime_reader["NAME_FISRT"] + " " + (string)prime_reader["NAME_LAST"];
                if (!(prime_reader["SPECIALTY"] == DBNull.Value))
                    tbPrimarySpecialty.Text = (string)prime_reader["SPECIALTY"];
                if (!(prime_reader["PHONE_MOBILE"] == DBNull.Value))
                    tbPrimaryMobileTelephone.Text = (string)prime_reader["PHONE_MOBILE"];
                if (!(prime_reader["PHONE_OFFICE"] == DBNull.Value))
                    primaryWork.Text = (string)prime_reader["PHONE_OFFICE"];
                if (!(prime_reader["FAX"] == DBNull.Value))
                    primaryFax.Text = (string)prime_reader["FAX"];
                if (!(prime_reader["EMAIL"] == DBNull.Value))
                    primeEmail.Text = (string)prime_reader["EMAIL"];
            }
            connection.Close();
            // We test to see if an image file exists, and if it does, we apply that Image
            // to the Profile Image box. Each image name is based on the ID of the client, so 
            // they should not ever duplicate.
            if (File.Exists(ID.Substring(0, ID.IndexOf(" ")) + ".jpg"))
            {
                using (FileStream stream = new FileStream(ID.Substring(0, ID.IndexOf(" ")) + ".jpg", FileMode.Open, FileAccess.Read))
                {
                    pbProfilePicture.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
            }
            if (File.Exists(ID.Substring(0, ID.IndexOf(" ")) + ".bmp"))
            {
                using (FileStream stream = new FileStream(ID.Substring(0, ID.IndexOf(" ")) + ".bmp", FileMode.Open, FileAccess.Read))
                {
                    pbProfilePicture.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
            }
            if (File.Exists(ID.Substring(0, ID.IndexOf(" ")) + ".gif"))
            {
                using (FileStream stream = new FileStream(ID.Substring(0, ID.IndexOf(" ")) + ".gif", FileMode.Open, FileAccess.Read))
                {
                    pbProfilePicture.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
            }
            if (File.Exists(ID.Substring(0, ID.IndexOf(" ")) + ".png"))
            {
                using (FileStream stream = new FileStream(ID.Substring(0, ID.IndexOf(" ")) + ".png", FileMode.Open, FileAccess.Read))
                {
                    pbProfilePicture.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
            }
        }
        // these handlers activate the Change Password Button if any relevant textboxes
        // are typed in.
        private void tbOldPassword_TextChanged(object sender, EventArgs e)
        {
            lblChangePassword.Enabled = true;
            lblCancel.Enabled = true;
        }
        private void tbKinPostalCode_TextChanged(object sender, EventArgs e)
        {
            lblChangePassword.Enabled = true;
            lblCancel.Enabled = true;
        }
        private void tbConfirmNewPassword_TextChanged(object sender, EventArgs e)
        {
            lblChangePassword.Enabled = true;
            lblCancel.Enabled = true;
        }
        // Essentially, we just use the password variable we created earlier, so we don't
        // have to use SQL statements. We do the obvious checks, and we don't have to do anything spectacular.
        private void lblChangePassword_Click(object sender, EventArgs e)
        {
            // first check to see if old password and new password are the same
            if(tbOldPassword.Text == tbNewPassword.Text)
            {
                MessageBox.Show("New and Old Passwords cannot match");
            }
            else
            {
                if(!(tbNewPassword.Text == tbConfirmNew.Text))
                {
                    MessageBox.Show("Passwords do not match");
                }
                else if(!(tbOldPassword.Text == password))
                {
                    MessageBox.Show("Incorrect Old Password");
                }
                else
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string com = "UPDATE PATIENTS_PASSWORDS" +
                        " SET PASSWORD = @p" +
                        " WHERE (PATIENT_ID = " + ID + ")";
                    SqlCommand command = new SqlCommand(com, connection);
                    command.Parameters.AddWithValue("@p", tbNewPassword.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Password Successfully Changed");
                    // make the password variable equal to the new password
                    // so users can change it different times in the same session.
                    password = tbNewPassword.Text;
                    tbOldPassword.Text = "";
                    tbNewPassword.Text = "";
                    tbConfirmNew.Text = "";
                    lblCancel.Enabled = false;
                    connection.Close();
                }
            }
        }
        public void enableItems(GroupBox b)
        {
            // this method is to get all of the objects in a groupbox and enable them so I don't have 
            // to manually go in and change everything to enabled
            foreach(Control item in b.Controls)
            {
                item.Enabled = true;
            }
        }
        public void cancelBtn(GroupBox b)
        {
            // this method is for the cancel button. We we want to disable everything, but not 
            // get rid of the text the user has put in, just in case they change their minds, 
            // they don't have to retype everything.
            foreach (Control item in b.Controls)
            {
                item.Enabled = false;
            }
        }

        private void PersonalHealthForm_Load(object sender, EventArgs e)
        {
            // load the form
        }

        private void lblProfilePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.gif; *.bmp; *.png)|*.jpg; *.gif; *.bmp; *.png;)";
            if(open.ShowDialog() == DialogResult.OK)
            {
                // the file name is all weird because the spacing in the ID is a little wonky
                string newFileName = ID.Substring(0, ID.IndexOf(" ")) + open.FileName.Substring(open.FileName.Length - 4);
                // this is to delete any potential files with different extensions
                // I was actually having an issue with the deleting the files because 
                // I would get an error saying the file was still being used, so I used
                // this solution I found online to wait for the system to unlock the image so
                // it could be safely deleted.
                if (File.Exists(newFileName.Substring(0, newFileName.Length - 4) + ".jpg"))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(newFileName.Substring(0, newFileName.Length - 4) + ".jpg");
                }
                if (File.Exists(newFileName.Substring(0, newFileName.Length - 4) + ".gif"))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(newFileName.Substring(0, newFileName.Length - 4) + ".gif");
                }
                if (File.Exists(newFileName.Substring(0, newFileName.Length - 4) + ".bmp"))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(newFileName.Substring(0, newFileName.Length - 4) + ".bmp");
                }
                if (File.Exists(newFileName.Substring(0, newFileName.Length - 4) + ".png"))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(newFileName.Substring(0, newFileName.Length - 4) + ".png");
                }
                using (FileStream stream = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                {
                    pbProfilePicture.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
                MessageBox.Show(open.FileName);
                // this saves the file as its extension
                File.Copy(open.FileName, newFileName);
            }
        }
        private void gbTestResults_Enter(object sender, EventArgs e)
        {
            // Test results enter 
        }
        // Essentially, all of the edit buttons do the same thing, they enable the
        // controls of a specific list box, and disable themselves so while you're editing 
        // you can't keep spamming the edit button. I don't think that would
        // break the code, but it's more for user friendliness.
        private void lblEdit1_Click(object sender, EventArgs e)
        {
            enableItems(gbPersonal);
            tbID.Enabled = false;
            lblEdit1.Enabled = false;
        }
        private void lblEdit2_Click(object sender, EventArgs e)
        {
            enableItems(gbContactDetails);
            lblEdit2.Enabled = false;
        }
        private void lblEdit3_Click(object sender, EventArgs e)
        {
            enableItems(gbEmergencyContact);
            lblEdit3.Enabled = false;
        }
        private void lblEdit4_Click(object sender, EventArgs e)
        {
            enableItems(gbPrimaryCare);
            lblEdit4.Enabled = false;
        }
        private void lblEdit5_Click(object sender, EventArgs e)
        {
            enableItems(gbHealthInsurance);
            lblEdit5.Enabled = false;
        }

        private void lblEdit6_Click(object sender, EventArgs e)
        {
            enableItems(gbPersonalMedicalDetails);
            lblEdit6.Enabled = false;
        }

        private void lblEdit7_Click(object sender, EventArgs e)
        {
            enableItems(gbAllergyDetails);
            lblEdit7.Enabled = false;
            lblSave7.Enabled = false;
        }

        private void lblEdit8_Click(object sender, EventArgs e)
        {
            enableItems(gbImmunizationDetails);
            lblEdit8.Enabled = false;
        }

        private void lblEdit9_Click(object sender, EventArgs e)
        {
            enableItems(gbPerscribedMedication);
            lblEdit9.Enabled = false;
        }

        private void lblEdit10_Click(object sender, EventArgs e)
        {
            enableItems(gbTestResults);
            lblEdit10.Enabled = false;
        }

        private void lblEdit11_Click(object sender, EventArgs e)
        {
            enableItems(gbMedicalCondition);
            lblEdit11.Enabled = false;
        }

        private void lblEdit12_Click(object sender, EventArgs e)
        {
            enableItems(gbMedicalProcedure);
            lblEdit12.Enabled = false;
        }
        // These buttons use the cancel method that I explained earlier. They disable everything in a group box
        // and set the items of a listbox (if applicable) back to their defaults.
        private void lblCancel1_Click(object sender, EventArgs e)
        {
            cancelBtn(gbPersonal);
            lblEdit1.Enabled = true;
            updateData();
        }
        private void lblCancel2_Click(object sender, EventArgs e)
        {
            cancelBtn(gbContactDetails);
            lblEdit2.Enabled = true;
            updateData();
        }
        private void lblCancel3_Click(object sender, EventArgs e)
        {
            cancelBtn(gbEmergencyContact);
            lblEdit3.Enabled = true;
            updateData();
        }
        private void lblCancel4_Click(object sender, EventArgs e)
        {
            cancelBtn(gbPrimaryCare);
            lblEdit4.Enabled = true;
            updateData();
        }
        private void lblCancel5_Click(object sender, EventArgs e)
        {
            cancelBtn(gbHealthInsurance);
            lblEdit5.Enabled = true;
            updateData();
        }
        private void lblCancel6_Click(object sender, EventArgs e)
        {
            cancelBtn(gbPersonalMedicalDetails);
            lblEdit6.Enabled = true;
            updateData();
        }
        private void lblCancel7_Click(object sender, EventArgs e)
        {
            cancelBtn(gbAllergyDetails);
            lblEdit7.Enabled = true;
            lbAllergies.Enabled = true;
            updateData();
        }
        private void lblCancel8_Click(object sender, EventArgs e)
        {
            cancelBtn(gbImmunizationDetails);
            lblEdit8.Enabled = true;
            lbImmunizations.Enabled = true;
            updateData();
        }
        private void lblCancel9_Click(object sender, EventArgs e)
        {
            cancelBtn(gbPerscribedMedication);
            lblEdit9.Enabled = true;
            lbMedication.Enabled = true;
            updateData();
        }
        private void lblCancel10_Click(object sender, EventArgs e)
        {
            cancelBtn(gbTestResults);
            lblEdit10.Enabled = true;
            lbTestResults.Enabled = true;
            updateData();
        }
        private void lblCancel11_Click(object sender, EventArgs e)
        {
            cancelBtn(gbMedicalCondition);
            lblEdit11.Enabled = true;
            lbMedicalConditions.Enabled = true;
            updateData();
        }
        private void lblCancel12_Click(object sender, EventArgs e)
        {
            cancelBtn(gbMedicalProcedure);
            lblEdit12.Enabled = true;
            lbMedicalProcedures.Enabled = true;
            updateData();
        }
        // Here we have to use SQL commands to update any changes the user makes to the database.
        // things here are pretty cut and dry, just update the Database, however, when you get to
        // the 7th save button, you have to start inserting records into the table, because 
        // each user can have multiple records per table.
        private void lblSave1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string com = "UPDATE PATIENT_TBL" +
                " SET TITLE = @t, INITIALS = @i, LAST_NAME = @l, FIRST_NAME = @f, DATE_Of_BIRTH = @d" +
                " WHERE (PATIENT_ID = " + ID + ")";
            DateTime d;
            DateTime.TryParse(dtpDOB.Text, out d);
            SqlCommand command = new SqlCommand(com, connection);
            command.Parameters.AddWithValue("@t", cbTitle.Text);
            command.Parameters.AddWithValue("@i", tbInitials.Text);
            command.Parameters.AddWithValue("@l", tbLastName.Text);
            command.Parameters.AddWithValue("@f", tbFirstName.Text);
            command.Parameters.AddWithValue("@d", d);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbPersonal);
            lblEdit1.Enabled = true;
        }
        // this is just a cancel button I forgot to double click on and didn't want to 
        // risk moving
        private void lblCancel_Click(object sender, EventArgs e)
        {
            lblChangePassword.Enabled = false;
            lblCancel.Enabled = false;
            tbOldPassword.Text = "";
            tbNewPassword.Text = "";
            tbConfirmNew.Text = "";
        }

        private void lblSave2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string com = "UPDATE PATIENT_TBL" +
                " SET ADDRESS_STREET = @as, ADDRESS_STATE = @ast, ADDRESS_CITY = @ac, ADDRESS_ZIP = @az, PHONE_HOME = @ph" +
                ", PHONE_MOBILE = @pm, PHONE_WORK = @pw, FAX = @f, EMAIL = @e" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand command = new SqlCommand(com, connection);
            // have to account for when the user inputs no value for address because of the string index use
            if (!(tbmyAddress.Text == ""))
            {
                if (tbmyAddress.Text.Contains(','))
                    command.Parameters.AddWithValue("@as", tbmyAddress.Text.Substring(0, tbmyAddress.Text.IndexOf(",")));
                else
                    command.Parameters.AddWithValue("@sa", tbmyAddress.Text);
            }
            else
            {
                command.Parameters.AddWithValue("@as", DBNull.Value);
            }
            if (tbmyAddress.Text.Contains(','))
            {
                command.Parameters.AddWithValue("@ast", tbmyAddress.Text.Substring(tbmyAddress.Text.IndexOf(",") + 1));
            }
            else
            {
                command.Parameters.AddWithValue("@ast", DBNull.Value);
            }
            command.Parameters.AddWithValue("@ac", tbMyCity.Text);
            command.Parameters.AddWithValue("@az", tbMyPostalCode.Text);
            command.Parameters.AddWithValue("@ph", tbMyHomeTelephone.Text);
            command.Parameters.AddWithValue("@pm", tbMyMobileTelephone.Text);
            command.Parameters.AddWithValue("@pw", tbMyWorkTelephone.Text);
            command.Parameters.AddWithValue("@f", tbMyFaxNumber.Text);
            command.Parameters.AddWithValue("@e", tbMyEmail.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbContactDetails);
            lblEdit2.Enabled = true;
        }

        private void lblSave3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string com = "UPDATE PATIENT_TBL" +
                " SET EMERGENCY_CONTACT = @a, EMERGENCY_RELATIONSHIP = @b, EMERGENCY_STREET = @c, EMERGENCY_STATE = @d" +
                ", EMERGENCY_CITY = @e, EMERGENCY_HOME = @f, EMERGENCY_WORK = @g, EMERGENCY_MOBILE = @h, " +
                "EMERGENCY_FAX = @i, EMERGENCY_EMAIL = @j" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand command = new SqlCommand(com, connection);
            command.Parameters.AddWithValue("@a", tbKin.Text);
            command.Parameters.AddWithValue("@b", tbRelationship.Text);
            // once again, we have to account for null values
            if (!(tbKinAddress.Text == ""))
            {
                if (tbKinAddress.Text.Contains(','))
                    command.Parameters.AddWithValue("@c", tbKinAddress.Text.Substring(0, tbKinAddress.Text.IndexOf(",")));
                else
                    command.Parameters.AddWithValue("@c", tbKinAddress.Text);
            }
            else
            {
                command.Parameters.AddWithValue("@c", DBNull.Value);
            }
            if (tbKinAddress.Text.Contains(','))
            {
                command.Parameters.AddWithValue("@d", tbKinAddress.Text.Substring(tbKinAddress.Text.IndexOf(",") + 1));
            }
            else
            {
                command.Parameters.AddWithValue("@d", DBNull.Value);
            }
            command.Parameters.AddWithValue("@e", tbKinCity.Text);
            command.Parameters.AddWithValue("@f", tbKinHomeTelephone.Text);
            command.Parameters.AddWithValue("@g", tbKinWorkTelephone.Text);
            command.Parameters.AddWithValue("@h", tbKinMobileTelephone.Text);
            command.Parameters.AddWithValue("@i", tbKinFaxNumber.Text);
            command.Parameters.AddWithValue("@j", tbKinEmail.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbEmergencyContact);
            lblEdit3.Enabled = true;
        }

        private void lblSave4_Click(object sender, EventArgs e)
        {
            // this code makes sure if the user enters a first name, they must enter a last name
            if (!(tbPrimaryName.Text.Contains(' ')) && !(tbPrimaryName.Text == ""))
            {
                MessageBox.Show("Cannot have empty last name");
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                // we need to create a new instance of primary care provider in the table
                // so we need to test to see if the user is actually in the table
                PRIMARY_CARE_TBLTableAdapter adapt = new PRIMARY_CARE_TBLTableAdapter();
                if(adapt.findDupe(ID) == null)
                {
                    adapt.Insert(ID, tbPrimaryName.Text.Substring(0, tbPrimaryName.Text.IndexOf(" ")),
                        tbPrimaryName.Text.Substring(tbPrimaryName.Text.IndexOf(" ") + 1), null, tbPrimarySpecialty.Text,
                        primaryWork.Text, tbPrimaryMobileTelephone.Text, null);
                }

                string com = "UPDATE PRIMARY_CARE_TBL" +
                    " SET NAME_LAST = @a, NAME_FISRT = @b, SPECIALTY = @c, PHONE_MOBILE = @d" +
                    ", PHONE_OFFICE = @e, FAX = @f, EMAIL = @g" +
                    " WHERE (PRIMARY_ID = " + ID + ")";
                SqlCommand command = new SqlCommand(com, connection);
                if (!(tbPrimaryName.Text == ""))
                {
                    command.Parameters.AddWithValue("@a", tbPrimaryName.Text.Substring(0, tbPrimaryName.Text.IndexOf(" ")));
                }
                else
                {
                    command.Parameters.AddWithValue("@a", "");
                }
                command.Parameters.AddWithValue("@b", tbPrimaryName.Text.Substring(tbPrimaryName.Text.IndexOf(" ") + 1));
                command.Parameters.AddWithValue("@c", tbPrimarySpecialty.Text);
                command.Parameters.AddWithValue("@d", tbPrimaryMobileTelephone.Text);
                command.Parameters.AddWithValue("@e", primaryWork.Text);
                command.Parameters.AddWithValue("@f", primaryFax.Text);
                command.Parameters.AddWithValue("@g", primeEmail.Text);
                command.ExecuteNonQuery();
                connection.Close();
                cancelBtn(gbPrimaryCare);
                lblEdit4.Enabled = true;
            }
        }
        private void lblSave5_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string com = "UPDATE PATIENT_TBL" +
                " SET [PLAN] = @b, INSURER = @a, INSURANCE_NUMBER = @c" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand command = new SqlCommand(com, connection);
            command.Parameters.AddWithValue("@a", tbInsurer.Text);
            command.Parameters.AddWithValue("@b", tbInsurancePlan.Text);
            command.Parameters.AddWithValue("@c", tbInsuranceNumber.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbHealthInsurance);
            lblEdit5.Enabled = true;
        }

        private void lblSave6_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string com = "UPDATE PER_DETAILS_TBL" +
                " SET BLOOD_TYPE = @a, ORGAN_DONOR = @b, HIV_STATUS = @c" +
                ", HEIGHT_INCHES = @d, WEIGHT_LBS = @e" +
                " WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand command = new SqlCommand(com, connection);
            command.Parameters.AddWithValue("@a", cbBloodType.Text);
            command.Parameters.AddWithValue("@b", cbOrganDonor.Checked);
            if(rbtnPositive.Checked)
            {
                command.Parameters.AddWithValue("@c", rbtnNegative.Checked);
            }
            else if(rbtnNegative.Checked)
            {
                command.Parameters.AddWithValue("@c", rbtnNegative.Checked);
            }
            else
            {
                command.Parameters.AddWithValue("@c", DBNull.Value);
            }
            command.Parameters.AddWithValue("@d", tbHeight.Text);
            command.Parameters.AddWithValue("@e", tbWeight.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbPersonalMedicalDetails);
            lblEdit6.Enabled = true;
        }
        // SO for Saves 7 and beyond, we had to create records in the table for each users allergy/medicine/etc.
        // In order to make sure the ID for each table (I.E. Allergy_ID) was unique, we counted how many records
        // were in the table with the User's ID, then we used that number to dictate what the new ID should be
        // (we added 1 to it). We then formatted it correctly, and added it to the table.
        private void lblSave7_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            // SO for these graphics, it is going to get very interesting, we need to find out which value
            // should be recorded for allergenID
            string checkCom = "SELECT ALLERGY_ID" +
                " FROM ALLERGY_TBL WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand checkCommand = new SqlCommand(checkCom, connection);
            SqlDataReader reader = checkCommand.ExecuteReader(CommandBehavior.CloseConnection);
            int idNum = 0;
            while(reader.Read())
            {
                idNum++;   
            }
            // now we need to us the idNum variable to increment and create a valid AllergyID
            string zeros = "000000";
            zeros = zeros.Substring(0, zeros.Length - (idNum.ToString().Length));
            string tempID = zeros + (idNum + 1).ToString();
            connection.Close();

            connection.Open();
            string com = "INSERT INTO ALLERGY_TBL (PATIENT_ID, ALLERGY_ID, ALLERGEN, ONSET_DATE, NOTE)" +
                " VALUES (@pid, @aid, @a, @b, @c)";
            SqlCommand command = new SqlCommand(com, connection);
            DateTime date;
            DateTime.TryParse(dtpOnset.Text, out date);
            command.Parameters.AddWithValue("@pid", ID);
            command.Parameters.AddWithValue("@aid", tempID);
            command.Parameters.AddWithValue("@a", tbAllergy.Text);
            command.Parameters.AddWithValue("@b", date);
            command.Parameters.AddWithValue("@c", tbAllergyNote.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbAllergyDetails);
            lbAllergies.Enabled = true;
            lblEdit7.Enabled = true;
            tbAllergy.Text = "";
            tbAllergyNote.Text = "";
            updateData();
        }

        private void lblSave8_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string checkCom = "SELECT IMMUNIZATION_ID" +
                " FROM IMMUNIZATION_TBL WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand checkCommand = new SqlCommand(checkCom, connection);
            SqlDataReader reader = checkCommand.ExecuteReader(CommandBehavior.CloseConnection);
            int idNum = 0;
            while (reader.Read())
            {
                idNum++;
            }
            string zeros = "000000";
            zeros = zeros.Substring(0, zeros.Length - (idNum.ToString().Length));
            string tempID = zeros + (idNum + 1).ToString();
            connection.Close();

            connection.Open();
            string com = "INSERT INTO IMMUNIZATION_TBL (PATIENT_ID, IMMUNIZATION_ID, IMMUNIZATION, DATE, NOTE)" +
                " VALUES (@pid, @iid, @a, @b, @c)";
            SqlCommand command = new SqlCommand(com, connection);
            DateTime date;
            DateTime.TryParse(dtpImmuneDate.Text, out date);
            command.Parameters.AddWithValue("@pid", ID);
            command.Parameters.AddWithValue("@iid", tempID);
            command.Parameters.AddWithValue("@a", tbImmunization.Text);
            command.Parameters.AddWithValue("@b", date);
            command.Parameters.AddWithValue("@c", tbImmunizationNote.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbImmunizationDetails);
            lbImmunizations.Enabled = true;
            lblEdit8.Enabled = true;
            tbImmunization.Text = "";
            tbImmunizationNote.Text = "";
            updateData();
        }

        private void lblSave9_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string checkCom = "SELECT MED_ID" +
                " FROM MEDICATION_TBL WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand checkCommand = new SqlCommand(checkCom, connection);
            SqlDataReader reader = checkCommand.ExecuteReader(CommandBehavior.CloseConnection);
            int idNum = 0;
            while (reader.Read())
            {
                idNum++;
            }
            string zeros = "000000";
            zeros = zeros.Substring(0, zeros.Length - (idNum.ToString().Length));
            string tempID = zeros + (idNum + 1).ToString();
            connection.Close();

            connection.Open();
            string com = "INSERT INTO MEDICATION_TBL (PATIENT_ID, MED_ID, MEDICATION, DATE, CHRONIC, NOTE)" +
                " VALUES (@pid, @iid, @a, @b, @c, @d)";
            SqlCommand command = new SqlCommand(com, connection);
            DateTime date;
            DateTime.TryParse(dtpPerscribed.Text, out date);
            command.Parameters.AddWithValue("@pid", ID);
            command.Parameters.AddWithValue("@iid", tempID);
            command.Parameters.AddWithValue("@a", tbMedication.Text);
            command.Parameters.AddWithValue("@b", date);
            command.Parameters.AddWithValue("@c", cbChronic.Checked);
            command.Parameters.AddWithValue("@d", tbMedicationNote.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbPerscribedMedication);
            lbMedication.Enabled = true;
            lblEdit9.Enabled = true;
            tbMedication.Text = "";
            tbMedicationNote.Text = "";
            updateData();
        }

        private void lblSave10_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string checkCom = "SELECT TEST_ID" +
                " FROM TEST_TBL WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand checkCommand = new SqlCommand(checkCom, connection);
            SqlDataReader reader = checkCommand.ExecuteReader(CommandBehavior.CloseConnection);
            int idNum = 0;
            while (reader.Read())
            {
                idNum++;
            }
            string zeros = "000000";
            zeros = zeros.Substring(0, zeros.Length - (idNum.ToString().Length));
            string tempID = zeros + (idNum + 1).ToString();
            connection.Close();

            connection.Open();
            string com = "INSERT INTO TEST_TBL (PATIENT_ID, TEST_ID, TEST, RESULT, DATE, NOTE)" +
                " VALUES (@pid, @tid, @a, @b, @c, @d)";
            SqlCommand command = new SqlCommand(com, connection);
            DateTime date;
            DateTime.TryParse(dtpTest.Text, out date);
            command.Parameters.AddWithValue("@pid", ID);
            command.Parameters.AddWithValue("@tid", tempID);
            command.Parameters.AddWithValue("@a", tbTest.Text);
            command.Parameters.AddWithValue("@b", tbResult.Text);
            command.Parameters.AddWithValue("@c", date);
            command.Parameters.AddWithValue("@d", tbTestNote.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbTestResults);
            lbTestResults.Enabled = true;
            lblEdit10.Enabled = true;
            tbTest.Text = "";
            tbResult.Text = "";
            tbTestNote.Text = ""; ;
            updateData();
        }

        private void lblSave11_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string checkCom = "SELECT CONDITION_ID" +
                " FROM CONDITION WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand checkCommand = new SqlCommand(checkCom, connection);
            SqlDataReader reader = checkCommand.ExecuteReader(CommandBehavior.CloseConnection);
            int idNum = 0;
            while (reader.Read())
            {
                idNum++;
            }
            string zeros = "000000";
            zeros = zeros.Substring(0, zeros.Length - (idNum.ToString().Length));
            string tempID = zeros + (idNum + 1).ToString();
            connection.Close();

            connection.Open();
            string com = "INSERT INTO CONDITION (PATIENT_ID, CONDITION_ID, CONDITION, ONSET_DATE, ACUTE, CHRONIC, NOTE)" +
                " VALUES (@pid, @cid, @a, @b, @c, @d, @e)";
            SqlCommand command = new SqlCommand(com, connection);
            DateTime date;
            DateTime.TryParse(dtpOnset2.Text, out date);
            command.Parameters.AddWithValue("@pid", ID);
            command.Parameters.AddWithValue("@cid", tempID);
            command.Parameters.AddWithValue("@a", tbCondition.Text);
            command.Parameters.AddWithValue("@b", date);
            command.Parameters.AddWithValue("@c", rbtnAcute.Checked);
            command.Parameters.AddWithValue("@d", rbtnChronic.Checked);
            command.Parameters.AddWithValue("@e", tbConditionNote.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbMedicalCondition);
            lbMedicalConditions.Enabled = true;
            lblEdit11.Enabled = true;
            tbCondition.Text = "";
            tbConditionNote.Text = "";
            updateData();
        }

        private void lblSave12_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string checkCom = "SELECT PROCEDURE_ID" +
                " FROM MED_PROC_TBL WHERE (PATIENT_ID = " + ID + ")";
            SqlCommand checkCommand = new SqlCommand(checkCom, connection);
            SqlDataReader reader = checkCommand.ExecuteReader(CommandBehavior.CloseConnection);
            int idNum = 0;
            while (reader.Read())
            {
                idNum++;
            }
            string zeros = "000000";
            zeros = zeros.Substring(0, zeros.Length - (idNum.ToString().Length));
            string tempID = zeros + (idNum + 1).ToString();
            connection.Close();

            connection.Open();
            string com = "INSERT INTO MED_PROC_TBL (PATIENT_ID, PROCEDURE_ID, MED_PROCEDURE, DATE, DOCTOR, NOTE)" +
                " VALUES (@pid, @cid, @a, @b, @c, @d)";
            SqlCommand command = new SqlCommand(com, connection);
            DateTime date;
            DateTime.TryParse(dtpProcedure.Text, out date);
            command.Parameters.AddWithValue("@pid", ID);
            command.Parameters.AddWithValue("@cid", tempID);
            command.Parameters.AddWithValue("@a", tbProcedure.Text);
            command.Parameters.AddWithValue("@b", date);
            command.Parameters.AddWithValue("@c", tbPreformedBy.Text);
            command.Parameters.AddWithValue("@d", tbProcedureNote.Text);
            command.ExecuteNonQuery();
            connection.Close();
            cancelBtn(gbMedicalProcedure);
            lbMedicalProcedures.Enabled = true;
            lblEdit12.Enabled = true;
            tbProcedure.Text = "";
            tbPreformedBy.Text = "";
            tbProcedureNote.Text = "";
            updateData();
        }
        // for the add buttons, we are simply going to add the data the user inputted to the table in
        // a rough format. This is so the user can look at what they entered to see if there is anything 
        // blatantly wrong with it. Then of course they can save it, which formats it correctly and
        // adds it to the database.
        private void lblAdd1_Click(object sender, EventArgs e)
        {
            lbAllergies.Items.Add(tbAllergy.Text + " , " + dtpOnset.Text + " , " + tbAllergyNote.Text);
            lblSave7.Enabled = true;
        }

        private void lblAdd2_Click(object sender, EventArgs e)
        {
            lbImmunizations.Items.Add(tbImmunization.Text + " , " + dtpImmuneDate.Text);
            lblSave8.Enabled = true;
        }

        private void lblAdd3_Click(object sender, EventArgs e)
        {
            lbMedication.Items.Add(tbMedication.Text + " , " + dtpPerscribed.Text);
            lblSave9.Enabled = true;
        }

        private void lblAdd4_Click(object sender, EventArgs e)
        {
            lbTestResults.Items.Add(tbTest.Text + " , " + tbResult.Text + " , " + dtpTest.Text);
            lblSave10.Enabled = true;
        }

        private void lblAdd5_Click(object sender, EventArgs e)
        {
            lbMedicalConditions.Items.Add(tbCondition.Text + " , " + dtpOnset2.Text);
            lblSave11.Enabled = true;
        }

        private void lblAdd6_Click(object sender, EventArgs e)
        {
            lbMedicalProcedures.Items.Add(tbProcedure.Text + " , " + dtpProcedure.Text + " , " + tbPreformedBy.Text);
            lblSave12.Enabled = true;
        }
        // This is where stuff really get's interesting. I was at a loss when I was trying to figure out how
        // to properly delete items from the database. I knew it was going to be connected to each of the tables' unique 
        // ID's, but the issue goes beyond that. Basically, I added the unique ID to the end of each entry on the 
        // relavent list box, then I used that ID to find out which entry to delete from the database. You may think we're all done,
        // but there's another layer. Adding stuff back to the table becomes an issue. This is because if you have three
        // items with ID's "000001", "000002" and "000003" respectively, and you deleted "000002", based on how I
        // structured my program, the next item that would be added would have an ID of "000003" because now there were 3
        // total entries, which is obviously a problem. So I heavily manipulated strings, used some SQL code I wasn't 
        // even aware existed, and managed to update every record in the table to fall back in numerica order.
        private void lblRemove1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (lbAllergies.Text.Length > 7 && lbAllergies.Text.Contains("00000"))
            {
                int indexDeleted = Int32.Parse(lbAllergies.Text.Substring(lbAllergies.Text.Length - 7));
                string com = "DELETE FROM ALLERGY_TBL" +
                    " WHERE (PATIENT_ID = " + ID + " AND ALLERGY_ID = " + lbAllergies.Text.Substring(lbAllergies.Text.Length - 7) + ")";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                updateData();
                connection.Close();
                // we know how many elements are in the table, because we can count the number of records in the list box
                connection.Open();
                int num = lbAllergies.Items.Count;
                if (num != 0)
                {
                    string fix = "DECLARE @a INT = " + indexDeleted +
                        " WHILE @a <= " + (num + 1) +
                        " BEGIN " +
                        " UPDATE ALLERGY_TBL" +
                        " SET ALLERGY_ID = RIGHT(CONCAT(" + "'0000000000000'" + " ,  @a), 6)" +
                        " WHERE (PATIENT_ID = " + ID + " AND CAST(ALLERGY_ID AS INT) > @a AND CAST(ALLERGY_ID AS INT) < @a + 2)" +
                        " SET @a = @a + 1" +
                        " END;";
                    SqlCommand cmd = new SqlCommand(fix, connection);
                    cmd.ExecuteNonQuery();
                    updateData();
                    connection.Close();
                }
            }
        }

        private void lblRemove2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (lbImmunizations.Text.Length > 7 && lbImmunizations.Text.Contains("00000"))
            {
                int indexDeleted = Int32.Parse(lbImmunizations.Text.Substring(lbImmunizations.Text.Length - 7));
                string com = "DELETE FROM IMMUNIZATION_TBL" +
                    " WHERE (PATIENT_ID = " + ID + " AND IMMUNIZATION_ID = " + lbImmunizations.Text.Substring(lbImmunizations.Text.Length - 7) + ")";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                updateData();
                connection.Close();
                connection.Open();
                int num = lbImmunizations.Items.Count;
                if (num != 0)
                {
                    string fix = "DECLARE @a INT = " + indexDeleted +
                        " WHILE @a <= " + (num + 1) +
                        " BEGIN " +
                        " UPDATE IMMUNIZATION_TBL" +
                        " SET IMMUNIZATION_ID = RIGHT(CONCAT(" + "'0000000000000'" + " ,  @a), 6)" +
                        " WHERE (PATIENT_ID = " + ID + " AND CAST(IMMUNIZATION_ID AS INT) > @a AND CAST(IMMUNIZATION_ID AS INT) < @a + 2)" +
                        " SET @a = @a + 1" +
                        " END;";
                    SqlCommand cmd = new SqlCommand(fix, connection);
                    cmd.ExecuteNonQuery();
                    updateData();
                    connection.Close();
                }
            }
        }

        private void lblRemove3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (lbMedication.Text.Length > 7 && lbMedication.Text.Contains("00000"))
            {
                int indexDeleted = Int32.Parse(lbMedication.Text.Substring(lbMedication.Text.Length - 7));
                string com = "DELETE FROM MEDICATION_TBL" +
                    " WHERE (PATIENT_ID = " + ID + " AND MED_ID = " + lbMedication.Text.Substring(lbMedication.Text.Length - 7) + ")";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                updateData();
                connection.Close();
                connection.Open();
                int num = lbMedication.Items.Count;
                if (num != 0)
                {
                    string fix = "DECLARE @a INT = " + indexDeleted +
                        " WHILE @a <= " + (num + 1) +
                        " BEGIN " +
                        " UPDATE MEDICATION_TBL" +
                        " SET MED_ID = RIGHT(CONCAT(" + "'0000000000000'" + " ,  @a), 6)" +
                        " WHERE (PATIENT_ID = " + ID + " AND CAST(MED_ID AS INT) > @a AND CAST(MED_ID AS INT) < @a + 2)" +
                        " SET @a = @a + 1" +
                        " END;";
                    SqlCommand cmd = new SqlCommand(fix, connection);
                    cmd.ExecuteNonQuery();
                    updateData();
                    connection.Close();
                }
            }
        }

        private void lblRemove4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (lbTestResults.Text.Length > 7 && lbTestResults.Text.Contains("00000"))
            {
                int indexDeleted = Int32.Parse(lbTestResults.Text.Substring(lbTestResults.Text.Length - 7));
                string com = "DELETE FROM TEST_TBL" +
                    " WHERE (PATIENT_ID = " + ID + " AND TEST_ID = " + lbTestResults.Text.Substring(lbTestResults.Text.Length - 7) + ")";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                updateData();
                connection.Close();
                connection.Open();
                int num = lbTestResults.Items.Count;
                if (num != 0)
                {
                    string fix = "DECLARE @a INT = " + indexDeleted +
                        " WHILE @a <= " + (num + 1) +
                        " BEGIN " +
                        " UPDATE TEST_TBL" +
                        " SET TEST_ID = RIGHT(CONCAT(" + "'0000000000000'" + " ,  @a), 6)" +
                        " WHERE (PATIENT_ID = " + ID + " AND CAST(TEST_ID AS INT) > @a AND CAST(TEst_ID AS INT) < @a + 2)" +
                        " SET @a = @a + 1" +
                        " END;";
                    SqlCommand cmd = new SqlCommand(fix, connection);
                    cmd.ExecuteNonQuery();
                    updateData();
                    connection.Close();
                }
            }
        }

        private void lblRemove5_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (lbMedicalConditions.Text.Length > 7 && lbMedicalConditions.Text.Contains("00000"))
            {
                int indexDeleted = Int32.Parse(lbMedicalConditions.Text.Substring(lbMedicalConditions.Text.Length - 7));
                string com = "DELETE FROM CONDITION" +
                    " WHERE (PATIENT_ID = " + ID + " AND CONDITION_ID = " + lbMedicalConditions.Text.Substring(lbMedicalConditions.Text.Length - 7) + ")";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                updateData();
                connection.Close();
                connection.Open();
                int num = lbMedicalConditions.Items.Count;
                if (num != 0)
                {
                    string fix = "DECLARE @a INT = " + indexDeleted +
                        " WHILE @a <= " + (num + 1) +
                        " BEGIN " +
                        " UPDATE CONDITION" +
                        " SET CONDITION_ID = RIGHT(CONCAT(" + "'0000000000000'" + " ,  @a), 6)" +
                        " WHERE (PATIENT_ID = " + ID + " AND CAST(CONDITION_ID AS INT) > @a AND CAST(CONDITION_ID AS INT) < @a + 2)" +
                        " SET @a = @a + 1" +
                        " END;";
                    SqlCommand cmd = new SqlCommand(fix, connection);
                    cmd.ExecuteNonQuery();
                    updateData();
                    connection.Close();
                }
            }
        }

        private void lblRemove6_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (lbMedicalProcedures.Text.Length > 7 && lbMedicalProcedures.Text.Contains("00000"))
            {
                int indexDeleted = Int32.Parse(lbMedicalProcedures.Text.Substring(lbMedicalProcedures.Text.Length - 7));
                string com = "DELETE FROM MED_PROC_TBL" +
                    " WHERE (PATIENT_ID = " + ID + " AND PROCEDURE_ID = " + lbMedicalProcedures.Text.Substring(lbMedicalProcedures.Text.Length - 7) + ")";
                SqlCommand command = new SqlCommand(com, connection);
                command.ExecuteNonQuery();
                updateData();
                connection.Close();
                connection.Open();
                int num = lbMedicalProcedures.Items.Count;
                if (num != 0)
                {
                    string fix = "DECLARE @a INT = " + indexDeleted +
                        " WHILE @a <= " + (num + 1) +
                        " BEGIN " +
                        " UPDATE MED_PROC_TBL" +
                        " SET PROCEDURE_ID = RIGHT(CONCAT(" + "'0000000000000'" + " ,  @a), 6)" +
                        " WHERE (PATIENT_ID = " + ID + " AND CAST(PROCEDURE_ID AS INT) > @a AND CAST(PROCEDURE_ID AS INT) < @a + 2)" +
                        " SET @a = @a + 1" +
                        " END;";
                    SqlCommand cmd = new SqlCommand(fix, connection);
                    cmd.ExecuteNonQuery();
                    updateData();
                    connection.Close();
                }
            }
        }
        // This updates the form to allow the remove label to be enabled after an item is 
        // clicked in the relevant list box.
        private void lbAllergies_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRemove1.Enabled = true;
        }

        private void lbImmunizations_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRemove2.Enabled = true;
        }

        private void lbMedication_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRemove3.Enabled = true;
        }

        private void lbTestResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRemove4.Enabled = true;
        }

        private void lbMedicalConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRemove5.Enabled = true;
        }

        private void lbMedicalProcedures_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRemove6.Enabled = true;
        }
        // this method was created to find a valid number for an ID based on how many characters the number was
        // for example, if I just add five zeros to the beginning of a number, eventually you'll end up
        // at 0000010, which is not in correct format, and would cause a BUNCH of issues. 
        public string zeros(string x, int y)
        {
            return x.Substring(0, x.Length - (y.ToString().Length)) + y;
        }
    }
}
