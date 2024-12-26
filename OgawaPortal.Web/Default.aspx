<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Default.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Main Page</title>
    <meta http-equiv="Expires" content="0" />
</head>
<%--Suggest background pic width 1920px--%>
<body class="VerticalTemplate" style ="background-image: url('../Images/Background.jpg'); background-repeat: no-repeat; background-size: 100%; 
background-blend-mode: screen;">
     <style>
/*         Details Caption*/
        .Caption
        {
            color:black !important;
            font-weight:bold !important;
        }
        .dxgvHeader_XafTheme
        {
            color:black !important;
            font-weight:bold !important;
        }
        .dx-vam
        {
            font-weight:initial !important;
        }
        .dxnb-ghtext
        {
            color:black !important;
        }
        .dxnb-item
        {
            font-weight:bold !important;
        }
        .dx-wrap
        {
            font-weight:bold !important;
        }
        .dxtv-nd
        {
            font-weight:bold !important;
        }
 /*       Navigation*/
        .dxnbLite_XafTheme .dxnb-content
        {
            border-top:1px solid #c6c6c6 !important;
            border-bottom:1px solid #c6c6c6 !important;
            border-left:1px solid #c6c6c6 !important;
            border-right:1px solid #c6c6c6 !important;
        }
        .dxnbLite_XafTheme .dxnb-item
        {
            border-bottom:1px solid #c6c6c6 !important;
        }
        .dxnbLite_XafTheme .dxnb-header, .dxnbLite_XafTheme .dxnb-headerCollapsed
        {
            border-left:1px solid #c6c6c6 !important;
            border-right:1px solid #c6c6c6 !important;
            border-radius: 20px !important;
            background-color:#EEEEEE !important;
        }
        .xafNav .dxnbLite_XafTheme
        {
            border-bottom:none !important;
            border-left:none !important;
            border-right:none !important;
        }
/*        Module header*/
/*       white
        {
            background-color:transparent !important;
        }*/
/*      .dxeEditArea_XafTheme.dxeDisabled_XafTheme
        {
            background-color:antiquewhite;
        }
        .dxeDisabled_XafTheme, .dxeDisabled_XafTheme td.dxe
        {
            background-color:antiquewhite;
        }*/
/*      .dxgvTable_XafTheme
        {
              background-color:lightyellow !important;
        }*/
        .dx-ar > *
        {
            float :left !important;
        }
    </style>
    <form id="form2" runat="server">
    <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
    <div runat="server" id="Content" />
    </form>
</body>
</html>