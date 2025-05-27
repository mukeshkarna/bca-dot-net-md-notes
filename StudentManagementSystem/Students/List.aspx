<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Students_List" %>

  <!DOCTYPE html>
  <html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>Students List</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
  </head>

  <body>
    <form id="form1" runat="server">
      <div class="container mt-4">
        <h1>Students List</h1>

        <div class="row mb-3">
          <div class="col-md-6">
            <div class="input-group">
              <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                placeholder="Search by name or email..."></asp:TextBox>
              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-secondary"
                OnClick="btnSearch_Click" />
              <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary"
                OnClick="btnClear_Click" />
            </div>
          </div>
          <div class="col-md-6 text-end">
            <asp:Button ID="btnAddNew" runat="server" Text="Add New Student" CssClass="btn btn-primary"
              OnClick="btnAddNew_Click" />
          </div>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-info" Visible="false">
          <asp:Literal ID="litMessage" runat="server"></asp:Literal>
        </asp:Panel>

        <asp:GridView ID="gvStudents" runat="server" CssClass="table table-striped table-bordered table-hover"
          AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
          OnPageIndexChanging="gvStudents_PageIndexChanging" OnRowCommand="gvStudents_RowCommand"
          DataKeyNames="StudentID">
          <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="FullName" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Program" HeaderText="Program" />
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="Actions">
              <ItemTemplate>
                <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-info btn-sm" CommandName="ViewDetails"
                  CommandArgument='<%# Eval("StudentID") %>' Text="View" />
                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary btn-sm" CommandName="EditItem"
                  CommandArgument='<%# Eval("StudentID") %>' Text="Edit" />
                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandName="DeleteItem"
                  CommandArgument='<%# Eval("StudentID") %>' Text="Delete"
                  OnClientClick="return confirm('Are you sure you want to delete this student?');" />
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
          <EmptyDataTemplate>
            <div class="alert alert-info">No students found.</div>
          </EmptyDataTemplate>
          <PagerStyle CssClass="pagination-container" HorizontalAlign="Center" />
        </asp:GridView>
      </div>
    </form>
  </body>

  </html>