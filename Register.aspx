<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="E_ReceiptTest.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

                <p>
                    Username:
                    <asp:TextBox ID="usernameTextBox" runat="server" CssClass="imput"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ErrorMessage="*"
                        ControlToValidate="usernameTextBox"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Password:
                    <asp:TextBox ID="passwordTextBox" runat="server" CssClass="imput" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ErrorMessage="*"
                        ControlToValidate="passwordTextBox"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Password:
                    <asp:TextBox ID="passwordConfirm" runat="server" CssClass="imput" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passwordConfirmValidator" runat="server" ErrorMessage="*"
                        ControlToValidate="passwordConfirm"></asp:RequiredFieldValidator>
                </p>
                <p>
                    First Name:
                    <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="imput"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="firstNameValidator" runat="server" ErrorMessage="*"
                        ControlToValidate="firstNameTextBox"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Last Name:
                    <asp:TextBox ID="lastNameTextBox" runat="server" CssClass="imput"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="lastNameValidator" runat="server" ErrorMessage="*"
                        ControlToValidate="lastNameTextBox"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Email Address:
                    <asp:TextBox ID="emailTextBox" runat="server" CssClass="imput"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="emailValidator" runat="server" ErrorMessage="*"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="emailTextBox"></asp:RegularExpressionValidator>
                </p>
                <p>
                 <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                ControlToCompare="passwordTextBox" ControlToValidate="passwordConfirm" 
                                ErrorMessage="Passwords must match"></asp:CompareValidator>
                    <asp:Button ID="registerButton" runat="server" Text="Sumbit" CssClass="button" OnClick="Button1_Click" />
                </p>

</asp:Content>
