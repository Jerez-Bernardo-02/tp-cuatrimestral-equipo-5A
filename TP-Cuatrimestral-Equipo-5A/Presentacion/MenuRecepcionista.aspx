<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuRecepcionista.aspx.cs" Inherits="Presentacion.MenuRecepcionista" %>

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
    <div class="container-fluid" style="height: 75vh;">
        <div class="d-flex flex-column justify-content-center align-items-center h-100">

            <div class="row text-center mt-3">

                <div class="col">
                    <a href="RecepcionistaPacientes.aspx" class="card h-100 shadow text-decoration-none" style="width: 18rem;">
                        <div class="card-body">
                            <h5 class="card-title">Pacientes</h5>
                            <img src="Imagenes/Icono_Paciente.png" class="img-fluid w-50 mx-auto mt-3" alt="Paciente">
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="RecepcionistaMedicos.aspx" class="card h-100 shadow text-decoration-none" style="width: 18rem;">
                        <div class="card-body">
                            <h5 class="card-title">Medicos</h5>
                            <img src="Imagenes/Icono_Medico.png" class="img-fluid w-50 mx-auto mt-3" alt="Medico">
                        </div>
                    </a>
                </div>

                <div class="col">
                    <a href="RecepcionistaTurnos.aspx" class="card h-100 shadow text-decoration-none" style="width: 18rem;">
                        <div class="card-body">
                            <h5 class="card-title">Turnos</h5>
                            <img src="Imagenes/Icono_Turnos.png" class="img-fluid w-50 mx-auto mt-3" alt="Imagen">
                        </div>
                    </a>
                </div>

            </div>

        </div>
    </div>
</asp:Content>

