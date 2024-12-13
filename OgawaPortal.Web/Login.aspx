<%@ Page Language="C#" AutoEventWireup="true" Inherits="LoginPage" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Login.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates.Controls" TagPrefix="tc" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
     <style>  
        #Logon_UPHeader 
        {  
            display: none;  
        }  
    </style>  
    <title>Logon</title>
</head>
<%--Suggest background pic width 1920px--%>
<body class="Dialog" style ="background-image: url('../Images/Background.jpg'); background-repeat: no-repeat; background-size: 100%;">
       <style>
        .StaticImage
        {
            padding-left :60px !important;
        }
        table.LogonContent
        {
            padding :20px 75px !important;
        }
        table.LogonContentWidth 
        {
            width :430px !important;
            border-radius: 30px;
            opacity: 0.88;
        }
        .Logon_UPHeader
        {
            display:none;
        }
        .Caption
        {
            color:black !important;
            font-weight:bold !important;
        }
        .StaticText
        {
            color:black !important;
            font-weight:bold !important;
        }
    </style>
    <div id="PageContent" class="PageContent DialogPageContent">
        <form id="form1" runat="server">
        <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
        <div id="Content" runat="server" />
        </form>
    </div>
</body>
</html>
