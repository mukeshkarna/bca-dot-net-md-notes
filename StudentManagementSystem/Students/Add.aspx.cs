using System;
using System.Web.UI;

public partial class Students_Add : Page
{
  private StudentRepository repository = new StudentRepository();

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      // Set date range for date of birth validator
      DateTime minDate = new DateTime(1980, 1, 1);
      DateTime maxDate = DateTime.Today;
      rvDateOfBirth.MinimumValue = minDate.ToString("yyyy-MM-dd");
      rvDateOfBirth.MaximumValue = maxDate.ToString("yyyy-MM-dd");

      // Default enrollment date to today
      txtEnrollmentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
    }
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    if (Page.IsValid)
    {
      Student student = new Student
      {
        FirstName = txtFirstName.Text,
        LastName = txtLastName.Text,
        Email = txtEmail.Text,
        DateOfBirth = DateTime.Parse(txtDateOfBirth.Text),
        Gender = rblGender.SelectedValue,
        Program = ddlProgram.SelectedValue,
        EnrollmentDate = DateTime.Parse(txtEnrollmentDate.Text),
        Active = true
      };

      try
      {
        int studentId = repository.AddStudent(student);

        if (studentId > 0)
        {
          Session["SuccessMessage"] = "Student added successfully!";
          Response.Redirect("~/Students/List.aspx");
        }
        else
        {
          // Handle error
        }
      }
      catch (Exception ex)
      {
        // Log and display error
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    Response.Redirect("~/Students/List.aspx");
  }
}