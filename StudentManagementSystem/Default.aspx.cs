using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class _Default : System.Web.UI.Page
{
  private DatabaseHelper dbHelper = new DatabaseHelper();

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      LoadStudents();
    }
  }

  private void LoadStudents()
  {
    try
    {
      string query = "SELECT * FROM Students WHERE Active = TRUE";
      DataTable dt = dbHelper.ExecuteQuery(query);

      gvStudents.DataSource = dt;
      gvStudents.DataBind();
    }
    catch (Exception ex)
    {
      lblMessage.Text = "Error loading students: " + ex.Message;
      lblMessage.CssClass = "text-danger";
    }
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    if (Page.IsValid)
    {
      try
      {
        string query = "INSERT INTO Students (FirstName, LastName, Email, Program, DateOfBirth, EnrollmentDate, Active) " +
                       "VALUES (@FirstName, @LastName, @Email, @Program, @DateOfBirth, @EnrollmentDate, @Active)";

        MySqlParameter[] parameters = new MySqlParameter[]
        {
                    new MySqlParameter("@FirstName", txtFirstName.Text),
                    new MySqlParameter("@LastName", txtLastName.Text),
                    new MySqlParameter("@Email", txtEmail.Text),
                    new MySqlParameter("@Program", ddlProgram.SelectedValue),
                    new MySqlParameter("@DateOfBirth", DateTime.Now), // In a real app, get from a date picker
                    new MySqlParameter("@EnrollmentDate", DateTime.Now),
                    new MySqlParameter("@Active", true)
        };

        int result = dbHelper.ExecuteNonQuery(query, parameters);

        if (result > 0)
        {
          lblMessage.Text = "Student added successfully!";
          lblMessage.CssClass = "text-success";
          ClearForm();
          LoadStudents();
        }
        else
        {
          lblMessage.Text = "Failed to add student.";
          lblMessage.CssClass = "text-danger";
        }
      }
      catch (Exception ex)
      {
        lblMessage.Text = "Error: " + ex.Message;
        lblMessage.CssClass = "text-danger";
      }
    }
  }

  private void ClearForm()
  {
    txtFirstName.Text = string.Empty;
    txtLastName.Text = string.Empty;
    txtEmail.Text = string.Empty;
    ddlProgram.SelectedIndex = 0;
  }
}