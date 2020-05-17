<%@ Page MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="ContactForm2.aspx.cs" Inherits="Form2WebApp.Pages.ContactCtrlForm2" %>

<%@ Register Src="~/UserControls/Form2Contact.ascx" TagPrefix="uc1" TagName="CtrlForm2Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <h1>
        <asp:Literal runat="server" ID="ltrH1" />
    </h1>

    <div>
        <asp:Literal ID="ltrContent" runat="server" />
    </div>

    <div>
        <uc1:CtrlForm2Contact runat="server" ID="CtrlForm2Contact" />
    </div>

</asp:Content>
