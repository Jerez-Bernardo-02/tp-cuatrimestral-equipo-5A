<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MedicoResumen.aspx.cs" Inherits="Presentacion.MenuMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <h1 class="display-6">Bienvenido Dr.
                <asp:Label ID="lblNombreMedico" runat="server" Text="Label"></asp:Label>!</h1>
        <span class="text-muted">Resumen diario</span>
    </div>

    <div class="row">
        <div class="col-md-9">
            <%--Card con borde y sombra: columna izquierda--%>
            <div class="card shadow-sm border-0 mb-3">

                <div class="row mt-3 ms-3 me-2">
                    <%-- BARRA DE FILTROS --%>
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h5 class="card-title mb-0">Listado de Turnos</h5>

                        <div class="d-flex align-items-center">
                            <label class="me-2 text-muted small">Filtrar por:</label>
                            <asp:DropDownList ID="ddlFiltroEstado" runat="server"
                                CssClass="form-select form-select-sm"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged"
                                Width="150px">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%--Grilla de turnos--%>
                    <asp:GridView ID="dgvTurnos" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="false"
                        OnRowCommand="dgvTurnos_RowCommand"
                        DataKeyNames="Id"
                        OnRowDataBound="dgvTurnos_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Fecha y hora" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                            <asp:TemplateField HeaderText="Paciente">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Paciente.Nombre") + " " + Eval("Paciente.Apellido") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Especialidad" DataField="Especialidad.Descripcion" />

                            <asp:BoundField HeaderText="Observaciones" DataField="Observaciones" ItemStyle-CssClass="text-break" ItemStyle-Width="30%"/>

                            <asp:BoundField HeaderText="Estado" DataField="Estado.Descripcion" />


                            <asp:TemplateField HeaderText="Acciones">

                                <%--Boton ver historia clinica--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnVerHC" runat="server"
                                        CommandName="VerHC"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-info btn-sm"
                                        ToolTip="Ver Historia Clínica">
                                            <i class="bi bi-eye-fill"></i>
                                    </asp:LinkButton>

                                    <%-- Boton finalizar --%>
                                    <asp:LinkButton ID="btnFinalizar" runat="server"
                                        CommandName="Finalizar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-success btn-sm ms-1"
                                        ToolTip="Finalizar Turno (Atendido)"
                                        OnClientClick="return confirm('¿Confirmar que el paciente fue atendido?');">
                                         <i class="bi bi-check-lg"></i>
                                     </asp:LinkButton>

                                    <%-- Boton cancelar--%>
                                    <asp:LinkButton ID="btnCancelar" runat="server"
                                        CommandName="Cancelar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-danger btn-sm ms-1"
                                        ToolTip="Cancelar Turno"
                                        OnClientClick="return confirm('¿Está seguro de cancelar este turno?');">
                                         <i class="bi bi-x-circle"></i>
                                     </asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>

        <div class="col-md-3">
            <%--Card con borde y sombra: columna derecha--%>
            <div class="card shadow-sm border-0 mb-3">
                <div class="card-body">
                    <h5 class="card-title fs-5">Resumen del día </h5>
                    <hr />
                    <%-- Total de Turnos --%>
                    <div class="d-flex justify-content-between">
                        <span class="text-muted">Total Turnos:</span>
                        <span class="text-muted">
                            <asp:Label ID="lblTotalTurnos" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                    <hr />

                    <%-- Atendidos --%>
                    <div class="d-flex justify-content-between mb-2">

                        <span class="text-success">Atendidos</span>
                        <span class="text-success">
                            <asp:Label ID="lblAtendidos" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                    <%-- Pendientes  --%>
                    <div class="d-flex justify-content-between mb-2">
                        <span class="text-primary">Pendientes</span>
                        <span class="text-primary">
                            <asp:Label ID="lblPendientes" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>

                    <%-- Cancelados --%>
                    <div class="d-flex justify-content-between">
                        <span class="text-danger">Cancelados</span>
                        <span class="text-danger">
                            <asp:Label ID="lblCancelados" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
