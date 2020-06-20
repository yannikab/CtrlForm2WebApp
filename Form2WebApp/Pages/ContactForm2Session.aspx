<%@ Page MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="ContactForm2Session.aspx.cs" Inherits="Form2WebApp.Pages.ContactForm2Session" %>

<%@ Register Src="~/UserControls/Form2SessionContact.ascx" TagPrefix="uc1" TagName="Form2SessionContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <h1>
        <asp:Literal runat="server" ID="ltrH1" />
    </h1>

    <div>
        <asp:Literal ID="ltrContent" runat="server" />
    </div>

    <div>
        <uc1:Form2SessionContact runat="server" ID="Form2SessionContact" />
    </div>

</asp:Content>
