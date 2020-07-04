<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactAdult.aspx.cs" Inherits="ContactAdult" %>

<%@ Register Src="~/UserControls/CtrlFormAdult.ascx" TagPrefix="uc1" TagName="CtrlFormAdult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>
    <script src="/assetsClever/vendors/js/jquery.min.js"></script>
    <script src="/assets/js/bootbox.min.js"></script>
    <script src="/assetsClever/vendors/js/bootstrap.min.js"></script>
    <script src="/assets/js/date-time/bootstrap-datepicker.min.js"></script>
    <link href="/assetsClever/vendors/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/assetsClever/css/style.css" rel="stylesheet" />
    <link href="/assetsClever/css/custom.css" rel="stylesheet" />
    <link href="/assets/css/datepicker.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <style>
        .form-id-container {
            padding: 20px;
        }

        .form-field label {
            font-size: 13px;
        }

        .form-title label {
            font-size: 25px;
            color: #363b4c;
            text-align: center;
            margin: 0 0 20px 0;
            font-weight: bold;
            display: block;
        }

        .form-field {
            display: flex;
            flex-direction: column;
            margin: 0 0 20px 0;
        }

        .form-textbox input,
        .form-datepicker input,
        .form-select select {
            border: 1px solid rgba(54, 59, 76, 0.3);
            border-radius: 4px;
            padding: 9px;
            color: #757575;
        }

        .form-textbox input:focus,
        .form-datepicker input:focus,
        .form-select select:focus {
            border: 1px solid var(--color-main) !important;
            outline: none;
        }

        .form-id-employmentstatus div {
            display: grid;
            grid-template-columns: 18px 115px 18px 86px 18px 65px;
            align-items: center;
        }

        .form-id-employmentstatus div label {
            margin: 0;
        }

        .form-id-submit {
            text-align: right;
        }

        .form-id-submit button {
            border: unset;
            background-color: #0064c1;
            color: #fff;
            padding: 7px 10px;
            border-radius: 4px;
            box-shadow: 0 0 8px 1px rgba(169, 169, 169, 0.7);
        }

        .form-id-submit button:hover {
            opacity: .7;
        }

        .form-validation-message {
            color: #ff6666;
        }
    </style>

    <form id="pageForm" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server" />
        <uc1:CtrlFormAdult ID="ctrlFormAdult" runat="server" />
    </form>

</body>

</html>
