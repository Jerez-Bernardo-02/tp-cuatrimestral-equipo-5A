<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuPaciente.aspx.cs" Inherits="Presentacion.MenuPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="container-fluid">
        <div class="row">
            <%-- Columna izquierda --%>
            <div class="col-md-4">
                <h1 class="mb-3 mt-3">Hola Juan!</h1>

                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <asp:Button 
                            ID="EditarPerfil" 
                            runat="server" 
                            Text="Editar Perfil" 
                            CssClass="btn btn-primary" 
                            OnClientClick="window.location.href='PacienteFormularioRegistro.aspx'; return false;" />
                    </div>
                </div>

                <h4 class="mb-3 mt-3">Próximos turnos:</h4>
                <div class="list-group shadow-sm">
                    <a href="#" class="list-group-item list-group-item-action active" aria-current="true">
                        <div class="d-flex align-items-center">
                            <div>
                                <strong class="mb-0">Dr. Gomez - Cardiología</strong>
                                <p class="mb-0 small text-muted">16:30 hs. Viernes, 12 de Diciembre 2025</p>
                            </div>
                        </div>
                    </a>

                    <a href="#" class="list-group-item list-group-item-action">
                        <div class="d-flex align-items-center">
                            <div>
                                <strong class="mb-0">Dra. Rodríguez - Dermatología</strong>
                                <p class="mb-0 small text-muted">Lunes, 14 de Diciembre 2025</p>
                            </div>
                        </div>
                    </a>
                </div>
            </div>

            <%-- Columna derecha --%>
            <div class="col-md-8">
                <div class="card shadow-sm border-0 mb-4">
                    <div class="card-body p-4">
                        <h5 class="card-title mb-4">Solicitar nuevo turno</h5>

                        <%-- Dropdown de especialidad --%>
                        <div class="dropdown mb-3">
                            <button class="btn btn-secondary dropdown-toggle w-100" type="button" data-bs-toggle="dropdown">
                                Seleccionar especialidad
                            </button>
                            <ul class="dropdown-menu w-100">
                                <li><a class="dropdown-item" href="#">Alergología</a></li>
                                <li><a class="dropdown-item" href="#">Anatomía patológica</a></li>
                                <li><a class="dropdown-item" href="#">Anestesiología</a></li>
                            </ul>
                        </div>

                        <%-- Dropdown de médico --%>
                        <div class="dropdown mb-4">
                            <button class="btn btn-secondary dropdown-toggle w-100" type="button" data-bs-toggle="dropdown">
                                Seleccionar médico
                            </button>
                            <ul class="dropdown-menu w-100">
                                <li><a class="dropdown-item" href="#">Carlos Romero</a></li>
                                <li><a class="dropdown-item" href="#">Dario Gomez</a></li>
                                <li><a class="dropdown-item" href="#">Pablo Massi</a></li>
                            </ul>
                        </div>

                        <%-- Fecha del turno --%>
                        <div class="mb-3">
                            <label for="txtFechaTurno" class="form-label">Fecha del turno</label>
                            <asp:TextBox ID="txtFechaTurno" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                          <%-- Hora del turno --%>
                        <div class="mb-3">
                            <label for="txtHoraTurno" class="form-label">Hora del turno</label>
                            <asp:TextBox ID="txtHoraTurno" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                        </div>

                        <%-- Botón guardar --%>
                        <div class="text-end mt-3">
                            <asp:Button ID="btnGuardarTurno" runat="server" Text="Guardar Turno" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
