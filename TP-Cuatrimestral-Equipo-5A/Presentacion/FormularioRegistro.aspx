<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioRegistro.aspx.cs" Inherits="Presentacion.FormularioRegistro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="ms-3 mb-3">Formulario de registro</h3>
        <h4 class="text-primary ms-3 mb-4">Datos personales</h4>.

        <div class="d-flex align-items-center mb-4">
            <div class="position-relative" style="width:80px; height:80px;">
               <asp:Image ID="imgPreview" runat="server" CssClass="rounded-circle w-100 h-100" ImageUrl="~images/default-profile.png" />

                <asp:FileUpload ID="fuImagen" runat="server" CssClass="d-none" OnChage="Imagen_Changed" />
                <label for="fuImagen" class="btn btn-link p-0 position-absolute" style="bottom:0; left:100%;">Cambiar foto</label>
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
            
                <label class="form-label d-block mb-2">Tipo de usuario</label>

                <div class="form-check w-100 mb-2 ms-2">
                    <asp:RadioButton ID="rbMedico" runat="server" GroupName="TipoUsuario" CssClass="form-check-input" />
                    <label for="rbMedico" class="form-check-label ms-2">Médico</label>
                </div>

                <div class="form-check w-100 mb-2 ms-2">
                    <asp:RadioButton ID="rbPaciente" runat="server" GroupName="TipoUsuario" CssClass="form-check-input" />
                    <label for="rbPaciente" class="form-check-label ms-2">Paciente</label>
                </div>

                <div class="form-check w-100 mb-2 ms-2">
                    <asp:RadioButton ID="rbPersonal" runat="server" GroupName="TipoUsuario" CssClass="form-check-input" />
                    <label for="rbPersonal" class="form-check-label ms-2">Personal</label>
                </div>
       
        </div>

        <div class="row mb-3">
            <asp:Button Text="Registrarse" ID="BtnRegistrarse" OnClick="BtnRegistrarse_Click" runat="server" class="btn btn-primary" />
        </div>
    </div>

</asp:Content>
