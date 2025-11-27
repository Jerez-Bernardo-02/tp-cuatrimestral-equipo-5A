<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OlvideMiClave.aspx.cs" Inherits="Presentacion.OlvideMiClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container d-flex justify-content-center align-items-center min-vh-100">

        <div class="row border rounded-5 p-3 bg-white shadow">

            <!-- Caja izquierda -->
            <div class="col-md-6 rounded-4 d-flex justify-content-center align-items-center flex-column">
                <div class="featured-image mb-3">
                    <img src="Imagenes/Logo.png" class="img-fluid" style="width: 450px;" alt="Imagen de login" />
                </div>
            </div>

            <!-- Caja derecha -->
            <div class="col-md-6" style="padding: 20px;">

                <div class="header-text mb-4">
                    <h3>Recuperación de Acceso</h3>
                    <p>Ingresá tu correo electrónico asociado y te enviaremos tus datos de acceso.</p>
                </div>

                <!-- Campo de correo -->
                <div class="mb-3">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control form-control-lg bg-light fs-6" placeholder="Correo electrónico" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio." ForeColor="Red" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email inválido." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ForeColor="Red" />
                </div>

                
                <div class="mb-3">
                    <asp:Button 
                        CssClass="btn btn-lg btn-primary w-100 fs-6" ID="btnRecupero" OnClick="btnRecupero_Click" runat="server" Text="Recuperar datos" />
                </div>
                <small>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger opacity-75" Visible="false">
                     </asp:Label>
                </small>
                 <small>
                    <asp:Label ID="lblExito" runat="server" CssClass="text-success opacity-75" Visible="false">
                    </asp:Label>
                </small>

            </div>

        </div>

    </div>

</asp:Content>
