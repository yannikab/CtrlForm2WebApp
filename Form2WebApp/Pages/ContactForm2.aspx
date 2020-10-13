﻿<%@ Page MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="ContactForm2.aspx.cs" Inherits="Form2WebApp.Pages.ContactCtrlForm2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="Server">
    <link rel="stylesheet" href="/assets/css/datepicker.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <h1>
        <asp:Literal runat="server" ID="ltrH1" />
    </h1>

    <div>
        <asp:Literal ID="ltrContent" runat="server" />
    </div>

    <div>
        <asp:Literal ID="ltrForm" runat="server" ViewStateMode="Disabled" />
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="Server">
    <script src="/assets/js/bootbox.min.js"></script>
    <script src="/assets/js/date-time/bootstrap-datepicker.min.js"></script>
</asp:Content>
