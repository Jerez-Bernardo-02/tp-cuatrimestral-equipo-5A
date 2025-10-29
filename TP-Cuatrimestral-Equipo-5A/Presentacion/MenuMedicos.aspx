<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMedicos.Master" AutoEventWireup="true" CodeBehind="MenuMedicos.aspx.cs" Inherits="Presentacion.MenuMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="mb-4">
            <h1 class="display-5">Buenos días doctor!</h1>
            <p>Resumen de tu jornada.</p>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="card shadow-sm border-0 mb-3">

                    <div class="card-body">

                        <h5 class="card-title">Turnos de hoy, 30 de Octubre</h5>
                        <hr />

                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <p class="card-text">Turno 1: 8:00 - Paciente Juan Perez.</p>
                            <a href="#" class="btn btn-primary">Ver historia clinica</a>
                        </div>
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <p class="card-text">Turno 2: 9:00 - Paciente Mauro Perez.</p>
                            <a href="#" class="btn btn-primary">Ver historia clinica</a>
                        </div>

                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <p class="card-text">Turno 3: 10:00 - Paciente Carlos Perez.</p>
                            <a href="#" class="btn btn-primary">Ver historia clinica</a>
                        </div>
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <p class="card-text">Turno 4: 11:00 - Paciente Pedro Gomez.</p>
                            <a href="#" class="btn btn-primary">Ver historia clinica</a>
                        </div>


                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <h5 class="card-title fs-5">Resumen del día </h5>
                        <hr />
                        <p class="card-text">Pacientes atendidos: 2/10</p>
                        <p class="card-text">Resultados pendientes: 3</p>
                        <p class="card-text">...</p>
                        <p class="card-text">...</p>
                        <p class="card-text">...</p>

                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
