<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlFormStudent.ascx.cs" Inherits="Form2WebApp.UserControls.CtrlFormStudent" %>

<asp:UpdatePanel runat="server" ID="upPanelForm" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Literal ID="ltrForm" runat="server"></asp:Literal>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress runat="server" ID="upProgress" AssociatedUpdatePanelID="upPanelForm">
    <ProgressTemplate>
        <div class="upProgressSquare">
            <div class="sk-cube-grid">
                <div class="sk-cube sk-cube1"></div>
                <div class="sk-cube sk-cube2"></div>
                <div class="sk-cube sk-cube3"></div>
                <div class="sk-cube sk-cube4"></div>
                <div class="sk-cube sk-cube5"></div>
                <div class="sk-cube sk-cube6"></div>
                <div class="sk-cube sk-cube7"></div>
                <div class="sk-cube sk-cube8"></div>
                <div class="sk-cube sk-cube9"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
