<%@ Page Title="Todo Interactive" Language="C#" 
         CodeBehind="Todo.aspx.cs" 
         Inherits="DotNet_Framework_WebApp.Pages.Todo" %>

<!DOCTYPE html>
<html>
<head>
    <title>Todo CRUD (Interactive)</title>
</head>
<body>
    <h1>Todo CRUD with ASPX (Interactive)</h1>
    
    <form id="todoForm" runat="server">

        <h3>Add Todo</h3>
        <label for="txtTitle">Title:</label>
        <asp:TextBox ID="txtTitle" runat="server" />
        <br />

        <label for="chkIsComplete">Is Complete:</label>
        <asp:CheckBox ID="chkIsComplete" runat="server" />
        <br />

        <asp:Button ID="btnAdd" runat="server" Text="Add Todo" OnClick="btnAdd_Click" />

        <hr />

        <asp:GridView 
            ID="gvTodos" 
            runat="server" 
            AutoGenerateColumns="false" 
            DataKeyNames="Id"
            OnRowEditing="gvTodos_RowEditing"
            OnRowCancelingEdit="gvTodos_RowCancelingEdit"
            OnRowUpdating="gvTodos_RowUpdating"
            OnRowDeleting="gvTodos_RowDeleting">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />

                <asp:BoundField DataField="Title" HeaderText="Title" />

                <asp:CheckBoxField DataField="IsComplete" HeaderText="Complete" />

                <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" 
                                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField DataField="UpdatedDate" HeaderText="UpdatedDate" 
                                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField DataField="FinishDate" HeaderText="FinishDate" 
                                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />

                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
