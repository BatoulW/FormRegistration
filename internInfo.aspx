<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="internInfo.aspx.cs" Inherits="patient.InternProgram.internInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <br />
&nbsp;
    <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
        <asp:Label ID="lblOutput" runat="server" BackColor="#2BFFCA"></asp:Label>
</p>
<table id="tblInfo" style="width: 74%;">
    <tr>
        <td style="height: 21px; width: 189px">User Name</td>
        <td style="height: 21px">
            <br />
        &nbsp;<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            &nbsp;&nbsp;
        </td>
        <td rowspan="8">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            
        
            
            
            <asp:GridView ID="gvInfo" runat="server" Height="200px" Width="363px" BackColor="#ECECEC" BorderColor="#00FF99" BorderStyle="Double" BorderWidth="5px" CellPadding="50" CellSpacing="50" Font-Bold="True" Font-Overline="False" Font-Size="Large" ForeColor="Black"  ShowHeaderWhenEmpty="True" style="margin-top: 2px; margin-left: 92px;">
                <HeaderStyle BorderColor="#00FF99" />
                <%-- <Columns>
                            <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Intern Infor">
                                <ItemTemplate>
                                     <asp:LinkButton ID="lnkupdate" runat="server"
                                        CommandArgument='<%# Bind("uName") %>' OnClick="displayIntern"
                                        Text='<%# Eval("uName")  %>'>  </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>--%>
                         <%--   <asp:BoundField DataField="uName" HeaderText="User Name" InsertVisible="False" ReadOnly="True" SortExpression="employeeId" />
                            <asp:BoundField DataField="fNameEn" HeaderText="Name (EN)" SortExpression="fNameEn" />
                            <asp:BoundField DataField="fNameAr" HeaderText="Name (AR)" SortExpression="fNameAr" />
                            <asp:BoundField DataField="country" HeaderText="Country" SortExpression="country" />
                            <asp:BoundField DataField="salary" HeaderText="Salary" SortExpression="salary" />
                            <asp:BoundField DataField="active" HeaderText="Active" SortExpression="active" />
                          <asp:BoundField DataField="hobbyName" HeaderText="Hobby Name" SortExpression="hobbyName" />--%>

                                                   
                      <%--  </Columns>--%>

            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="height: 21px; width: 189px">
            <br />
            First Name (English)</td>
        <td style="height: 21px">
            <br />
            <asp:TextBox ID="txtFNameEn" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 189px">
            <br />
            First Name(Arabic)</td>
        <td>
            <br />
            <asp:TextBox ID="txtFNameAr" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 189px">
            <br />
            Country</td>
        <td>
            <br />
            <asp:DropDownList ID="ddlCountry" runat="server">
            </asp:DropDownList>
            
        </td>
    </tr>
    <tr>
        <td style="width: 189px">
            <br />
            Salary</td>
        <td>
            <br />
            <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 189px">
            <br />
            Active</td>
        <td>
            &nbsp;
            <asp:RadioButtonList ID="rbActive" runat="server" Height="57px" Width="63px">
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:RadioButtonList>
&nbsp;</td>
    </tr>
    <tr>
        <td rowspan="2" style="width: 189px">Hobbies</td>
    </tr>
    <tr>
        <td>
            <asp:CheckBoxList ID="cblHobbies" runat="server">
                <asp:ListItem Value="1">Reading</asp:ListItem>
                <asp:ListItem Value="2">Tennis</asp:ListItem>
                <asp:ListItem Value="3">Writing</asp:ListItem>
                <asp:ListItem Value="4">Playing Piano</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    </table>
<p>
    &nbsp;</p>
<p>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="btnInsert" runat="server" Text="Add" OnClick="btnInsert_Click" style="margin-left: 0" Width="70px" />
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" Height="28px" Text="Update" Width="68px" OnClick="btnUpdate_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnDelete" runat="server" Text="Remove" OnClick="btnDelete_Click" />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnSD" runat="server" Text="Show Data" OnClick="btnSD_Click" />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
&nbsp;&nbsp;
    <asp:Button ID="btnSI" runat="server" Text="Show Intern" OnClick="btnSI_Click" />
</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
</asp:Content>
