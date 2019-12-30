<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <input type="file" id="fileUpload" class="btn" style="background: #428bca; color: #fff;"
    accept="application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
      data-bind="event:{ change: onFileSelectedEvent }" />
<input type="submit" data-bind="click: uploadFile" />
<script src="JScript1.js" type="text/javascript"></script>
</asp:Content>
