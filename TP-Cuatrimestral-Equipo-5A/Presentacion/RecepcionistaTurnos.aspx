<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RecepcionistaTurnos.aspx.cs" Inherits="Presentacion.RecepcionistaTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 20px;">

        <header class="d-flex justify-content-between align-items-center pb-3 mb-4 border-bottom">
            <h1 class="h1">Gestión de Turnos</h1>

            <div class="d-flex gap-2">
                <%--Botón para Agregar Pacientes--%>
                <asp:LinkButton runat="server" ID="btnNuevoPaciente" CssClass="btn btn-primary" OnClick="btnNuevoPaciente_Click">
                    <i class="bi bi-plus-lg"></i>
                    Nuevo Paciente
                </asp:LinkButton>

                <%--Botón para Agregar Turnos--%>
                <asp:LinkButton runat="server" ID="btnNuevoTurno" CssClass="btn btn-success" OnClick="btnNuevoTurno_Click">
                    <i class="bi bi-plus-lg"></i>
                    Nuevo Turno
                </asp:LinkButton>
            </div>

        </header>

    </div>

    <div class="row align-items-stretch">
        <div class="col-8">
            <div class="card">
                <div class="card-body">

                    <%--Filtros--%>
                    <div class="row">

                        <%--Filtro por Fecha--%>
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label Text="Fecha" runat="server" />
                                <asp:TextBox runat="server" ID="txtFecha" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <%--Filtro por Especialidad--%>
                        <div class="col-4">
                            <div class="mb-3">
                                <asp:Label Text="Especialidad" runat="server" />
                                <asp:DropDownList runat="server" ID="ddlEspecialidad" CssClass="form-select">
                                    <asp:ListItem Text="Especialidad" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <%--Filtro por Dni del Paciente--%>
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label Text="Dni del paciente" runat="server" />
                                <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" PlaceHolder="Ej: 12345678"></asp:TextBox>
                            </div>
                        </div>

                        <%--Botón para Filtrar--%>
                        <div class="col-2">
                            <div class="mb-3 mt-4">
                                <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                            </div>
                        </div>

                    </div>

                    <%--Listado de turnos--%>
                    <asp:GridView runat="server" ID="dgvTurnos" AutoGenerateColumns="False" CssClass="table table-hover text-center" OnPageIndexChanging="dgvTurnos_PageIndexChanging" AllowPaging="true" PageSize="10" OnRowCommand="dgvTurnos_RowCommand" HeaderStyle-CssClass="table-secondary">

                        <Columns>

                            <%--Columna de Fecha--%>
                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

                            <%--Columna de Hora--%>
                            <asp:BoundField HeaderText="Hora" DataField="Fecha" DataFormatString="{0:HH:mm}" />

                            <%--Columna de Especialidad--%>
                            <asp:BoundField HeaderText="Especialidad" DataField="Especialidad.Descripcion" />

                            <%--Columna de Dni del Paciente--%>
                            <asp:BoundField HeaderText="Dni del Paciente" DataField="Paciente.Dni" />

                            <%--Columna con el Estado del Turno--%>
                            <asp:BoundField HeaderText="Estado" DataField="Estado.Descripcion" />

                            <%--Columna de Acciones--%>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkBtnModificarTurno" Text="Editar" CssClass="btn btn-warning btn-sm" ToolTip="Modificar Turno" CommandName="Modificar" CommandArgument='<%# Eval("Id") %>'>
                                        <i class="bi bi-pencil"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </div>

        <div class="col-4">
            <%--Detalle del Turno--%>
            <div class="card">
                <div class="card-body">
                    <div class="row mb-4">
                        <div class="d-flex justify-content-between align-items-center">
                            <h3 class="mb-0">Modificar Turno</h3>

                            <asp:DropDownList ID="ddlEstados" runat="server" CssClass="btn btn-warning dropdown-toggle w-auto" AutoPostBack="true" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>

                    <asp:HiddenField ID="hdnFldIdTurno" runat="server" />
                    <asp:HiddenField ID="hdnFldIdMedico" runat="server" />
                    <asp:HiddenField ID="hdnFldIdEspecialidad" runat="server" />
                    <asp:HiddenField ID="hdnFldIdEstado" runat="server" />
                    <asp:HiddenField ID="hdnFldPacienteDni" runat="server" />

                    <p>
                        <i class="bi bi-calendar-check"></i>
                        <b>Fecha:</b>
                        <asp:Label ID="lblFecha" runat="server" CssClass="card-body"></asp:Label>
                    </p>
                    <p>
                        <i class="bi bi-clock"></i>
                        <b>Horario:</b>
                        <asp:Label ID="lblHora" runat="server" CssClass="card-body"></asp:Label>
                    </p>
                    <p>
                        <i class="bi bi-person-fill"></i>
                        <b>Paciente:</b>
                        <asp:Label ID="lblPaciente" runat="server" CssClass="card-body"></asp:Label>
                    </p>
                    <p>
                        <i class="bi bi-person-vcard"></i>
                        <b>Médico:</b>
                        <asp:Label ID="lblMedico" runat="server" CssClass="card-body"></asp:Label>
                    </p>
                    <p>
                        <i class="bi bi-chat-right-quote"></i>
                        <b>Observaciones:</b>
                        <asp:Label ID="lblObservaciones" runat="server" CssClass="card-body"></asp:Label>
                    </p>
                    <asp:Panel ID="pnlReprogramar" runat="server" Visible="false">
                        <%-- Calendario --%>
                        <div class="col-md mb-3">
                            <label class="form-label d-block">Seleccione una nueva fecha</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtCalendario" runat="server" TextMode="Date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCalendario_TextChanged"> </asp:TextBox>

                                <%-- Boton para buscar fecha con proximos turnos. --%>
                                <asp:Button ID="btnProximaFechaTurno" runat="server" Text="Próxima fecha" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnProximaFechaTurno_Click" />
                            </div>
                        </div>
                        <%-- Horarios (con Repeater o foreach) --%>
                        <div class="col-md mb-3">
                            <label class="form-label d-block">Horarios disponibles</label>
                            <div class="border rounded p-2" style="min-height: 150px; max-height: 300px; overflow-y: auto;">
                                <asp:Label ID="lblInfoHorarios" runat="server" Text="Seleccione una fecha para ver los horarios disponibles." CssClass="text-muted" Visible="true">
                                </asp:Label>

                                <div class="row g-2">
                                    <asp:Repeater runat="server" ID="repHorarios" OnItemDataBound="repHorarios_ItemDataBound" OnItemCommand="repHorarios_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnHorario" runat="server" Text="<%# Container.DataItem %>" CommandArgument="<%# Container.DataItem %>" CommandName="Seleccionar" CssClass="btn btn-outline-info w-100 btn-sm" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="md-12">
                        <%-- Botón Confirmar --%>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success btn-lg w-100" Enabled="false" OnClick="btnGuardar_Click" />
                    </div>
                </div>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success mt-3 d-block text-center" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>