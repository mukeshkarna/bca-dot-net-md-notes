<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

  <!DOCTYPE html>
  <html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>Student Management System</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
  </head>

  <body>
    <form id="form1" runat="server">
      <div class="container mt-4">
        <h1>Student Management System</h1>

        <div class="row mt-4">
          <div class="col-md-6">
            <div class="card">
              <div class="card-header">
                <h5>Student List</h5>
              </div>
              <div class="card-body">
                <asp:GridView ID="gvStudents" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                  DataKeyNames="StudentID">
                  <Columns>
                    <asp:BoundField DataField="StudentID" HeaderText="ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Program" HeaderText="Program" />
                  </Columns>
                </asp:GridView>
              </div>
            </div>
          </div>

          <div class="col-md-6">
            <div class="card">
              <div class="card-header">
                <h5>Add New Student</h5>
              </div>
              <div class="card-body">
                <div class="mb-3">
                  <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="txtFirstName">
                  </asp:Label>
                  <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    CssClass="text-danger" ErrorMessage="First name is required." Display="Dynamic">
                  </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                  <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName">
                  </asp:Label>
                  <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                    CssClass="text-danger" ErrorMessage="Last name is required." Display="Dynamic">
                  </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                  <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail"></asp:Label>
                  <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="Email is required." Display="Dynamic">
                  </asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="Invalid email format."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic">
                  </asp:RegularExpressionValidator>
                </div>

                <div class="mb-3">
                  <asp:Label ID="lblProgram" runat="server" Text="Program:" AssociatedControlID="ddlProgram">
                  </asp:Label>
                  <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-select">
                    <asp:ListItem Text="-- Select Program --" Value=""></asp:ListItem>
                    <asp:ListItem Text="Computer Science" Value="Computer Science"></asp:ListItem>
                    <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                    <asp:ListItem Text="Engineering" Value="Engineering"></asp:ListItem>
                    <asp:ListItem Text="Mathematics" Value="Mathematics"></asp:ListItem>
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlProgram"
                    CssClass="text-danger" ErrorMessage="Program is required." Display="Dynamic" InitialValue="">
                  </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                  <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                    OnClick="btnSave_Click" />
                </div>

                <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
  </body>

  </html>