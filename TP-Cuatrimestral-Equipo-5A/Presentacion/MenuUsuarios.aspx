<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuUsuarios.aspx.cs" Inherits="Presentacion.MenuRecepcionista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card:hover {
            transform: scale(1.05);
            transition: 0.3s ease;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.3);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Panel Paciente -->
    <asp:Panel ID="PanelPaciente" runat="server">

        <div class="container d-flex align-items-center justify-content-center" style="height: 75vh;">

            <div class="row row-cols-3 g-4">

                <div class="col">
                    <a href="#" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Agregar.png" width="100" height="100" alt="Solicitar Turno">
                            <h5 class="card-text mt-3">Nuevo Turno</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="#" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Turnos.png" width="100" height="100" alt="Mis Turnos">
                            <h5 class="card-text mt-3">Mis Turnos</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="#" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Paciente.png" width="100" height="100" alt="Mis Datos">
                            <h5 class="card-text mt-3">Mis Datos</h5>
                        </div>
                    </a>
                </div>

            </div>

        </div>

    </asp:Panel>

    <!-- Panel Medico -->
    <asp:Panel ID="PanelMedico" runat="server">

        <div class="container d-flex align-items-center justify-content-center" style="height: 75vh;">

            <div class="row row-cols-3 g-4">

                <div class="col">
                    <a href="MedicoResumen.aspx" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Turnos.png" width="100" height="100" alt="Mis Turnos">
                            <h5 class="card-text mt-3">Resumen Diario</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="MedicoHorarios.aspx" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Reloj.png" width="100" height="100" alt="Mis Horarios">
                            <h5 class="card-text mt-3">Mis Horarios</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="MedicoTurnos.aspx" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Pacientes.png" width="100" height="100" alt="Mis Pacientes">
                            <h5 class="card-text mt-3">Gestion de turnos</h5>
                        </div>
                    </a>
                </div>

            </div>

        </div>

    </asp:Panel>

    <!-- Panel Recepcionista -->
    <asp:Panel ID="PanelRecepcionista" runat="server">

        <div class="container d-flex align-items-center justify-content-center" style="height: 75vh;">

            <div class="row row-cols-3 g-4">

                <div class="col">
                    <a href="RecepcionistaPacientes.aspx" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Pacientes.png" width="100" height="100" alt="Pacientes">
                            <h5 class="card-text mt-3">Pacientes</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="RecepcionistaMedicos.aspx" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Medico.png" width="100" height="100" alt="Medicos">
                            <h5 class="card-text mt-3">Medicos</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="RecepcionistaTurnos.aspx" class="card text-center text-decoration-none shadow w-100 h-100">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Turnos.png" width="100" height="100" alt="Turnos">
                            <h5 class="card-text mt-3">Turnos</h5>
                        </div>
                    </a>
                </div>

            </div>

        </div>

    </asp:Panel>

    <!-- Panel Administrador -->
    <asp:Panel ID="PanelAdministrador" runat="server">

        <div class="container d-flex align-items-center justify-content-center" style="height: 75vh;">

            <div class="row row-cols-3 g-4">

                <div class="col">
                    <a href="AdministradorPacientes.aspx" class="card text-center text-decoration-none shadow">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Pacientes.png" width="100" height="100" alt="Pacientes">
                            <h5 class="card-text mt-3">Pacientes</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="AdministradorMedicos.aspx" class="card text-center text-decoration-none shadow">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Medico.png" width="100" height="100" alt="Medicos">
                            <h5 class="card-text mt-3">Medicos</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="AdministradorRecepcionistas.aspx" class="card text-center text-decoration-none shadow">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Recepcionista.png" width="100" height="100" alt="Recepcionistas">
                            <h5 class="card-text mt-3">Recepcionistas</h5>
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="AdministradorEspecialidades.aspx" class="card text-center text-decoration-none shadow">
                        <div class="card-body">
                            <img src="Imagenes/Icono_Especialidades.png" width="100" height="100" alt="Especialidades">
                            <h5 class="card-text mt-3">Especialidades</h5>
                        </div>
                    </a>
                </div>

            </div>

        </div>

    </asp:Panel>

</asp:Content>

