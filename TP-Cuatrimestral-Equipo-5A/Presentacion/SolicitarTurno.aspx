<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SolicitarTurno.aspx.cs" Inherits="Presentacion.Turnos1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="mb-4 ms-3">
            <h1 class="display-5">Solicitar un Nuevo Turno</h1>
            <p>Siga los pasos para agendar su cita.</p>
        </div>

        <div class="row">
            <%-- COLUMNA IZQUIERDA (Filtros y Calendario) --%>
            <div class="col-md-7">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">

                        <%-- PASO 1: Profesional --%>
                        <h5 class="card-title">Paso 1: Profesional y Especialidad</h5>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Especialidad</label>
                                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Médico</label>
                                <asp:DropDownList ID="ddlMedicos" runat="server" CssClass="form-select"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlMedicos_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr />

                        <%-- PASO 2: Fecha y Hora --%>
                        <h5 class="card-title">Paso 2: Elija una Fecha y Hora</h5>
                        <div class="row">
                            <%-- Calendario --%>
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Seleccione una fecha</label>
                                <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"
                                    CssClass="form-control" AutoPostBack="true">
                                </asp:TextBox>
                            </div>

                            <%-- Horarios (con Repeater o foreach) --%>
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Horarios disponibles</label>
                                <div class="border rounded p-2" style="max-height: 200px; overflow-y: auto;">
                                    <asp:Label ID="lblNoHorarios" runat="server" Text="Seleccione un médico y una fecha." Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Columna derecha Resumen del turno --%>
            <div class="col-md-5">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Resumen del Turno</h5>
                        <hr />

                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
