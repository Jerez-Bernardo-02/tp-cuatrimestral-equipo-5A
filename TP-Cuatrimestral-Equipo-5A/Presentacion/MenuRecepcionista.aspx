<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageRecepcionista.Master" AutoEventWireup="true" CodeBehind="MenuRecepcionista.aspx.cs" Inherits="Presentacion.MenuRecepcionista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid d-flex justify-content-center align-items-center" style="height: 100vh;">
        <div class="row text-center">
            <div class="col">
                <div class="card h-100 shadow" style="width: 18rem;">
                    <img src="https://img.icons8.com/?size=100&id=oO0pZgktLNpK&format=png&color=000000" class="img-fluid w-50 mx-auto mt-3" alt="Paciente">
                    <div class="card-body">
                        <a href="RecepcionistaPacientes.aspx" class="btn btn-primary">Administrar Pacientes</a>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card h-100 shadow" style="width: 18rem;">
                    <img src="https://img.icons8.com/?size=100&id=eKkd5GHc7XOw&format=png&color=000000" class="img-fluid w-50 mx-auto mt-3" alt="Medico">
                    <div class="card-body">
                        <a href="#" class="btn btn-primary">Administrar Medicos</a>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card h-100 shadow" style="width: 18rem;">
                    <img src="https://img.icons8.com/?size=100&id=18914&format=png&color=000000" class="img-fluid w-50 mx-auto mt-3" alt="Imagen">
                    <div class="card-body">
                        <a href="#" class="btn btn-primary">Registrar Turno</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

