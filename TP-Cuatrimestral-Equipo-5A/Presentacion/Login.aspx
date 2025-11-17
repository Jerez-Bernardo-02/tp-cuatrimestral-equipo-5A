<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Login" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">

    <style>
        .validacion{
            color: red;
            font-size: 15px;
            display: block;
            min-height: 25px;
        }
    </style>


</head>
<body>
    <form id="Login" runat="server">

        <%-- Main Container --%>
        <div class="container d-flex justify-content-center align-items-center min-vh-100">

            <%-- Login Container --%>
            <div class="row border rounded-5 p-3 bg-white shadow">

                <%-- Caja izquierda --%>
                <div class="col-md-6 rounded-4 d-flex justify-content-center align-items-center flex-column">
                    <div class="featured-image mb-3">
                        <img src="Imagenes/Login.jpg" class="img-fluid" style="width: 350px;" alt="Imagen de login"/>
                    </div>
                </div>
                
                <%-- Caja derecha --%>
                <div class="col-md-6" style="padding: 20px;">
                    <div class="row align-items-center">

                        <div class="header-text mb-4">
                            <h3>¡Bienvenido!</h3>
                            <p>Ingrese sus credenciales para iniciar sesión.</p>
                        </div>

                        <div class="input-group">
                            <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control form-control-lg bg-light fs-6" placeholder="Usuario"/>
                        </div>
                        <asp:RequiredFieldValidator  runat="server" ErrorMessage="* Complete el campo" ControlToValidate="txtUsuario" CssClass="validacion"/>

                        <div class="input-group">
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control form-control-lg bg-light fs-6" placeholder="Contraseña" TextMode="Password"/>
                        </div>
                        <asp:RequiredFieldValidator  runat="server" ErrorMessage="* Complete el campo" ControlToValidate="txtPassword" CssClass="validacion"/>

                        <asp:CustomValidator ID="cvLogin" runat="server" CssClass="validacion"></asp:CustomValidator>

                        <div class="input-group mb-3">

                            <%--<div class="form-check">
                                <asp:CheckBox CssClass="form-check-input" ID="cbxRecordar" runat="server" />
                                <label for="cbxRecordar" class="form-check-label text-secondary"><small>Recordarme</small></label>
                            </div>--%>

                            <%-- Un redirect a pestaña de olvido contraseña? --%>
                            <div class="input-group mb-3">
                                <asp:Button CssClass="btn btn-lg btn-primary w-100 fs-6" ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Ingresar"/>
                            </div>

                            <%-- Crear cuenta --%>
                            <div class="input-group mb-3">
                                <asp:Button CssClass="btn btn-lg btn-light w-100 fs-6" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" runat="server" Text="Registrarse" CausesValidation="False" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
