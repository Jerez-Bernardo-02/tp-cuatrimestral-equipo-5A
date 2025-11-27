<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OlvideMiClave.aspx.cs" Inherits="Presentacion.OlvideMiClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%-- Main Container --%>
    <div class="container d-flex justify-content-center align-items-center min-vh-100">

        <%--  Container General --%>
        <div class="row border rounded-5 p-3 bg-white shadow">

            <%-- Caja izquierda con logo --%>
            <div class="col-md-6 rounded-4 d-flex justify-content-center align-items-center flex-column">
                <div class="featured-image mb-3">
                    <img src="Imagenes/Logo.png" class="img-fluid" style="width: 450px;" alt="Imagen de login" />
                </div>
            </div>

            <%-- Caja derecha para inputs--%>
            <div class="col-md-6" style="padding: 20px;">
                <div class="row align-items-center">

                    <div class="header-text mb-4">
                        <h3>¡Bienvenido!</h3>
                        <p>Ingrese sus credenciales para iniciar sesión.</p>
                    </div>
                    <asp:TextBox runat="server" text="Hola es una prueba" ID="HOLA"/> 
                </div>
            </div>
        </div>
    </div>
</asp:Content>
