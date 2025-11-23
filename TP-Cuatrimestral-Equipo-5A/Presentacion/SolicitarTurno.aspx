<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SolicitarTurno.aspx.cs" Inherits="Presentacion.Turnos1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="mb-4 ms-3">
            <h1 class="display-5">Solicitar un Nuevo Turno</h1>
            <p>Siga los pasos para agendar un nuevo turno.</p>
        </div>

        <div class="row">
            <%-- COLUMNA IZQUIERDA (Filtros, calendario, hora y horarios disponibles) --%>
            <div class="col-md-7">
                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body">

                        <%-- PASO 1: Elegirr medico y especialidad --%>
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

                        <%-- PASO 2: Fecha y repeater con horarios disponibles --%>
                        <h5 class="card-title">Paso 2: Elija una Fecha y Hora</h5>
                        <div class="row">
                            <%-- Calendario --%>
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Seleccione una fecha</label>
                                <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"
                                    CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFecha_TextChanged">
                                </asp:TextBox>
                            </div>

                            <%-- Horarios (con Repeater o foreach) --%>
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Horarios disponibles</label>
                                <div class="border rounded p-2" style="max-height: 200px; overflow-y: auto;">
                                    <asp:Label ID="lblNoHorarios" runat="server" Text="Seleccione un médico y una fecha." Visible="false" />

                                    <div class="row g-2">
                                        <asp:Repeater runat="server" ID="repHorarios" OnItemDataBound="repHorarios_ItemDataBound" OnItemCommand="repHorarios_ItemCommand">
                                            <ItemTemplate>
                                                <div class="col-md-4">
                                                    <asp:Button ID="btnHorario" runat="server"
                                                        Text="<%# Container.DataItem %>"
                                                        CommandArgument="<%# Container.DataItem %>"
                                                        CommandName="Seleccionar"
                                                        CssClass="btn btn-outline-info w-100 btn-sm" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
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

                        <div class="mb-2">
                            <strong>Especialidad:</strong>
                            <asp:Label ID="lblResumenEspecialidad" runat="server" Text="-" CssClass="d-block text-muted"></asp:Label>
                        </div>

                        <div class="mb-2">
                            <strong>Médico:</strong>
                            <asp:Label ID="lblResumenMedico" runat="server" Text="-" CssClass="d-block text-muted"></asp:Label>
                        </div>

                        <div class="mb-2">
                            <strong>Fecha:</strong>
                            <asp:Label ID="lblResumenFecha" runat="server" Text="-" CssClass="d-block text-muted"></asp:Label>
                        </div>

                        <div class="mb-3">
                            <strong>Hora:</strong>
                            <asp:Label ID="lblResumenHora" runat="server" Text="-" CssClass="d-block text-muted"></asp:Label>
                        </div>

                        <div class="md-12">
                            <%-- Botón Confirmar --%>
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Turno"
                                CssClass="btn btn-success btn-lg w-100" Enabled="false"
                                OnClick="btnConfirmar_Click" />
                        </div>

                        <%-- Mensajes de éxito y error --%>
                        <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success mt-3 d-block text-center" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
