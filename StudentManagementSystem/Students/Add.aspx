<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Students_Add" %>

  <!DOCTYPE html>
  <html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>Add Student</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
  </head>

  <body>
    <form id="form1" runat="server">
      <div class="container mt-4">
        <h1>Add New Student</h1>

        <div class="row">
          <div class="col-md-8">
            <div class="card">
              <div class="card-body">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                  HeaderText="Please correct the following errors:" CssClass="alert alert-danger" />

                <div class="mb-3 row">
                  <label for="txtFirstName" class="col-sm-3 col-form-label">First Name:</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                      CssClass="text-danger" ErrorMessage="First name is required." Display="Dynamic">*
                    </asp:RequiredFieldValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <label for="txtLastName" class="col-sm-3 col-form-label">Last Name:</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                      CssClass="text-danger" ErrorMessage="Last name is required." Display="Dynamic">*
                    </asp:RequiredFieldValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <label for="txtEmail" class="col-sm-3 col-form-label">Email:</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                      CssClass="text-danger" ErrorMessage="Email is required." Display="Dynamic">*
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                      CssClass="text-danger" ErrorMessage="Invalid email format."
                      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic">*
                    </asp:RegularExpressionValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <label for="txtDateOfBirth" class="col-sm-3 col-form-label">Date of Birth:</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" TextMode="Date">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                      CssClass="text-danger" ErrorMessage="Date of birth is required." Display="Dynamic">*
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                      CssClass="text-danger" ErrorMessage="Date of birth must be between 01/01/1980 and today."
                      Type="Date" Display="Dynamic">*</asp:RangeValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <label for="rblGender" class="col-sm-3 col-form-label">Gender:</label>
                  <div class="col-sm-9">
                    <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal"
                      CssClass="form-check form-check-inline">
                      <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                      <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                      <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rblGender"
                      CssClass="text-danger" ErrorMessage="Gender is required." Display="Dynamic">*
                    </asp:RequiredFieldValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <label for="ddlProgram" class="col-sm-3 col-form-label">Program:</label>
                  <div class="col-sm-9">
                    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-select">
                      <asp:ListItem Text="-- Select Program --" Value=""></asp:ListItem>
                      <asp:ListItem Text="Computer Science" Value="Computer Science"></asp:ListItem>
                      <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                      <asp:ListItem Text="Engineering" Value="Engineering"></asp:ListItem>
                      <asp:ListItem Text="Psychology" Value="Psychology"></asp:ListItem>
                      <asp:ListItem Text="Mathematics" Value="Mathematics"></asp:ListItem>
                      <asp:ListItem Text="Arts" Value="Arts"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlProgram"
                      CssClass="text-danger" ErrorMessage="Program is required." Display="Dynamic" InitialValue="">*
                    </asp:RequiredFieldValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <label for="txtEnrollmentDate" class="col-sm-3 col-form-label">Enrollment Date:</label>
                  <div class="col-sm-9">
                    <asp:TextBox ID="txtEnrollmentDate" runat="server" CssClass="form-control" TextMode="Date">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEnrollmentDate" runat="server"
                      ControlToValidate="txtEnrollmentDate" CssClass="text-danger"
                      ErrorMessage="Enrollment date is required." Display="Dynamic">*</asp:RequiredFieldValidator>
                  </div>
                </div>

                <div class="mb-3 row">
                  <div class="col-sm-9 offset-sm-3">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                      OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                      OnClick="btnCancel_Click" CausesValidation="false" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
  </body>

  </html>