<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMedicos.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="Presentacion.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="container-fluid">

            <div class="row">
                <%--Columna izquierda--%>
           
                <div class="col-md-4">

                    <h3 class="mb-3 mt-3">Pacientes</h3>

                    <div class="card shadow-sm border-0 mb-3">
                        <div class="card-body">
                            <asp:TextBox ID="txtBuscarPaciente" runat="server"
                                CssClass="form-control"
                                placeholder="Buscar por nombre o DNI...">
                        </asp:TextBox>
                        </div>
                    </div>

                    <div class="list-group shadow-sm">

                        <a href="#" class="list-group-item list-group-item-action active" aria-current="true">
                            <div class="d-flex align-items-center">
                                <div>
                                    <strong class="mb-0">Juan Pedro Gomez</strong>
                                    <p class="mb-0 small text-muted">DNI: 12345678A</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" class="list-group-item list-group-item-action">
                            <div class="d-flex align-items-center">
                                <div>
                                    <strong class="mb-0">Carlos Rodríguez</strong>
                                    <p class="mb-0 small text-muted">DNI: 87654321B</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" class="list-group-item list-group-item-action">
                            <div class="d-flex align-items-center">
                                <div>
                                    <strong class="mb-0">Lucia Perez</strong>
                                    <p class="mb-0 small text-muted">DNI: 11223344C</p>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <%--Columna derecha--%>
                <div class="col-md-8">

                    <div class="card shadow-sm border-0 mb-4">
                        <div class="card-body p-4">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h2 class="mb-0">Juan Pedro Gomez</h2>
                                    <p class="lead text-muted mb-0">34 años, Masculino</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card shadow-sm border-0 mb-4">
                        <div class="card-body p-4">
                            <h5 class="card-title">Añadir nueva observación a la historia clínica</h5>

                            <asp:TextBox ID="txtNuevaObservacion" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="4"
                                placeholder="Escriba aquí las nuevas observaciones...">
                        </asp:TextBox>

                            <div class="text-end mt-3">
                                <asp:Button ID="btnGuardarHistorial" runat="server"
                                    Text="Guardar en Historial"
                                    CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>

                    <div class="card shadow-sm border-0 mb-4">
                        <div class="card-body p-4">
                            <h5 class="card-title">Historial de Visitas</h5>

                            <p class ="card-text"> ...</p>
                            <p class ="card-text"> ...</p>
                            <p class ="card-text"> ...</p>


                        </div>
                    </div>

                </div>
            </div>
        </div>
</asp:Content>
