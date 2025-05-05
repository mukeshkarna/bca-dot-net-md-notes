# ASP.NET Web Forms: Teaching Guide

## 1. Introduction to ASP.NET Web Forms
   
### What is ASP.NET Web Forms?
- A programming model within the ASP.NET framework
- Uses a page-based architecture and event-driven programming model
- Follows the Page Controller pattern (each page has its own handler class)
- Pages (.aspx) are tightly coupled with code-behind (.aspx.cs) files
- Designed to be similar to Windows Forms development

### Comparing Web Forms and MVC

| Feature | Web Forms | MVC |
|---------|-----------|-----|
| Architecture | Page Controller pattern | Model-View-Controller pattern |
| State Management | ViewState (automatic) | No automatic state management |
| Page Structure | ASPX pages with code-behind | Views, Controllers, Models |
| URL Structure | Based on physical files | Route-based |
| HTML Control | Limited - server controls generate HTML | Full control over HTML output |
| Development Model | Event-driven, similar to desktop apps | Request-based, separation of concerns |
| Controls | Server controls with runat="server" | HTML helpers or tag helpers |
| Learning Curve | Easier for Windows developers | Requires understanding of MVC pattern |

## 2. ASP.NET Web Forms Page Lifecycle

### Basic Page Lifecycle
1. **Page Initialization (Init)**: Controls are initialized
2. **Page Load**: Page and controls load their state
3. **Control Events**: Events from controls are processed
4. **Pre-Render**: Final chance to modify the page
5. **Render**: Page and controls render their HTML
6. **Unload**: Page is unloaded from memory

### Example of Page_Load
```csharp
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        // First-time page load code
        lblMessage.Text = "Welcome to our site!";
    }
    else
    {
        // Subsequent postback code
    }
}
```

### ViewState
- Mechanism to maintain control values across postbacks
- Stored in a hidden field on the page
- Automatically restores control values after a postback
- Can increase page size with large amounts of data

## 3. Server Controls Overview

### Types of Server Controls
1. **HTML Server Controls**: Standard HTML elements with `runat="server"`
2. **Web Server Controls**: ASP.NET-specific controls with the `asp:` prefix
3. **Validation Controls**: For validating user input
4. **Data Controls**: For displaying and manipulating data
5. **Navigation Controls**: For site navigation
6. **Login Controls**: For user authentication
7. **AJAX Controls**: For asynchronous operations

### Web Forms Architecture Diagram
```
┌─────────────────────────────────────┐
│         Web Browser (Client)        │
└───────────────────┬─────────────────┘
                    │
                    ▼
┌─────────────────────────────────────┐
│         ASP.NET Web Forms           │
│  ┌─────────────────────────────┐    │
│  │       ASPX Page (.aspx)     │    │
│  │                             │    │
│  │  ┌─────────────────────┐    │    │
│  │  │   Server Controls   │    │    │
│  │  └─────────────────────┘    │    │
│  └─────────────────┬───────────┘    │
│                    │                │
│  ┌─────────────────▼───────────┐    │
│  │ Code-behind (.aspx.cs/.vb)  │    │
│  └─────────────────────────────┘    │
└─────────────────────────────────────┘
```

## 4. Basic Web Server Controls

### TextBox Control
```html
<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
```

**Key Properties:**
- `ID`: Unique identifier
- `Text`: The text content
- `TextMode`: SingleLine, MultiLine, Password
- `MaxLength`: Maximum character length
- `AutoPostBack`: Causes immediate postback when value changes

### Button Control
```html
<asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
```

**Key Properties:**
- `ID`: Unique identifier
- `Text`: The button text
- `CommandName`: Command name for event handling
- `CommandArgument`: Additional data for the command
- `CausesValidation`: Whether clicking triggers validation

### Label Control
```html
<asp:Label ID="lblMessage" runat="server" Text="Welcome"></asp:Label>
```

**Key Properties:**
- `ID`: Unique identifier
- `Text`: The displayed text
- `AssociatedControlID`: Links label to another control for accessibility

### DropDownList Control
```html
<asp:DropDownList ID="ddlCountries" runat="server">
    <asp:ListItem Text="United States" Value="US"></asp:ListItem>
    <asp:ListItem Text="Canada" Value="CA"></asp:ListItem>
</asp:DropDownList>
```

**Key Properties:**
- `ID`: Unique identifier
- `SelectedIndex`: Index of selected item
- `SelectedValue`: Value of selected item
- `AutoPostBack`: Whether selection change causes immediate postback

### CheckBox Control
```html
<asp:CheckBox ID="chkSubscribe" runat="server" Text="Subscribe to newsletter" />
```

**Key Properties:**
- `ID`: Unique identifier
- `Checked`: Whether checkbox is checked
- `Text`: Text displayed next to checkbox

### RadioButton Control
```html
<asp:RadioButton ID="rbMale" runat="server" Text="Male" GroupName="Gender" />
<asp:RadioButton ID="rbFemale" runat="server" Text="Female" GroupName="Gender" />
```

**Key Properties:**
- `ID`: Unique identifier
- `Checked`: Whether radio button is selected
- `GroupName`: Groups radio buttons (only one in a group can be selected)

## 5. Event Handling in Web Forms

### Event-Driven Model
- Controls raise events when users interact with them
- Events are handled in code-behind files
- Common events: Click, TextChanged, SelectedIndexChanged

### Button Click Example
```html
<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
```

```csharp
protected void btnSubmit_Click(object sender, EventArgs e)
{
    lblMessage.Text = "Hello, " + txtName.Text + "!";
}
```

### AutoPostBack Example
```html
<asp:DropDownList ID="ddlCountries" runat="server" AutoPostBack="true" 
    OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged">
    <asp:ListItem Text="United States" Value="US"></asp:ListItem>
    <asp:ListItem Text="Canada" Value="CA"></asp:ListItem>
</asp:DropDownList>
```

```csharp
protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
{
    lblSelectedCountry.Text = "You selected: " + ddlCountries.SelectedItem.Text;
}
```

## 6. Validation Controls

### Types of Validation Controls

1. **RequiredFieldValidator**
```html
<asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" 
    ErrorMessage="Name is required" runat="server"></asp:RequiredFieldValidator>
```

2. **RangeValidator**
```html
<asp:RangeValidator ID="rvAge" ControlToValidate="txtAge" 
    MinimumValue="18" MaximumValue="100" Type="Integer"
    ErrorMessage="Age must be between 18 and 100" runat="server"></asp:RangeValidator>
```

3. **RegularExpressionValidator**
```html
<asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="Invalid email format" runat="server"></asp:RegularExpressionValidator>
```

4. **CompareValidator**
```html
<asp:CompareValidator ID="cvPassword" ControlToValidate="txtConfirmPassword" 
    ControlToCompare="txtPassword" Operator="Equal"
    ErrorMessage="Passwords do not match" runat="server"></asp:CompareValidator>
```

5. **CustomValidator**
```html
<asp:CustomValidator ID="cvCustom" ControlToValidate="txtInput" 
    OnServerValidate="CustomValidator_ServerValidate"
    ErrorMessage="Custom validation failed" runat="server"></asp:CustomValidator>
```

```csharp
protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
{
    // Custom validation logic
    args.IsValid = YourCustomValidationLogic(args.Value);
}
```

6. **ValidationSummary**
```html
<asp:ValidationSummary ID="vsummary" HeaderText="Please correct the following errors:"
    DisplayMode="BulletList" runat="server" />
```

### Validation Process
1. Client-side validation happens in the browser (using JavaScript)
2. Server-side validation happens on postback
3. `Page.IsValid` property indicates whether all validators passed

```csharp
protected void btnSubmit_Click(object sender, EventArgs e)
{
    if (Page.IsValid)
    {
        // Process the form
        SaveData();
    }
}
```

### Validation Groups
For validating specific sections of a form independently:

```html
<asp:TextBox ID="txtName" runat="server" ValidationGroup="PersonalInfo"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" 
    ErrorMessage="Name is required" ValidationGroup="PersonalInfo" runat="server"></asp:RequiredFieldValidator>

<asp:Button ID="btnSavePersonal" Text="Save Personal Info" 
    ValidationGroup="PersonalInfo" runat="server" OnClick="btnSavePersonal_Click" />
```

## 7. Working with Databases

### Database Connection Approaches

#### 1. ADO.NET Direct Access
Provides the most control with explicit connection management:

```csharp
protected void SaveUser()
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
        
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", txtName.Text);
        command.Parameters.AddWithValue("@Email", txtEmail.Text);
        
        connection.Open();
        int result = command.ExecuteNonQuery();
        
        if (result > 0)
        {
            lblMessage.Text = "User saved successfully!";
        }
    }
}
```

#### 2. SqlDataSource Control
Declarative database access:

```html
<!-- SQL Data Source -->
<asp:SqlDataSource ID="sdsUsers" runat="server"
    ConnectionString="<%$ ConnectionStrings:MyDbConnection %>"
    SelectCommand="SELECT * FROM Users"
    InsertCommand="INSERT INTO Users (Name, Email) VALUES (@Name, @Email)">
    <InsertParameters>
        <asp:ControlParameter Name="Name" ControlID="txtName" PropertyName="Text" />
        <asp:ControlParameter Name="Email" ControlID="txtEmail" PropertyName="Text" />
    </InsertParameters>
</asp:SqlDataSource>
```

#### 3. Entity Framework
Object-relational mapping for database access:

```csharp
public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class MyDbContext : DbContext
{
    public MyDbContext() : base("name=MyDbConnection")
    {
    }
    
    public DbSet<User> Users { get; set; }
}

// Using Entity Framework
protected void SaveUser()
{
    using (var context = new MyDbContext())
    {
        User user = new User
        {
            Name = txtName.Text,
            Email = txtEmail.Text
        };
        
        context.Users.Add(user);
        context.SaveChanges();
        
        lblMessage.Text = "User saved successfully!";
    }
}
```

### Data Controls

#### GridView
For displaying tabular data:

```html
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False"
    DataKeyNames="UserId">
    <Columns>
        <asp:BoundField DataField="UserId" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>
```

#### DetailsView
For displaying a single record:

```html
<asp:DetailsView ID="dvUser" runat="server" AutoGenerateRows="False"
    DataKeyNames="UserId" DefaultMode="Insert">
    <Fields>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:CommandField ShowInsertButton="True" />
    </Fields>
</asp:DetailsView>
```

#### FormView
For flexible record display and editing:

```html
<asp:FormView ID="fvUser" runat="server" DefaultMode="Insert">
    <InsertItemTemplate>
        <div>
            Name: <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
        </div>
        <div>
            Email: <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
        </div>
        <div>
            <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="Insert" />
        </div>
    </InsertItemTemplate>
</asp:FormView>
```

## 8. Complete Example - User Registration Form

### ASPX Markup (Simplified)
```html
<form id="form1" runat="server">
    <div>
        <h2>User Registration</h2>
        
        <asp:ValidationSummary ID="vsRegistration" HeaderText="Please correct errors:"
            DisplayMode="BulletList" runat="server" />
        
        <div>
            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" 
                ErrorMessage="Name is required" runat="server"></asp:RequiredFieldValidator>
        </div>
        
        <div>
            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" 
                ErrorMessage="Email is required" runat="server"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ErrorMessage="Invalid email format" runat="server"></asp:RegularExpressionValidator>
        </div>
        
        <div>
            <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label>
            <asp:DropDownList ID="ddlCountry" runat="server">
                <asp:ListItem Text="Select a country" Value=""></asp:ListItem>
                <asp:ListItem Text="United States" Value="US"></asp:ListItem>
                <asp:ListItem Text="Canada" Value="CA"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCountry" ControlToValidate="ddlCountry" 
                ErrorMessage="Please select a country" runat="server"></asp:RequiredFieldValidator>
        </div>
        
        <div>
            <asp:Button ID="btnRegister" runat="server" Text="Register" 
                OnClick="btnRegister_Click" />
        </div>
        
        <div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        
        <h3>Registered Users</h3>
        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Country" HeaderText="Country" />
            </Columns>
        </asp:GridView>
    </div>
</form>
```

### Code-Behind
```csharp
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        BindGridView();
    }
}

protected void btnRegister_Click(object sender, EventArgs e)
{
    if (Page.IsValid)
    {
        SaveUser();
        ClearForm();
        BindGridView();
    }
}

private void SaveUser()
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string query = "INSERT INTO Users (Name, Email, Country) VALUES (@Name, @Email, @Country)";
        
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", txtName.Text);
        command.Parameters.AddWithValue("@Email", txtEmail.Text);
        command.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
        
        connection.Open();
        command.ExecuteNonQuery();
        
        lblMessage.Text = "Registration successful!";
    }
}

private void BindGridView()
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string query = "SELECT UserId, Name, Email, Country FROM Users";
        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        
        gvUsers.DataSource = dt;
        gvUsers.DataBind();
    }
}

private void ClearForm()
{
    txtName.Text = string.Empty;
    txtEmail.Text = string.Empty;
    ddlCountry.SelectedIndex = 0;
}
```

## 9. Teaching Tips and Best Practices

### Teaching Order
1. Start with basic controls and event handling
2. Introduce page lifecycle and ViewState
3. Add validation to forms
4. Connect to databases
5. Use data controls to display data
6. Explore more advanced topics (caching, AJAX, etc.)

### Common Pitfalls
- ViewState size growing too large
- Page lifecycle confusion
- Server control IDs vs. client IDs
- Improper validation logic
- Missing PostBack handling

### Coding Standards
- Use meaningful control IDs
- Group related controls in panels
- Handle Page_Load events properly (check IsPostBack)
- Secure database connection strings
- Use parameterized queries for all database operations
- Validate all user input

### Performance Considerations
- Limit ViewState usage for large data sets
- Use caching where appropriate
- Disable ViewState for read-only controls
- Minimize database operations
- Use asynchronous operations for long-running tasks

## 10. Legacy Status and Future Directions

- ASP.NET Web Forms is not included in .NET Core/.NET 5+
- Microsoft recommends ASP.NET Core MVC or Razor Pages for new development
- Web Forms remains supported in .NET Framework 4.8
- Many legacy applications still use Web Forms
- Migration paths exist to newer technologies
