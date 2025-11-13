<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioEspecialidades.aspx.cs" Inherits="Presentacion.FormularioEspecialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-4">

        <h3 class="ms-3 mb-3">Formulario de Especialidad</h3>
        <h4 class="text-primary ms-3 mb-4">Datos de especialidad</h4>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" placeholder="Descripción de la especialidad"></asp:TextBox>
               
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4">
                <asp:Button Text="Registrar" ID="btnRegistrar" runat="server" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                <asp:Button Text="Volver" ID="btnVolver" runat="server" CssClass="btn btn-outline-secondary ms-2" OnClick="btnVolver_Click" />
        
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-12">
                <asp:Panel ID="pnlResultado" runat="server" Visible="false" CssClass="alert alert-success text-center mt-3">
                    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </div>
        </div>

    </div>
</asp:Content>
