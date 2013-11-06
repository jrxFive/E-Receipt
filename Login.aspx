<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="E_ReceiptTest.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Username:
        <asp:TextBox ID="usernameTextBox" runat="server" CssClass="imput"></asp:TextBox>
        <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ErrorMessage="*"
            ControlToValidate="usernameTextBox"></asp:RequiredFieldValidator>
    <p>
        Password:
        <asp:TextBox ID="passwordTextBox" runat="server" CssClass="imput" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ErrorMessage="*"
            ControlToValidate="passwordTextBox"></asp:RequiredFieldValidator>
    <p>
        <asp:Button ID="signInButton" runat="server" Text="Sign In" CssClass="button" 
            onClick="LoginButton_Click1" />
</asp:Content>
