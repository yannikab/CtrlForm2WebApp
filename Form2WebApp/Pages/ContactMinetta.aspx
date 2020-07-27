[<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactMinetta.aspx.cs" Inherits="ContactMinetta" %>

<%@ Register Src="~/UserControls/CtrlFormMinetta.ascx" TagPrefix="uc1" TagName="CtrlFormMinetta" %>

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
        .formIdContainer {
            padding: 20px;
        }

        .formField label {
            font-size: 13px;
        }

        .formTitle label {
            font-size: 25px;
            color: #363b4c;
            text-align: center;
            margin: 0 0 20px 0;
            font-weight: bold;
            display: block;
        }

        .formField {
            display: flex;
            flex-direction: row;
            flex-wrap: wrap;
            margin: 0 0 20px 0;
        }

        .formField > label:first-child {
            margin-right: 2.5px;
        }

        .formTextBox input,
        .formDatePicker input,
        .formSelect select,
        .formRadioGroup div,
        .formNumberBox div {
            width: 100%;
        }

        .formTextBox input,
        .formDatePicker input,
        .formSelect select,
        .formNumberBox input {
            border: 1px solid rgba(54, 59, 76, 0.3);
            border-radius: 4px;
            padding: 9px;
            color: #757575;
        }

        .formNumberBox input[type="button"][disabled] {
            opacity: 0.7;
        }

        .formNumberBox input[type="button"] {
            max-width: 41px;
            width: 100%;
        }

        .formTextBox input:focus,
        .formDatePicker input:focus,
        .formSelect select:focus,
        .formNumberBox input[type="text"]:focus {
            /*border: 1px solid var(--color-main) !important;*/
            border: 1px solid !important;
            border-color: blue;
            outline: none;
        }

        .formIdSubmit {
            text-align: right;
        }

        .formIdSubmit button {
            border: unset;
            background-color: #0064c1;
            color: #fff;
            padding: 7px 10px;
            border-radius: 4px;
            box-shadow: 0 0 8px 1px rgba(169, 169, 169, 0.7);
        }

        .formIdSubmit button:hover {
            opacity: .7;
        }

        .formValidationMessage {
            color: #ff6666;
            margin-top: 2px;
        }

    </style>

    <form id="pageForm" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server" />
        <uc1:CtrlFormMinetta ID="ctrlFormMinetta" runat="server" />
    </form>

</body>

</html>
