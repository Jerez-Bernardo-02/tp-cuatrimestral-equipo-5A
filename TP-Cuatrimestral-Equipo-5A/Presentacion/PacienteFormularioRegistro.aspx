<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" 
CodeBehind="PacienteFormularioRegistro.aspx.cs" Inherits="Presentacion.FormularioRegistro" 
UnobtrusiveValidationMode="None"  %> 
	
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
                <!--
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." ForeColor="Red"/>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre solo puede contener letras." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" ForeColor="Red"/>
                -->
            </div>

            <div class="col-md-6">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" placeholder="Apellido"></asp:TextBox>
                <!--
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio." ForeColor="Red" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido solo puede contener letras." ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" ForeColor="Red"/>
                -->
            </div>
        </div>

        <div class="mb-3">
            <label for="TextFechaNacimiento" class="form-label">Fecha de nacimiento</label>
            <asp:TextBox ID="TextFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            <!--
            <asp:RequiredFieldValidator runat="server" ControlToValidate="TextFechaNacimiento" ErrorMessage="La fecha de nacimiento es obligatoria." ForeColor="Red" />
            <asp:CustomValidator ID="cvFechaNacimiento" runat="server" ControlToValidate="TextFechaNacimiento" ErrorMessage="La fecha de nacimiento no puede ser futura." ForeColor="Red" Display="Dynamic" />
            -->
        </div>

        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email</label>
            <div class="input-group">
                <span class="input-group-text">@</span>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
            </div>
            <!--
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio." ForeColor="Red"/>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email inválido." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ForeColor="Red" />
            -->
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtDocumento" class="form-label">Documento</label>
                <asp:TextBox runat="server" ID="txtDocumento" CssClass="form-control" placeholder="Documento"></asp:TextBox>
                <!--
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDocumento" ErrorMessage="El documento es obligatorio." ForeColor="Red" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDocumento" ErrorMessage="Sólo números (entre 7 y 8 dígitos)" ValidationExpression="^\d{7,8}$" ForeColor="Red" />
                -->
            </div>

            <div class="col-12 mt-2">
                <!--
                <asp:CustomValidator ID="cvEmailDni" runat="server" ErrorMessage="El email o DNI ya están registrados." ForeColor="Red" Display="Dynamic" />
                -->
            </div>

            <div class="col-md-6">
                <label for="txtTelefono" class="form-label">Teléfono</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" placeholder="Teléfono"></asp:TextBox>
                <!--
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefono" ErrorMessage="El teléfono es obligatorio." ForeColor="Red" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTelefono" ErrorMessage="El teléfono debe tener 10 números." ValidationExpression="^\d{10}$" ForeColor="Red" />
                -->
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6" id="divMatricula" runat="server" visible="false">
                <label for="txtMatricula" class="form-label">Matricula</label>
                <!--
                <asp:TextBox runat="server" ID="txtMatricula" CssClass="form-control" placeholder="Matricula"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula" ErrorMessage="La matrícula es obligatoria para médicos." ForeColor="Red" Enabled="false" />
                -->
            </div>  
        </div>

        <h4 class="text-primary ms-3 mb-4">Datos de acceso</h4>
        <div class="row mb-3">
            <div class="col-md-6">
                <label for="txtUsuario" class="form-label">Usuario</label>
                <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" placeholder="Nombre de usuario"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label for="txtContrasenia" class="form-label">Contraseña</label>
                <asp:TextBox runat="server" ID="txtContrasenia" CssClass="form-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-4">
                <asp:Button Text="Registrarse" ID="BtnRegistrarse" OnClick="BtnRegistrarse_Click" runat="server" class="btn btn-primary" />
            </div>  
        </div>
    </div>
</asp:Content>