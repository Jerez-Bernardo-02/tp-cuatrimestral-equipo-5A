<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="Presentacion.GestionUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container mt-5">
        <h3 class="ms-3 mb-3">Gestión de usuarios</h3>
        
        <div class="d-flex justify-content-end mb-3">
             <asp:Button Text="CrearUsuario" ID="BtnCrearUsuario" OnClick="BtnCrearUsuario_Click" runat="server" class="btn btn-primary" />
        </div>

        <table class="table table-hover align-middle text-center shadow-sm rounded">
            <thead class="table-light">
                <tr>
                    <th scope="col">Nombre</th>
                    <th scope="col">Apellido</th>
                    <th scope="col">Usuario</th>
                    <th scope="col">Tipo Usuario</th>
                    <th scope="col">Estado</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                      <td>Nombre1</td>
                      <td>Apellido1</td>
                      <td>Usuario1</td>
                      <td>TipoUsuario1</td>
                      <td>
                         <span class="badge bg-success-subtle text-success border border-success rounded-pill px-3">Activo</span>
                      </td>
                      <td>
                        <asp:Button ID="BtnHabilitarBtn" runat="server" CssClass="btn btn-outline-primary btn-sm" OnClick="BtnHabilitar_Click" Text="" UseSubmitBehavior="false" />
                        <asp:Literal ID="LitHabilitarIcon" runat="server" Text='<i class="bi bi-pencil"></i>' />

                         <asp:Button ID="BtnDeshabilitarBtn" runat="server" CssClass="btn btn-outline-danger btn-sm" OnClick="BtnDeshabilitar_Click" Text="" UseSubmitBehavior="false" />
                         <asp:Literal ID="LitDeshabilitarIcon" runat="server" Text='<i class="bi bi-x-circle"></i>' />
                     </td>
                <tr>
                    <td>Nombre2</td>
                    <td>Apellido2</td>
                    <td>Usuario2</td>
                    <td>TipoUsuario2</td>
                    <td><span class="badge bg-success-subtle text-success border border-success rounded-pill px-3">Activo</span></td>
                    <td>
                        <button class="btn btn-outline-primary btn-sm"><i class="bi bi-pencil"></i></button>
                        <button class="btn btn-outline-danger btn-sm"><i class="bi bi-x-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <td>Nombre3</td>
                    <td>Apellido3</td>
                    <td>Usuairo3</td>
                    <td>TipoUsuario3</td>
                    <td><span class="badge bg-danger-subtle text-danger border border-danger rounded-pill px-3">Inactivo</span></td>
                    <td>
                        <button class="btn btn-outline-success btn-sm"><i class="bi bi-check-circle"></i></button>
                        <button class="btn btn-outline-danger btn-sm"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <td>Nombre4</td>
                    <td>Apellido4</td>
                    <td>Usuario4</td>
                    <td>TipoUsuario4</td>
                    <td><span class="badge bg-success-subtle text-success border border-success rounded-pill px-3">Activo</span></td>
                    <td>
                        <button  class="btn btn-outline-primary btn-sm"><i class="bi bi-pencil"></i></button>
                        <button class="btn btn-outline-danger btn-sm"><i class="bi bi-x-circle"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

</asp:Content>
