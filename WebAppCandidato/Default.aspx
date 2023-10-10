<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppCandidato._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblHora" Text="" runat="server" CssClass="label label-danger" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="btnCarga" Text="Carga tabla" runat="server" CssClass="btn btn-default" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" ViewStateMode="Enabled" ValidateRequestMode="Enabled">
                <ContentTemplate>
                    <asp:Table ID="tblClientes" runat="server" CssClass="table" ViewStateMode="Enabled" EnableViewState="true">
                        <asp:TableHeaderRow runat="server" TableSection="TableHeader">
                            <asp:TableHeaderCell runat="server">Nombre</asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server">Primer Apellido</asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server">Segundo Apellido</asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server">Sexo</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                    </asp:Table>
               
                      <asp:Table ID="tblInfo" runat="server" CssClass="table" ViewStateMode="Enabled" EnableViewState="true">

                      </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

