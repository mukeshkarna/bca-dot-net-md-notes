using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Students_List : Page
{
  private StudentRepository repository = new StudentRepository();

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      BindGridView();

      // Check for success message in session
      if (Session["SuccessMessage"] != null)
      {
        pnlMessage.Visible = true;
        litMessage.Text = Session["SuccessMessage"].ToString();
        Session["SuccessMessage"] = null;
      }
    }
  }

  private void BindGridView(string searchTerm = null)
  {
    List<Student> students;

    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
      // TODO: Implement search functionality in repository
      students = repository.GetAllStudents();
      // Filter here for simplicity
      students = students.FindAll(s =>
          s.FirstName.Contains(searchTerm) ||
          s.LastName.Contains(searchTerm) ||
          s.Email.Contains(searchTerm));

      pnlMessage.Visible = true;
      litMessage.Text = $"Found {students.Count} student(s) matching '{searchTerm}'";
    }
    else
    {
      students = repository.GetAllStudents();
      pnlMessage.Visible = false;
    }

    gvStudents.DataSource = students;
    gvStudents.DataBind();
  }

  protected void btnSearch_Click(object sender, EventArgs e)
  {
    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
    {
      BindGridView(txtSearch.Text);
    }
    else
    {
      BindGridView();
    }
  }

  protected void btnClear_Click(object sender, EventArgs e)
  {
    txtSearch.Text = string.Empty;
    BindGridView();
  }

  protected void btnAddNew_Click(object sender, EventArgs e)
  {
    Response.Redirect("~/Students/Add.aspx");
  }

  protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
    gvStudents.PageIndex = e.NewPageIndex;

    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
    {
      BindGridView(txtSearch.Text);
    }
    else
    {
      BindGridView();
    }
  }

  protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    if (e.CommandName == "ViewDetails")
    {
      int studentId = Convert.ToInt32(e.CommandArgument);
      Response.Redirect($"~/Students/Details.aspx?id={studentId}");
    }
    else if (e.CommandName == "EditItem")
    {
      int studentId = Convert.ToInt32(e.CommandArgument);
      Response.Redirect($"~/Students/Edit.aspx?id={studentId}");
    }
    else if (e.CommandName == "DeleteItem")
    {
      int studentId = Convert.ToInt32(e.CommandArgument);

      bool success = repository.DeleteStudent(studentId);

      if (success)
      {
        Session["SuccessMessage"] = "Student deleted successfully.";
      }
      else
      {
        Session["SuccessMessage"] = "Failed to delete student.";
      }

      Response.Redirect("~/Students/List.aspx");
    }
  }
}