<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdministrador.Master" AutoEventWireup="true" CodeBehind="GestionPersonal.aspx.cs" Inherits="Presentacion.GestionPersonal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid">
    <div class="row">
        <!-- Columna izquierda -->
        <div class="col-md-4">
            <h3 class="mb-3 mt-3">Gestión de Personal</h3>

            <div class="card shadow-sm border-0 mb-3">
                <div class="card-body">
                    <asp:TextBox ID="txtBuscarPersonal" runat="server"
                                 CssClass="form-control"
                                 placeholder="Buscar por nombre o Documento..."></asp:TextBox>
                </div>
            </div>

            <div class="list-group shadow-sm">
                <a href="#" class="list-group-item list-group-item-action d-flex justify-content-between align-items-center">
                    <div>
                        <strong class="mb-0">Personal 1</strong>
                        <p class="mb-0 small text-muted">Dni.: ********</p>
                    </div>
                    <i class="bi bi-pencil text-primary" style="font-size:1.2rem; "></i>
                </a>

                <a href="#" class="list-group-item list-group-item-action d-flex justify-content-between align-items-center">
                    <div>
                        <strong class="mb-0">Personal 2</strong>
                        <p class="mb-0 small text-muted">Dni.: ********</p>
                    </div>
                    <i class="bi bi-pencil text-primary" style="font-size:1.2rem;"></i>
                </a>

                <a href="#" class="list-group-item list-group-item-action d-flex justify-content-between align-items-center">
                    <div>
                        <strong class="mb-0">Personal 3</strong>
                        <p class="mb-0 small text-muted">Dni.: ********</p>
                    </div>
                    <i class="bi bi-pencil text-primary" style="font-size:1.2rem; "></i>
                </a>
            </div>
        </div>

        <!-- Columna derecha -->
        <div class="col-md-8">
            <div class="container mt-5">
                <h3 class="ms-3 mb-3">Habilitar cuentas</h3>

                <div class="d-flex justify-content-end mb-3">
                    <asp:Button Text="Crear Usuario" ID="BtnCrearUsuario" OnClick="BtnCrearUsuario_Click" runat="server" class="btn btn-primary" />
                </div>

                <table class="table table-hover align-middle text-center shadow-sm rounded">
                    <thead class="table-light">
                        <tr>
                            <th scope="col">Nombre</th>
                            <th scope="col">Apellido</th>
                            <th scope="col">Documento</th>
                            <th scope="col">Usuario</th>
                            <th scope="col">Estado</th>
                            <th scope="col">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Nombre1</td>
                            <td>Apellido1</td>
                            <td>Documento1</td>
                            <td>Usuario1</td>
                            <td>
                                <span class="badge bg-success-subtle text-success border border-success rounded-pill px-3">Activo</span>
                            </td>
                            <td>
                                <asp:Button ID="BtnHabilitarBtn" runat="server" CssClass="btn btn-outline-primary btn-sm" OnClick="BtnHabilitarBtn_Click"  />
                                <asp:Button ID="BtnDeshabilitarBtn" runat="server" CssClass="btn btn-outline-danger btn-sm" OnClick="BtnDeshabilitarBtn_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre2</td>
                            <td>Apellido2</td>
                            <td>Documento2</td>
                            <td>Usuario2</td>
                            <td>
                                <span class="badge bg-success-subtle text-success border border-success rounded-pill px-3">Activo</span>
                            </td>
                            <td>
                                <asp:Button ID="BtnHabilitarBtn2" runat="server" CssClass="btn btn-outline-primary btn-sm"  />
                                <asp:Button ID="BtnDeshabilitarBtn2" runat="server" CssClass="btn btn-outline-danger btn-sm" />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre3</td>
                            <td>Apellido3</td>
                            <td>Documento3</td>
                            <td>Usuario3</td>
                            <td>
                                <span class="badge bg-danger-subtle text-danger border border-danger rounded-pill px-3">Inactivo</span>
                            </td>
                            <td>
                                <asp:Button ID="BtnHabilitarBtn3" runat="server" CssClass="btn btn-outline-success btn-sm" />
                                <asp:Button ID="BtnDeshabilitarBtn3" runat="server" CssClass="btn btn-outline-danger btn-sm"  />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre4</td>
                            <td>Apellido4</td>
                            <td>Documento4</td>
                            <td>Usuario4</td>
                            <td>
                                <span class="badge bg-success-subtle text-success border border-success rounded-pill px-3">Activo</span>
                            </td>
                            <td>
                                <asp:Button ID="BtnHabilitarBtn4" runat="server" CssClass="btn btn-outline-primary btn-sm"  />
                                <asp:Button ID="BtnDeshabilitarBtn4" runat="server" CssClass="btn btn-outline-danger btn-sm"  />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
</asp:Content>
