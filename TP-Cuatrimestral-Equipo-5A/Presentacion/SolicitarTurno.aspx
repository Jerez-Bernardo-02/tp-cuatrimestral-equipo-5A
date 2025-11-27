<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SolicitarTurno.aspx.cs" Inherits="Presentacion.Turnos1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="mb-4 ms-3">
            <h1 class="display-5">Solicitar un Nuevo Turno</h1>
            <p>Siga los pasos para agendar un nuevo turno.</p>
        </div>

        <div class="mb-3">
            <label class="form-label">Paciente</label>

            <div class="input-group">
                <asp:TextBox ID="txtDniPaciente" runat="server" CssClass="form-control"
                    placeholder="Ingrese DNI del paciente"></asp:TextBox>
                <%-- Boton de buscar paciente --%>
                <asp:Button ID="btnBuscarPaciente" runat="server" Text="Buscar"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnBuscarPaciente_Click" />
            </div>
                <asp:RequiredFieldValidator ErrorMessage="Debe ingresar un DNI" ControlToValidate="txtDniPaciente" runat="server" ForeColor="Red" />

            <%-- Nombre del paciente encontrado --%>
            <div class="mt-2">
                <asp:TextBox ID="txtNombrePaciente" runat="server"
                    CssClass="form-control bg-light" ReadOnly="true" Visible="false">
                </asp:TextBox>
            </div>

            <asp:HiddenField ID="hfIdPaciente" runat="server" />

            <asp:Label ID="lblErrorPaciente" runat="server" CssClass="text-danger smzall" Visible="false"></asp:Label>
        </div>

        <div class="row mt-5">
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
                                <div class="input-group">
                                    <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"
                                        CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFecha_TextChanged">
                                    </asp:TextBox>

                                    <%-- Boton para buscar fecha con proximos turnos. --%>
                                    <asp:Button ID="btnProximaFechaTurno" runat="server" Text="Próxima fecha"
                                        CssClass="btn btn-outline-secondary btn-sm"
                                        OnClick="btnProximaFechaTurno_Click" Enabled="false" />
                                </div>
                            </div>
                            <%-- Horarios (con Repeater o foreach) --%>
                            <div class="col-md-6 mb-3">
                                <label class="form-label d-block">Horarios disponibles</label>
                                <div class="border rounded p-2" style="min-height: 150px; max-height: 300px; overflow-y: auto;">
                                    <asp:Label ID="lblInfoHorarios" runat="server" Text="Seleccione una fecha para ver los horarios disponibles."
                                        CssClass="text-muted"
                                        Visible="true">
                                    </asp:Label>

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
                        <h5 class="card-title">Observaciones (Opcional)</h5>
                        <div class="mb-3">
                            <asp:TextBox ID="txtObservaciones" runat="server"
                                TextMode="MultiLine" Rows="3"
                                CssClass="form-control"
                                placeholder="Ingrese motivo de consulta o notas adicionales..."
                                AutoPostBack="true" MaxLength="30"
                                OnTextChanged="txtObservaciones_TextChanged"></asp:TextBox>
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
                            <strong>Paciente:</strong>
                            <asp:Label ID="lblResumenPaciente" runat="server" Text="-" CssClass="d-block text-muted"></asp:Label>
                        </div>


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

                        <div class="mb-3">
                            <strong>Observaciones:</strong>
                            <asp:Label ID="lblResumenObservaciones" runat="server" Text="-"
                                CssClass="d-block text-muted "></asp:Label>
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
