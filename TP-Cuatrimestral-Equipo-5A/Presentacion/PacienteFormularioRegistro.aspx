<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PacienteFormularioRegistro.aspx.cs" Inherits="Presentacion.FormularioRegistro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="ms-3 mb-3">Formulario de registro</h3>
        <h4 class="text-primary ms-3 mb-4">Datos personales</h4>

        <div class="d-flex align-items-center mb-4">
            <div class="position-relative" style="width:80px; height:80px;">
               
                 <img src="https://cdn-icons-png.flaticon.com/512/847/847969.png" class="rounded-circle w-100 h-100" alt="imagenPerfil" />
           </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" placeholder="Apellido"></asp:TextBox>
            </div>
        </div>

        <div class="mb-3">
            <label for="TextFechaNacimiento" class="form-label">Fecha de nacimiento</label>
            <asp:TextBox ID="TextFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email</label>
            <div class="input-group">
                <span class="input-group-text">@</span>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtDocumento" class="form-label">Documento</label>
                <asp:TextBox runat="server" ID="txtDocumento" CssClass="form-control" placeholder="Documento"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label for="TextTelefono" class="form-label">Teléfono</label>
                <asp:TextBox runat="server" ID="TextTelefono" CssClass="form-control" placeholder="Teléfono"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
             <div class="col-md-4">
                <asp:Button Text="Registrarse" ID="BtnRegistrarse" OnClick="BtnRegistrarse_Click" runat="server" class="btn btn-primary" />
            </div>  
       </div>
   </div>
</asp:Content>
